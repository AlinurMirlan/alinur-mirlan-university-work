using AutoMapper;
using BudgetTracker.Areas.Income.Controllers;
using BudgetTracker.Infrastructure;
using BudgetTracker.Infrastructure.ModelState;
using BudgetTracker.Models;
using BudgetTracker.Models.DataObjects;
using BudgetTracker.Models.ViewModels;
using BudgetTracker.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

namespace BudgetTracker.Controllers
{
    public class BudgetsController : Controller
    {
        private const string SuccessfulAdditionMessage = "Successfully added.";
        private readonly EntryRepository entryRepo;
        private readonly ILogger<BudgetsController> logger;
        private readonly IMapper mapper;
        private readonly int itemsPerPage;
        private readonly string orderByKey;
        private readonly string filterKey;
        private readonly string searchCriteriaKey;

        public BudgetsController(EntryRepository entryRepo, IConfiguration config, ILogger<BudgetsController> logger, IMapper mapper)
        {
            this.entryRepo = entryRepo;
            this.logger = logger;
            this.mapper = mapper;
            string items = config["Pagination:ItemsPerPage"] ?? throw new InvalidOperationException("Pagination settings in the configuration aren't initialized.");
            itemsPerPage = int.Parse(items);
            orderByKey = config["Session:Keys:BudgetsOrderBy"] ?? throw new InvalidOperationException("Session Keys settings in the configuration aren't initialized.");
            filterKey = config["Session:Keys:BudgetsFilterOption"] ?? throw new InvalidOperationException("Session Keys settings in the configuration aren't initialized.");
            searchCriteriaKey = config["Session:Keys:BudgetsSearchCriteria"] ?? throw new InvalidOperationException("Session Keys settings in the configuration aren't initialized.");
        }

        [ImportModelState]
        public async Task<IActionResult> Index(int page = 1)
        {
            OrderByDto? orderBy = HttpContext.Session.Get<OrderByDto?>(orderByKey);
            if (orderBy is null)
            {
                orderBy = new OrderByDto();
                HttpContext.Session.Set(orderByKey, orderBy);
            }

            var order = (OrderBy)(orderBy.Property | orderBy.Direction);
            var filterVm = HttpContext.Session.Get<BudgetFilterVm?>(filterKey);
            if (filterVm is null)
            {
                filterVm = new BudgetFilterVm();
                filterVm.FilterOption = FilterOption.Active;
                HttpContext.Session.Set(filterKey, filterVm);
            }

            var userId = User.GetUserId();
            var searchCriteria = HttpContext.Session.Get<BudgetCriteriaDto>(searchCriteriaKey);
            (var totalItems, var budgets) = await entryRepo.GetBudgetsAsync(order, filterVm.FilterOption, searchCriteria, userId, page, itemsPerPage);
            List<SelectListItem> selectCategories = new();
            foreach (var category in entryRepo.GetCategories(userId, EntryName.Expense))
            {
                selectCategories.Add(new(category.CategoryName, category.Id.ToString()));
            }

            var pagingInfo = new PagingInfo()
            {
                CurrentPage = page,
                ItemsPerPage = itemsPerPage,
                TotalItems = totalItems
            };
            return View(new BudgetsVm()
            {
                Filter = filterVm,
                NewBudget = new() { Categories = selectCategories },
                SearchForm = new() { Criteria = searchCriteria ?? new BudgetCriteriaDto(), Categories = selectCategories },
                Budgets = budgets,
                Order = orderBy,
                PagingInfo = pagingInfo
            });
        }

        [HttpPost]
        [ExportModelState]
        public async Task<IActionResult> Index(BudgetFormVm budgetForm)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index));
            }

            if (budgetForm.BudgetDto is null)
            {
                logger.LogError($"{nameof(BudgetFormVm)} instance of the {nameof(Budget)} form is null.");
                return RedirectToAction(nameof(Index));
            }

            var budget = mapper.Map<Budget>(budgetForm.BudgetDto);
            try
            {
                await entryRepo.InsertBudgetAsync(budget);
            }
            catch (InvalidOperationException)
            {
                ModelState.AddModelError($"{nameof(budgetForm.BudgetDto)}.{nameof(budgetForm.BudgetDto.CategoryId)}", "Chosen category already has an active budget. Close that budget to add a new one.");
                return RedirectToAction(nameof(Index));
            }

            TempData["Success"] = SuccessfulAdditionMessage;
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Filter(BudgetFilterVm filterForm)
        {
            var selectedOption = filterForm.FilterOptions.Find(f => f.Value == filterForm.FilterOption.ToString());
            if (selectedOption is null)
            {
                logger.LogError("{controller} {action} got a non-existent {enum} enum value", nameof(BudgetsController), nameof(Filter), nameof(FilterOption));
                throw new InvalidOperationException();
            }

            selectedOption.Selected = true;
            HttpContext.Session.Set(filterKey, filterForm);
            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        public IActionResult Order(OrderByDto orderDto)
        {
            var directionItem = orderDto.DirectionOptions.Find(i => i.Value == orderDto.Direction.ToString());
            var propertyItem = orderDto.PropertyOptions.Find(i => i.Value == orderDto.Property.ToString());
            if (directionItem is null || propertyItem is null)
            {
                logger.LogError("{controller} {action} got a non-existent {enum} enum value", nameof(BudgetsController), nameof(Order), nameof(OrderBy));
                throw new InvalidOperationException();
            }

            directionItem.Selected = true;
            propertyItem.Selected = true;
            OrderBy orderBy = (OrderBy)(orderDto.Property | orderDto.Direction);
            if (orderBy == OrderBy.None)
            {
                logger.LogError("Invalid {orderBy} combination in the {controller}", nameof(OrderBy), nameof(BudgetsController));
                return RedirectToAction(nameof(Index));
            }

            HttpContext.Session.Set(orderByKey, orderDto);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Search(BudgetSearchFormVm searchForm)
        {
            HttpContext.Session.Set(searchCriteriaKey, searchForm.Criteria);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult ResetSearch(BudgetSearchFormVm searchForm)
        {
            HttpContext.Session.Remove(searchCriteriaKey);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task <IActionResult> Close(int categoryId)
        {
            await entryRepo.CloseCategoryBudget(categoryId);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task <IActionResult> Delete(BudgetVm budgetVm, int page = 1)
        {
            await entryRepo.DeleteAsync(budgetVm.Budget);
            return RedirectToAction(nameof(Index), new { page });
        }

        [HttpPost]
        public IActionResult Edit(BudgetVm budgetVm, int page = 1)
        {
            return View(new BudgetEditVm()
            {
                BudgetDto = mapper.Map<BudgetDto>(budgetVm.Budget),
                ReturnPage = page
            });
        }

        [HttpPost]
        public async Task<IActionResult> PerformEdit(BudgetEditVm budgetEditVm)
        {
            var budget = mapper.Map<Budget>(budgetEditVm.BudgetDto);
            await entryRepo.EditBudgetAsync(budget);
            return RedirectToAction(nameof(Index), new { page = budgetEditVm.ReturnPage });
        }
    }
}

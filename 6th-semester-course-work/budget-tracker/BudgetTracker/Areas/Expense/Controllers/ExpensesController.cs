using AutoMapper;
using BudgetTracker.Infrastructure;
using BudgetTracker.Infrastructure.ModelState;
using BudgetTracker.Models;
using BudgetTracker.Models.DataObjects;
using BudgetTracker.Models.ViewModels;
using BudgetTracker.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BudgetTracker.Areas.Expense.Controllers
{
    [Area("Expense")]
    [Authorize]
    public class ExpensesController : Controller
    {
        private const string SuccessfulAdditionMessage = "Successfully added.";
        private readonly ILogger<ExpensesController> logger;
        private readonly EntryRepository entryRepo;
        private readonly IMapper mapper;
        private readonly int itemsPerPage;
        private readonly string orderByKey;
        private readonly string searchCriteriaKey;

        public ExpensesController(ILogger<ExpensesController> logger, EntryRepository entryRepo, IMapper mapper, IConfiguration config)
        {
            string items = config["Pagination:ItemsPerPage"] ?? throw new InvalidOperationException("Pagination settings in the configuration aren't initialized.");
            itemsPerPage = int.Parse(items);
            orderByKey = config["Session:Keys:ExpensesOrderBy"] ?? throw new InvalidOperationException("Session Keys settings in the configuration aren't initialized.");
            searchCriteriaKey = config["Session:Keys:ExpensesSearchCriteria"] ?? throw new InvalidOperationException("Session Keys settings in the configuration aren't initialized.");
            this.logger = logger;
            this.entryRepo = entryRepo;
            this.mapper = mapper;
        }

        [ImportModelState]
        public async Task<IActionResult> Index(int page = 1, int? budgetId = null)
        {
            var userId = User.GetUserId();
            List<SelectListItem> selectCategories = new();
            foreach (var category in entryRepo.GetCategories(userId, EntryName.Expense))
            {
                selectCategories.Add(new(category.CategoryName, category.Id.ToString()));
            }

            OrderByDto? orderBy = HttpContext.Session.Get<OrderByDto?>(orderByKey);
            if (orderBy is null)
            {
                orderBy = new OrderByDto();
                HttpContext.Session.Set(orderByKey, orderBy);
            }

            var searchCriteria = HttpContext.Session.Get<EntryCriteriaDto>(searchCriteriaKey);
            OrderBy order = (OrderBy)(orderBy.Property | orderBy.Direction);
            var (totalItems, incomes) = await entryRepo.GetEntriesAsync(order, searchCriteria, userId, budgetId, EntryName.Expense, page, itemsPerPage);
            var pagingInfo = new PagingInfo()
            {
                CurrentPage = page,
                ItemsPerPage = itemsPerPage,
                TotalItems = totalItems
            };
            return View(new EntriesVm()
            {
                NewEntry = new() { Categories = selectCategories, EntryType = EntryName.Expense },
                SearchForm = new() { Criteria = searchCriteria ?? new EntryCriteriaDto(), Categories = selectCategories, EntryType = EntryName.Expense },
                Entries = incomes,
                Order = orderBy,
                PagingInfo = pagingInfo,
                BudgetId = budgetId
            });
        }

        [HttpPost]
        [ExportModelState]
        public async Task<IActionResult> Index(EntryFormVm incomeForm)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index));
            }

            if (incomeForm.EntryDto is null)
            {
                logger.LogError("Income instance of the Income form is null.");
                return RedirectToAction(nameof(Index));
            }

            var income = mapper.Map<Entry>(incomeForm.EntryDto);
            await entryRepo.InsertEntryAsync(income);
            TempData["Success"] = SuccessfulAdditionMessage;
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Order(OrderByDto orderDto)
        {
            var directionItem = orderDto.DirectionOptions.Find(i => i.Value == orderDto.Direction.ToString());
            var propertyItem = orderDto.PropertyOptions.Find(i => i.Value == orderDto.Property.ToString());
            if (directionItem is null || propertyItem is null)
            {
                logger.LogError("{controller} {action} got a non-existent {enum} enum value", nameof(ExpensesController), nameof(Order), nameof(OrderBy));
                throw new InvalidOperationException();
            }

            directionItem.Selected = true;
            propertyItem.Selected = true;
            OrderBy orderBy = (OrderBy)(orderDto.Property | orderDto.Direction);
            if (orderBy == OrderBy.None)
            {
                logger.LogError("Invalid {orderBy} combination in the {controller}", nameof(OrderBy), nameof(ExpensesController));
                return RedirectToAction(nameof(Index));
            }

            HttpContext.Session.Set(orderByKey, orderDto);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Search(EntrySearchFormVm searchForm)
        {
            HttpContext.Session.Set(searchCriteriaKey, searchForm.Criteria);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult ResetSearch(EntrySearchFormVm searchForm)
        {
            HttpContext.Session.Remove(searchCriteriaKey);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Entry entry, int page = 1, int? budgetId = null)
        {
            await entryRepo.DeleteEntryAsync(entry);
            return RedirectToAction(nameof(Index), new { page, budgetId });
        }

        [HttpPost]
        public IActionResult Edit(Entry entry, string stringTags, int entryTypeId, int page = 1, int? budgetId = null)
        {
            EntryDto entryDto = new()
            {
                Id = entry.Id,
                Description = entry.Description,
                StringTags = stringTags,
                Amount = entry.Amount
            };

            return View(new EntryEditVm()
            {
                EntryDto = entryDto,
                EntryType = (EntryName)entryTypeId,
                ReturnPage = page,
                ReturnBudgetId = budgetId
            });
        }

        [HttpPost]
        public async Task<IActionResult> PerformEdit(EntryEditVm entryEditVm)
        {
            if (!ModelState.IsValid)
            {
                return View(nameof(Edit), entryEditVm);
            }

            await entryRepo.EditEntryAsync(mapper.Map<Entry>(entryEditVm.EntryDto));
            return RedirectToAction(nameof(Index), new { page = entryEditVm.ReturnPage });
        }
    }
}

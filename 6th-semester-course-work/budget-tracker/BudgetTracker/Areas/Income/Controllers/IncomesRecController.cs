using AutoMapper;
using BudgetTracker.Infrastructure;
using BudgetTracker.Infrastructure.ModelState;
using BudgetTracker.Models;
using BudgetTracker.Models.DataObjects;
using BudgetTracker.Models.ViewModels;
using BudgetTracker.Repositories;
using BudgetTracker.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BudgetTracker.Areas.Income.Controllers
{
    [Area("Income")]
    [Authorize]
    public class IncomesRecController : Controller
    {
        private const string SuccessfulAdditionMessage = "Successfully added.";
        private readonly ILogger<IncomesRecController> logger;
        private readonly EntryRepository entryRepo;
        private readonly IMapper mapper;
        private readonly RecurringJobs recurringJobs;
        private readonly int itemsPerPage;
        private readonly string orderByKey;
        private readonly string searchCriteriaKey;

        public IncomesRecController(ILogger<IncomesRecController> logger, EntryRepository entryRepo, IMapper mapper, IConfiguration config, RecurringJobs recurringJobs)
        {
            string items = config["Pagination:ItemsPerPage"] ?? throw new InvalidOperationException("Pagination settings in the configuration aren't initialized.");
            itemsPerPage = int.Parse(items);
            orderByKey = config["Session:Keys:IncomesRecOrderBy"] ?? throw new InvalidOperationException("Session Keys settings in the configuration aren't initialized.");
            searchCriteriaKey = config["Session:Keys:IncomesRecSearchCriteria"] ?? throw new InvalidOperationException("Session Keys settings in the configuration aren't initialized.");
            this.logger = logger;
            this.entryRepo = entryRepo;
            this.mapper = mapper;
            this.recurringJobs = recurringJobs;
        }

        [ImportModelState]
        public async Task<IActionResult> Index(int page = 1)
        {
            var userId = User.GetUserId();
            List<SelectListItem> selectCategories = new();
            foreach (var category in entryRepo.GetCategories(userId, EntryName.Income))
            {
                selectCategories.Add(new(category.CategoryName, category.Id.ToString()));
            }

            OrderByDto? orderBy = HttpContext.Session.Get<OrderByDto?>(orderByKey);
            if (orderBy is null)
            {
                orderBy = new OrderByDto();
                HttpContext.Session.Set(orderByKey, orderBy);
            }

            OrderBy order = (OrderBy)(orderBy.Property | orderBy.Direction);
            var searchCriteria = HttpContext.Session.Get<EntryCriteriaDto>(searchCriteriaKey);
            var (totalItems, incomes) = await entryRepo.GetEntriesRecAsync(order, searchCriteria, userId, EntryName.Income, page, itemsPerPage);
            var pagingInfo = new PagingInfo()
            {
                CurrentPage = page,
                ItemsPerPage = itemsPerPage,
                TotalItems = totalItems
            };
            return View(new EntriesRecVm()
            {
                NewEntry = new() { Categories = selectCategories, EntryType = EntryName.Income },
                SearchForm = new() { Categories = selectCategories },
                EntriesRec = incomes,
                Order = orderBy,
                PagingInfo = pagingInfo
            });
        }

        [HttpPost]
        [ExportModelState]
        public async Task<IActionResult> Index(EntryRecFormVm incomeRecForm)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index));
            }

            if (incomeRecForm.EntryRecDto is null)
            {
                logger.LogError("Income instance of the Income form is null.");
                return RedirectToAction(nameof(Index));
            }

            var entryRecDto = incomeRecForm.EntryRecDto;
            if (entryRecDto.StartDate > entryRecDto.EndDate)
            {
                ModelState.AddModelError($"{nameof(incomeRecForm.EntryRecDto)}.{nameof(incomeRecForm.EntryRecDto.EndDate)}", "Termination date must be further than today.");
                return RedirectToAction(nameof(Index));
            }

            var incomeRec = mapper.Map<EntryRecurring>(entryRecDto);
            await entryRepo.InsertEntryRecAsync(incomeRec);
            var entry = mapper.Map<Entry>(incomeRec);
            await entryRepo.InsertEntryAsync(entry);
            recurringJobs.AddRecurringEntryAsync(incomeRec, EntryName.Income);

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
                logger.LogError("{controller} {action} got a non-existent {enum} enum value", nameof(IncomesController), nameof(Order), nameof(OrderBy));
                throw new InvalidOperationException();
            }

            directionItem.Selected = true;
            propertyItem.Selected = true;
            OrderBy orderBy = (OrderBy)(orderDto.Property | orderDto.Direction);
            if (orderBy == OrderBy.None)
            {
                logger.LogError("Invalid {orderBy} combination in the {controller}", nameof(OrderBy), nameof(IncomesController));
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
        public async Task<IActionResult> Delete(EntryRecurring entryRec, int page = 1)
        {
            await entryRepo.DeleteAsync(entryRec);
            recurringJobs.RemoveRecurringEntry(entryRec.Id, EntryName.Income);
            return RedirectToAction(nameof(Index), new { page });
        }

        [HttpPost]
        public IActionResult Edit(Entry entry, string stringTags, int entryTypeId, int page = 1)
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
                ReturnPage = page
            });
        }

        [HttpPost]
        public async Task<IActionResult> PerformEdit(EntryEditVm entryEditVm)
        {
            if (!ModelState.IsValid)
            {
                return View(nameof(Edit), entryEditVm);
            }

            var entry = mapper.Map<Entry>(entryEditVm.EntryDto);
            await entryRepo.EditEntryRecurringAsync(mapper.Map<EntryRecurring>(entry));
            return RedirectToAction(nameof(Index), new { page = entryEditVm.ReturnPage });
        }
    }
}

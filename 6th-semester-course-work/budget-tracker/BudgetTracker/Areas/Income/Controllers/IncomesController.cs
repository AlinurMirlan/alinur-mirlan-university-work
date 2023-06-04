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

namespace BudgetTracker.Areas.Income.Controllers
{
    [Area("Income")]
    [Authorize]
    public class IncomesController : Controller
    {
        private const string SuccessfulAdditionMessage = "Successfully added.";
        private readonly ILogger<IncomesController> logger;
        private readonly EntryRepository incomeRepo;
        private readonly IMapper mapper;
        private readonly int itemsPerPage;
        private readonly string orderByKey;

        public IncomesController(ILogger<IncomesController> logger, EntryRepository entryRepo, IMapper mapper, IConfiguration config)
        {
            string items = config["Pagination:ItemsPerPage"] ?? throw new InvalidOperationException("Pagination settings in the configuration aren't initialized.");
            itemsPerPage = int.Parse(items);
            orderByKey = config["Session:Keys:IncomesOrderBy"] ?? throw new InvalidOperationException("Session Keys settings in the configuration aren't initialized.");
            this.logger = logger;
            this.incomeRepo = entryRepo;
            this.mapper = mapper;
        }

        [ImportModelState]
        public async Task<IActionResult> Index(int page = 1)
        {
            var userId = User.GetUserId();
            List<SelectListItem> selectCategories = new();
            foreach (var category in incomeRepo.GetCategories(userId, EntryName.Income))
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
            var (totalItems, incomes) = await incomeRepo.GetEntriesAsync(order, userId, EntryName.Income, page, itemsPerPage);
            var pagingInfo = new PagingInfo()
            {
                CurrentPage = page,
                ItemsPerPage = itemsPerPage,
                TotalItems = totalItems
            };
            return View(new EntriesVm()
            {
                NewEntry = new() { Categories = selectCategories, EntryType = EntryName.Income },
                SearchForm = new() { Categories = selectCategories },
                Entries = incomes,
                Order = orderBy,
                PagingInfo = pagingInfo
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

            if (incomeForm.IncomeDto is null)
            {
                logger.LogError("Income instance of the Income form is null.");
                return RedirectToAction(nameof(Index));
            }

            var userId = User.GetUserId();
            var income = mapper.Map<Entry>(incomeForm.IncomeDto);
            await incomeRepo.InsertEntryAsync(userId, income);
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
    }
}

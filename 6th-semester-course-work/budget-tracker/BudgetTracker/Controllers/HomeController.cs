using AutoMapper;
using BudgetTracker.Infrastructure.ModelState;
using BudgetTracker.Infrastructure;
using BudgetTracker.Models;
using BudgetTracker.Models.DataObjects;
using BudgetTracker.Models.ViewModels;
using BudgetTracker.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BudgetTracker.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly EntryRepository entryRepo;
        private readonly IMapper mapper;
        private readonly int itemsPerPage;
        private readonly string orderByKey;
        private readonly string searchCriteriaKey;

        public HomeController(ILogger<HomeController> logger, EntryRepository entryRepo, IConfiguration config, IMapper mapper)
        {
            string items = config["Pagination:ItemsPerPage"] ?? throw new InvalidOperationException("Pagination settings in the configuration aren't initialized.");
            itemsPerPage = int.Parse(items);
            orderByKey = config["Session:Keys:EntriesOrderBy"] ?? throw new InvalidOperationException("Session Keys settings in the configuration aren't initialized.");
            searchCriteriaKey = config["Session:Keys:EntriesSearchCriteria"] ?? throw new InvalidOperationException("Session Keys settings in the configuration aren't initialized.");
            this.logger = logger;
            this.entryRepo = entryRepo;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var userId = User.GetUserId();
            OrderByDto? orderBy = HttpContext.Session.Get<OrderByDto?>(orderByKey);
            if (orderBy is null)
            {
                orderBy = new OrderByDto();
                HttpContext.Session.Set(orderByKey, orderBy);
            }

            OrderBy order = (OrderBy)(orderBy.Property | orderBy.Direction);
            var searchCriteria = HttpContext.Session.Get<EntryCriteriaDto>(searchCriteriaKey);
            var (totalItems, incomes) = await entryRepo.GetEntriesAsync(order, searchCriteria, userId, page, itemsPerPage);
            var pagingInfo = new PagingInfo()
            {
                CurrentPage = page,
                ItemsPerPage = itemsPerPage,
                TotalItems = totalItems
            };
            return View(new EntriesVm()
            {
                SearchForm = new() { Criteria = searchCriteria ?? new EntryCriteriaDto() },
                Entries = incomes,
                Order = orderBy,
                PagingInfo = pagingInfo
            });
        }

        [HttpPost]
        public IActionResult Order(OrderByDto orderDto)
        {
            var directionItem = orderDto.DirectionOptions.Find(i => i.Value == orderDto.Direction.ToString());
            var propertyItem = orderDto.PropertyOptions.Find(i => i.Value == orderDto.Property.ToString());
            if (directionItem is null || propertyItem is null)
            {
                logger.LogError("{controller} {action} got a non-existent {enum} enum value", nameof(HomeController), nameof(Order), nameof(OrderBy));
                throw new InvalidOperationException();
            }

            directionItem.Selected = true;
            propertyItem.Selected = true;
            OrderBy orderBy = (OrderBy)(orderDto.Property | orderDto.Direction);
            if (orderBy == OrderBy.None)
            {
                logger.LogError("Invalid {orderBy} combination in the {controller}", nameof(OrderBy), nameof(HomeController));
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
        public async Task<IActionResult> Delete(Entry entry, int page = 1)
        {
            await entryRepo.DeleteEntryAsync(entry);
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
            await entryRepo.EditEntryAsync(mapper.Map<Entry>(entryEditVm.EntryDto));
            return RedirectToAction(nameof(Index), new { page = entryEditVm.ReturnPage });
        }
    }
}
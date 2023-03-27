using CookBook.Infrastructure;
using CookBook.Library.Entities;
using CookBook.Library.Repositories.Abstractions;
using CookBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace CookBook.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ITabRepository _tabRepo;

        public OrdersController(ITabRepository tabRepo)
        {
            this._tabRepo = tabRepo;
        }
        public IActionResult Index()
        {
            bool orderBy = !HttpContext.Session.Get<bool>("orderByDescending");
            IList<Tab> tabs = _tabRepo.GetTabs(orderBy);
            string? returnUrl = Url.Action(nameof(Index));
            OrdersViewModel viewModel = new() { Tabs = tabs, OrderByDescending = orderBy, ReturnUrl = returnUrl };
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Index(bool orderByDescending, string returnUrl)
        {
            HttpContext.Session.Set("orderByDescending", orderByDescending);
            return Redirect(returnUrl);
        }

        public IActionResult Order(int tabId, int tabNumber)
        {
            Tab tab = _tabRepo.GetTabDishes(tabId);
            tab.TabNumber = tabNumber;
            return View(tab);
        }

        public IActionResult OrdersByDate(DateTime orderDate)
        {
            bool orderBy = !HttpContext.Session.Get<bool>("orderByDescending");
            if (orderDate == default)
                return RedirectToAction(nameof(Index));

            IList<Tab> tabs = _tabRepo.GetTabsByDate(orderDate, orderBy);
            string? returnUrl = Url.Action(nameof(OrdersByDate), new { orderDate = orderDate });
            OrdersViewModel viewModel = new() { Tabs = tabs, OrderByDescending = orderBy, Date = orderDate, ReturnUrl = returnUrl };
            return View(nameof(Index), viewModel);
        }

        [HttpPost]
        public IActionResult Expenditure(DateTime dateStart, DateTime dateEnd)
        {
            if (dateStart > dateEnd || dateStart == default || dateEnd == default)
                return RedirectToAction(nameof(Index));

            IList<IngredientExpenditure> expenditures = _tabRepo.GetProvisionExpenditure(dateStart, dateEnd);
            ExpenditureViewModel model = new() { DateStart = dateStart, DateEnd = dateEnd, Expenditures = expenditures };
            return View(model);
        }
    }
}

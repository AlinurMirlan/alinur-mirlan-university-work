using CookBook.Infrastructure;
using CookBook.Library.Entities;
using CookBook.Library.Repositories.Abstractions;
using CookBook.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CookBook.Controllers
{
    public class StoreController : Controller
    {
        private readonly IDishRepository _dishRepo;

        public StoreController(IDishRepository dishRepo)
        {
            _dishRepo = dishRepo;
        }

        public IActionResult Index()
        {
            Tab? tab = HttpContext.Session.Get<Tab>("tab") ?? new();
            MenuTabViewModel menuTab = new()
            {
                Tab = tab,
                Dishes = _dishRepo.GetDishes()
            };

            return View(menuTab);
        }

        [HttpPost]
        public IActionResult Index(Dish dish)
        {
            Tab? tab = HttpContext.Session.Get<Tab>("tab") ?? new();
            tab.AddDish(dish);
            HttpContext.Session.Set("tab", tab);
            MenuTabViewModel menuTab = new()
            {
                Tab = tab,
                Dishes = _dishRepo.GetDishes()
            };

            return View(menuTab);
        }

        [HttpPost]
        public IActionResult OrderUp(int dishId)
        {
            Tab? tab = HttpContext.Session.Get<Tab>("tab") ?? new();
            tab.OrderUpDish(dishId);
            HttpContext.Session.Set("tab", tab);
            return RedirectToAction(nameof(Order));
        }


        [HttpPost]
        public IActionResult OrderDown(int dishId)
        {
            Tab? tab = HttpContext.Session.Get<Tab>("tab") ?? new();
            tab.OrderDownDish(dishId);
            HttpContext.Session.Set("tab", tab);
            return RedirectToAction(nameof(Order));
        }

        public IActionResult Order()
        {
            Tab? tab = HttpContext.Session.Get<Tab>("tab") ?? new();
            return View(tab);
        }

        public IActionResult FinishOrder(int itemsOrdered, [FromServices] ITabRepository tabRepo)
        {
            if (itemsOrdered == 0)
                return RedirectToAction(nameof(Order));

            Tab tab = HttpContext.Session.Get<Tab>("tab")!;
            tabRepo.AddTab(tab);
            HttpContext.Session.Clear();
            tab = new();
            HttpContext.Session.Set("tab", tab);
            MenuTabViewModel menuTab = new()
            {
                Tab = tab,
                Dishes = _dishRepo.GetDishes(),
                OrderFinished = true
            };

            return View(nameof(Index), menuTab);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
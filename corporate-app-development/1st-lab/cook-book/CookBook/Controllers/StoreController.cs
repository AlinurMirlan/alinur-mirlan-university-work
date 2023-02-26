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
            Cart? cart = HttpContext.Session.Get<Cart>("cart") ?? new();
            DishesNCart dishesNCart = new()
            {
                Cart = cart,
                Dishes = _dishRepo.GetDishes()
            };

            return View(dishesNCart);
        }

        [HttpPost]
        public IActionResult Index(Dish dish)
        {
            Cart? cart = HttpContext.Session.Get<Cart>("cart") ?? new();
            cart.AddOneItem(dish);
            HttpContext.Session.Set("cart", cart);
            DishesNCart dishesNCart = new()
            {
                Cart = cart,
                Dishes = _dishRepo.GetDishes()
            };

            return View(dishesNCart);
        }

        public IActionResult Order()
        {
            Cart? cart = HttpContext.Session.Get<Cart>("cart") ?? new();
            return View(cart);
        }

        public IActionResult FinishOrder(int itemsOrdered)
        {
            if (itemsOrdered == 0)
                return RedirectToAction(nameof(Order));

            HttpContext.Session.Clear();
            Cart cart = new();
            HttpContext.Session.Set("cart", cart);
            DishesNCart dishesNCart = new()
            {
                Cart = cart,
                Dishes = _dishRepo.GetDishes(),
                OrderFinished = true
            };

            return View(nameof(Index), dishesNCart);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
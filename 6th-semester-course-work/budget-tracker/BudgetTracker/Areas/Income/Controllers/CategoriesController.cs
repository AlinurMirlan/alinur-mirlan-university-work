using BudgetTracker.Areas.Income.Models;
using BudgetTracker.Areas.Income.Models.ViewModels;
using BudgetTracker.Areas.Income.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BudgetTracker.Areas.Income.Controllers
{
    [Area("Income")]
    public class CategoriesController : Controller
    {
        private const string SuccessfulAdditionMessage = "Successfully added.";
        private readonly ILogger<CategoriesController> logger;
        private readonly IncomeRepository incomeRepo;

        public CategoriesController(ILogger<CategoriesController> logger, IncomeRepository incomeRepo)
        {
            this.logger = logger;
            this.incomeRepo = incomeRepo;
        }

        public IActionResult Index()
        {
            CategoriesViewModel viewModel = new()
            {
                Categories = incomeRepo.GetCategories()
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Index(IncomeCategory category)
        {
            if (!ModelState.IsValid)
            {
                return View(new CategoriesViewModel()
                {
                    NewCategory = category,
                    Categories = incomeRepo.GetCategories()
                });
            }

            var incomeCategory = await incomeRepo.GetCategoryAsync(category.CategoryName);
            if (incomeCategory is not null)
            {
                logger.LogInformation("Attempt to insert a category with a reserved name: {name}.", category.CategoryName);
                ModelState.AddModelError(nameof(IncomeCategory.CategoryName), "Entered category already exists.");
                return View(new CategoriesViewModel()
                {
                    NewCategory = category,
                    Categories = incomeRepo.GetCategories()
                });
            }

            await incomeRepo.AddAsync(category);
            TempData["Success"] = SuccessfulAdditionMessage;
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int categoryId)
        {
            var category = await incomeRepo.GetCategoryAsync(categoryId);
            if (category is null)
            {
                logger.LogWarning("Attemp to delete a non-existent category with the id: {id}", categoryId);
                return RedirectToAction(nameof(Index));
            }

            await incomeRepo.DeleteAsync(category);
            return RedirectToAction(nameof(Index));
        }
    }
}

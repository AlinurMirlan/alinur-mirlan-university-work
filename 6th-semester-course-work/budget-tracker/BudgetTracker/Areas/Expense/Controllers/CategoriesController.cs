using AutoMapper;
using BudgetTracker.Infrastructure;
using BudgetTracker.Infrastructure.ModelState;
using BudgetTracker.Models;
using BudgetTracker.Models.DataObjects;
using BudgetTracker.Models.ViewModels;
using BudgetTracker.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BudgetTracker.Areas.Expense.Controllers
{
    [Area("Expense")]
    [Authorize]
    public class CategoriesController : Controller
    {
        private const string SuccessfulAdditionMessage = "Successfully added.";
        private readonly int itemsPerPage;
        private readonly string searchCriteriaKey;
        private readonly ILogger<CategoriesController> logger;
        private readonly EntryRepository entryRepo;
        private readonly IMapper mapper;

        public CategoriesController(ILogger<CategoriesController> logger, EntryRepository entryRepo, IMapper mapper, IConfiguration config)
        {
            string items = config["Pagination:ItemsPerPage"] ?? throw new InvalidOperationException("Pagination settings aren't initialized.");
            itemsPerPage = int.Parse(items);
            searchCriteriaKey = config["Session:Keys:ExpensesCategoryCriteria"] ?? throw new InvalidOperationException("Session Keys settings in the configuration aren't initialized.");
            this.logger = logger;
            this.entryRepo = entryRepo;
            this.mapper = mapper;
        }

        [ImportModelState]
        public IActionResult Index(int page = 1)
        {
            var userId = User.GetUserId();
            var searchCriteria = HttpContext.Session.Get<CategoryCriteriaDto>(searchCriteriaKey);
            var categories = entryRepo.GetCategories(userId, searchCriteria?.Category, EntryName.Expense, page, itemsPerPage, out int totalItems);
            var viewModel = new CategoriesVm()
            {
                NewCategory = new() { UserId = userId },
                Categories = categories,
                PagingInfo = new()
                {
                    CurrentPage = page,
                    TotalItems = totalItems,
                    ItemsPerPage = itemsPerPage
                }
            };

            return View(viewModel);
        }

        [HttpPost]
        [ExportModelState]
        public async Task<IActionResult> Index(CategoryDto categoryDto)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index));
            }

            var category = mapper.Map<Category>(categoryDto);
            try
            {
                await entryRepo.InsertCategoryAsync(category, EntryName.Expense);
            }
            catch (InvalidOperationException)
            {
                logger.LogInformation("Attempt to insert a category with a reserved name: {name}.", categoryDto.CategoryName);
                ModelState.AddModelError(nameof(Category.CategoryName), "Entered category already exists.");
                return RedirectToAction(nameof(Index));
            }

            TempData["Success"] = SuccessfulAdditionMessage;
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int categoryId)
        {
            await entryRepo.DeleteCategoryAsync(categoryId);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Edit(Category category, int page =  1)
        {
            return View(new CategoryEditVm() 
            { 
                CategoryDto = mapper.Map<CategoryDto>(category),  
                ReturnPage = page
            });
        }

        [HttpPost]
        public async Task<IActionResult> PerformEdit(CategoryEditVm categoryEditVm)
        {
            if (!ModelState.IsValid)
            {
                return View(nameof(Edit), categoryEditVm);
            }

            try
            {
                await entryRepo.EditCategoryAsync(mapper.Map<Category>(categoryEditVm.CategoryDto));
            }
            catch (InvalidOperationException)
            {
                ModelState.AddModelError($"{nameof(categoryEditVm.CategoryDto)}.{nameof(categoryEditVm.CategoryDto.CategoryName)}", "There is already a category with the given name.");
                return View(nameof(Edit), categoryEditVm);
            }

            return RedirectToAction(nameof(Index), new { page = categoryEditVm.ReturnPage });
        }

        [HttpPost]
        public IActionResult Search(CategoryCriteriaDto criteria)
        {
            HttpContext.Session.Set(searchCriteriaKey, criteria);
            return RedirectToAction(nameof(Index));
        }
    }
}

using BudgetTracker.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BudgetTracker.Controllers.Income
{
    [Route("Income/Categories")]
    public class CategoriesController : Controller
    {
        private readonly ILogger<CategoriesController> logger;
        private readonly IIncomeRepository incomeRepo;

        public CategoriesController(ILogger<CategoriesController> logger, IIncomeRepository incomeRepo)
        {
            this.logger = logger;
            this.incomeRepo = incomeRepo;
        }

        public IActionResult Index()
        {   
            return View(incomeRepo.GetCategories());
        }
    }
}

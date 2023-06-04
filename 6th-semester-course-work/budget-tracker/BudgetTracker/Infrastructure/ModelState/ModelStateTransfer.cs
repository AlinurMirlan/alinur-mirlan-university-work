using Microsoft.AspNetCore.Mvc.Filters;

namespace BudgetTracker.Infrastructure.ModelState
{
    public abstract class ModelStateTransfer : ActionFilterAttribute
    {
        protected const string Key = nameof(ModelStateTransfer);
    }
}

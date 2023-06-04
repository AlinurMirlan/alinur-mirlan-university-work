using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace BudgetTracker.Infrastructure.ModelState
{
    public class ExportModelStateAttribute : ModelStateTransfer
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            //Only export when ModelState is not valid
            if (!context.ModelState.IsValid)
            {
                //Export if we are redirecting
                if (context.Result is RedirectResult
                    || context.Result is RedirectToRouteResult
                    || context.Result is RedirectToActionResult)
                {
                    var controller = context.Controller as Controller;
                    if (controller != null && context.ModelState != null)
                    {
                        var modelState = ModelStateHelpers.SerialiseModelState(context.ModelState);
                        controller.TempData[Key] = modelState;
                    }
                }
            }

            base.OnActionExecuted(context);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Employees.Controllers.ActionFilters
{
    public class ValidationFilterAttribute : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context) { }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var action = context.RouteData.Values["action"];
            var controller = context.RouteData.Values["controller"];
            var param = context
                .ActionArguments
                .SingleOrDefault(x => x.Value.ToString().Contains("Dto"))
                .Value;

            if (param is null)
            {
                context.Result = new BadRequestObjectResult(
                    $"object is null. Action: {action}, Controller: {controller}"
                );
                return;
            }

            if (!context.ModelState.IsValid)
                context.Result = new UnprocessableEntityObjectResult(context.ModelState);
        }
    }
}

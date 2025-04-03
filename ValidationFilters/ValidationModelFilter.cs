using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System.IO;

namespace URLSHORTENER.ValidationFilters;

public class ValidationModelFilter : IActionFilter
{
    private readonly ILogger<ValidationModelFilter> _logger;

    public ValidationModelFilter(ILogger<ValidationModelFilter> logger)
    {
        _logger = logger;
    }
    public void OnActionExecuting(ActionExecutingContext ctx)
    {
        if(!ctx.ModelState.IsValid) ctx.Result = new BadRequestObjectResult(ctx.ModelState);
    }

    public void OnActionExecuted(ActionExecutedContext ctx)
    {

    }
}

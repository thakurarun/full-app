using backend.Exceptions.Common;
using DataStore.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace backend.Controllers.ActionFilter
{
    public class ErrorResponse
    {
        public int Status { get; set; } = 500;
        public string Identifier { get; set; }
        public string ErrorMessage { get; set; }
    }

    public class HttpResponseExceptionFilter : IActionFilter, IOrderedFilter
    {
        public int Order { get; set; } = int.MaxValue - 10;

        public void OnActionExecuting(ActionExecutingContext context) { }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            switch (context.Exception)
            {
                case KnownDbException knownDbException:
                    {
                        context.Result = new JsonResult(new ErrorResponse
                        {
                            Identifier = knownDbException.Identifier,
                            ErrorMessage = knownDbException.ErrorMessage,
                            Status = knownDbException.StatusCode ?? 500
                        });
                        context.HttpContext.Response.StatusCode = knownDbException.StatusCode ?? 500;
                        context.ExceptionHandled = true;
                        break;
                    }
                case KnownException knownException:
                    {
                        context.Result = new JsonResult(new ErrorResponse
                        {
                            Identifier = knownException.Identifier,
                            ErrorMessage = knownException.ErrorMessage,
                            Status = knownException.StatusCode.StatusCode
                        });
                        context.HttpContext.Response.StatusCode = knownException.StatusCode.StatusCode;
                        context.ExceptionHandled = true;
                        break;
                    }
            }
        }
    }
}

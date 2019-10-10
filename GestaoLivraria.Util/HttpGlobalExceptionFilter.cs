using GestaoLivraria.Util.Exceptions;
using GestaoLivraria.Util.Util;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using System.Web.Mvc;
using ExceptionContext = Microsoft.AspNetCore.Mvc.Filters.ExceptionContext;
using IExceptionFilter = Microsoft.AspNetCore.Mvc.Filters.IExceptionFilter;

namespace GestaoLivraria.Util
{
    public class HttpGlobalExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is BadRequestException)
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            else if (context.Exception is NotFoundException)
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;

            else
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }

            var errorDetails = new ErrorDetails()
            {
                StatusCode = context.HttpContext.Response.StatusCode,
                Message = context.Exception.Message ?? Constantes.ERRO_NAO_TRATADO
            };

            context.Result = new ObjectResult(errorDetails);
            context.ExceptionHandled = true;
        }
    }
}

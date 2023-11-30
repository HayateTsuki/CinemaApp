using System;
using System.Reflection;
using Cinema.Core.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;

namespace Cinema.Core.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        private const string UnhandledExceptionCode = "UnhandledException";

        private const string UnhandledExceptionMessage = "Unhandled exception occured";

        public void OnException(ExceptionContext context)
        {
            var exception = context.Exception;
            if (exception is TargetInvocationException && exception.InnerException != null)
            {
                exception = exception.InnerException;
            }

            var errorResult = new ErrorResult(UnhandledExceptionMessage, UnhandledExceptionCode);
            int httpStatus = StatusCodes.Status500InternalServerError;
            switch (exception)
            {
                case OperationCanceledException _:
                    httpStatus = StatusCodes.Status400BadRequest;
                    errorResult = new ErrorResult("Request cancelled", "OperationCancelled");
                    break;
                case BusinessOperationException e:
                    httpStatus = StatusCodes.Status400BadRequest;
                    errorResult = new ErrorResult(e.ErrorMessage, "OperationFailed");
                    Log.Warning(e, e.ErrorMessage);
                    break;
                case EntityNotFoundException e:
                    httpStatus = StatusCodes.Status404NotFound;
                    Log.Warning(e, "Id {0} not found.", e.Id);
                    errorResult = new ErrorResult(e.ErrorMessage, "EntityNotFound");
                    break;
                case EnitityAlreadyExists e:
                    httpStatus = StatusCodes.Status400BadRequest;
                    errorResult = new ErrorResult(e.ErrorMessage, "EntityAlreadyExists");
                    break;
            }

            context.HttpContext.Response.ContentType = System.Net.Mime.MediaTypeNames.Application.Json;
            context.HttpContext.Response.StatusCode = httpStatus;
            context.Result = new JsonResult(errorResult);
            if (httpStatus == StatusCodes.Status500InternalServerError)
            {
                Log.Error(exception, exception.Message);
            }
        }
    }
}

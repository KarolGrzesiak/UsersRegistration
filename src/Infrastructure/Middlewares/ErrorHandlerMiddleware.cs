using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Application.Users.Exceptions;
using Domain.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Middlewares
{
    public class ErrorHandlerMiddleware : IMiddleware
    {
        private readonly IDictionary<Type, Func<HttpContext,Exception,Task>> _exceptionHandlers;

        public ErrorHandlerMiddleware()
        {
            _exceptionHandlers = new Dictionary<Type, Func<HttpContext,Exception,Task>>
            {
                { typeof(UserNotFoundException), HandleUserNotFoundException },
                { typeof(InvalidAggregateIdException), HandleInvalidAggregateIdException},
                { typeof(InvalidBuildingNumberException), HandleInvalidBuildingNumberException},
                { typeof(InvalidPersonalNumberException), HandleInvalidPersonalNumberException},
                { typeof(InvalidZipcodeException), HandleInvalidZipcodeException},
                { typeof(ValidationException), HandleValidationException},
            };
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception exception)
            {
                await HandleErrorAsync(context, exception);
            }
        }

        private async Task HandleErrorAsync(HttpContext context, Exception exception)
        {
            Type type = exception.GetType();
            if (_exceptionHandlers.ContainsKey(type))
            {
                await _exceptionHandlers[type].Invoke(context,exception);
                return;
            }

            await HandleUnknownException(context, exception);
        }

        private async Task HandleUnknownException(HttpContext context, Exception exception)
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";
            await JsonSerializer.SerializeAsync(context.Response.Body, "An error occurred when processing the request.");
        }

        private async Task  HandleInvalidZipcodeException(HttpContext context, Exception exception)
        {
            await HandleBadRequest(context, exception);
        }
        
        private async Task  HandleValidationException(HttpContext context, Exception exception)
        {
            await HandleBadRequest(context, exception);
        }

        private async Task  HandleInvalidPersonalNumberException(HttpContext context, Exception exception)
        {
            await HandleBadRequest(context, exception);
        }

        private async Task  HandleInvalidBuildingNumberException(HttpContext context, Exception exception)
        {
            await HandleBadRequest(context, exception);
        }

        private async Task  HandleInvalidAggregateIdException(HttpContext context, Exception exception)
        {
            await HandleBadRequest(context, exception);
        }
        
        private async Task HandleUserNotFoundException(HttpContext context, Exception exception)
        {
            if (exception is UserNotFoundException userNotFoundException)
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                context.Response.ContentType = "application/json";
                await JsonSerializer.SerializeAsync(context.Response.Body, userNotFoundException.Message);
            }
        }

        private async Task HandleBadRequest(HttpContext context, Exception exception)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            context.Response.ContentType = "application/json";
            await JsonSerializer.SerializeAsync(context.Response.Body, exception.Message);
        }
    }
}
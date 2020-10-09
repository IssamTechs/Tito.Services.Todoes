using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tito.Services.Todoes.Application.Exceptions;
using Tito.Services.Todoes.Core.Exceptions;

namespace Tito.Services.Todoes.Api
{
    public class ErrorHandlerMiddleware : IMiddleware
    {
        public readonly ILogger<ErrorHandlerMiddleware> _logger;

        private static readonly JsonSerializerSettings JsonSettings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            Formatting = Formatting.Indented
        };
        public ErrorHandlerMiddleware(ILogger<ErrorHandlerMiddleware> logger)
        {
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
                switch(exception)
                {
                    case AppException appException:
                        await HandleCustomException(context, appException.Code, appException.Message);
                        return;
                    case DomainException domainException:
                        await HandleCustomException(context, domainException.Code, domainException.Message);
                        return;
                    default: throw;
                }
                throw;
            }
            
        }

        private static async Task HandleCustomException(HttpContext context, string code, string message)
        {
            context.Response.StatusCode = 400;
            var response = new
            {
                code, message
            };
            await context.Response.WriteAsync(JsonConvert.SerializeObject(response, JsonSettings));
        }
    }
}

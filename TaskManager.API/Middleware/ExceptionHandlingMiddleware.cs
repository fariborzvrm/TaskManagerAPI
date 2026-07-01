using Microsoft.AspNetCore.Http;
using Microsoft.Identity.Client;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using TaskManager.API.Models;
using TaskManager.Application.Exceptions;




namespace TaskManager.API.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next,ILogger<ExceptionHandlingMiddleware> logger) { 
        
        _next = next;
        _logger = logger;

        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }

            catch (Exception ex) {

                _logger.LogError(ex, "Unhandled exception occurred. TraceId: {TraceId}", context.TraceIdentifier);

                var statusCode = ex switch
                {
                    Application.Exceptions.ArgumentException => HttpStatusCode.NotFound,
                    System.ArgumentException => HttpStatusCode.BadRequest,
                    UnauthorizedException => HttpStatusCode.Unauthorized,
                    _ => HttpStatusCode.InternalServerError
                };

                context.Response.StatusCode = (int)statusCode;

                var response = new ErrorResponse
                {
                     StatusCode = context.Response.StatusCode,
                     Message = ex.Message,
                     TraceId = context.TraceIdentifier
                };

                var json = JsonSerializer.Serialize(response);
                await context.Response.WriteAsync(json);
            }
        
        }
        

        }
    }


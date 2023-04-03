using System.Net;
using System.Text.Json;

using TicketBooking.Common.AppExceptions;

using BadRequestException = TicketBooking.Common.AppExceptions.BadRequestException;
using NotImplementedException = TicketBooking.Common.AppExceptions.NotImplementedException;
using UnauthorizedAccessException = TicketBooking.Common.AppExceptions.UnauthorizedAccessException;
using NotFoundException = TicketBooking.Common.AppExceptions.NotFoundException;
using System.Diagnostics;
using System;

namespace ErrorManagement.Configurations;

public class HandleExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger _logger;

    public HandleExceptionMiddleware(RequestDelegate next, ILogger<HandleExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Source, ex.HelpLink, ex.TargetSite);
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        HttpStatusCode status;
        string? requestId;
        string message;
        Activity? currentActiviy;
        var exceptionType = exception.GetType();

        if (exceptionType == typeof(BadRequestException))
        {
            requestId = Activity.Current?.Id ?? context.TraceIdentifier;
            message = "Bad request";
            status = HttpStatusCode.BadRequest;
            currentActiviy = Activity.Current;
        }
        else if (exceptionType == typeof(NotFoundException))
        {
            message = "Not Found";
            requestId = Activity.Current?.Id ?? context.TraceIdentifier;
            status = HttpStatusCode.NotFound;
            currentActiviy = Activity.Current;
        }
        else if (exceptionType == typeof(NotImplementedException))
        {
            status = HttpStatusCode.NotImplemented;
            message = "Not Implemented";
            requestId = Activity.Current?.Id ?? context.TraceIdentifier;
            currentActiviy = Activity.Current;
        }
        else if (exceptionType == typeof(UnauthorizedAccessException))
        {
            status = HttpStatusCode.Unauthorized;
            message = "Unauthorized Access";
            requestId = Activity.Current?.Id ?? context.TraceIdentifier;
            currentActiviy = Activity.Current;
        }
        else
        {
            status = HttpStatusCode.InternalServerError;
            message = "An error occurred while processing your request.";
            requestId = Activity.Current?.Id ?? context.TraceIdentifier;
            currentActiviy = Activity.Current;
        }

        var exceptionResult = JsonSerializer.Serialize(new { error = message, requestId, currentActiviy, status });
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)status;

        return context.Response.WriteAsync(exceptionResult);
    }
}

using Newtonsoft.Json;

namespace EmployeeManagement.RegisterServices
{
    public class GlobalExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;

        public GlobalExceptionHandlerMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                _logger.LogInformation(context.Request.ToString());
               
                await _next(context);
            }
            catch (Exception ex)
            {
                
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            _logger.LogError(exception, "An unhandled exception has occurred.");

            
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            
            var response = new ApiResponse<object>(new object(), "Something went wrong", StatusCodes.Status500InternalServerError);

            return context.Response.WriteAsJsonAsync(response);
        }
    }
}





using System.Net;

namespace NZWalks.API.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        //Inject ILogger for logging and RequestDelegate
        //RequestDelegate returns a task that represents the completion of a request processing and it's a function that can process an HTTP request
        private readonly ILogger<ExceptionHandlerMiddleware> logger;
        private readonly RequestDelegate next;

        public ExceptionHandlerMiddleware(ILogger<ExceptionHandlerMiddleware> logger,
            RequestDelegate next)
        {
            this.logger = logger;
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            //mimic try-catch block that we wish to add to every API
            try
            {
                await next(httpContext); //if anything happens during the calls,
                                         //we want to log the exceptions or
                                         //handle the exceptions at a single location (= in the catch block below)
            }
            catch (Exception ex)
            {
                //we want to send a unique identifier to the log file and send as part of the response
                var errorId = Guid.NewGuid();

                //Log this exception
                logger.LogError(ex,$"{errorId} : {ex.Message}");
                
                //Return a custom error response for consistency
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError; //500
                httpContext.Response.ContentType = "application/json";

                var error = new
                {
                    Id = errorId,
                    ErrorMessage = "Something went wrong. We are looking into resolving this."
                };

                await httpContext.Response.WriteAsJsonAsync(error);
            }
        }
    }
}

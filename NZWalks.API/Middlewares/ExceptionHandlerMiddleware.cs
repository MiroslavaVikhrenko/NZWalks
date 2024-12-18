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
    }
}

namespace NZWalks.API.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        //Inject ILogger for logging
        private readonly ILogger<ExceptionHandlerMiddleware> logger;   
        public ExceptionHandlerMiddleware(ILogger<ExceptionHandlerMiddleware> logger)
        {
            this.logger = logger;
        }
    }
}

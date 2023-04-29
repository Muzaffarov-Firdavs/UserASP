using ClassWork.Service.Exceptions;

namespace ClassWork.Web.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionHandlerMiddleware> logger;
        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }


        public async Task Invoke(HttpContext context)
        {
            try
            {
                await this.next(context);
            }
            catch (CustomException exception)
            {
                context.Response.StatusCode = exception.Code;
                await context.Response.WriteAsJsonAsync(new
                {
                    code = exception.Code,
                    Error = exception.Message
                });
            }
            catch(Exception ex)
            {
                this.logger.LogError($"{ex.ToString()}\n");
                context.Response.StatusCode = 500;
                await context.Response.WriteAsJsonAsync(new
                {
                    Code = 500,
                    Error = ex.Message
                });
            }
        }
    }
}

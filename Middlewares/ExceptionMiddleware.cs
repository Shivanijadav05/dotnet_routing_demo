namespace MyWebApi.Middlewares;
public class ExceptionMiddleware{
    
    private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next=next;
         }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(Exception)
            {
                context.Response.StatusCode=500;
               await context.Response.WriteAsJsonAsync(new
                    {
                        success = false,
                        statusCode = 500,
                        message = "Something went wrong",
                        traceId = context.TraceIdentifier
                    });
            }
        } 
}
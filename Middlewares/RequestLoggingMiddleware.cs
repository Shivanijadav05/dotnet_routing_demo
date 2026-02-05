namespace MyWebApi.Middlewares;



    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestLoggingMiddleware(RequestDelegate next)
        {
            _next=next;
         }
         public async Task InvokeAsync(HttpContext context)
         {
            Console.WriteLine("Before request");
            foreach (var header in context.Request.Headers)
                {
                    Console.WriteLine($"{header.Key}: {header.Value}");
                }

            await _next(context);

            Console.WriteLine("After request");
         }
    }



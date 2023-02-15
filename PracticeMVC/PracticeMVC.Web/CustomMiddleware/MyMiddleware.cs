namespace PracticeMVC.Web.CustomMiddleware
{
    public class MyMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            /// Write here any logic that execute before invoking next meyhod.

            Console.WriteLine("Custom Middleware started!");

            await next(context);

            /// Write logic here that execute after next method invoking.

            Console.WriteLine("Custom Middleware finished!");
        }
    }
}

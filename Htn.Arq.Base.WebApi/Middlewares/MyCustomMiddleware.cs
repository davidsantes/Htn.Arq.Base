namespace Htn.Arq.Base.WebApi.Middlewares
{
    public class MyCustomMiddleware : IMiddleware
    {
        public MyCustomMiddleware() { }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            await next(context);
        }
    }
}

namespace Htn.Arq.Base.WebApi.MiddleWares
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

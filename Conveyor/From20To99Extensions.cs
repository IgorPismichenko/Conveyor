namespace Conveyor
{
    public static class From20To99Extensions
    {
        public static IApplicationBuilder UseFrom20To99(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<From20To99Middleware>();
        }
    }
}

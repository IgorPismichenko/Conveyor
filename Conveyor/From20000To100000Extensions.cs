namespace Conveyor
{
    public static class From20000To100000Extensions
    {
        public static IApplicationBuilder UseFrom20000To100000(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<From20000To100000Middleware>();
        }
    }
}

namespace Conveyor
{
    public static class From11To19Extensions
    {
        public static IApplicationBuilder UseFrom11To19(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<From11To19Middleware>();
        }
    }
}

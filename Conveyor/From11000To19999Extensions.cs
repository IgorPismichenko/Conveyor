namespace Conveyor
{
    public static class From11000To19999Extensions
    {
        public static IApplicationBuilder UseFrom11000To19999(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<From11000To19999Middleware>();
        }
    }
}

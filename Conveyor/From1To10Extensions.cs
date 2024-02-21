namespace Conveyor
{
    public static class From1To10Extensions
    {
        public static IApplicationBuilder UseFrom1To10(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<From1To10Middleware>();
        }
    }
}

namespace Conveyor
{
    public static class From1000To10999Extensions
    {
        public static IApplicationBuilder UseFrom1000To10999(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<From1000To10999Middleware>();
        }
    }
}

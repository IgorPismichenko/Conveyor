namespace Conveyor
{
    public static class From100To999Extensions
    {
        public static IApplicationBuilder UseFrom100To999(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<From100To999Middleware>();
        }
    }
}

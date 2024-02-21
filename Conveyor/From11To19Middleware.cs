namespace Conveyor
{
    public class From11To19Middleware
    {
        private readonly RequestDelegate _next;

        public From11To19Middleware(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            string? token = context.Request.Query["number"];
            try
            {
                int number = Convert.ToInt32(token);
                number = Math.Abs(number);
                string[] Arr = { "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
                if (number < 11)
                {
                    await _next.Invoke(context);
                }
                else if(number > 19 && number % 100 > 10 && number % 100 < 20)
                {
                    context.Session.SetString("number", Arr[number % 100 - 11]);
                }
                else if(number > 19 && (number % 100 < 11 || number % 100 > 19))
                {
                    await _next.Invoke(context);
                }
                else
                {
                    await context.Response.WriteAsync("Your number is " + Arr[number - 11]);
                }
            }
            catch (Exception)
            {
                await context.Response.WriteAsync("Incorrect parameter");
            }
        }
    }
}

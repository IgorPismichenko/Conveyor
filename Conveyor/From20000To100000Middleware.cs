namespace Conveyor
{
    public class From20000To100000Middleware
    {
        private readonly RequestDelegate _next;

        public From20000To100000Middleware(RequestDelegate next)
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
                if (number < 20000)
                {
                    await _next.Invoke(context);
                }
                else if(number > 100000)
                {
                    await context.Response.WriteAsync("Number greater than one hundred thousand");
                }
                else if(number == 100000)
                {
                    await context.Response.WriteAsync("Your number is one hundred thousand");
                }
                else
                {
                    string[] Arr = { "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety"};
                    if(number % 10000 == 0)
                        await context.Response.WriteAsync("Your number is " + Arr[number / 10000 - 2] + " thousand");
                    else if (number % 10000 != 0 && number % 10000 < 1000)
                    {
                        await _next.Invoke(context);
                        string? result = string.Empty;
                        result = context.Session.GetString("number");
                        await context.Response.WriteAsync("Your number is " + Arr[number / 10000 - 2] + " thousand " + result);
                    }
                    else
                    {
                        await _next.Invoke(context);
                        string? result = string.Empty;
                        result = context.Session.GetString("number");
                        await context.Response.WriteAsync("Your number is " + Arr[number / 10000 - 2] + " " + result);
                    }
                }
            }
            catch (Exception)
            {
                await context.Response.WriteAsync("Incorrect parameter");
            }
        }
    }
}

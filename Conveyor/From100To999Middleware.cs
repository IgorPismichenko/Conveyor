namespace Conveyor
{
    public class From100To999Middleware
    {
        private readonly RequestDelegate _next;

        public From100To999Middleware(RequestDelegate next)
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
                string[] Arr = { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
                if (number < 100)
                {
                    await _next.Invoke(context);
                }
                else if (number > 999)
                {
                    if (number % 100 == 0)
                    {
                        context.Session.SetString("number", Arr[number % 1000 / 100 - 1] + " hundred");
                    }
                    else if(number % 100 != 0 && number % 1000 > 100)
                    {
                        await _next.Invoke(context);
                        string? result = string.Empty;
                        result = context.Session.GetString("number");
                        context.Session.SetString("number", Arr[number % 1000 / 100 - 1] + " hundred " + result);
                    }
                    else
                    {
                        await _next.Invoke(context);
                        string? result = string.Empty;
                        result = context.Session.GetString("number");
                        context.Session.SetString("number", result);
                    }
                }
                else
                {
                    if (number % 100 == 0)
                    {
                        await context.Response.WriteAsync("Your number is " + Arr[number / 100 - 1] + " hundred");
                    }
                    else
                    {
                        await _next.Invoke(context);
                        string? result = string.Empty;
                        result = context.Session.GetString("number");
                        await context.Response.WriteAsync("Your number is " + Arr[number / 100 - 1] + " hundred " + result);
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

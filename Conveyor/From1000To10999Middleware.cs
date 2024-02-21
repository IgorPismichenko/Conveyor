namespace Conveyor
{
    public class From1000To10999Middleware
    {
        private readonly RequestDelegate _next;

        public From1000To10999Middleware(RequestDelegate next)
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
                string[] Arr = { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten" };
                if (number < 1000)
                {
                    await _next.Invoke(context);
                }
                else if(number > 20000)
                {
                    if (number % 1000 == 0)
                    {
                        context.Session.SetString("number", Arr[number % 10000 / 1000 - 1] + " thousand");
                    }
                    else if (number % 1000 != 0 && number % 10000 > 1000)
                    {
                        await _next.Invoke(context);
                        string? result = string.Empty;
                        result = context.Session.GetString("number");
                        context.Session.SetString("number", Arr[number % 10000 / 1000 - 1] + " thousand " + result);
                    }
                    else
                    {
                        await _next.Invoke(context);
                        string? result = string.Empty;
                        result = context.Session.GetString("number");
                        context.Session.SetString("number", result);
                    }
                }
                else if(number > 10999 && number < 20000)
                {
                    await _next.Invoke(context);
                }
                else
                {
                    if(number % 1000 == 0)
                    {
                        await context.Response.WriteAsync("Your number is " + Arr[number / 1000 - 1] + " thousand");
                    }
                    else
                    {
                        await _next.Invoke(context);
                        string? result = string.Empty;
                        result = context.Session.GetString("number");
                        await context.Response.WriteAsync("Your number is " + Arr[number / 1000 - 1] + " thousand " + result);
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

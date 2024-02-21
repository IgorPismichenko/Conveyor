namespace Conveyor
{
    public class From20To99Middleware
    {
        private readonly RequestDelegate _next;

        public From20To99Middleware(RequestDelegate next)
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
                string[] Arr = { "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };
                if (number < 20)
                {
                    await _next.Invoke(context);
                }
                else if (number > 99)
                {
                    if (number % 10 == 0 && number % 100 > 10)
                    {
                        context.Session.SetString("number", Arr[number % 100 / 10 - 2]);
                    }
                    else if (number % 10 == 0 && number % 100 == 10)
                    {
                        await _next.Invoke(context);
                    }
                    else if((number % 100) > 19)
                    {
                        await _next.Invoke(context);
                        string? result = string.Empty;
                        result = context.Session.GetString("number");
                        context.Session.SetString("number", Arr[number % 100 / 10 - 2] + " " + result);
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
                    if (number % 10 == 0)
                    {
                        await context.Response.WriteAsync("Your number is " + Arr[number / 10 - 2]);
                    }
                    else
                    {
                        await _next.Invoke(context);
                        string? result = string.Empty;
                        result = context.Session.GetString("number");
                        await context.Response.WriteAsync("Your number is " + Arr[number / 10 - 2] + " " + result);
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

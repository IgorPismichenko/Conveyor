﻿namespace Conveyor
{
    public class From11000To19999Middleware
    {
        private readonly RequestDelegate _next;

        public From11000To19999Middleware(RequestDelegate next)
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
                if (number < 11000 || number > 19999)
                {
                    await _next.Invoke(context);
                }
                else
                {
                    string[] Arr = { "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
                    if (number % 1000 == 0)
                        await context.Response.WriteAsync("Your number is " + Arr[number / 1000 - 11] + " thousand");
                    else
                    {
                        await _next.Invoke(context);
                        string? result = string.Empty;
                        result = context.Session.GetString("number");
                        await context.Response.WriteAsync("Your number is " + Arr[number / 1000 - 11] + " thousand " + result);
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

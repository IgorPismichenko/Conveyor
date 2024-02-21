namespace Conveyor
{
    public class From1To10Middleware
    {
        private readonly RequestDelegate _next;

        public From1To10Middleware(RequestDelegate next)
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
                if (number == 10)
                {
                    await context.Response.WriteAsync("Your number is ten");
                }
                else
                {
                    string[] Ones = { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
                    if (number > 20 && number % 10 != 0)
                        context.Session.SetString("number", Ones[number % 10 - 1]);
                    else if(number > 20 && number % 10 == 0)
                    {
                        context.Session.SetString("number", "ten");
                    }
                    else
                        await context.Response.WriteAsync("Your number is " + Ones[number - 1]); 
                }
            }
            catch (Exception)
            {
                await context.Response.WriteAsync("Incorrect parameter");
            }
        }
    }
}

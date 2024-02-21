using Conveyor;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();
var app = builder.Build();
app.UseSession();

app.UseFrom20000To100000();
app.UseFrom11000To19999();
app.UseFrom1000To10999();
app.UseFrom100To999();
app.UseFrom20To99();
app.UseFrom11To19();
app.UseFrom1To10();

app.Run();

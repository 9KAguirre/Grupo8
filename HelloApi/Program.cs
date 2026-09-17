var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "ke onda kompa ñeri");
app.MapGet("/about", () => "Muestra un texto");

app.Run();

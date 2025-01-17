using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<ControleGasto.Dados.DB>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("dbo") + ";TrustServerCertificate=True", options =>
    {
        options.EnableRetryOnFailure();
    });
    opt.LogTo(Console.WriteLine);
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("Geral",
    builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseCors("Geral");


app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapControllers();

app.Run();


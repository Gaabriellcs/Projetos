using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

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

var key = Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:SecretKey"] ?? throw new InvalidOperationException("Chave JWT não encontrada!"));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseCors("Geral");

app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();



app.MapControllers();

app.Run();


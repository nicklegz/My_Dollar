using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Repositories.Interfaces;
using Repositories;
using Config;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(
    options => options.UseNpgsql(
        builder.Configuration["MyDollar:DBConnectionString"]));

builder.Services.AddTransient<ITransactionRepository, TransactionRepository>();

CorsPolicyConfig.ConfigureCorsPolicy(builder.Services, "CorsPolicy");

JwtConfig.ConfigureJwtAuthentication(builder.Services, builder.Configuration);

builder.Services.AddHttpContextAccessor();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

DbInit.CreateDbIfNotExists(app);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

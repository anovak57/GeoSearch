using FluentValidation.AspNetCore;
using GeoSearch.API.Filters;
using GeoSearch.API.Hubs;
using GeoSearch.API.Middlewares;
using GeoSearch.BusinessLogicLayer;
using GeoSearch.DataAccessLayer;
using GeoSearch.DataAccessLayer.Context;
using GeoSearch.DataAccessLayer.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDataAccessServices(builder.Configuration);
builder.Services.AddBusinessLogicServices();
builder.Services.AddMemoryCache();
builder.Services.AddScoped<IdempotencyFilter>();

builder.Services.AddControllers();

builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddSignalR();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    DbInitializer.Seed(context);
}

app.UseRouting();
app.UseSwagger();
app.UseSwaggerUI();

app.UseMiddleware<ApiKeyAuthMiddleware>();

app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapHub<SearchHub>("/searchHub");
app.MapControllers();
app.Run();
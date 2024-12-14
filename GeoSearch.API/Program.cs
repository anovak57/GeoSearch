using GeoSearch.API.Middlewares;
using GeoSearch.BusinessLogicLayer;
using GeoSearch.DataAccessLayer;
using GeoSearch.DataAccessLayer.Context;
using GeoSearch.DataAccessLayer.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDataAccessServices(builder.Configuration);
builder.Services.AddBusinessLogicServices();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

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

app.MapControllers();
app.Run();
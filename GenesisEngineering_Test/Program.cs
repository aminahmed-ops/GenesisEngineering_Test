using BusinessLogicLayer;
using BusinessLogicLayer.Interfaces;
using DAL.DataAccess;
using DataAccessLayer.DataAccess;
using DataAccessLayer.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



#region Service Resolver
builder.Services.AddScoped<IUniversityService, UniversityService>();
builder.Services.AddScoped<IBaseDataAccess, BaseDataAccess>();
builder.Services.AddScoped<IUniversityDataAccess, UniversityDataAccess>();

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=University}/{action=Index}/{id?}");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

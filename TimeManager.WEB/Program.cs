using TimeManager.DAL.Interfaces;
using TimeManager.DAL.Repositories;
using TimeManager.BLL.Interfaces;
using TimeManager.BLL.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddSingleton<IUnitOfWork>(x => ActivatorUtilities.CreateInstance<EFUnitOfWork>(x, connectionString));
builder.Services.AddTransient<IProjectService, ProjectService>();
builder.Services.AddTransient<ITaskService, TaskService>();



var allServices = builder.Services;

var app = builder.Build();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
            name: "default",
            pattern: "{controller}/{action}");
});

app.UseMvc();

app.UseHttpsRedirection();

app.MapControllers();


app.Run();

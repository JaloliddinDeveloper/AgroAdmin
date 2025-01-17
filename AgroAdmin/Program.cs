using AgroAdmin.Brokers.Storages;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddTransient<IStorageBroker, StorageBroker>();

var app = builder.Build();

app.UseStaticFiles();

app.MapControllerRoute(name:"def",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

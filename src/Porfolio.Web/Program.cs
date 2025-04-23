using EasyData.Services;
using Porfolio.Web.Core;
using Porfolio.Web.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMemoryCache();

// Add services to the container.
builder.Services.AddControllersWithViews();

// Fix: Pass the correct type of argument to AddServices
builder.Services.AddServices(builder);

builder.Services.AddRepositories();
builder.Services.AddPorfolioDbContext(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapEasyData(options =>
{
    options.UseDbContext<PortfolioContext>();
});

using (var scope = app.Services.CreateScope())
{
    var scopedServices = scope.ServiceProvider;
    SeedData.InitializeData(scopedServices);
}

app.Run();

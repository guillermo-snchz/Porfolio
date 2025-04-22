using Porfolio.Web.Integrations.Github;
using Porfolio.Web.Services.MemoryCache;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMemoryCache();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.Configure<GithubOptions>(builder.Configuration.GetSection("GitHub"));
builder.Services.AddHttpClient<IGithubService, GithubService>();

builder.Services.AddSingleton<IApiCacheService, ApiMemoryCacheService>();


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

app.Run();

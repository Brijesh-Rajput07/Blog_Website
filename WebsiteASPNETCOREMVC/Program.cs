using Microsoft.EntityFrameworkCore;
using WebsiteASPNETCOREMVC.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var provider=builder.Services.BuildServiceProvider();
var config=provider.GetRequiredService<IConfiguration>();


builder.Services.AddDbContext<UserDBContext>(item => item.UseSqlServer(config.GetConnectionString("mypr")));
builder.Services.AddDbContext<ContactViewDBContext>(item => item.UseSqlServer(config.GetConnectionString("copr")));
builder.Services.AddDbContext<BlogDBContext>(item => item.UseSqlServer(config.GetConnectionString("blpr")));

builder.Services.AddSession();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSession();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Login}/{id?}");

app.Run();

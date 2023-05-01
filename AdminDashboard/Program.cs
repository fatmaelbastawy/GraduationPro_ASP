//using AdminDashboard.Data;
using Context;
using Domain.Entities;
using Firebase.Storage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DbConnectionStringMostafa") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<DContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

#region builder.Services.AddDefaultIdentity<User>

//builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
//    .AddEntityFrameworkStores<DContext>();
//builder.Services.AddControllersWithViews();

//builder.Services.AddAuthorization(options =>
//{
//    options.FallbackPolicy = new AuthorizationPolicyBuilder()
//        .RequireAuthenticatedUser()
//        .Build();
//});
#endregion

builder.Services.AddIdentity<User, IdentityRole<long>>()
    .AddEntityFrameworkStores<DContext>();
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/User/SignIn";
    // options.AccessDeniedPath = "/User/NotAuthorized";
});

builder.Services.AddControllersWithViews();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy =>
    {
        policy.RequireRole("Admin");
    });
});

//builder.Services.AddSingleton(x => new FirebaseStorageOptions
//{
//    AuthTokenAsyncFactory = () => Task.FromResult("noon-ada7a.appspot.com"),
//    ThrowOnCancel = false,
//});
builder.Services.AddDirectoryBrowser();

var app = builder.Build();

#region
//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Error");
//    app.UseHsts();
//}

// Configure the HTTP request pipeline.
#endregion

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
    //pattern: "{controller=User}/{action=SignIn}/{id?}");
    //app.MapRazorPages();

app.Run();

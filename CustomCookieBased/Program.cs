using CustomCookieBased.Context;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

public class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddDbContext<CookieContext>(opt => 
        {
            opt.UseSqlServer("server = (localdb)\\mssqllocaldb; databse = CookieDb; integrated security = true;");
        });

        // cookie based authentication
        builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(opt => 
        {
            opt.Cookie.Name = "CustomCookie";
            opt.Cookie.HttpOnly = true;
            opt.Cookie.SameSite = SameSiteMode.Strict;
            opt.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
            opt.ExpireTimeSpan = TimeSpan.FromDays(10);
            opt.LoginPath = new PathString("/Home/SignIn");
            opt.LogoutPath = new PathString("/Home/LogOut");
            opt.AccessDeniedPath = new PathString("/Home/AccessDenied");
        });
        builder.Services.AddControllersWithViews();

        var app = builder.Build();

        app.UseAuthentication();
        app.UseAuthorization();
        app.MapDefaultControllerRoute();

        app.Run();
    }
}
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Options;

namespace zSkinCareBookingFE_RazorPage
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();

            builder.Services.AddAuthentication().AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, option =>
            {
                option.LoginPath = new PathString("/Pages/Login");
                option.AccessDeniedPath = new PathString("/Account/Forbidden");
                option.ExpireTimeSpan = TimeSpan.FromMinutes(5);
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

			app.MapGet("/", () => Results.Redirect("/Pages/Login"));
			app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}

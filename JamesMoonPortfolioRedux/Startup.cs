using JamesMoonPortfolioRedux.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.FileProviders;

namespace JamesMoonPortfolioRedux
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddRazorPages();
        }

        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseStaticFiles(); // For the wwwroot folder
            var provider = new FileExtensionContentTypeProvider();
            provider.Mappings[".rive"] = "text/rive";
            app.UseStaticFiles(new StaticFileOptions
            {
                ContentTypeProvider = provider
            });
            app.UseAuthorization();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.Use(async (context, next) => { if (context.Request.Path == "/") { context.Response.Redirect("https://jamesmoonit.azurewebsites.net/fun/floppatest.html"); } else { await next(); } });
            app.MapRazorPages();
            app.Run();
        }
    }
}

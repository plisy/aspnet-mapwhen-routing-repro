using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace aspnet_core_routing
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.MapWhen(IsPF, app =>
            {
                app.UseRouting();
                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllerRoute(name: "default1", pattern: "yes/{*path}", defaults: new { controller = "Default", action = "Get" });
                });
            });

            app.MapWhen(Not(IsPF), app =>
            {
                app.UseRouting();

                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllerRoute(name: "default2", pattern: "no/{*path}", defaults: new { controller = "NotDefault", action = "NotGet" });
                });
            });
        }

        private bool IsPF(HttpContext context)
        {
            return context.Request.Headers.ContainsKey("X-Yes");
        }

        private Func<HttpContext, bool> Not(Func<HttpContext, bool> predicate)
        {
            return (arg) => !predicate(arg);
        }
    }
}

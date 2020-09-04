using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Web
{
    public class Startup
    {
        private static List<Model> collection = new List<Model>
        {
            new Model { Id = Guid.NewGuid(), Name = Faker.Company.Name() },
            new Model { Id = Guid.NewGuid(), Name = $"{Faker.Company.Name()} awesome" },
            new Model { Id = Guid.NewGuid(), Name = Faker.Company.Name() },
            new Model { Id = Guid.NewGuid(), Name = Faker.Company.Name() },
            new Model { Id = Guid.NewGuid(), Name = $"The awesome {Faker.Company.Name()}" },
        };
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository), sp => new Repository(collection));
            services.AddScoped<IService, Service>();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}

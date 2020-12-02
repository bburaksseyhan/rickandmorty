using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MovieApp.Api.Model;
using MovieApp.Api.Repository;
using MovieApp.Api.Services;

namespace MovieApp.Api
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
            //in-memory cache
            services.AddTransient<IMovieRepository, MovieRepository>();
            services.AddTransient<ICharacterService<Character>, CharacterService>();

            services.AddHttpClient("characterApi", (client) =>
            {
                client.BaseAddress = new Uri(Configuration.GetSection("ApiEndpoint").Value);
                client.DefaultRequestHeaders.Add("Api-Version", "1.0");
            });

            services.AddMemoryCache();

            services.AddDistributedRedisCache(options => {
                options.Configuration = "localhost";
                options.InstanceName = "MovieAppInstance";
            });

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MovieApp.Api", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MovieApp.Api v1"));
            }
            
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

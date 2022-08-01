using ComplexProjet.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComplexProjet
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
            services.AddSwaggerGen();
            // var ConnectionString = Configuration.GetConnectionString("con");
            services.AddDbContext<ComplexContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("con")));
           // services.AddCors(cors => cors.AddPolicy(name: "AllowOrigin", builder => builder.WithOrigins().AllowAnyHeader().AllowAnyMethod());
        
             services.AddCors(c =>
             {
                 c.AddPolicy( "AllowOrigin", builders =>  builders.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod());
             }); 
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors("AllowOrigin");
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

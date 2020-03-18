using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using WebApiYo.Contexts;
using WebApiYo.Data;
using WebApiYo.Models;
using WebApiYo.Services;

namespace WebApiYo
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

            //3
            services.AddCors(options =>
            {
                options.AddPolicy("PermitirApiRequest",
                    builder => builder.WithOrigins("http://www.apirequest.io").WithMethods("GET", "POST").AllowAnyHeader());
            });

            //==================PROCEDIMIENTO ALMACENADO
            //=====================5
            services.AddScoped<ValuesRepository>();
            //=====================

            //3
            services.AddScoped<HashService>();
            services.AddDataProtection();


            ////02 (importante instalar --> Microsoft.EntityFrameworkCore.SqlServer <--)
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("defaultConnection")));

            //01
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            ////5
            //services.AddScoped<ValuesRepository>();


            services.AddControllers();


            //4
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                     options.TokenValidationParameters = new TokenValidationParameters
                     {
                         ValidateIssuer = false,
                         ValidateAudience = false,
                         ValidateLifetime = true,
                         ValidateIssuerSigningKey = true,
                         IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(Configuration["jwt:key"])),
                         ClockSkew = TimeSpan.Zero
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

            //01
            app.UseAuthentication();

            app.UseAuthorization();

            //02
            //app.UseCors();
            //app.UseCors(builder => builder.WithOrigins("https://www.apirequest.io").WithMethods("GET", "POST").AllowAnyHeader());
            app.UseCors(builder => builder.WithOrigins("*").WithMethods("GET", "POST").AllowAnyHeader());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

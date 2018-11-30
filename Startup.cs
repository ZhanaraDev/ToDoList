using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WebApplication1.Models;
using Microsoft.EntityFrameworkCore;
using WebApplication1.services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebApplication1.Helpers;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Diagnostics;

namespace WebApplication1
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
            services.AddDbContext<DataContext>(options =>
            options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));
           

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
           .AddJwtBearer(x =>
           {
               x.Events = new JwtBearerEvents
               {    
                   OnTokenValidated = context =>
                   {
                       Trace.WriteLine("Some shitty witty");
                       var userService = context.HttpContext.RequestServices.GetRequiredService<IUserService>();
                       Trace.WriteLine("Some shitty witty1");
                       Trace.WriteLine(context.Principal.Identity.Name.ToString());
                       Trace.WriteLine("Some shitty witty2");
                       Trace.WriteLine(context.Principal.Identity.IsAuthenticated);
                       var userId = long.Parse(context.Principal.Identity.Name);
                       Trace.WriteLine(userId);
                       var user = userService.GetById(userId);
                       if (user == null)
                       {
                            // return unauthorized if user no longer exists
                            context.Fail("Unauthorized");
                       }
                       return System.Threading.Tasks.Task.CompletedTask;
                   }
               };
               Trace.WriteLine("sonice");
               x.RequireHttpsMetadata = false;
               x.SaveToken = true;
               x.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuerSigningKey = true,
                   IssuerSigningKey = new SymmetricSecurityKey(key),
                   ValidateIssuer = false,
                   ValidateAudience = false
               };
           });

            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IUserProfileService, UserProfileService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseAuthentication();
           // app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
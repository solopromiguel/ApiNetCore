using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using WebApplication21.Data;
using WebApplication21.Helpers;
using WebApplication21.sakila;

namespace WebApplication21
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
            var key = Encoding.ASCII
                           .GetBytes(Configuration.GetSection("AppSettings:Token").Value);

            
            //services.AddDbContext<new_schemaContext>(x => x.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<new_schemaContext>(options =>
                    options.UseMySql(Configuration.GetConnectionString("DefaultConnection")));

            IdentityBuilder builder = services.AddIdentityCore<Users>(opt =>
            {
                opt.Password.RequireDigit = false;
                opt.Password.RequiredLength = 4;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireUppercase = false;
            });

            builder = new IdentityBuilder(builder.UserType, typeof(Role),builder.Services);
            builder.AddEntityFrameworkStores<new_schemaContext>();
            builder.AddRoleValidator<RoleValidator<Role>>();

            builder.AddRoleManager<RoleManager<Role>>();
            builder.AddSignInManager<SignInManager<Users>>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(options =>
               {
                   options.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuerSigningKey = true,
                       IssuerSigningKey = new SymmetricSecurityKey(key),
                        // Our issuer and audience is localhost so we don't need to validate
                        ValidateIssuer = false,
                       ValidateAudience = false
                   };
               });

            services.AddMvc(options=> 
            {
                var policy = new AuthorizationPolicyBuilder()
                 .RequireAuthenticatedUser()
                 .Build();

                options.Filters.Add(new AuthorizeFilter(policy));

                

            }
            )
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);


            
            services.AddCors();
            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
            services.AddTransient<Seed>();
            services.AddScoped<IAuthRepository, AuthRepository>();

            



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
                app.UseExceptionHandler(builder => {
                    // Adds a terminal middleware delegate to the application
                    // request pipeline
                    // The Run() method can access the http request/response context
                    builder.Run(async context => {
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                        var error = context.Features.Get<IExceptionHandlerFeature>();

                        if (error != null)
                        {
                            // We are going to extend context.Response so that we can add our
                            // own custom error response headers
                            context.Response.AddApplicationError(error.Error.Message);

                            await context.Response.WriteAsync(error.Error.Message);
                        }
                    });
                });
            }
            app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().AllowCredentials());
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}

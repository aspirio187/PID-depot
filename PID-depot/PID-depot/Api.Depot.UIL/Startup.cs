using Api.Depot.BLL;
using Api.Depot.UIL.Events;
using Api.Depot.UIL.Managers;
using Api.Depot.UIL.Models;
using DevHopTools.Connection;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Api.Depot.UIL
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
#if DEBUG
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
#endif
            ////////////////////////////////////////////////
            /* Authorization using JwtToken configuration */
            ////////////////////////////////////////////////

            services.Configure<JwtModel>(Configuration.GetSection("Jwt"));
            JwtModel jwtModel = Configuration.GetSection("Jwt").Get<JwtModel>();

            services.AddAuthentication(options =>
                {
                    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidIssuer = jwtModel.Issuer,
                        ValidAudience = jwtModel.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtModel.Secret)),
                        ClockSkew = TimeSpan.Zero
                    };
                })
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
                 {
                     options.LoginPath = "/Account/Login";
                     options.AccessDeniedPath = "/Forbidden";

                     options.Events = new CookieAuthenticationEvents()
                     {
                         OnRedirectToLogin = (ctx) =>
                         {
                             if (ctx.Request.Path.StartsWithSegments("/api") && ctx.Response.StatusCode == 200)
                             {
                                 ctx.Response.StatusCode = 401;
                             }

                             return Task.CompletedTask;
                         },
                         OnRedirectToAccessDenied = (ctx) =>
                         {
                             if (ctx.Request.Path.StartsWithSegments("/api") && ctx.Response.StatusCode == 200)
                             {
                                 ctx.Response.StatusCode = 403;
                             }

                             return Task.CompletedTask;
                         }
                     };

                     options.EventsType = typeof(SecurityStampUpdatedCookieAuthenticationEvent);
                 });

            AuthorizationPolicy multiSchemePolicy = new AuthorizationPolicyBuilder(CookieAuthenticationDefaults.AuthenticationScheme, JwtBearerDefaults.AuthenticationScheme)
                .RequireAuthenticatedUser()
                .Build();

            services.AddAuthorization(options =>
            {
                options.DefaultPolicy = multiSchemePolicy;
            });

            ///////////////////////////
            /* Dependency injections */
            ///////////////////////////

            services.AddHttpContextAccessor();

            services.InjectBLL(connectionString);
            services.AddScoped<IAuthManager, AuthManager>();
            services.AddScoped<SecurityStampUpdatedCookieAuthenticationEvent>();

            ///////////////////////////////
            /* Application Configuration */
            ///////////////////////////////

            services.AddRazorPages()
                .AddRazorRuntimeCompilation();
            services.AddControllers();

            ///////////////////////////
            /* Swagger configuration */
            ///////////////////////////

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "api.depot", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Description = "Jwt containing user claims",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                OpenApiSecurityRequirement security = new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme()
                        {
                            Reference = new OpenApiReference()
                            {
                                Id = "Bearer",
                                Type= ReferenceType.SecurityScheme
                            },
                            UnresolvedReference = true
                        },
                        new List<string>()
                    }
                };

                c.AddSecurityRequirement(security);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "api.depot v1"));
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapRazorPages();
            });
        }
    }
}

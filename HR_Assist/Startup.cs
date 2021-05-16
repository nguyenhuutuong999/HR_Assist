using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using HR_Assist.Core.Constants;
using HR_Assist.Core.Entities;
using HR_Assist.Core.Entities.Contexts;
using HR_Assist.Core.Infrastructure.Filters;
using HR_Assist.Core.Infrastructure.Utilities;
using HR_Assist.Core.Services;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HR_Assist
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
            services.AddControllers(
               options =>
               {
                   options.Filters.Add(typeof(HttpGlobalExceptionFilter));
                   options.Filters.Add(typeof(ValidateModelFilter));
               })
               .AddNewtonsoftJson(options =>
               {
                   options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                   options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                   options.SerializerSettings.DefaultValueHandling = DefaultValueHandling.Include;
                   options.SerializerSettings.NullValueHandling = NullValueHandling.Include;
               });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Microservice API",
                    Description = "phungdkh@gmail.com 123456",
                    TermsOfService = null,
                    Contact = new OpenApiContact { Name = "DINH KHAC HOAI PHUNG", Email = "phungdkh@gmail.com", Url = null },
                });

                c.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = JwtBearerDefaults.AuthenticationScheme
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = JwtBearerDefaults.AuthenticationScheme
                            },
                            Scheme = JwtBearerDefaults.AuthenticationScheme,
                            Name = JwtBearerDefaults.AuthenticationScheme,
                            In = ParameterLocation.Header,

                        },
                        new List<string>()
                    }
                });
                c.DescribeAllParametersInCamelCase();
            });


            string msSqlConnectionString = Configuration.GetValue<string>("database:msSql:connectionString");

            services.AddDbContext<HR_AssistDbContext>(opt =>
                opt.UseSqlServer(
                    msSqlConnectionString,
                    options =>
                    {
                        options.EnableRetryOnFailure();
                    }));

            services.AddIdentity<ApplicationUser, ApplicationRole>(o =>
            {
                o.Password.RequireDigit = false;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequiredLength = 6;
            }).AddEntityFrameworkStores<HR_AssistDbContext>().AddDefaultTokenProviders();

            services.AddHealthChecks()
               .AddSqlServer(msSqlConnectionString);

            services.AddMediatR(typeof(Startup));
            services.AddAutoMapper(typeof(Startup));

            services
              .AddAuthentication(options =>
              {
                  options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                  options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                  options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
              })
              .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
              {
                  options.RequireHttpsMetadata = false;
                  options.SaveToken = true;
                  options.TokenValidationParameters = new TokenValidationParameters()
                  {
                      ValidateIssuer = true,
                      ValidateAudience = true,
                      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetSection("AppSettings")["ThirdPartyRelationshipSecret"])),
                      ValidAudience = Configuration.GetSection("AppSettings")["TokenAudience"],
                      ValidIssuer = Configuration.GetSection("AppSettings")["TokenIssuer"],
                      ClockSkew = TimeSpan.Zero // remove delay of token when expire
                  };
              });
           

            // Add Policy for each Role
            services.AddAuthorization(options =>
            {

                // Basic Access  
                options.AddPolicy("BasicAccess", policy =>
                    policy.RequireAssertion(context =>
                                context.User.IsInRole(RoleConstants.PM)
                                || context.User.IsInRole(RoleConstants.PO)
                                || context.User.IsInRole(RoleConstants.TEAM_LEADER)
                                || context.User.IsInRole(RoleConstants.HR)
                                || context.User.IsInRole(RoleConstants.DIRECTOR)));

                //Manage All
                options.AddPolicy("AdminAccess", policy =>
                     policy.RequireAssertion(context =>
                                  context.User.IsInRole(RoleConstants.HR)
                                 || context.User.IsInRole(RoleConstants.DIRECTOR)));

                // Leader (PM/TeamLeader/PO) allowed to manage team information, add requests about needed workforce.  
                options.AddPolicy("LeaderAccess", policy =>
                    policy.RequireAssertion(context =>
                                context.User.IsInRole(RoleConstants.PM)
                                || context.User.IsInRole(RoleConstants.PO)
                                || context.User.IsInRole(RoleConstants.TEAM_LEADER)));

               
            });

            services.AddCors();
            services.AddHttpContextAccessor();

            ServiceLocator.SetLocatorProvider(services.BuildServiceProvider());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
           if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "HR_Assist v1");
                    c.DocumentTitle = "HR Assist API Document";
                    c.DocExpansion(DocExpansion.None);
                });
            }
            
            app.UseCors(x => x
               .SetIsOriginAllowed(origin => true)
               .AllowAnyMethod()
               .AllowAnyHeader()
               .AllowCredentials());

            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            using var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<HR_AssistDbContext>();

            // Auto run migrate
            db.Database.MigrateAsync().Wait();

            // Get the service  
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

            // Init user
            this.InitializeSystemAdminUser(db, userManager, roleManager).Wait();
        }
        private async Task InitializeSystemAdminUser(HR_AssistDbContext context,
                              UserManager<ApplicationUser> userManager,
                              RoleManager<ApplicationRole> roleManager)
        {
            context.Database.EnsureCreated();

            string password = "123456";

            if (await roleManager.FindByNameAsync(RoleConstants.TEAM_LEADER) == null)
            {
                await roleManager.CreateAsync(new ApplicationRole(RoleConstants.TEAM_LEADER));
            }
            
            if(await roleManager.FindByNameAsync(RoleConstants.HR) == null)
            {
                await roleManager.CreateAsync(new ApplicationRole(RoleConstants.HR));
            }

            if (await userManager.FindByNameAsync("phung.dinh@scrumbase.com") == null)
            {
                var user = new ApplicationUser
                {
                    UserName = "phungdkh@gmail.com",
                    Email = "phungdkh@gmail.com",
                    PhoneNumber = "0983260830",
                    Name = "Đinh Khắc Hoài Phụng"
                };
                var user1 = new ApplicationUser
                {
                    UserName = "hongminh@gmail.com",
                    Email = "hongminh@gmail.com",
                    PhoneNumber = "0123456789",
                    Name = "Hồng Minh"
                };

                var result = await userManager.CreateAsync(user);
                var result1 = await userManager.CreateAsync(user1);
                if (result.Succeeded)
                {
                    await userManager.AddPasswordAsync(user, password);
                    await userManager.AddToRoleAsync(user, RoleConstants.TEAM_LEADER);

                }
                if (result1.Succeeded)
                {
                    await userManager.AddPasswordAsync(user1, password);
                    await userManager.AddToRoleAsync(user1, RoleConstants.HR);
                }
            }
        }
    }
}

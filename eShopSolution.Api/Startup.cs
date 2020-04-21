using eShopSolution.Api.Application.Handler.Catalog.Category;
using eShopSolution.Api.Application.Handler.Catalog.Product;
using eShopSolution.Api.Application.Handler.Common;
using eShopSolution.Api.SwaggerOptions;
using eShopSolution.Application.Catalog.Product;
using eShopSolution.Application.User;
using eShopSolution.Data.EF;
using eShopSolution.Data.Entities;
using eShopSolution.Utilities.Contants;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace eShopSolution.Api
{
    public class Startup
    {
        public Startup(IWebHostEnvironment environment, IConfiguration configuration)
        {
            this.Environment = environment;
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            Assembly assembly = AppDomain.CurrentDomain.Load("eShopSolution.Api");

            services.AddDbContext<EShopDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString(SystemContants.MainConnectionString)));

            services.AddMvc(option =>
            {
                option.EnableEndpointRouting = false;
            });

            services.AddIdentity<AppUser, AppRole>()
            .AddEntityFrameworkStores<EShopDbContext>()
            .AddDefaultTokenProviders();
            // dependency injection
            services.AddTransient<IManageProductHandler, ManageProductHandler>();
            services.AddTransient<IStorageService, FileStorageService>();
            services.AddTransient<ICategoryHandler, CategoryHandler>();
            services.AddTransient<IUserHandler, UserHandler>();
            services.AddTransient<UserManager<AppUser>, UserManager<AppUser>>();
            services.AddTransient<SignInManager<AppUser>, SignInManager<AppUser>>();
            services.AddTransient<RoleManager<AppRole>, RoleManager<AppRole>>();

            services.AddControllersWithViews()
             .AddFluentValidation(fv =>
             {
                 fv.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
                 fv.RegisterValidatorsFromAssemblyContaining<Startup>();
             });

            services.AddMvc(option =>
            {
                option.EnableEndpointRouting = false;
            });

            services.AddOptions();

            services.AddCors(options =>
            {
                options.AddPolicy(
                    "AllowAllOrigins",
                    builder =>
                    {
                        builder
                            .AllowAnyOrigin()
                            .AllowAnyHeader()
                            .WithExposedHeaders("Content-Disposition", "Api-Supported-Versions")
                            .AllowAnyMethod();
                    });
            });

            services.AddAntiforgery(options => options.HeaderName = "X-XSRF-TOKEN");
            services.AddRouting(options => options.LowercaseUrls = true);

            services.AddSingleton<IConfiguration>(this.Configuration);

            //api versioning
            services.AddApiVersioning(
                config =>
                {
                    config.ReportApiVersions = true;
                    config.AssumeDefaultVersionWhenUnspecified = true;
                    config.DefaultApiVersion = new ApiVersion(1, 0);
                    // config.ApiVersionReader = new HeaderApiVersionReader("api-version");
                });

            services.AddVersionedApiExplorer(
                options =>
                {
                    options.GroupNameFormat = "'v'VVV";

                    // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
                    // can also be used to control the format of the API version in route templates
                    options.SubstituteApiVersionInUrl = true;
                });

            // config token
            string issuer = Configuration.GetValue<string>("Tokens:Issuer");
            string signingKey = Configuration.GetValue<string>("Tokens:Key");
            byte[] signingKeyBytes = System.Text.Encoding.UTF8.GetBytes(signingKey);

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = false,
                    ValidIssuer = issuer,
                    ValidateAudience = false,
                    ValidAudience = issuer,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = System.TimeSpan.Zero,
                    IssuerSigningKey = new SymmetricSecurityKey(signingKeyBytes)
                };
            });

            // Enable authentication JWT
            services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                  {
                    {
                      new OpenApiSecurityScheme
                      {
                        Reference = new OpenApiReference
                          {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                          },
                          Scheme = "oauth2",
                          Name = "Bearer",
                          In = ParameterLocation.Header,
                        },
                        new List<string>()
                      }
                    });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
                              IApiVersionDescriptionProvider provider, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")),
                RequestPath = "/wwwroot"
            });

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            app.UseSwaggerUI(
                options =>
                {
                    foreach (var description in provider.ApiVersionDescriptions)
                    {
                        options.SwaggerEndpoint(
                            $"/swagger/{description.GroupName}/swagger.json",
                            description.GroupName.ToUpperInvariant());
                    }
                    options.OAuthClientId(this.Configuration.GetSection("Tokens:ClientId").Value);
                    options.OAuthClientSecret(this.Configuration.GetSection("Tokens:Secret").Value);
                });

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseMvc();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
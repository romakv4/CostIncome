#pragma warning disable 1591
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using AutoMapper;
using CostIncomeCalculator.Data;
using CostIncomeCalculator.Data.AuthData;
using CostIncomeCalculator.Data.CostData;
using CostIncomeCalculator.Data.IncomeData;
using CostIncomeCalculator.Data.LimitData;
using CostIncomeCalculator.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Microsoft.OpenApi.Models;

namespace CostIncomeCalculator
{
    public class Startup
    {

        public Startup(IWebHostEnvironment env)
        {
            var configBuilder = new ConfigurationBuilder()
                                .SetBasePath(env.ContentRootPath)
                                .AddJsonFile("appsettings.json", optional: true)
                                .AddJsonFile("appsettings.development.json", optional: true)
                                .AddEnvironmentVariables();
            this.Configuration = configBuilder.Build();
        }

        public IConfiguration Configuration { get; }
        
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DataContext>(x => x.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));
            services.AddCors();
            services.AddControllers();
            services.AddAutoMapper(typeof(Startup));
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<IUserHelper, UserHelper>();
            services.AddScoped<IDatesHelper, DatesHelper>();
            services.AddScoped<ITokenHelper, TokenHelper>();
            services.AddScoped<IEmailHelper, EmailHelper>();
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<ICostRepository, CostRepository>();
            services.AddScoped<IIncomeRepository, IncomeRepository>();
            services.AddScoped<ILimitRepository, LimitRepository>();
            services.AddMvcCore().AddApiExplorer().AddDataAnnotations();
            services.AddRouting(options => options.LowercaseUrls = true);
            services.AddFluentEmail("noreply@costincome.com", "CostIncome")
                    .AddMailGunSender(Configuration.GetSection("Mailgun:Domain").Value,
                                        Configuration.GetSection("Mailgun:APIkey").Value);
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.GetSection("AppSettings:Token").Value)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
            services.AddApiVersioning(o => {
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.DefaultApiVersion = new ApiVersion(new DateTime(2019, 11, 1 ));
            });

            Directory.CreateDirectory("Logs");
            if (!File.Exists("./Logs/errors.txt"))
                File.Create("./Logs/errors.txt");
                
            services.AddSingleton((ILogger)new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File("./Logs/errors.txt", outputTemplate:
                    "{Timestamp:HH:mm:ss} {Message}{NewLine}")
                .CreateLogger());

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

                var filePath = Path.Combine(System.AppContext.BaseDirectory, "CostIncomeCalculator.xml");
                c.IncludeXmlComments(filePath);

                 c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Name = "Bearer",
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                });
            });
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
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "CostIncomeAPI V1");
                    c.RoutePrefix = string.Empty;
                });
            }
            else
            {
                app.UseExceptionHandler(builder => {
                    builder.Run(async context => {
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        var error = context.Features.Get<IExceptionHandlerFeature>();
                        if (error != null)
                        {
                            context.Response.AddApplicationError(error.Error.Message);
                            await context.Response.WriteAsync(error.Error.Message);
                        }
                    });
                });
            }

            app.UseRouting();
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}

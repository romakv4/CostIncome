using System.IO;
using System.Net;
using System.Text;
using AutoMapper;
using cost_income_calculator.api.Data;
using cost_income_calculator.api.Data.AuthData;
using cost_income_calculator.api.Data.CostData;
using cost_income_calculator.api.Data.IncomeData;
using cost_income_calculator.api.Data.LimitData;
using cost_income_calculator.api.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Serilog;

namespace cost_income_calculator.api
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
            services.AddDbContext<DataContext>(x => x.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));
            services.AddCors();
            services.AddAutoMapper(typeof(Startup));
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<IUserHelper, UserHelper>();
            services.AddScoped<IDatesHelper, DatesHelper>();
            services.AddScoped<ITokenHelper, TokenHelper>();
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<ICostRepository, CostRepository>();
            services.AddScoped<IIncomeRepository, IncomeRepository>();
            services.AddScoped<ILimitRepository, LimitRepository>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
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

            Directory.CreateDirectory("Logs");
            if (!File.Exists("./Logs/errors.txt"))
                File.Create("./Logs/errors.txt");
                
            services.AddSingleton((ILogger)new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File("./Logs/errors.txt", outputTemplate:
                    "{Timestamp:HH:mm:ss} {Message}{NewLine}")
                .CreateLogger());
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                // app.UseHsts();
            }

            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}

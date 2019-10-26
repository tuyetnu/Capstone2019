﻿using System;
using System.IO;
using System.Reflection;
using System.Text;
using AutoMapper;
using DormyWebService.Entities;
using DormyWebService.Repositories;
using DormyWebService.Services.EquipmentServices;
using DormyWebService.Services.HomeService;
using DormyWebService.Services.MoneyServices;
using DormyWebService.Services.NewFolder;
using DormyWebService.Services.NewsServices;
using DormyWebService.Services.ParamServices;
using DormyWebService.Services.RoomServices;
using DormyWebService.Services.TicketServices;
using DormyWebService.Services.UserServices;
using DormyWebService.Utilities;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Sieve.Services;
using Swashbuckle.AspNetCore.Swagger;

namespace DormyWebService
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
            //Allow Cross Origins
            services.AddCors();

            //Configure AutoMapper
            services.AddAutoMapper(typeof(Startup));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //Register DBContext and database connection string
            services.AddDbContext<DormyDbContext>(op => op.UseSqlServer(Configuration["ConnectionString:DormyDB"]));

            // Register the Swagger generator, defining 1 or more Swagger documents, v1 is for version 
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { 
                    Title = "Dormy API", 
                    Version = "v1" , 
                    Description = "This API helps manage a dormitory"}
                );

                //Locate the XML file being generated by ASP.NET...
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.XML";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                //... and tell Swagger to use those XML comments.
                c.IncludeXmlComments(xmlPath);
            });

            //Setup JWT
            var authenticationSettingSection = Configuration.GetSection("AuthenticationSetting");
            services.Configure<AuthenticationSetting>(authenticationSettingSection);
            var appSettings = authenticationSettingSection.Get<AuthenticationSetting>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
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

            //Dependency Injection for Sieve for pagination, sorting, filtering
            services.AddScoped<ISieveProcessor,SieveProcessor>();

            // Configure Dependency Injection
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<IStaffService, StaffService>();
            services.AddScoped<IParamService, ParamService>();
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<INewsServices, NewsService>();
            services.AddScoped<IEquipmentService, EquipmentService>();
            services.AddScoped<IRoomService, RoomService>();
            services.AddScoped<IParamTypeService, ParamTypeService>();
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
            services.AddScoped<IRoomBookingService, RoomBookingService>();
            services.AddScoped<IRoomTransferService, RoomTransferService>();
            services.AddScoped<IIssueTicketService, IssueTicketService>();
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
            services.AddScoped<IHomeService, HomeService>();
            services.AddScoped<IMoneyTransactionService, MoneyTransactionService>();
            services.AddScoped<IRoomMonthlyBillService, RoomMonthlyBillService>();
            services.AddScoped<IStudentMonthlyBillService, StudentMonthlyBillService>();

            //Dependency Injection for fluent validation
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // Global cross origins policy
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            //Exception Handler
            app.UseMiddleware(typeof(ExceptionHandlingMiddleware));

            //For JWT
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}

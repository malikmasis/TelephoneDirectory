using GreenPipes;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Text;
using TelephoneDirectory.Report.Consumers;
using TelephoneDirectory.Report.Data;
using TelephoneDirectory.Report.Interfaces;
using TelephoneDirectory.Report.Services;

namespace TelephoneDirectory.Report
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
            services.AddDbContext<ReportDbContext>(options =>
               options.UseNpgsql(
                   Configuration.GetConnectionString("DefaultConnection"),
                   b => b.MigrationsAssembly(typeof(ReportDbContext).Assembly.FullName)));

            services.AddScoped<IReportDbContext>(provider => provider.GetService<ReportDbContext>());
            services.AddScoped<IReportService, ReportService>();

            services.AddSwaggerGen(swagger =>
            {
                swagger.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Report Microservice API"
                });
                swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                });
                swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}

                    }
                });
            });

            services.AddMassTransit(x =>
            {
                x.AddConsumer<PersonConsumer>();
                x.AddConsumer<SubmitTokenConsumer>();

                x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    cfg.UseHealthCheck(provider);
                    cfg.Host(Configuration["Rabbitmq:Url"], "/", h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });
                    cfg.ReceiveEndpoint("ticketQueue", ep =>
                    {
                        ep.UseCircuitBreaker(cb =>
                        {
                            cb.TrackingPeriod = TimeSpan.FromMinutes(1);
                            cb.TripThreshold = 15;
                            cb.ActiveThreshold = 10;
                            cb.ResetInterval = TimeSpan.FromMinutes(5);
                        });

                        ep.PrefetchCount = 16;
                        ep.UseMessageRetry(r => r.Interval(2, 10));
                        ep.UseRateLimit(1000, TimeSpan.FromMinutes(1));

                        ep.ConfigureConsumer<PersonConsumer>(provider);
                        ep.ConfigureConsumer<SubmitTokenConsumer>(provider);

                    });
                }));
            });
            services.AddMassTransitHostedService();

            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true, // validate the server
                        ValidateAudience = true, // Validate the recipient of token is authorized to receive
                        ValidateLifetime = true, // Check if token is not expired and the signing key of the issuer is valid 
                        ValidateIssuerSigningKey = true, // Validate signature of the token 

                        //Issuer and audience values are same as defined in generating Token
                        ValidIssuer = Configuration.GetSection("Jwt")["Issuer"].ToString(), // stored in appsetting file
                        ValidAudience = Configuration["Jwt:Issuer"], // stored in appsetting file
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"])) // stored in appsetting file
                    };
                });

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Report.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

﻿
using ILS_BE.Application.Authorization;
using ILS_BE.Common.Extensions;
using ILS_BE.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace ILS_BE
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);
            var configuration = builder.Configuration;

            // Add services to the container.
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("PostgresConnection"),
                      o => o.SetPostgresVersion(new Version(17, 4))));




            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: "AllowReactClient",
                    policy =>
                    {
                        policy.AllowAnyOrigin()
                              .AllowAnyHeader()
                              .AllowAnyMethod();
                    });
            });

            builder.Services.AddScoped<AppDbContext>();

            builder.Services.AddSingleton<UserPermissionStore>();

            builder.Services.AddRepositories();

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            builder.Services.AddServices();

            builder.Services.AddOpenApi();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddControllers();

            builder.Services.AddJWTAuthentication(builder.Configuration);

            if (!(AppDomain.CurrentDomain.FriendlyName == "ef"))
            {
                using (var scope = builder.Services.BuildServiceProvider())
                {
                    var dbContext = scope.GetRequiredService<AppDbContext>();
                    dbContext.Database.Migrate();
                }
                builder.Services.AddAuthorizationPermissions();
            }

            builder.Services.AddDistributedMemoryCache();

            builder.Services.AddHttpContextAccessor();

            builder.WebHost.ConfigureKestrel(options =>
            {
                builder.Configuration.GetSection("Kestrel").Bind(options);
            }).UseKestrel();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.MapOpenApi();

                //PreTest(app.Configuration);
            }

            //app.UseExceptionHandler();

            app.UseHttpsRedirection();

            app.UseCors("AllowReactClient");

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }

        //private static void PreTest(IConfiguration configuration)
        //{
        //    // Test the connection to the PostgreSQL database
        //    PostgresConnectionTester tester = new(configuration);
        //    tester.TestConnection();
        //}
    }
}

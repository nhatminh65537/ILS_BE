
using ILS_BE.Common.Extensions;
using ILS_BE.Infrastructure;
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

            builder.Services.AddScoped<DbContext, AppDbContext>();

            builder.Services.AddRepositories();

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            builder.Services.AddServices();

            builder.Services.AddOpenApi();

            builder.Services.AddControllers();

            builder.Services.AddJWTAuthentication(builder.Configuration);

            builder.Services.AddAuthorizationPermissions(); // Ignore When Migration

            builder.Services.AddDistributedMemoryCache();

            builder.Services.AddHttpContextAccessor();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();

                //PreTest(app.Configuration);
            }

            //app.UseExceptionHandler();

            app.UseHttpsRedirection();

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

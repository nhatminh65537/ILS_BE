using ILS_BE.Domain.Interfaces;
using ILS_BE.Domain.Models;
using ILS_BE.Domain.DTOs;
using ILS_BE.Infrastructure.Repositories;
using ILS_BE.Application.Services;
using ILS_BE.Application.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ILS_BE.Application.Authorization;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using ILS_BE.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace ILS_BE.Common.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IPaginatedRepository<LearnModule>, ModuleRepository>();
            services.AddScoped<IGenericRepository<LearnLesson>, LessonRepository>();
            services.AddScoped<IUserRepository,  UserRepository>();
            services.AddScoped<IContentItemRepository, LearnNodeRepository>();
        }

        public static void AddServices(this IServiceCollection services)
        {
            services.AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>();
            services.AddScoped<PasswordService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IMyUserService, MyUserService>();
            services.AddScoped<IPasswordService, PasswordService>();
            services.AddScoped<IContentItemService, ContentItemService>();
            services.AddScoped<IPaginatedDataService<ModuleDTO>, PaginatedDataService<LearnModule, ModuleDTO>>();
            services.AddScoped<IDataService<LessonDTO>, DataService<LearnLesson, LessonDTO>>();
            services.AddScoped<IDataService<LearnNodeDTO>, DataService<LearnNode, LearnNodeDTO>>();

            services.AddScoped(typeof(IDataService<TagDTO>), typeof(DataService<LearnTag, TagDTO>));
            services.AddScoped(typeof(IDataService<CategoryDTO>), typeof(DataService<LearnCategory, CategoryDTO>));
            services.AddScoped(typeof(IDataService<PermissionDTO>), typeof(DataService<Permission, PermissionDTO>));
            services.AddScoped(typeof(IDataService<LessonTypeDTO>), typeof(DataService<LearnLessonType, LessonTypeDTO>));
            services.AddScoped(typeof(IDataService<ProgressStateDTO>), typeof(DataService<LearnProgressState, ProgressStateDTO>));
            services.AddScoped(typeof(IDataService<LifecycleStateDTO>), typeof(DataService<LearnLifecycleState, LifecycleStateDTO>));
        }

        public static void AddJWTAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = false,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = configuration["JwtSettings:Issuer"],
                        ValidAudience = configuration["JwtSettings:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:SecretKey"]!))
                    };
                    options.Events = new JwtBearerEvents
                    {
                        OnTokenValidated = async context =>
                        {
                            var revocationService = context.HttpContext.RequestServices
                                .GetRequiredService<ITokenService>();

                            var jti = context.Principal?.FindFirstValue(JwtRegisteredClaimNames.Jti);

                            if (await revocationService.IsTokenRevokedAsync(jti!))
                            {
                                context.Fail("Token revoked");
                            }
                        },
                      
                        OnAuthenticationFailed = context =>
                        {
                            Console.WriteLine($"Authentication failed: {context.Exception.Message}");
                            return Task.CompletedTask;
                        },
                    };
                   
                });
        }

        public static void AddAuthorizationPermissions(this IServiceCollection services)
        { 
            services.AddScoped<IAuthorizationHandler, PermissionHandler>();


            using var serviceProvider = services.BuildServiceProvider();
            var context = serviceProvider.GetRequiredService<AppDbContext>();
            context.Database.Migrate();

            var _permissionRepository = serviceProvider.GetRequiredService<IGenericRepository<Permission>>();
            var permissions = _permissionRepository.GetAll().ToList();

            services.AddAuthorization(options =>
            {
                foreach (var permission in permissions)
                {
                    options.AddPolicy(permission.Name, policy =>
                        policy.Requirements.Add(new PermissionRequirement(permission.Id)));
                }
            });
        }
        
    }
}

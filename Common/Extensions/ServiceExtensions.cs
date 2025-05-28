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
using ILS_BE.Application.Services.DataServices;

namespace ILS_BE.Common.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<UserEffectivePermissionRepository>();
            services.AddScoped<IPaginatedRepository<LearnModule>, LearnModuleRepository>();
            services.AddScoped<LearnModuleRepository>();
            services.AddScoped<IRepository<LearnLesson>, LearnLessonRepository>();
            services.AddScoped<IRepository<LearnNode>, LearnNodeRepository>();
            services.AddScoped<IUserRepository,  UserRepository>();
            
        }

        public static void AddServices(this IServiceCollection services)
        {
            services.AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>();
            services.AddScoped<PasswordService>();
            services.AddScoped<LearnNodeService>();
            services.AddScoped<LearnLessonService>();
            services.AddScoped<LearnModuleService>();

            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IMyUserService, MyUserService>();
            services.AddScoped<IPasswordService, PasswordService>();
            services.AddScoped<INodeService<LearnNodeDTO>, LearnNodeService>();
            services.AddScoped<IPaginatedDataService<LearnModuleDTO>, PaginatedDataService<LearnModule, LearnModuleDTO>>();
            services.AddScoped<IDataService<LearnLessonNodeDTO>, DataService<LearnLesson, LearnLessonNodeDTO>>();
            services.AddScoped<IDataService<LearnNodeDTO>, DataService<LearnNode, LearnNodeDTO>>();

            services.AddScoped(typeof(IDataService<LearnTagDTO>), typeof(DataService<LearnTag, LearnTagDTO>));
            services.AddScoped(typeof(IDataService<LearnCategoryDTO>), typeof(DataService<LearnCategory, LearnCategoryDTO>));
            services.AddScoped(typeof(IDataService<PermissionDTO>), typeof(DataService<Permission, PermissionDTO>));
            services.AddScoped(typeof(IDataService<LearnLessonTypeDTO>), typeof(DataService<LearnLessonType, LearnLessonTypeDTO>));
            services.AddScoped(typeof(IDataService<LearnProgressStateDTO>), typeof(DataService<LearnProgressState, LearnProgressStateDTO>));
            services.AddScoped(typeof(IDataService<LearnLifecycleStateDTO>), typeof(DataService<LearnLifecycleState, LearnLifecycleStateDTO>));
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
            services.AddSingleton<UserPermissionStore>();
            services.AddScoped<IAuthorizationHandler, PermissionHandler>();

                                                                    
            using var serviceProvider = services.BuildServiceProvider();

            var _permissionRepository = serviceProvider.GetRequiredService<IRepository<Permission>>();
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

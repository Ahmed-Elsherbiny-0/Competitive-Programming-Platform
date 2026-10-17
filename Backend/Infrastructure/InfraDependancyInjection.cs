using Application.Dtos.Auth.AuthValidators;
using Application.Interfaces;
using Application.Mapping;
using Domain.Entities;
using Domain.Interfaces;
using FluentValidation;
using FluentValidation.AspNetCore;
using Infrastructure.Data;
using Infrastructure.Helper.Email;
using Infrastructure.Options;
using Infrastructure.Services;
using Infrastructure.Services.Auth;
using Infrastructure.Services.Email;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class InfraDependancyInjection
    {
        public static IServiceCollection AddDependancies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(o =>
            {
                o.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
            services.AddScoped<IEmailSender, EmailService>();
            services.AddAuthConfig(configuration).AddFluentValidationConfig().AddMapsterConfig();
            services.AddScoped<IEmailBodyBuilder, EmailBodyBuilder>();
            services.AddScoped<IPhotoService, PhotoService>();

            services.Configure<EmailOptions>(configuration.GetSection("MailSettings"));
            services.Configure<CloudinarySettings>(configuration.GetSection("CloudinarySetting"));

            return services;
        }


        private static IServiceCollection AddAuthConfig(this IServiceCollection services,
             IConfiguration configuration)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<JwtOptions>(configuration.GetSection("Jwt"));

            var jwtSettings = configuration.GetSection("Jwt").Get<JwtOptions>();

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddSingleton<IJwtProvider, JwtProvider>();
            services.AddScoped<IAuthService, AuthService>();


            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
               .AddJwtBearer(o =>
               {
                   o.SaveToken = true;
                   o.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuerSigningKey = true,
                       ValidateIssuer = true,
                       ValidateAudience = true,
                       ValidateLifetime = true,
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings?.Key!)),
                       ValidIssuer = jwtSettings?.Issuer,
                       ValidAudience = jwtSettings?.Audience
                   };
                   o.Events = new JwtBearerEvents
                   {
                       OnMessageReceived = context =>
                       {
                           var accessToken = context.Request.Query["access_token"];
                           var path = context.HttpContext.Request.Path;
                           var accessToken2 = context.Request.Cookies["access_token"];
                           if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/hubs"))
                           {
                               context.Token = accessToken;
                           }
                           if (context.Token == null && !string.IsNullOrEmpty(accessToken2))
                           {
                               context.Token = accessToken2;
                           }
                           return Task.CompletedTask;
                       }
                   };
               });


            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 8;
                options.SignIn.RequireConfirmedEmail = true;
                options.User.RequireUniqueEmail = true;
            });
            return services;

        }
        private static IServiceCollection AddFluentValidationConfig(this IServiceCollection services)
        {

            return services.AddFluentValidationAutoValidation().AddValidatorsFromAssemblyContaining<RegisterRequestDtoValidator>();

        }
        private static IServiceCollection AddMapsterConfig(this IServiceCollection services)
        {

            var mappingConfig = TypeAdapterConfig.GlobalSettings;
            mappingConfig.Scan(typeof(MappingConfiguration).Assembly);
            services.AddSingleton(mappingConfig);

            services.AddSingleton<IMapper>(new Mapper(mappingConfig));

            return services;

        }
    }
}
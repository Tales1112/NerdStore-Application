﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetDevPack.Security.Jwt;
using NetDevPack.Security.Jwt.Store.EntityFrameworkCore;
using NSE.Identidade.API.Data;
using NSE.Identidade.API.Extensions;

namespace NSE.Identidade.API.Configuration
{
    public static class IdentityConfig
    {
        public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services, IConfiguration Configuration)
        {
            var appSettingsSection = Configuration.GetSection("AppTokenSettings");
            services.Configure<AppTokenSettings>(appSettingsSection);

            services.AddJwksManager(options => options.Jws = JwsAlgorithm.ES256)
                .PersistKeysToDatabaseStore<ApplicationDbContext>();

            services.AddDbContext<ApplicationDbContext>(options =>
           options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<IdentityUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            return services;
        }
    }
}

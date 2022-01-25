using Assignments.Business.Dto.Authentification;
using Assignments.Business.Settings.Authentification;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Assignments.API.Extentions
{
    public static class AuthentificationExtention
    {
        public static void ConfigureAuthentification(this IServiceCollection services, IConfiguration config)
        {
            var jwtTokenConfig = config.GetSection(nameof(JwtTokenConfig));
            var jwtTokenConfigValue = jwtTokenConfig.Get<JwtTokenConfig>();
            services.Configure<JwtTokenConfig>(jwtTokenConfig);

            services.AddScoped<UserIdentity>();

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = true;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidIssuer = jwtTokenConfigValue.Issuer,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(jwtTokenConfigValue.Secret)),
                    ValidAudience = jwtTokenConfigValue.Audience,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(jwtTokenConfigValue.AccessTokenExpiration)
                };
            });
        }
    }
}
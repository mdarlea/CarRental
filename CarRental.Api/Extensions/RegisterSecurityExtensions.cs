using CarRental.Api.Authentication;
using CarRental.Identity.Data;
using CarRental.Identity.Data.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace CarRental.Identity.Api.Extensions
{
	public static class RegisterSecurityExtensions
	{
		private const string SecretKey = "iNivDmHLpUA223sqsfhqGbMRdRj1PVkH";

		public static void AddSecurity(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<ApplicationDbContext>(optionsBuilder => {
				optionsBuilder.UseMySQL(
					configuration.GetConnectionString("CarRentalDataSource"));
			});			

			// add identity
			var builder = services.AddIdentityCore<ApplicationUser>(config =>
			{
				// configure identity options
				config.Password.RequireDigit = false;
				config.Password.RequireLowercase = false;
				config.Password.RequireUppercase = false;
				config.Password.RequireNonAlphanumeric = false;
				config.Password.RequiredLength = 6;

				config.SignIn.RequireConfirmedEmail = true;
			});
			builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole), builder.Services);
			builder.AddEntityFrameworkStores<ApplicationDbContext>()				
				.AddUserManager<ApplicationUserManager>()
				.AddDefaultTokenProviders();
			
			services.AddJwtAuthentication(configuration);

			// api user claim policy
			services.AddAuthorization(options =>
			{
				options.AddPolicy("ApiUser", policy => policy.RequireClaim("rol", "api_access"));
			});
		}

		public static void AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddSingleton<IJwtFactory, JwtFactory>();

			var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));

			// Get options from app settings
			var jwtAppSettingOptions = configuration.GetSection(nameof(JwtIssuerOptions));

			// Configure JwtIssuerOptions
			services.Configure<JwtIssuerOptions>(options =>
			{
				options.Issuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
				options.Audience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)];
				options.SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
			});

			var tokenValidationParameters = new TokenValidationParameters
			{
				ValidateIssuer = true,
				ValidIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)],

				ValidateAudience = true,
				ValidAudience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)],

				ValidateIssuerSigningKey = true,
				IssuerSigningKey = signingKey,

				RequireExpirationTime = false,
				ValidateLifetime = true,
				ClockSkew = TimeSpan.Zero
			};

			services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(configureOptions =>
			{
				configureOptions.ClaimsIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
				configureOptions.TokenValidationParameters = tokenValidationParameters;
				configureOptions.SaveToken = true;
			}); ;
		}
	}
}

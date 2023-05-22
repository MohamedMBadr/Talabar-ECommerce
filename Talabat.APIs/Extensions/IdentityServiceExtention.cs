using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Talabat.core.Entities.Identity;
using Talabat.core.Services;


using Talabat.Repository.Identity;
using Talabat.Service;

namespace Talabat.APIs.Extensions
{
	public static class IdentityServiceExtention
	{
		public static IServiceCollection AddIdentityServices(this IServiceCollection services ,IConfiguration configuration)
		{
			services.AddScoped<ITokenService, TokenService>();


			services.AddIdentity<AppUser, IdentityRole>(options =>
			{
			
				//options.Password.RequireLowercase= true;
				//options.Password.RequireUppercase= true;
				//options.Password.RequireDigit = true;
				//options.Password.RequireNonAlphanumeric = true;
				//options.Password.RequiredLength = 8 ;

			})
			.AddEntityFrameworkStores<AppIdentityDbContext>();

			services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme= JwtBearerDefaults.AuthenticationScheme;
			})
				.AddJwtBearer(options =>
				{
					options.TokenValidationParameters = new TokenValidationParameters()
					{
						ValidateIssuer = true,
						ValidIssuer = configuration["JWT:ValidIssuer"],
						ValidateAudience = true,
						ValidAudience = configuration["JWT:ValidAudience"],
						ValidateLifetime = true,
						ValidateIssuerSigningKey = true,
						IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:key"]))
					};
				
				});
			return services;


		}
	}
}

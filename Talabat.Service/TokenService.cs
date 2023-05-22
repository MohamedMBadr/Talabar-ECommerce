using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Talabat.core.Entities.Identity;
using Talabat.core.Services;

namespace Talabat.Service
{
	public class TokenService : ITokenService
	{
		private readonly IConfiguration _configuration;

		public TokenService(IConfiguration configuration)
		{
			_configuration = configuration;
		}
		
		public async Task<string> createTokenAsync(AppUser uer , UserManager<AppUser> userManager)
		{
			//Privit claims [ user defined]
			var authClaims = new List<Claim>()
			{
				new Claim(ClaimTypes.GivenName ,uer.DisplayName),
				new Claim(ClaimTypes.Email,uer.Email),
			};
			var userRoles = await userManager.GetRolesAsync(uer);
			foreach (var role in userRoles) authClaims.Add(new Claim(ClaimTypes.Role, role));

			var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:key"]));

			var token = new JwtSecurityToken(
				issuer: _configuration["JWT:ValidIssuer"],
				audience: _configuration["JWT:ValidAudience"],
				expires:DateTime.Now.AddDays(double.Parse( _configuration["JWT:DurtionInDays"])),
				claims: authClaims,
				signingCredentials:new SigningCredentials(authKey,SecurityAlgorithms.HmacSha256Signature)
				);
			return	new JwtSecurityTokenHandler().WriteToken(token);
			//Test

		}
	}
}

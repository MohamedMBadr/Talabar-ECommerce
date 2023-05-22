using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.core.Entities.Identity;

namespace Talabat.Repository.Identity
{
	public static class AppIdentityDbContextSeed
	{
		public static async Task SeedUserAsync(this UserManager<AppUser> userManager)
		{
			if(!userManager.Users.Any())
			{
				var useer = new AppUser()
				{
					DisplayName="Mohamed Badr",
					Email="mohamed.Badr@gmail.com",
					UserName="MohamedBadr",
					PhoneNumber="01122334455"  
				};
				await userManager.CreateAsync(useer,"Pa$$w0rd");
			}
		}
	}
}

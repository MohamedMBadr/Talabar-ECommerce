using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using Talabat.APIs.Errors;
using Talabat.APIs.Extensions;
using Talabat.APIs.Helper;
using Talabat.APIs.MiddelWares;
using Talabat.core.Entities;
using Talabat.core.Entities.Identity;
using Talabat.core.Repositories;
using Talabat.Repository;
using Talabat.Repository.Data;
using Talabat.Repository.Identity;

namespace Talabat.APIs
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);


			// Add services to the container.


			#region Configure Services


			builder.Services.AddControllers();


			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

			builder.Services.AddSwaggerServices();
		

			builder.Services.AddDbContext<StoreContext>(options =>
			{
				options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
			});

			builder.Services.AddDbContext<AppIdentityDbContext>(options =>
			{
				options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
			}
			);

			builder.Services.AddSingleton<IConnectionMultiplexer>(s =>
			{
				var connection = builder.Configuration.GetConnectionString("Redis");
				return  ConnectionMultiplexer.Connect(connection);
			});

			builder.Services.AddApplicationServises();


			builder.Services.AddIdentityServices(builder.Configuration);





			#endregion









			var app = builder.Build();

			using var scope = app.Services.CreateScope();

			var sevices = scope.ServiceProvider;

			var loggerFactory = sevices.GetRequiredService<ILoggerFactory>();

			try
			{
				var dbContext = sevices.GetRequiredService<StoreContext>();

				await dbContext.Database.MigrateAsync(); // apply migration(Update-DataBase)
				await StoreContextSeed.SeedAsync(dbContext);


				var IdentitydbContext = sevices.GetRequiredService<AppIdentityDbContext>();
				await IdentitydbContext.Database.MigrateAsync(); // apply migration(Update-DataBase)

				var userManager = sevices.GetRequiredService<UserManager<AppUser>>();
				await AppIdentityDbContextSeed.SeedUserAsync(userManager);
			}
			catch (Exception ex)
			{
				var logger = loggerFactory.CreateLogger<Program>();

				logger.LogError(ex, "An Error occured during apply migration ");
			}

			#region  Configure Kestrel MiddleWares

			app.UseMiddleware<ExceptionMiddleWare>();

			
;           // Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwaggerMiddleWares();
			}

			app.UseStatusCodePagesWithReExecute("/errors/{0}");
			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseAuthentication();
			app.UseAuthorization();	


			app.MapControllers();


			#endregion

			app.Run();

		}
	}
}
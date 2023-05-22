using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Errors;
using Talabat.APIs.Helper;
using Talabat.core.Repositories;
using Talabat.Repository;

namespace Talabat.APIs.Extensions
{
	public static class ApplicationsevicesExtention
	{

		public static IServiceCollection AddApplicationServises(this IServiceCollection services) {

			services.AddScoped(typeof(IBasketRepository), typeof(BasketRepository));

			//builder.Services.AddScoped<iGenericRepository<Product> , GenericRepository<Product>>();
			services.AddScoped(typeof(iGenericRepository<>), typeof(GenericRepository<>));
			services.AddAutoMapper(typeof(MappingProfiles));


			services.Configure<ApiBehaviorOptions>(options =>
			{
				options.InvalidModelStateResponseFactory = (ActionContext) =>
				{
					var errors = ActionContext.ModelState.Where(p => p.Value.Errors.Count() > 0)
														.SelectMany(e => e.Value.Errors)
														.Select(e => e.ErrorMessage).ToArray();
					var validationErrorResponce = new ApiValidationErrorReponseL()
					{
						Errors = errors
					};

					return new BadRequestObjectResult(validationErrorResponce);
				};
			});

			return services;
		}
	}

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.core.Entities;

namespace Talabat.Repository.Data
{
	public static class StoreContextSeed
	{
		public static async Task SeedAsync(StoreContext context)
		{
			if (!context.ProductBrands.Any())
			{
				var brandData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/brands.json");
				var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandData);

				if (brands is not null && brands.Count > 0)
				{
					foreach (var brand in brands)
						await context.Set<ProductBrand>().AddAsync(brand);

					await context.SaveChangesAsync();
				}
			}

			if (!context.ProductTypes.Any())
			{
				var typeData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/types.json");
				var types = JsonSerializer.Deserialize<List<ProductType>>(typeData);

				if (types is not null && types.Count > 0)
				{
					foreach (var type in types)
						await context.Set<ProductType>().AddAsync(type);

					await context.SaveChangesAsync();
				}
			}

			if (!context.Products.Any())
			{
				var productData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/products.json");
				var products = JsonSerializer.Deserialize<List<Product>>(productData);

				if (products is not null && products.Count > 0)
				{
					foreach (var product in products)
						await context.Set<Product>().AddAsync(product);

					await context.SaveChangesAsync();
				}
			}



		}
	}
}

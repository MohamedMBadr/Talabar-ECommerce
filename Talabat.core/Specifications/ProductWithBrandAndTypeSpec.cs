using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.core.Entities;

namespace Talabat.core.Specifications
{
	public class ProductWithBrandAndTypeSpec : BaseSpecifications<Product>
	{
		public ProductWithBrandAndTypeSpec(ProudctSpecParams specParams)
			:base(p=> 
						(string.IsNullOrEmpty(specParams.search) || p.Name.ToLower().Contains(specParams.search))&&
						(!specParams.brandId.HasValue || p.ProductBrandId == specParams.brandId.Value) &&
						(!specParams.typeId.HasValue || p.ProductTypeId == specParams.typeId.Value)


			)
		{
			Includes.Add(p => p.ProductBrand);
			Includes.Add(p => p.ProductType);
		//	AddOredBy(p => p.Name);

			if (!string.IsNullOrEmpty(specParams.sort))
			{
				switch(specParams.sort)
				{
					case "priceAsc": AddOredBy(p => p.Price);
						break;

					case "PriceDesc":AddOredByDesc(p => p.Price);
						break;

					default: AddOredBy(p => p.Name);
						break;
				}

			}

			ApplyPagination(specParams.PageSize * (specParams.PageIndex-1), specParams.PageSize);



		}
		public ProductWithBrandAndTypeSpec(int id) : base(p => p.Id == id) 
		{
			Includes.Add(p => p.ProductBrand);
			Includes.Add(p => p.ProductType);
		}
	}
}

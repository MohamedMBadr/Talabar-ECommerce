using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.core.Entities;

namespace Talabat.core.Specifications
{
	public class ProductWithFilterationForCountSpecs :BaseSpecifications<Product>
	{
		public ProductWithFilterationForCountSpecs(ProudctSpecParams specParams)
				: base(p =>
						(!specParams.brandId.HasValue || p.ProductBrandId == specParams.brandId.Value) &&
						(!specParams.typeId.HasValue || p.ProductTypeId == specParams.typeId.Value)


			)
		{

		}
	}
}

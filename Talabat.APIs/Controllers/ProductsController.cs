using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using Talabat.APIs.Dtos;
using Talabat.APIs.Errors;
using Talabat.APIs.Helper;
using Talabat.core.Entities;
using Talabat.core.Repositories;
using Talabat.core.Specifications;

namespace Talabat.APIs.Controllers
{
	public class ProductsController : BaseApiController
	{
		private readonly iGenericRepository<Product> _productRepo;
		private readonly iGenericRepository<ProductBrand> _brandRepo;
		private readonly iGenericRepository<ProductType> _typeRepo;
		private readonly IMapper _mapper;

		public ProductsController( iGenericRepository<Product> productRepo,
			iGenericRepository<ProductBrand> brandRepo,
			iGenericRepository<ProductType> typeRepo,
			IMapper mapper)
		{
			_productRepo = productRepo;
			_brandRepo = brandRepo;
			_typeRepo = typeRepo;
			_mapper = mapper;
		}
		//[Authorize /*(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)*/]
		[HttpGet]
		public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProduct([FromQuery]ProudctSpecParams specParams )
		{
			var spec = new ProductWithBrandAndTypeSpec(specParams);

			var Products =await _productRepo.GetAllWithSpecAsync(spec);

			var countSpec = new ProductWithFilterationForCountSpecs(specParams);

			var count = await _productRepo.GetCountWithSpecsAsync(countSpec);

			var data=	_mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(Products);
			

			return Ok(new Pagination<ProductToReturnDto>(specParams.PageIndex ,  specParams.PageSize  , count,  data));

		}





		[ProducesResponseType(typeof(ProductToReturnDto), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]

		[HttpGet("{id}")]

		public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
		{
			var spec = new ProductWithBrandAndTypeSpec(id);
			
			var product = await _productRepo.GetByIdWithSpecAsync(spec);
			if (product is null) return NotFound(new ApiResponse(404));
			return Ok(_mapper.Map<Product,ProductToReturnDto>(product)) ;

		}

		[HttpGet("brands") ]

		
		public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrands()
		{
			var brands=await _brandRepo.GetAllAsync();
			return Ok(brands);
		}

		[HttpGet("types")]

		public async Task<ActionResult<IReadOnlyList<ProductType>>> GetTypes()
		{
			var types = await _brandRepo.GetAllAsync();
			return Ok(types);
		}




	}
}

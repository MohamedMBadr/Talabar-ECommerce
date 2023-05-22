using AutoMapper;
using Talabat.APIs.Dtos;
using Talabat.core.Entities;
using Talabat.core.Entities.Identity;

namespace Talabat.APIs.Helper
{
	public class MappingProfiles : Profile
	{
		public MappingProfiles() 
		{
			CreateMap<Product, ProductToReturnDto>()
				.ForMember(d => d.ProductBrand, o => o.MapFrom(s => s.ProductBrand.Name))
				.ForMember(d => d.ProductType, o => o.MapFrom(s => s.ProductType.Name))
				.ForMember(d => d.PictureUrl, o => o.MapFrom<ProductPictureUrlResolver>());

			CreateMap<Address, AddressDto>().ReverseMap();
				

		
		
		}
	}
}

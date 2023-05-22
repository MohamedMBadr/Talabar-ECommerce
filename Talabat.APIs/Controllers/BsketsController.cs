using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Errors;
using Talabat.core.Entities;
using Talabat.core.Repositories;

namespace Talabat.APIs.Controllers
{

	public class BsketsController : BaseApiController
	{
		private readonly IBasketRepository _basketRepository;

		public BsketsController(IBasketRepository basketRepository)
		{
			_basketRepository = basketRepository;
		}


		[HttpGet]

		public async Task<ActionResult<CustmerBasket>> GetBasket(string id)
		{
			var basket= await _basketRepository.GetBasketAsync(id);
			return basket ?? new CustmerBasket(id);
		}

		[HttpPost]
		public async Task<ActionResult<CustmerBasket>> UpdateBasket(CustmerBasket basket)
		{
			var CreatedOrUpdatedBasket= await _basketRepository.UpdateBasketAsync(basket);
			if (CreatedOrUpdatedBasket is null) return BadRequest(new ApiResponse(400)) ;
			return CreatedOrUpdatedBasket;
		}

		[HttpDelete]

		public async Task<ActionResult<bool>> DeleteBasket(string basketId)
		{
			return await _basketRepository.DeleteBasketAsync(basketId);
		}
	}
}

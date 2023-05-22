using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.core.Entities;
using Talabat.core.Repositories;



namespace Talabat.Repository
{
	public class BasketRepository : IBasketRepository
	{
		private readonly IDatabase _dataBase;

		public BasketRepository(IConnectionMultiplexer redis)
		{
			_dataBase = redis.GetDatabase();
		}
		public async Task<bool> DeleteBasketAsync(string BasketId)
		{
			return await _dataBase.KeyDeleteAsync(BasketId);
		}

		public async Task<CustmerBasket?> GetBasketAsync(string BaskedId)
		{
			var basket = await _dataBase.StringGetAsync(BaskedId);
			return basket.IsNull ? null : JsonSerializer.Deserialize<CustmerBasket>(basket);
		}

		public async Task<CustmerBasket?> UpdateBasketAsync(CustmerBasket custmerBasket)
		{
			var upadetOrCreateBasket = await _dataBase.StringSetAsync(custmerBasket.Id, JsonSerializer.Serialize(custmerBasket), TimeSpan.FromDays(1));
			if (upadetOrCreateBasket is false) return null;
			return await GetBasketAsync(custmerBasket.Id);
		}
	}
}

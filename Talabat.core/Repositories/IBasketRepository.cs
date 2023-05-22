using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.core.Entities;

namespace Talabat.core.Repositories
{
	public interface IBasketRepository
	{
		 Task<CustmerBasket ?> GetBasketAsync(string BaskedId);

		Task<CustmerBasket?> UpdateBasketAsync(CustmerBasket custmerBasket);

		Task<bool> DeleteBasketAsync(string BasketId);	



	}
}

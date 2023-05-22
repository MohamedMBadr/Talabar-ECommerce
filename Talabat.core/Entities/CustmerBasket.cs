
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.core.Entities
{
	public class CustmerBasket
	{
		public string Id { get; set; }

		public List<BasketItem> Items { get; set; } = new List<BasketItem>();
		public CustmerBasket(string id)
		{
			Id = id;
		}
	}
}

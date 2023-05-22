using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.core.Entities;

namespace Talabat.core.Specifications
{
	public interface ISpecifications<T> where T : BaseEntity
	{
		public Expression<Func<T, bool>> Criateria { get; set; }
		public List<Expression<Func<T, Object>>> Includes { get;set; }

		public Expression<Func<T,object>> orderBy { get; set; }
		public Expression<Func<T, object>> orderByDesending { get; set; }

		public int Take { get; set; }
		public int Skip { get; set; }

		public bool IsPaginationEnabeled { get; set; }


	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.core.Entities;

namespace Talabat.core.Specifications
{
	public class BaseSpecifications<T> : ISpecifications<T> where T : BaseEntity
	{
		public Expression<Func<T, bool>> Criateria { get ; set; }
		public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>();
		public Expression<Func<T, object>> orderBy { get; set; }
		public Expression<Func<T, object>> orderByDesending { get; set; }
		public int Take { get ; set; }
		public int Skip { get ; set; }
		public bool IsPaginationEnabeled { get; set ; }

		public BaseSpecifications()
		{
		}
		public BaseSpecifications(Expression<Func<T, bool>> CriateriaExpretion)
		{
			Criateria= CriateriaExpretion;
		}

		public void AddOredBy (Expression<Func<T,object>> orderByExpression)
		{
			orderBy= orderByExpression;
		}

		public void AddOredByDesc(Expression<Func<T, object>> orderByDecsExpression)
		{
			orderByDesending = orderByDecsExpression;
		}

		public void ApplyPagination(int skip , int take)
		{
			IsPaginationEnabeled= true;
			Skip= skip;
			Take= take;

		}

		


	}
}

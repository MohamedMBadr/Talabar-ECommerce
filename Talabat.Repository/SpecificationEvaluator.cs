using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.core.Entities;
using Talabat.core.Specifications;

namespace Talabat.Repository
{
	public static class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
	{
		public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery , ISpecifications<TEntity> spec)
		{
			var query = inputQuery;
			if (spec.Criateria is not null)
				query = query.Where(spec.Criateria);




			if(spec.orderBy is not null)
				query = query.OrderBy(spec.orderBy);


			if (spec.orderByDesending is not null)
				query = query.OrderByDescending(spec.orderByDesending);

			if (spec.IsPaginationEnabeled)
				query = query.Skip(spec.Skip).Take(spec.Take);


			query = spec.Includes.Aggregate(query ,(currentQuery , IncludeExpression) => currentQuery.Include(IncludeExpression));

			return query;
		
		}
	}
}

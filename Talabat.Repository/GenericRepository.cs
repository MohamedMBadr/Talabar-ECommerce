 using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.core.Entities;
using Talabat.core.Repositories;
using Talabat.core.Specifications;
using Talabat.Repository.Data;

namespace Talabat.Repository
{
	public class GenericRepository<T> : iGenericRepository<T> where T : BaseEntity
	{
		private readonly StoreContext _dbContext;

		public GenericRepository(StoreContext dbContext )
		{
			_dbContext = dbContext;
		}
		public async Task<IReadOnlyList<T>> GetAllAsync()
		{
			if(typeof(T)== typeof(Product))
			{
				return (IReadOnlyList<T>) await _dbContext.Products.Include(p=>p.ProductBrand )
					.Include(p=>p.ProductType).ToListAsync();
			}
			return await _dbContext.Set<T>().ToListAsync();
		}
		

		public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecifications<T> spec)
		{
			return await ApplySpecification(spec).ToListAsync();
		}

		public async Task<T> GetByIdAsync(int id)
		{
			return await _dbContext.Set<T>().FindAsync(id);
		}

		public async Task<T> GetByIdWithSpecAsync(ISpecifications<T> spec)
		{
			return await ApplySpecification(spec).FirstOrDefaultAsync();
		}

		public async Task<int> GetCountWithSpecsAsync(ISpecifications<T> spec)
		{
			return await ApplySpecification(spec).CountAsync();
		}

		private IQueryable<T> ApplySpecification(ISpecifications<T> spec)
		{
			return SpecificationEvaluator<T>.GetQuery(_dbContext.Set<T>() , spec);
		}
	}
}

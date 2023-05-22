using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.core.Entities;
using Talabat.core.Specifications;

namespace Talabat.core.Repositories
{
	public interface iGenericRepository<T>where T : BaseEntity
	{
		Task<IReadOnlyList<T>> GetAllAsync();	

		Task<T> GetByIdAsync(int id);

		Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecifications<T> spec);

		Task<T> GetByIdWithSpecAsync(ISpecifications<T> spec);

		Task<int> GetCountWithSpecsAsync(ISpecifications<T> spec);

	}
}

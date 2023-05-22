using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.core.Entities
{
	public class Department : BaseEntity
	{
		public string Name { get; set; }

		public DateOnly DateOfCReation { get; set; }
	}
}

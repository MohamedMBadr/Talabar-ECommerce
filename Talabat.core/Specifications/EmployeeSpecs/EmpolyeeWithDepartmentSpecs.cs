//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Talabat.core.Entities;

//namespace Talabat.core.Specifications.EmployeeSpecs
//{
//	public class EmpolyeeWithDepartmentSpecs :BaseSpecifications<Employee>
//	{
//		public EmpolyeeWithDepartmentSpecs()
//		{
//			Includes.Add(e => e.Department);
//		}
//		public EmpolyeeWithDepartmentSpecs(int id):base(e =>e.Id ==id)
//		{
//			Includes.Add(e => e.Department);

//		}

//	}
//}

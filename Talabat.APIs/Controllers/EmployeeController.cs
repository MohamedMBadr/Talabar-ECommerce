//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Talabat.core.Entities;
//using Talabat.core.Repositories;
//using Talabat.core.Specifications.EmployeeSpecs;

//namespace Talabat.APIs.Controllers
//{

//	public class EmployeeController : BaseApiController
//	{
//		private readonly iGenericRepository<Employee> _employeesRepo;

//		public EmployeeController(iGenericRepository<Employee> EmployeesRepo)
//		{
//			_employeesRepo = EmployeesRepo;
//		}

//		[HttpGet]
//		public async Task<ActionResult<IReadOnlyList<Employee>>> GetEmployees()
//		{
//			var spec = new EmpolyeeWithDepartmentSpecs();
//			var employess = await _employeesRepo.GetAllWithSpecAsync(spec);
//			return Ok(employess);
//		}
//		[HttpGet("{id}")]
//		public async Task<ActionResult<Employee>> GetEmployeeById(int id)
//		{
//			var spec = new EmpolyeeWithDepartmentSpecs(id);
//			var employee = await _employeesRepo.GetAllWithSpecAsync(spec);
//			return Ok(employee);
//		}
//	}
//}

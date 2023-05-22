using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Errors;
using Talabat.Repository.Data;

namespace Talabat.APIs.Controllers
{
	
	public class BuggyController : BaseApiController
	{
		private readonly StoreContext _dbContext;

		public BuggyController(StoreContext dbContext) 
		{
			_dbContext = dbContext;
		}

		[HttpGet("notfound")] //  api/buggy/notfound
		public ActionResult GetNotFoundRequest() 
		{
			var product = _dbContext.Products.Find(100);
			if (product is null) return NotFound( new ApiResponse(404));
			return Ok(product);
		}

		[HttpGet("servererror")]  //api/buggy/servererror
		public ActionResult GetSeverError()
		{
			var product = _dbContext.Products.Find(100);
			var productToReturn = product.ToString();
			return Ok(productToReturn);
		}

		[HttpGet("badrequest")] //   api/buggy/badrequest
		public ActionResult GetBadRequest() 
		{
			return BadRequest(new ApiResponse(400));
		}

		[HttpGet("badrequest/{id}")] // api/buggy/badrequest/five

		public ActionResult GetBadRequest(int id)
		{
			return Ok();
		}

	}
}

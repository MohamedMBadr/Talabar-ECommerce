using System.Net;
using System.Text.Json;
using Talabat.APIs.Errors;

namespace Talabat.APIs.MiddelWares
{
	public class ExceptionMiddleWare
	{
		private readonly RequestDelegate next;
		private readonly ILogger<ExceptionMiddleWare> logger;
		private readonly IHostEnvironment env;

		public ExceptionMiddleWare(RequestDelegate next , ILogger<ExceptionMiddleWare> logger ,IHostEnvironment env)
		{
			this.next = next;
			this.logger = logger;
			this.env = env;
		}

		public async Task InvokeAsync(HttpContext context)
		{
			try
			{
				await next.Invoke(context);
			}
			catch (Exception ex)
			{
				logger.LogError(ex, ex.Message);


				context.Response.ContentType = "application/json";
				context.Response.StatusCode = (int) HttpStatusCode.InternalServerError ;

				
				var response =  env.IsDevelopment()?
					new ApiExceptionResponse((int)HttpStatusCode.InternalServerError ,ex.Message , ex.StackTrace.ToString()) :
					new ApiExceptionResponse((int)HttpStatusCode.InternalServerError) ;

				var options = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase}	 ;

				var json = JsonSerializer.Serialize(response , options);
				context.Response.WriteAsync(json);

			}
		}
	}
}

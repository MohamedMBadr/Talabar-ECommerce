namespace Talabat.APIs.Errors
{
	public class ApiResponse
	{
		public int StatusCode { get; set; }
		public string? Message { get; set; }

		public ApiResponse(int statusCode, string? message = null)
		{
			StatusCode = statusCode;
			Message = message ?? GetDfautMessageForStatusCode(statusCode);
		}

		private string? GetDfautMessageForStatusCode(int statusCode)
		{
			return statusCode switch
			{
				400 => "A Bad Request",
				401 => "you are not Authorized",
				404 => "Resource Not Found",
				500 => "not found path " ,
				 _ => null

			};
		}
	}
}

namespace Talabat.APIs.Errors
{
	public class ApiValidationErrorReponseL:ApiResponse
	{
		public IEnumerable<string> Errors { get; set; }

		public ApiValidationErrorReponseL():base(400)
		{
			Errors = new List<string>();


		}
	}
}

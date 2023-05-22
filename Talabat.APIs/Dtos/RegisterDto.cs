using System.ComponentModel.DataAnnotations;

namespace Talabat.APIs.Dtos
{
	public class RegisterDto
	{
		[Required]
		public string DisplayName { get; set; }
		[Required]
		[EmailAddress]
		public string Email { get; set; }
		[Required]
		[Phone]
		public string PhoneNumber { get; set; }
		[Required]
		[RegularExpression("(?=^.{6,10}$)(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&amp;*()_+}{&quot;:;'?/&gt;.&1t;,])(?!.*\\s).*$",
			ErrorMessage ="password must contaion atlest 1 digit , 1 upper ,1 lower,not alpha")]
		public string Password { get; set; }
	}
}

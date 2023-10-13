using System.ComponentModel.DataAnnotations;

namespace LoginApplication.Shared
{
    public class LoginModel
	{
        [Required, EmailAddress]
        public string? Email { get; set; }

        [Required, DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}


using System.ComponentModel.DataAnnotations;

namespace CarService.ViewModel
{
    public class LoginViewModel
    {
        [Required]
        public string Username { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool rememberMe { get; set; }
    }
}

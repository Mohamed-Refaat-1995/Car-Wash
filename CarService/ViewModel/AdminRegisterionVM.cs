using System.ComponentModel.DataAnnotations;

namespace CarService.ViewModel
{
    public class AdminRegisterionVM
    {



        [Required]


        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }


        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}

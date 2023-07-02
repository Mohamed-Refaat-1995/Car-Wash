using System.ComponentModel.DataAnnotations;

namespace CarService.ViewModel
{
    public class RegistrationViewModel
    {

        [Required]

        //[Display("الاسم الاول")]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public int CityID { get; set; }
        public string Address { get; set; }
        public int RulesID { get; set; }


        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

    }
}

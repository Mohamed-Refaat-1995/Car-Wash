using CarService.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarService.ViewModel
{
    public class PreRequestVM
    {
        [Required(ErrorMessage = "يرجى اختيار المدينة")]
        public int CityId { get; set; }

        [Required(ErrorMessage = "يرجى إدخال الحي")]
        public string Destrict { get; set; }
        public int ServiceId { get; set; }

        [Required(ErrorMessage = "الرجاء إدخال رقم جوال صحيح يبدأ بـ 05.")]

        [RegularExpression("^05\\d{8}$")]
        public string PhoneNumber { get; set; }//regx

        public string? Source { get; set; }

    }
}

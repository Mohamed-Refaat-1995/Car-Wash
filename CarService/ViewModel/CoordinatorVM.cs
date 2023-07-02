using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace CarService.ViewModel
{
    public class CoordinatorVM
    {
        public int Id { get; set; }
        public string? Notes { get; set; }
        public string? Date { get; set; }
        public string? Time { get; set; }
        public string? City { get; set; }
        public string? Destrict { get; set; }
        public string? Name { get; set; }
        public string? Service { get; set; }
        public DateTime ServiceDate { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Price { get; set; }
        public string? OrderStatus { get; set; }

        public bool? IsPayed { get; set; }
        public string? PaymentMethod { get; set; }
        public string? TabbyID { get; set; }

        [NotMapped]
        [DisplayName("uploadImg")]
        public IFormFile? PaymentImageFile { get; set; }
        public string? PaymentImage { get; set; }

        public DateTime AssignDate { get; set; }
        public string AssignTime { get; set; }
        public string TechnicanID { get; set; }



    }
}

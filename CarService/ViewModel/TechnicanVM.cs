using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace CarService.ViewModel
{
    public class TechnicanVM
    {
        public int Id { get; set; }
        public string? Date { get; set; }
        public string? Time{ get; set; }
        public string ClientName { get; set; }
        public string City { get; set; }
        public string Destrict { get; set; }
        public string Service { get; set; }
        public bool? IsPayed { get; set; }
        public string PaymentMethod { get; set; }
        public string Notes { get; set; }
        public string? location { get; set; }

        public int? StatusId { get; set; }

        public string ?ImageStatus { get; set; }
        [NotMapped]
        [DisplayName("uploadImg")]
        public IFormFile ? ImageStatusFile { get; set; }

     /*   public string ?CarAfterService { get; set; }
        [NotMapped]
        [DisplayName("uploadImg")]
        public IFormFile? CarAfterServiceFile { get; set; }


        public string? paymentImg { get; set; }
        [NotMapped]
        [DisplayName("uploadImg")]
        public IFormFile? paymentImgFile { get; set; }

        public string? BillAfterService { get; set; }
        [NotMapped]
        [DisplayName("uploadImg")]
        public IFormFile? BillAfterServiceFile { get; set; }
*/

    }
}

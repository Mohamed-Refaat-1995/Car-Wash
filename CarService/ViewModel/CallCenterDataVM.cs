using CarService.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarService.ViewModel
{
    public class CallCenterDataVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Service { get; set; }

        [Required]
        //[RegularExpression("^(?!0)[0-9]*$")]
        public string Price { get; set; }
        public string Notes { get; set; }


        public string? Date { get; set; }
        public string Time { get; set; }

        public string City { get; set; }
        public string Destrict { get; set; }
        public string PhoneNumber { get; set; }
        public string Source { get; set; }
        public DateTime ServiceDate { get; set; }
        public string OrderStatus { get; set; }
    }
}

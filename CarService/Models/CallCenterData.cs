using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CarService.Models
{
    public class CallCenterData
    {
        [Key]
        [ForeignKey("PreRequst")]
        public int PreReqId { get; set; }
        public string? Date { get; set; }
        public string? Time { get; set; }
        public string? Name { get; set; }
        public string? CarType { get; set; }
        public string? Service { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Source { get; set; }
        public string? Destrict { get; set; }
        public string? Price { get; set; }
        public string? Notes { get; set; }
        [Column(TypeName = "date")]
        public DateTime ServiceDate { get; set; }
        public string? OrderStatus { get; set; }
        public bool? CoordenaitorExist { get; set; }
        public virtual PreRequst PreRequst { get; set; }
    }
}

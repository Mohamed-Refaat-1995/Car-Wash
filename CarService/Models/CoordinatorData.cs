using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace CarService.Models
{
    public class CoordinatorData
    {
        [Key]
        [ForeignKey("CallCenterData")]
        public int PreReqId { get; set; }
        public string? City { get; set; }
        public bool? IsPayed { get; set; }
        public string? PaymentMethod { get; set; }
        public string? TabbyID { get; set; }

        [NotMapped]
        [DisplayName("uploadImg")]
        public IFormFile PaymentImageFile { get; set; }
        public string? PaymentImage { get; set; }

        [Column(TypeName = "date")]
        public DateTime? AssignDate { get; set; }

        //[DataType(DataType.Time)]
        public string? AssignTime { get; set; }
        [ForeignKey("Technican")]
        public string? TechnicanID { get; set; }

        public bool TechnicanExist { get; set; }

        public virtual Technican Technican { get; set; }
        public virtual CallCenterData CallCenterData { get; set; }
    }
}

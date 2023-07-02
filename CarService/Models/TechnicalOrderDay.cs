using System.ComponentModel.DataAnnotations.Schema;

namespace CarService.Models
{
    public class TechnicalOrderDay
    {
        public int Id { get; set; }

        [ForeignKey("Technican")]
        public string TechnicalId { get; set; }

        [Column(TypeName = "date")]
        public DateTime ServiceDay { get; set; }



        public virtual Technican Technican { get; set; }
    }
}

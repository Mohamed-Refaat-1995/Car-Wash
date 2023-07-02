using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarService.Models
{
    public class CallCenter
    {
        [Key]
        [ForeignKey("User")]
        public string Id { get; set; }

        [ForeignKey("City")]
        public int CityID { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual City City { get; set; }
    }
}

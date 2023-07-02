using System.ComponentModel.DataAnnotations.Schema;

namespace CarService.Models
{
    public class TechnicalOrderDayTime
    {
        public int Id { get; set; }
        [ForeignKey("TechnicalOrderDay")]
        public int OrderDayID { get; set; }
        [ForeignKey("Times")]
        public int TimeId { get; set; }
        [ForeignKey("PreRequst")]
        public int ClientDataId { get; set; }
        public virtual TechnicalOrderDay TechnicalOrderDay { get; set; }
        public virtual Times Times { get; set; }
        public virtual PreRequst PreRequst { get; set; }
    }
}

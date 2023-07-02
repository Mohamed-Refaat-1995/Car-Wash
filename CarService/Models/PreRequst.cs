using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security;

namespace CarService.Models
{
    public class PreRequst
    {
        public int Id { get; set; }

        public string? Date { get; set; }

        public string? Time { get; set; }

        [ForeignKey("City")]
        public int CityId { get; set; }
        public string Destrict { get; set; }

        [ForeignKey("Services")]
        public int? ServiceId { get; set; }
        public string PhoneNumber { get; set; }

        [ForeignKey("CallCenter")]
        public string? CallCenterId { get; set; }


        public bool IsExist { get; set; }
        public bool IsFinshed { get; set; }

        public virtual City City { get; set; }
        public virtual CallCenter CallCenter { get; set; }
        public virtual Services Services { get; set; }


    }
}

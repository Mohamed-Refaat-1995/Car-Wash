using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace CarService.Models
{
    public class TechnicanData
    {
        public int Id { get; set; }
        [ForeignKey("Technican")]
        public string TechnicanId { get; set; }
        public int? StatusId { get; set; }
        [ForeignKey("CoordinatorData")]
        public int ClineDataId { get; set; }
        public string? Location { get; set; }

        public string? ImageStatus { get; set; }
        [NotMapped]
        [DisplayName("Upload ImageStatus")]
        public IFormFile? ImageStatusFile { get; set; }

        public string Date { get; set; }
        public string Time { get; set; }
        public virtual CoordinatorData CoordinatorData { get; set; }
        public virtual Technican Technican { get; set; }

    }
}

using CollegeHub.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace CollegeHub.Models {
    public class Activity {
        public Guid Guid { get; set; }
        public Guid StudentId { get; set; }
        public DateTime CreatedOn { get; set; }
        public Subject Subject { get; set; }
        public Exam Exam { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public Decimal Grade { get; set; }
    }
}

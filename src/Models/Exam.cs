using CollegeHub.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace CollegeHub.Models {
    public class Exam : Entity {
        public Guid TeacherId { get; set; }
        public Subject Subject { get; set; }
        public List<Question> Questions { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public Decimal Value { get; set; }

    }
}

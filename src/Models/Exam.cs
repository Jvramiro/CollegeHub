using CollegeHub.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace CollegeHub.Models {
    public class Exam : Entity {
        public Guid TeacherId { get; private set; }
        public Subject Subject { get; private set; }

        [Column(TypeName = "decimal(10,2)")]
        public Decimal Value { get; private set; }

        public Exam(Guid TeacherId, Subject Subject, Decimal Value) {
            this.TeacherId = TeacherId;
            this.Subject = Subject;
            this.Value = Value;

            CreatedOn = DateTime.Now;
            EditedOn = DateTime.Now;
            Active = true;
        }

    }
}

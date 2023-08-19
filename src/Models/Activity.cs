using CollegeHub.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace CollegeHub.Models {
    public class Activity {
        public Guid Id { get; set; }
        public string Student { get; set; }
        public Guid StudentId { get; set; }
        public Guid ExamId { get; set; }
        public DateTime CreatedOn { get; set; }
        public Subject Subject { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public Decimal Grade { get; set; }

        public Activity(string Student, Guid StudentId, Guid ExamId, Subject subject, Decimal Grade) {

            this.Student = Student;
            this.StudentId = StudentId;
            this.ExamId = ExamId;
            this.Subject = subject;
            this.Grade = Grade;

            CreatedOn = DateTime.Now;

        }
    }
}

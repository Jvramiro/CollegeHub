using CollegeHub.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace CollegeHub.Models {
    public class Question {
        public Guid Id { get; set; }
        public Guid ExamId { get; set; }
        public string Text { get; set; }
        public string AnswerA { get; set; }
        public string AnswerB { get; set; }
        public string? AnswerC { get; set; }
        public string? AnswerD { get; set; }
        public string? AnswerE { get; set; }
        public int CorrectAnswer { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public Decimal IndividualValue { get; set; }

    }
}

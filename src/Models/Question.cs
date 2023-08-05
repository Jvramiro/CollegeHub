using System.ComponentModel.DataAnnotations.Schema;

namespace CollegeHub.Models {
    public class Question {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string Text { get; set; }
        public List<Answer> Answers { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public Decimal Value { get; set; }

    }
}

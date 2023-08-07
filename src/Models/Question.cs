using CollegeHub.Enums;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CollegeHub.Models {
    public class Question {
        public Guid Id { get; set; }
        public Guid ExamId { get; private set; }
        public string Text { get; private set; }
        public string AnswerA { get; private set; }
        public string AnswerB { get; private set; }
        public string? AnswerC { get; private set; }
        public string? AnswerD { get; private set; }
        public string? AnswerE { get; private set; }
        public int CorrectAnswer { get; private set; }

        [Column(TypeName = "decimal(10,2)")]
        public Decimal IndividualValue { get; private set; }

        public Question(Guid ExamId, string Text, int CorrectAnswer, Decimal IndividualValue, string AnswerA, string AnswerB, 
            string? AnswerC = null, string? AnswerD = null, string? AnswerE = null) {

            this.ExamId = ExamId;
            this.Text = Text;
            this.CorrectAnswer = CorrectAnswer;
            this.IndividualValue = IndividualValue;
            this.AnswerA = AnswerA;
            this.AnswerB = AnswerB;
            this.AnswerC = AnswerC ?? null;
            this.AnswerD = AnswerD ?? null;
            this.AnswerE = AnswerE ?? null;

        }

    }
}

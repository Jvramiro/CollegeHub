namespace CollegeHub.DTO.QuestionDTO {
    public record QuestionRequest(
        string Text,
        int CorrectAnswer,
        string AnswerA,
        string AnswerB,

        string? AnswerC = null,
        string? AnswerD = null,
        string? AnswerE = null,
        decimal? IndividualValue = null
     );
}

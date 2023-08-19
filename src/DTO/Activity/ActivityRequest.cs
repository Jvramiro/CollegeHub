namespace CollegeHub.DTO.Activity {
    public record ActivityRequest(Guid ExamId, List<KeyValuePair<Guid,int>> Answers);
}

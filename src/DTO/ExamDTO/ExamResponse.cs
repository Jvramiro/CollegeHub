using CollegeHub.DTO.QuestionDTO;
using CollegeHub.Enums;
using CollegeHub.Models;

namespace CollegeHub.DTO.ExamDTO {
    public record ExamResponse(Guid Id, string Subject, Decimal Value);
    public record ExamUnitResponse(Guid Id, string Subject, List<Question> Questions, Decimal Value);
}

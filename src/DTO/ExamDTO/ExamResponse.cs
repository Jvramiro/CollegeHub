using CollegeHub.Enums;
using CollegeHub.Models;

namespace CollegeHub.DTO.ExamDTO {
    public record ExamResponse(string Subject, List<Question> Questions, Decimal Value);
}

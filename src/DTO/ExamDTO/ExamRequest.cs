using CollegeHub.Enums;
using CollegeHub.Models;

namespace CollegeHub.DTO.ExamDTO {
    public record ExamRequest(Guid TeacherId, Subject Subject, List<Question> Questions, Decimal Value, bool DistributeValue);
}

using CollegeHub.DTO.QuestionDTO;
using CollegeHub.Enums;
using CollegeHub.Models;

namespace CollegeHub.DTO.ExamDTO {
    public record ExamRequest(
        Guid TeacherId, Subject Subject, List<QuestionRequest> Questions, Decimal Value, bool DistributeValue
    );
}

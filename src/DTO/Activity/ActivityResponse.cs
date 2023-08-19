using CollegeHub.Enums;
using CollegeHub.Models;

namespace CollegeHub.DTO.Activity {
    public record ActivityUnitResponse(string Student, Subject Subject, DateTime Date, decimal Grade);
    public record ActivityResponse(string Student, Subject Subject, DateTime Date, decimal Grade);
}

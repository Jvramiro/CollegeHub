using CollegeHub.Models;

namespace CollegeHub.DTO {
    public record UserResponse(string Name, string Email, Role Role, bool Active);
}

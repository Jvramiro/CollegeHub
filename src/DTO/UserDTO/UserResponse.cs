using CollegeHub.Models;

namespace CollegeHub.DTO.UserDTO
{
    public record UserResponse(string Name, string Email, string Role, bool Active);
}

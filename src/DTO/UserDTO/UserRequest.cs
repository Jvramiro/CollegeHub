using CollegeHub.Models;

namespace CollegeHub.DTO.UserDTO
{
    public record UserRequest(string Name, string Email, string Password, string CPF, string Phone, Role Role);
    public record UserUpdate(string? Name, string? Password, string? Phone);
}

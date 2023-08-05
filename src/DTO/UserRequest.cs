using CollegeHub.Models;

namespace CollegeHub.DTO {
    public record UserRequest(string Name, string Email, string Password, string CPF, string Phone, Role Role);
}

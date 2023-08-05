using CollegeHub.DTO;
using CollegeHub.Models;
using Microsoft.AspNetCore.Mvc;

namespace CollegeHub.Controllers {
    [Route("/[controller]")]
    [ApiController]
    public class UserController : ControllerBase {

        [HttpGet]        
        public async Task<IResult> GetUserById([FromRoute] Guid id) {

            var response = new UserResponse("John Doe", "John@mail.com", Role.Student, true);

            return Results.Ok(response);

        }
        
    }
}

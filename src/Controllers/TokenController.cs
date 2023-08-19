using CollegeHub.Data;
using CollegeHub.DTO;
using CollegeHub.Extensions;
using CollegeHub.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CollegeHub.Controllers {

    [Route("/Login")]
    [ApiController]
    public class TokenController : ControllerBase {

        private readonly DBContext dbContext;
        private readonly IConfiguration configuration;
        public TokenController(DBContext dbContext, IConfiguration configuration) {
            this.dbContext = dbContext;
            this.configuration = configuration;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginRequest request) {

            var user = await dbContext.User.AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == request.Email && u.Password == request.Password.HashPassword());

            if (user == null) {
                return Unauthorized();
            }

            var token = TokenService.GenerateToken(user.Email, user.Id, user.Role, configuration);

            var response = new {
                Token = token
            };

            return Ok(response);

        }

    }
}

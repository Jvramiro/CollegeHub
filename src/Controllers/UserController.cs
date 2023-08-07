using CollegeHub.Data;
using CollegeHub.DTO.UserDTO;
using CollegeHub.Extensions;
using CollegeHub.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CollegeHub.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class UserController : ControllerBase {

        private readonly DBContext dbContext;
        public UserController(DBContext dbContext) {
            this.dbContext = dbContext;
        }

        [HttpGet]
        [Authorize]
        public async Task<IResult> GetCurrentUser() {

            var userId = HttpContext.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value;

            var user = await dbContext.User.FirstOrDefaultAsync(u => u.Id == Guid.Parse(userId));

            if (user == null) {
                return Results.Problem("Current stored Id not found in database");
            }

            var response = new UserResponse(user.Name, user.Email, user.Role.ToString(), user.Active);
            return Results.Ok(response);

        }

        [HttpGet("GetAll")]
        [Authorize(Roles = "Adm")]
        public async Task<IResult> GetAll(int page = 1, int rows = 30) {

            if(rows > 100) {
                return Results.BadRequest("The number of rows cannot exceed 100");
            }

            var users = await dbContext.User.AsNoTracking().Skip((page - 1) * rows).Take(rows).ToListAsync();

            if(users == null) {
                return Results.BadRequest("No user found");
            }

            var response = users.Select(u => new UserResponse(u.Name, u.Email, u.Role.ToString(), u.Active));
            return Results.Ok(response);

        }

        [HttpGet("id/{id}")]
        [Authorize(Roles = "Adm")]
        public async Task<IResult> GetUserById([FromRoute] Guid id) {

            var user = await dbContext.User.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);

            if(user == null) {
                return Results.NotFound("Id not found");
            }

            var response = new UserResponse(user.Name, user.Email, user.Role.ToString(), user.Active);
            return Results.Ok(response);

        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Adm")]
        public async Task<IResult> Update([FromRoute] Guid id, UserUpdate request) {

            var user = await dbContext.User.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);

            if (user == null) {
                return Results.NotFound("Id not found");
            }

            user.Update(
                request.Name ?? null,
                request.Password != null ? request.Password.HashPassword() : null,
                request.Phone ?? null,
                request.Active ?? null
            );

            var editedBy = HttpContext.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value;
            user.EditedBy = Guid.Parse(editedBy);

            dbContext.User.Update(user);
            await dbContext.SaveChangesAsync();

            var response = new UserResponse(user.Name, user.Email, user.Role.ToString(), user.Active);
            return Results.Ok(response);

        }

        [HttpPost]
        [Authorize(Roles = "Adm")]
        public async Task<IResult> Create(UserRequest request) {

            if (!ModelState.IsValid) {
                return Results.BadRequest("Invalid data");
            }

            var createdBy = HttpContext.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value;

            var user = new User(request.Name, request.Email, request.Password.HashPassword(), request.CPF, request.Phone,
                request.Role, Guid.Parse(createdBy));

            await dbContext.User.AddAsync(user);
            await dbContext.SaveChangesAsync();

            return Results.Created($"User {user.Name} created successfully", user.Role);

        }

        [HttpDelete("{id}")]
        public async Task<IResult> Delete([FromRoute] Guid id) {

            var user = await dbContext.User.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);

            if (user == null) {
                return Results.NotFound("Id not found");
            }

            dbContext.User.Remove(user);
            await dbContext.SaveChangesAsync();

            return Results.Ok($"User {user.Name} deleted successfully");

        }

    }
}

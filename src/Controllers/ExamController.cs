using CollegeHub.Data;
using CollegeHub.DTO.ExamDTO;
using CollegeHub.DTO.UserDTO;
using CollegeHub.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CollegeHub.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class ExamController : ControllerBase {

        private readonly DBContext dbContext;
        public ExamController(DBContext dbContext) {
            this.dbContext = dbContext;
        }

        [HttpGet]
        [Authorize(Roles = "Teacher, Adm")]
        public async Task<IResult> GetAll(int page = 1, int rows = 30) {

            if (rows > 100) {
                return Results.BadRequest("The number of rows cannot exceed 100");
            }

            var exams = await dbContext.Exam.AsNoTracking().Skip((page - 1) * rows).Take(rows).ToListAsync();

            if (exams == null) {
                return Results.BadRequest("No Exam found");
            }

            var response = exams.Select(e => new ExamResponse(e.Subject.ToString(), e.Questions, e.Value));
            return Results.Ok(response);

        }

        [HttpGet("id/{id}")]
        [Authorize(Roles = "Teacher, Adm")]
        public async Task<IResult> GetById([FromRoute] Guid id) {

            var exam = await dbContext.Exam.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);

            if(exam == null) {
                return Results.NotFound("Id not found");
            }

            var response = new ExamResponse(exam.Subject.ToString(), exam.Questions, exam.Value);
            return Results.Ok(response);

        }

        [HttpPost]
        [Authorize(Roles = "Teacher, Adm")]
        public async Task<IResult> Create(ExamRequest examRequest) {

            if (!ModelState.IsValid) {
                return Results.BadRequest("Invalid data");
            }

            var exam = new Exam(
                examRequest.TeacherId,
                examRequest.Subject,
                examRequest.Questions,
                examRequest.Value,
                examRequest.DistributeValue
            );

            await dbContext.Exam.AddAsync(exam);
            await dbContext.SaveChangesAsync();

            return Results.Created($"Exam created successfully", exam.Id);
        }

        [HttpDelete("{id}")]
        public async Task<IResult> Delete([FromRoute] Guid id) {

            var exam = await dbContext.Exam.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);

            if(exam == null) {
                return Results.NotFound("Id not found");
            }

            dbContext.Exam.Remove(exam);
            await dbContext.SaveChangesAsync();

            return Results.Ok($"Exam deleted successfully");

        }

    }
}

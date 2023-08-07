using CollegeHub.Data;
using CollegeHub.DTO.ExamDTO;
using CollegeHub.DTO.QuestionDTO;
using CollegeHub.DTO.UserDTO;
using CollegeHub.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

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
        public async Task<IResult> GetAll(int page = 1, int rows = 10) {

            if (rows > 10) {
                return Results.BadRequest("The number of rows cannot exceed 10");
            }

            var exams = await dbContext.Exam.AsNoTracking().Skip((page - 1) * rows).Take(rows).ToListAsync();

            if (exams == null) {
                return Results.BadRequest("No Exam found");
            }

            var response = exams.Select(e => new ExamResponse(e.Id, e.Subject.ToString(), e.Value));
            return Results.Ok(response);

        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Teacher, Adm")]
        public async Task<IResult> GetById([FromRoute] Guid id) {

            var exam = await dbContext.Exam.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);

            if(exam == null) {
                return Results.NotFound("Id not found");
            }

            var questions = await dbContext.Question.AsNoTracking().Where(q => q.ExamId == id).ToListAsync();

            if (questions == null) {
                return Results.NotFound("Invalid Exam");
            }

            var response = new ExamUnitResponse(exam.Subject.ToString(), questions, exam.Value);
            return Results.Ok(response);

        }

        [HttpPost]
        [Authorize(Roles = "Teacher, Adm")]
        public async Task<IResult> Create(ExamRequest examRequest) {

            if (!ModelState.IsValid) {
                return Results.BadRequest("Invalid data");
            }

            decimal individualValue = examRequest.Value / examRequest.Questions.Count;
            decimal value = examRequest.Value;
            if (!examRequest.DistributeValue) {
                try {
                    individualValue = examRequest.Questions.Sum(q => (decimal)q.IndividualValue);
                }
                catch {
                    return Results.BadRequest("DistributeValue is true and not all questions have valid individual values");
                }
            }

            var exam = new Exam(
                examRequest.TeacherId,
                examRequest.Subject,
                value
            );

            var createdBy = HttpContext.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value;
            exam.CreatedBy = Guid.Parse(createdBy);
            exam.EditedBy = Guid.Parse(createdBy);

            await dbContext.Exam.AddAsync(exam);
            await dbContext.SaveChangesAsync();

            var questions = examRequest.Questions.Select(current => new Question(
                exam.Id,
                current.Text,
                current.CorrectAnswer,
                examRequest.DistributeValue ? individualValue : (decimal)current.IndividualValue,
                current.AnswerA,
                current.AnswerB,
                current.AnswerC,
                current.AnswerD,
                current.AnswerE
            )).ToList();

            await dbContext.Question.AddRangeAsync(questions);
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

using CollegeHub.Data;
using CollegeHub.DTO.Activity;
using CollegeHub.DTO.ExamDTO;
using CollegeHub.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;

namespace CollegeHub.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class ActivityController : ControllerBase {

        private readonly DBContext dbContext;
        public ActivityController(DBContext dbContext) {
            this.dbContext = dbContext;
        }

        [HttpGet]
        [Authorize(Roles = "Teacher, Adm")]
        public async Task<IActionResult> GetAll(int page = 1, int rows = 10) {

            if (rows > 100) {
                return BadRequest("The number of rows cannot exceed 100");
            }

            var activities = await dbContext.Activity.AsNoTracking().Skip((page - 1) * rows).Take(rows).ToListAsync();

            if (activities == null) {
                return BadRequest("No Activity found");
            }

            var response = activities.Select(a => new ActivityUnitResponse(a.Student, a.Subject, a.CreatedOn, a.Grade));

            return Ok(response);

        }

        [Authorize(Roles = "Teacher, Adm")]
        public async Task<IActionResult> GetById([FromRoute] Guid id) {

            var activity = await dbContext.Activity.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);

            if (activity == null) {
                return NotFound("Id not found");
            }

            var questions = await dbContext.Question.AsNoTracking().Where(q => q.ExamId == activity.ExamId).ToListAsync();

            if (questions == null) {
                return NotFound("Invalid Activity");
            }

            var response = new ActivityResponse(activity.Student, activity.Subject, activity.CreatedOn, activity.Grade);
            return Ok(response);

        }

        [HttpPost]
        [Authorize(Roles = "Student, Teacher, Adm")]
        public async Task<IActionResult> Create(ActivityRequest activityRequest) {

            if (!ModelState.IsValid) {
                return BadRequest("Invalid data");
            }

            var userID = HttpContext.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var user = await dbContext.User.AsNoTracking().FirstOrDefaultAsync(u => u.Id == Guid.Parse(userID));

            if(user == null) {
                return NotFound("Your User Connection id was not found");
            }

            var exam = await dbContext.Exam.AsNoTracking().FirstOrDefaultAsync(e => e.Id == activityRequest.ExamId);

            if(exam == null) {
                return NotFound("Exam was not found");
            }

            var questions = await dbContext.Question.AsNoTracking().Where(q => q.ExamId == exam.Id).ToListAsync();

            if(questions == null) {
                return NotFound("Questions were not found");
            }

            if(questions.Count != activityRequest.Answers.Count) {
                return BadRequest("The number of questions sent does not correspond to the number of questions in the exam");
            }

            var correctAnswers = questions.Select((q, i) => activityRequest.Answers[i].Value == q.CorrectAnswer).ToList();

            decimal grade = 0;
            for(int i = 0; i < questions.Count; i++) {
                if (correctAnswers[i]) {
                    grade += questions[i].IndividualValue;
                }
            }

            var activity = new Models.Activity(
                user.Name,
                user.Id,
                exam.Id,
                exam.Subject,
                grade
            );

            await dbContext.Activity.AddAsync(activity);
            await dbContext.SaveChangesAsync();

            var response = new ActivityResponse(activity.Student, activity.Subject, activity.CreatedOn, activity.Grade);
            return Created($"Activity created successfully", activity.Id);
        
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id) {

            var activity = await dbContext.Activity.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);

            if (activity == null) {
                return NotFound("Activity not found");
            }

            dbContext.Activity.Remove(activity);
            await dbContext.SaveChangesAsync();

            return Ok($"Activity deleted successfully");

        }

    }
}

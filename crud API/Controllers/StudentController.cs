using crud_API.Data;
using crud_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace crud_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public StudentController(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetStudents()
        {
            var students = await _context.Students.AsNoTracking().ToListAsync();
            return Ok(students);
        }

        [HttpPost]
        public async Task<IActionResult> Create (Student student)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _context.Students.AddAsync(student);
            var result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                return Ok(result);
            }
            
            return BadRequest();
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetStudent(int id)
        {
            var student = _context.Students.FindAsync(id);

            if (student == null)
            {
                return NotFound();
            }
            return Ok(student);
        }

        [HttpPost("{id:int}")]
        public async Task<IActionResult> UpdateStudent(int id,Student student)
        {
            var studentFromDb = await _context.Students.FindAsync(id);

            if (studentFromDb == null)
            {
                return NotFound("Student Not Found");
            }

            studentFromDb.Name = student.Name;
            studentFromDb.Address = student.Address;
            studentFromDb.Email = student.Email;
            studentFromDb.PhoneNumber = student.PhoneNumber;

            var result = _context.SaveChanges();

            if (result > 0)
            {
                return Ok("Student Updated");
            }
            return BadRequest();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var student = await _context.Students.FindAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            _context.Students.Remove(student);

            var result = _context.SaveChanges();
            if (result > 0)
            {
                return Ok("Student Deleted");
            }
            return BadRequest("unable to delete");
        }
    }
}

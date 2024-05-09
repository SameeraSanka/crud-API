using crud_API.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult index()
        {
            return Ok("ok");
        }
    }
}

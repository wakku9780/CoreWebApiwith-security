using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoreWebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class StudentAPIController : ControllerBase
    {
        private readonly StudentDBContext _context;

        public StudentAPIController(StudentDBContext context )
        {
            this._context = context;
        }



        // GET: api/StudentAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        {
            var students = await _context.Students.ToListAsync();
            return Ok(students);
        }

        // GET: api/StudentAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);

            if (student == null)
            {
                return NotFound($"Student with ID {id} not found.");
            }

            return Ok(student);
        }

        // POST: api/StudentAPI
        [HttpPost]
        public async Task<ActionResult<Student>> CreateStudent([FromBody] Student student)
        {
            if (student == null)
            {
                return BadRequest("Student data is null.");
            }

            // Optionally, add validation here

            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            // Returns a 201 response with a Location header pointing to the newly created resource
            return CreatedAtAction(nameof(GetStudent), new { id = student.Id }, student);
        }

        // PUT: api/StudentAPI/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(int id, [FromBody] Student updatedStudent)
        {
            if (updatedStudent == null || id != updatedStudent.Id)
            {
                return BadRequest("Student ID mismatch or data is null.");
            }

            var existingStudent = await _context.Students.FindAsync(id);
            if (existingStudent == null)
            {
                return NotFound($"Student with ID {id} not found.");
            }

            // Update the properties
            existingStudent.Name = updatedStudent.Name;
            existingStudent.Gender = updatedStudent.Gender;
            existingStudent.Age = updatedStudent.Age;

            // Mark the entity as modified
            _context.Entry(existingStudent).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Handle concurrency issues if any
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating data.");
            }

            // 204 No Content is a standard response for successful PUT requests when not returning data
            return NoContent();
        }

        // DELETE: api/StudentAPI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound($"Student with ID {id} not found.");
            }

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            // 204 No Content is a standard response for successful DELETE requests
            return NoContent();
        }


    }
}

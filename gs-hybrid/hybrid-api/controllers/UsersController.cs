using gs_hybrid.hybrid_api.model;
using Microsoft.AspNetCore.Mvc;
using gs_hybrid.hybrid_api.data;
using Microsoft.EntityFrameworkCore;

namespace gs_hybrid.hybrid_api.controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly HybridApiDbContext _db;
        public UsersController(HybridApiDbContext db) => _db = db;

        //GET: /api/v1/users
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _db.Users.ToListAsync();
            return Ok(users);
        }

        //GET: /api/v1/users/{id}
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var user = await _db.Users.FindAsync(id);
            return user == null ? NotFound() : Ok(user);
        }

        //POST: /api/v1/users
        [HttpPost]
        public async Task<IActionResult> Create(User user)
        {
            if (await _db.Users.AnyAsync(u => u.Email == user.Email))
                return BadRequest(new { message = "Email já cadastrado." });

            _db.Users.Add(user);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = user.Id, version = "1.0" }, user);
        }

        //PUT: /api/v1/users/{id}
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, User updated)
        {
            var user = await _db.Users.FindAsync(id);
            if (user == null) return NotFound();

            user.FullName = updated.FullName;
            user.Email = updated.Email;
            user.Role = updated.Role;

            await _db.SaveChangesAsync();
            return Ok(user);
        }

        //DELETE: /api/v1/users/{id}
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var user = await _db.Users.FindAsync(id);
            if (user == null) return NotFound();

            _db.Users.Remove(user);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}

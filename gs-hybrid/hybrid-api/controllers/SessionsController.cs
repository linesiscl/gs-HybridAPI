using gs_hybrid.hybrid_api.data;
using gs_hybrid.hybrid_api.dto;
using gs_hybrid.hybrid_api.model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace gs_hybrid.hybrid_api.controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class SessionsController : ControllerBase
    {
        private readonly HybridApiDbContext _db;
        public SessionsController(HybridApiDbContext db) => _db = db;

        //GET: /api/v1/sessions
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var sessions = await _db.WorkSessions.Include(s => s.Pauses).ToListAsync();
            return Ok(sessions);
        }

        //GET: /api/v1/sessions/{id}
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var s = await _db.WorkSessions.Include(x => x.Pauses)
                                          .FirstOrDefaultAsync(x => x.Id == id);
            return s == null ? NotFound() : Ok(s);
        }

        //POST: /api/v1/sessions
        [HttpPost]
        public async Task<IActionResult> Create(CreateWorkSessionDto dto)
        {
            var user = await _db.Users.FindAsync(dto.UserId);
            if (user == null)
                return NotFound(new { message = "Usuário não encontrado." });

            var session = new WorkSession
            {
                Id = Guid.NewGuid(),
                UserId = dto.UserId,
                StartUtc = dto.StartUtc,
                IsProductive = dto.IsProductive
            };

            _db.WorkSessions.Add(session);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = session.Id, version = "1.0" }, session);
        }

        //PUT: /api/v1/sessions/{id}/close
        [HttpPut("{id:guid}/close")]
        public async Task<IActionResult> Close(Guid id, CloseWorkSessionDto dto)
        {
            var session = await _db.WorkSessions.FindAsync(id);
            if (session == null) return NotFound();

            if (session.EndUtc != null)
                return BadRequest(new { message = "Sessão já encerrada." });

            session.EndUtc = dto.EndUtc;
            await _db.SaveChangesAsync();

            return Ok(session);
        }

        //DELETE: /api/v1/sessions/{id}
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var s = await _db.WorkSessions.FindAsync(id);
            if (s == null) return NotFound();

            _db.WorkSessions.Remove(s);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}

using gs_hybrid.hybrid_api.dto;
using gs_hybrid.hybrid_api.model;
using gs_hybrid.hybrid_api.data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace gs_hybrid.hybrid_api.controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class PausesController : ControllerBase
    {
        private readonly HybridApiDbContext _db;
        public PausesController(HybridApiDbContext db) => _db = db;

        //GET: /api/v1/pauses
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var pauses = await _db.Pauses.Include(p => p.WorkSession).ToListAsync();
            return Ok(pauses);
        }

        //GET: /api/v1/pauses/{id}
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var pause = await _db.Pauses.Include(p => p.WorkSession).FirstOrDefaultAsync(p => p.Id == id);
            return pause == null ? NotFound() : Ok(pause);
        }

        //POST: /api/v1/pauses
        [HttpPost]
        public async Task<IActionResult> Create(PauseDto dto)
        {
            var session = await _db.WorkSessions.FindAsync(dto.WorkSessionId);
            if (session == null) return NotFound(new { message = "Sessão não encontrada." });

            var pause = new Pause
            {
                Id = Guid.NewGuid(),
                WorkSessionId = dto.WorkSessionId,
                StartUtc = dto.StartUtc,
                EndUtc = dto.EndUtc,
                PauseType = dto.PauseType
            };

            _db.Pauses.Add(pause);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = pause.Id, version = "1.0" }, pause);
        }

        //PUT: /api/v1/pauses/{id}
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, PauseDto dto)
        {
            var pause = await _db.Pauses.FindAsync(id);
            if (pause == null) return NotFound();

            pause.StartUtc = dto.StartUtc;
            pause.EndUtc = dto.EndUtc;
            pause.PauseType = dto.PauseType;

            await _db.SaveChangesAsync();
            return Ok(pause);
        }

        //DELETE: /api/v1/pauses/{id}
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var pause = await _db.Pauses.FindAsync(id);
            if (pause == null) return NotFound();

            _db.Pauses.Remove(pause);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}

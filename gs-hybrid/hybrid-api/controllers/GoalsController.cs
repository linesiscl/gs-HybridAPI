using gs_hybrid.hybrid_api.model;
using Microsoft.AspNetCore.Mvc;
using gs_hybrid.hybrid_api.data;
using Microsoft.EntityFrameworkCore;

namespace gs_hybrid.hybrid_api.controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class GoalsController : ControllerBase
    {
        private readonly HybridApiDbContext _db;
        public GoalsController(HybridApiDbContext db) => _db = db;

        //GET: /api/v1/goals
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var goals = await _db.Goals.ToListAsync();
            return Ok(goals);
        }

        //GET: /api/v1/goals/{id}
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var goal = await _db.Goals.FindAsync(id);
            return goal == null ? NotFound() : Ok(goal);
        }

        //POST: /api/v1/goals
        [HttpPost]
        public async Task<IActionResult> Create(Goal goal)
        {
            var user = await _db.Users.FindAsync(goal.UserId);
            if (user == null)
                return NotFound(new { message = "Usuário não encontrado." });

            _db.Goals.Add(goal);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = goal.Id, version = "1.0" }, goal);
        }

        //PUT: /api/v1/goals/{id}
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, Goal updated)
        {
            var goal = await _db.Goals.FindAsync(id);
            if (goal == null) return NotFound();

            goal.Title = updated.Title;
            goal.Description = updated.Description;
            goal.TargetDateUtc = updated.TargetDateUtc;
            goal.IsCompleted = updated.IsCompleted;

            await _db.SaveChangesAsync();
            return Ok(goal);
        }

        //DELETE: /api/v1/goals/{id}
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var goal = await _db.Goals.FindAsync(id);
            if (goal == null) return NotFound();

            _db.Goals.Remove(goal);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}

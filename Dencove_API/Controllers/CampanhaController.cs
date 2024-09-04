using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Dencove_API.Data;
using Dencove_API.Models;

namespace Dencove_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CampanhaController : ControllerBase
    {
        private readonly Context _context;

        public CampanhaController(Context context)
        {
            _context = context;
        }

        // GET: api/Campanha
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CampanhaModel>>> GetCampanhaModels()
        {
            return await _context.CampanhaModels.ToListAsync();
        }

        // GET: api/Campanha/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CampanhaModel>> GetCampanhaModel(Guid id)
        {
            var campanhaModel = await _context.CampanhaModels.FindAsync(id);

            if (campanhaModel == null)
            {
                return NotFound();
            }

            return campanhaModel;
        }

        // PUT: api/Campanha/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCampanhaModel(Guid id, CampanhaModel campanhaModel)
        {
            if (id != campanhaModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(campanhaModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CampanhaModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Campanha
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CampanhaModel>> PostCampanhaModel(CampanhaModel campanhaModel)
        {
            _context.CampanhaModels.Add(campanhaModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCampanhaModel", new { id = campanhaModel.Id }, campanhaModel);
        }

        // DELETE: api/Campanha/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCampanhaModel(Guid id)
        {
            var campanhaModel = await _context.CampanhaModels.FindAsync(id);
            if (campanhaModel == null)
            {
                return NotFound();
            }

            _context.CampanhaModels.Remove(campanhaModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CampanhaModelExists(Guid id)
        {
            return _context.CampanhaModels.Any(e => e.Id == id);
        }
    }
}

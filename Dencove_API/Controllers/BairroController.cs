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
    public class BairroController : ControllerBase
    {
        private readonly Context _context;

        public BairroController(Context context)
        {
            _context = context;
        }

        // GET: api/Bairro
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BairroModel>>> GetBairro()
        {
            return await _context.BairroModels.ToListAsync();
        }

        // GET: api/Bairro/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BairroModel>> GetBairro(int id)
        {
            var bairroModel = await _context.BairroModels.FindAsync(id);

            if (bairroModel == null)
            {
                return NotFound();
            }

            return bairroModel;
        }

        // PUT: api/Bairro/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBairro(int id, BairroModel bairroModel)
        {
            if (id != bairroModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(bairroModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BairroModelExists(id))
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

        // POST: api/Bairro
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BairroModel>> PostBairro(BairroModel bairroModel)
        {
            _context.BairroModels.Add(bairroModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBairro", new { id = bairroModel.Id }, bairroModel);
        }

        // DELETE: api/Bairro/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBairro(int id)
        {
            var bairroModel = await _context.BairroModels.FindAsync(id);
            if (bairroModel == null)
            {
                return NotFound();
            }

            _context.BairroModels.Remove(bairroModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BairroModelExists(int id)
        {
            return _context.BairroModels.Any(e => e.Id == id);
        }
    }
}

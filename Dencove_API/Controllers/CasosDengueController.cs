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
    public class CasosDengueController : ControllerBase
    {
        private readonly Context _context;

        public CasosDengueController(Context context)
        {
            _context = context;
        }

        // GET: api/CasosDengue
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CasosDengueModel>>> GetCasosDengueModels()
        {
            return await _context.CasosDengueModels.ToListAsync();
        }

        // GET: api/CasosDengue/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CasosDengueModel>> GetCasosDengueModel(int id)
        {
            var casosDengueModel = await _context.CasosDengueModels.FindAsync(id);

            if (casosDengueModel == null)
            {
                return NotFound();
            }

            return casosDengueModel;
        }

        // PUT: api/CasosDengue/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCasosDengueModel(int id, CasosDengueModel casosDengueModel)
        {

            if (id != casosDengueModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(casosDengueModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CasosDengueModelExists(id))
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

        // POST: api/CasosDengue
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CasosDengueModel>> PostCasosDengueModel(CasosDengueModel casosDengueModel, string nome_pessoa, string endereco, string telefone, string email, bool status, DateOnly datacaso, int bairroid)
        {
            casosDengueModel.Nome_Pessoa = nome_pessoa;
            casosDengueModel.Endereco = endereco;
            casosDengueModel.BairroId = bairroid;
            casosDengueModel.Telefone = telefone;
            casosDengueModel.Email = email;
            casosDengueModel.Status = status;
            casosDengueModel.Data_Caso = datacaso;

            _context.CasosDengueModels.Add(casosDengueModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCasosDengueModel", new { id = casosDengueModel.Id }, casosDengueModel);
        }

        // DELETE: api/CasosDengue/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCasosDengueModel(int id)
        {
            var casosDengueModel = await _context.CasosDengueModels.FindAsync(id);
            if (casosDengueModel == null)
            {
                return NotFound();
            }

            _context.CasosDengueModels.Remove(casosDengueModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CasosDengueModelExists(int id)
        {
            return _context.CasosDengueModels.Any(e => e.Id == id);
        }
    }
}

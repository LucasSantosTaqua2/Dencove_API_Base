using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Dencove_API.Data;
using Dencove_API.Models;
using Dencove_API.Requests;

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
        public async Task<IActionResult> PutCasosDengueModel(int id, CasosDengueModel casosDengueModel, string nome_pessoa, string endereço, string telefone, string email, bool status, DateOnly datacaso)

        {
            casosDengueModel.Nome_Pessoa = nome_pessoa;
            casosDengueModel.Endereco = endereço;
            casosDengueModel.Telefone = telefone;
            casosDengueModel.Email = email;
            casosDengueModel.Status = status;
            casosDengueModel.Data_Caso = datacaso;
            


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

        [HttpPost]
        public ActionResult<CasoRequest> Add(CasoRequest request)
        {
            var x = request.Nome_Pessoa;
            return Ok();
        }

        // POST: api/CasosDengue
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
       /* [HttpPost]
        public async Task<ActionResult<CasosDengueModel>> PostCasosDengueModel(CasoRequest novoCaso)
        {
         /*   if (novoCaso == null || novoCaso.BairroId <= 0)
                return BadRequest("Dados inválidos.");

            var bairro = _context.BairroModels.FirstOrDefault(b => b.Id == novoCaso.BairroId);
            if (bairro == null)
                return NotFound("Bairro não encontrado."); 

            var casoDengue = new CasosDengueModel
            {
                Nome_Pessoa = novoCaso.Nome_Pessoa,
                BairroId = novoCaso.BairroId,
                Status = novoCaso.Status,
                Data_Caso = novoCaso.Data_Caso, 
                Telefone = novoCaso.Telefone,
                Email = novoCaso.Email,
                Endereco = novoCaso.Endereco, 
            };

            _context.CasosDengueModels.Add(casoDengue);
            _context.SaveChanges();

            if (casoDengue.Status)
            {
            //    bairro.CasosConfirmados += 1;
                _context.SaveChanges();
            }

            return Ok(new { message = "Caso de dengue cadastrado com sucesso." });
        }*/

        /*[HttpPost]
        public async Task<ActionResult<CasosDengueModel>> PostCasosDengueModel(CasoRequest novoCaso)
        {
            var bairro = _context.BairroModels.FirstOrDefault(b => b.Id == novoCaso.BairroId);
            if (bairro == null)
                return NotFound("Bairro não encontrado.");

            var casoDengue = new CasosDengueModel
            {
                BairroId = novoCaso.BairroId,
                Status = novoCaso.Status,
                Data_Caso = novoCaso.Data_Caso,
                Nome_Pessoa = novoCaso.Nome_Pessoa,
                Telefone = novoCaso.Telefone,
                Email = novoCaso.Email,
                Endereco = novoCaso.Endereco,
            };

            _context.CasosDengueModels.Add(casoDengue);
            _context.SaveChanges();

            if (casoDengue.Status)
            {
                bairro.CasosConfirmados += 1;  
                _context.SaveChanges();
            }

            return Ok(new { message = "Caso de dengue cadastrado com sucesso." });
        } */

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

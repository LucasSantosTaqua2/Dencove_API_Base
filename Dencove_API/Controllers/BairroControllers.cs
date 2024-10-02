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
    public class BairroControllers : ControllerBase
    {
        private readonly Context _context;

        public BairroControllers(Context context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("api/[controller]/{bairroId}/grau-risco")]
        public int CalcularGrauDeRisco(int casosConfirmados, int alertaMaximo)
        {
            if (alertaMaximo == 0) 
                return 1; 

            
            double porcentagemCasos = ((double)casosConfirmados / alertaMaximo) * 100;

            if (porcentagemCasos < 50)
            {
                return 1; // Baixo risco (menos de 50% do alerta máximo)
            }
            else if (porcentagemCasos < 80)
            {
                return 2; // Médio risco (entre 50% e 80% do alerta máximo)
            }
            else
            {
                return 3; // Alto risco (acima de 80% do alerta máximo)
            }
        }


        [HttpPut]
        public void AtualizarGrauDeRisco(int bairroId)
        {
            // Obtém o bairro do banco de dados
            var bairro = _context.BairroModels.FirstOrDefault(b => b.Id == bairroId);
            if (bairro == null)
                throw new Exception("Bairro não encontrado");

            // Conta o número de casos confirmados para aquele bairro
            int casosConfirmados = _context.CasosDengueModels.Count(c => c.BairroId == bairroId);

            // Calcula o novo grau de risco
            int grauAtencao = Convert.ToInt32(CalcularGrauDeRisco(casosConfirmados, bairro.QtdAlertaMax));

            // Atualiza o grau de risco do bairro
            bairro.GrauAtencao = grauAtencao;

            // Salva as alterações no banco de dados
            _context.SaveChanges();
        }




        // GET: api/BairroControllers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BairroModel>>> GetBairroModels()
        {
            return await _context.BairroModels.ToListAsync();
        }

        // GET: api/BairroControllers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BairroModel>> GetBairroModel(int id)
        {

            var bairros = _context.BairroModels.Select(b => new {
                b.Id,
                b.Nome
            }).ToList();

            var bairroModel = await _context.BairroModels.FindAsync(id);
            AtualizarGrauDeRisco(id);

            if (bairroModel == null)
            {
                return NotFound();
            }

            return bairroModel;
        }



        // PUT: api/BairroControllers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBairroModel(int id, BairroModel bairroModel, string nome, int qtdAlertaMax, int casosConfirmados)
        {
            bairroModel.Nome = nome;
            bairroModel.QtdAlertaMax = qtdAlertaMax;
            bairroModel.CasosConfirmados = casosConfirmados;
       

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

        // POST: api/BairroControllers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BairroModel>> PostBairroModel(BairroModel bairroModel, string nome, int qtdAlertaMax, int casosConfirmados)
        {
            bairroModel.Nome = nome;
            bairroModel.QtdAlertaMax = qtdAlertaMax;
            bairroModel.CasosConfirmados = casosConfirmados;

            _context.BairroModels.Add(bairroModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBairroModel", new { id = bairroModel.Id }, bairroModel);
        }

        // DELETE: api/BairroControllers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBairroModel(int id)
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

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
    public class DenunciaController : ControllerBase
    {
        private readonly Context _context;
        private string imgLocate;

        public DenunciaController(Context context, IWebHostEnvironment system)
        {

            _context = context;
            imgLocate = system.WebRootPath;
        }

        // GET: api/Denuncia
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DenunciaModel>>> GetDenunciaModels()
        {
            return await _context.DenunciaModels.ToListAsync();
        }

        // GET: api/Denuncia/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DenunciaModel>> GetDenunciaModel(Guid id)
        {
            var denunciaModel = await _context.DenunciaModels.FindAsync(id);

            if (denunciaModel == null)
            {
                return NotFound();
            }

            return denunciaModel;
        }

        // PUT: api/Denuncia/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDenunciaModel(Guid id, DenunciaModel denunciaModel)
        {
            if (id != denunciaModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(denunciaModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DenunciaModelExists(id))
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

        // POST: api/Denuncia
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DenunciaModel>> PostDenunciaModel(DenunciaModel denunciaModel, IFormFile img)
        {
            string saveImgLocate = imgLocate + "\\img\\Denuncia\\";
            string nomeImg = Guid.NewGuid() + "_" + img.FileName;

            if (!Directory.Exists(saveImgLocate))
            {
                Directory.CreateDirectory(saveImgLocate);
            }


            using (var stream = System.IO.File.Create(saveImgLocate + nomeImg))
            {
                await img.CopyToAsync(stream);
            }

            denunciaModel.ImgURL = nomeImg;

            _context.Add(denunciaModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDenunciaModel", new { id = denunciaModel.Id }, denunciaModel);
        }

        // DELETE: api/Denuncia/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDenunciaModel(Guid id)
        {
            var denunciaModel = await _context.DenunciaModels.FindAsync(id);
            if (denunciaModel == null)
            {
                return NotFound();
            }

            _context.DenunciaModels.Remove(denunciaModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DenunciaModelExists(Guid id)
        {
            return _context.DenunciaModels.Any(e => e.Id == id);
        }
    }
}

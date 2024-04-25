using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SafeGuardPro.Context;
using SafeGuardPro.Models;

namespace SafeGuardPro.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class EntregaController : ControllerBase
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// 
        /// </summary>

        public EntregaController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retorna todos os cadastros das entregas existentes 
        /// </summary>
        ///<remarks>
        ///Get do cadastro, os dados retornados serão:
        ///  {
        ///     "codEntrega": 0,
        ///    "dataVal": "2024-04-25",
        ///     "codEpi": 0,
        ///     "codCol": 0,
        ///     "dataEntrega": "2024-04-25"
        ///   }
        ///</remarks>
        // GET: api/Entrega
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Entrega>>> GetEntregas()
        {
            if (_context.Entregas == null)
            {
                return NotFound();
            }
            return await _context.Entregas.ToListAsync();
        }

        /// <summary>
        /// Retorna um cadastro de uma entrega específica
        /// </summary>
        ///<remarks>
        ///GetId do cadastro, os dados da entrega desejada retornada será: 
        ///  {
        ///     "codEntrega": 0,
        ///    "dataVal": "2024-04-25",
        ///     "codEpi": 0,
        ///     "codCol": 0,
        ///     "dataEntrega": "2024-04-25"
        ///   }
        ///</remarks>
        // GET: api/Entrega/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Entrega>> GetEntrega(int id)
        {
            if (_context.Entregas == null)
            {
                return NotFound();
            }
            var entrega = await _context.Entregas.FindAsync(id);

            if (entrega == null)
            {
                return NotFound();
            }

            return entrega;
        }

        // PUT: api/Entrega/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754

        /// <summary>
        /// Altera os dados da entrega desejada
        /// </summary>
        ///<remarks>
        ///Put do cadastro, você deve informar os valores conforme no exemplo abaxo: 
        ///  {
        ///     "codEntrega": 0,
        ///    "dataVal": "2024-04-25",
        ///     "codEpi": 0,
        ///     "codCol": 0,
        ///     "dataEntrega": "2024-04-25"
        ///   }
        ///</remarks>

        [HttpPut("{id}")]
        public async Task<IActionResult> PutEntrega(int id, Entrega entrega)
        {
            if (id != entrega.CodEntrega)
            {
                return BadRequest();
            }

            _context.Entry(entrega).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EntregaExists(id))
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

        // POST: api/Entrega
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754

        /// <summary>
        /// Posta um novo cadastro de uma entrega
        /// </summary>
        ///<remarks>
        ///Post do cadastro, preencha os dados abaixo para postar um novo colaborador:  
        ///  {
        ///     "codEntrega": null,
        ///    "dataVal": "2024-04-25",
        ///     "codEpi": 0,
        ///     "codCol": 0,
        ///     "dataEntrega": "2024-04-25"
        ///   }
        ///</remarks>
        [HttpPost]
        public async Task<ActionResult<Entrega>> PostEntrega(Entrega entrega)
        {
            if (_context.Entregas == null)
            {
                return Problem("Entity set 'AppDbContext.Entregas'  is null.");
            }
            _context.Entregas.Add(entrega);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEntrega", new { id = entrega.CodEntrega }, entrega);
        }

        /// <summary>
        /// Deleta um cadastro de uma entrega desejada
        /// </summary>
        // DELETE: api/Entrega/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEntrega(int id)
        {
            if (_context.Entregas == null)
            {
                return NotFound();
            }
            var entrega = await _context.Entregas.FindAsync(id);
            if (entrega == null)
            {
                return NotFound();
            }

            _context.Entregas.Remove(entrega);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EntregaExists(int id)
        {
            return (_context.Entregas?.Any(e => e.CodEntrega == id)).GetValueOrDefault();
        }
    }
}

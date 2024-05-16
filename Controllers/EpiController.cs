using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize("Admin")]

    public class EpiController : ControllerBase
    {
        private readonly AppDbContext _context;
        /// <summary>
        /// 
        /// </summary>
        public EpiController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retorna todos os cadastros dos EPIs existentes 
        /// </summary>
        /// <remarks>
        /// Get do cadastro, os dados retornados serão: 
        ///   {
        ///     "codEpi": 0,
        ///     "nomeEpi": "string",
        ///     "descricao": "string"
        ///   }
        /// </remarks>
        // GET: api/Epi
        [HttpGet]
        [Authorize("Admin")]

        public async Task<ActionResult<IEnumerable<Epi>>> GetEpis()
        {
            if (_context.Epis == null)
            {
                return NotFound();
            }
            return await _context.Epis.ToListAsync();
        }

        // GET: api/Epi/5

        /// <summary>
        /// Retorna um cadastro de um EPI específico
        /// </summary>
        /// <remarks>
        /// GetId do cadastro, os dados do EPI desejado retornado será: 
        ///   {
        ///     "codEpi": 0,
        ///     "nomeEpi": "string",
        ///     "descricao": "string"
        ///   }
        /// </remarks>
        [HttpGet("{id}")]
        [Authorize("Admin")]
        public async Task<ActionResult<Epi>> GetEpi(int id)
        {
            if (_context.Epis == null)
            {
                return NotFound();
            }
            var epi = await _context.Epis.FindAsync(id);

            if (epi == null)
            {
                return NotFound();
            }

            return epi;
        }

        // PUT: api/Epi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754

        /// <summary>
        /// Altera os dados do EPI desejado
        /// </summary>
        /// <remarks>
        /// Put do cadastro, você deve informar os valores conforme no exemplo abaxo: 
        ///   {
        ///     "codEpi": 0,
        ///     "nomeEpi": "string",
        ///     "descricao": "string"
        ///   }
        /// </remarks>
        [HttpPut("{id}")]
        [Authorize("Admin")]
        public async Task<IActionResult> PutEpi(int id, Epi epi)
        {
            if (id != epi.CodEpi)
            {
                return BadRequest();
            }

            _context.Entry(epi).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EpiExists(id))
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

        // POST: api/Epi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754

        /// <summary>
        /// Posta um novo cadastro de um EPI
        /// </summary>
        /// <remarks>
        /// Post do cadastro, preencha os dados abaixo para postar um novo EPI: 
        ///   {
        ///     "codEpi": 0,
        ///     "nomeEpi": "string",
        ///     "descricao": "string"
        ///   }
        /// </remarks>
        [HttpPost]
        [Authorize("Admin")]
        public async Task<ActionResult<Epi>> PostEpi(Epi epi)
        {
            if (_context.Epis == null)
            {
                return Problem("Entity set 'AppDbContext.Epis'  is null.");
            }
            _context.Epis.Add(epi);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEpi", new { id = epi.CodEpi }, epi);
        }

        /// <summary>
        /// Deleta um cadastro de um EPI desejado
        /// </summary>
        // DELETE: api/Epi/5
        [HttpDelete("{id}")]
        [Authorize("Admin")]
        public async Task<IActionResult> DeleteEpi(int id)
        {
            if (_context.Epis == null)
            {
                return NotFound();
            }
            var epi = await _context.Epis.FindAsync(id);
            if (epi == null)
            {
                return NotFound();
            }

            _context.Epis.Remove(epi);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EpiExists(int id)
        {
            return (_context.Epis?.Any(e => e.CodEpi == id)).GetValueOrDefault();
        }
    }
}

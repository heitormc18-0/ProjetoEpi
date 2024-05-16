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
    public class ColaboradorController : ControllerBase
    {
        private readonly AppDbContext _context;
        /// <summary>
        /// 
        /// </summary>

        public ColaboradorController(AppDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Retorna todos os cadastros dos colaboradores existentes 
        /// </summary>
        ///<remarks>
        ///Get do cadastro, os dados retornados serão: 
        ///{
        ///   "codCol": 0,
        ///   "nomeCol": "string",
        ///   "cpf": 0,
        ///   "ctps": 0,
        ///  "dataAdmissao": "2024-04-25",
        ///   "numTel": 0,
        ///   "email": "string"
        /// }
        /// </remarks>

        // GET: api/Colaborador
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Colaborador>>> GetColaboradors()
        {
            if (_context.Colaboradors == null)
            {
                return NotFound();
            }
            return await _context.Colaboradors.ToListAsync();
        }
        /// <summary>
        /// Retorna um cadastro de um colaborador específico
        /// </summary>
        ///<remarks>
        ///GetId do cadastro, os dados do colaborador desejado retornado será: 
        ///{
        ///   "codCol": 0,
        ///   "nomeCol": "string",
        ///   "cpf": 0,
        ///   "ctps": 0,
        ///  "dataAdmissao": "2024-04-25",
        ///   "numTel": 0,
        ///   "email": "string"
        /// }
        /// </remarks>
        // GET: api/Colaborador/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<Colaborador>> GetColaborador(int id)
        {
            if (_context.Colaboradors == null)
            {
                return NotFound();
            }
            var colaborador = await _context.Colaboradors.FindAsync(id);

            if (colaborador == null)
            {
                return NotFound();
            }

            return colaborador;
        }

        // PUT: api/Colaborador/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754

        /// <summary>
        /// Altera os dados do Colaborador desejado
        /// </summary>
        ///<remarks>
        ///Put do cadastro, você deve informar os valores conforme no exemplo abaxo: 
        ///{
        ///   "codCol": 0,
        ///   "nomeCol": "string",
        ///   "cpf": 0,
        ///   "ctps": 0,
        ///  "dataAdmissao": "2024-04-25",
        ///   "numTel": 0,
        ///   "email": "string"
        /// }
        /// </remarks>

        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize("Admin")]
        public async Task<IActionResult> PutColaborador(int id, Colaborador colaborador)
        {
            if (id != colaborador.CodCol)
            {
                return BadRequest();
            }

            _context.Entry(colaborador).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ColaboradorExists(id))
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

        // POST: api/Colaborador
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754

        /// <summary>
        /// Posta um cadastro de um novo colaborador 
        /// </summary>
        ///<remarks>
        ///Post do cadastro, preencha os dados abaixo para postar um novo colaborador: 
        ///{
        ///   "codCol": 0,
        ///   "nomeCol": "string",
        ///   "cpf": 0,
        ///   "ctps": 0,
        ///  "dataAdmissao": "2024-04-25",
        ///   "numTel": 0,
        ///   "email": "string"
        /// }
        /// </remarks>
        [HttpPost]
        [Authorize("Admin")]
        public async Task<ActionResult<Colaborador>> PostColaborador(Colaborador colaborador)
        {
            if (_context.Colaboradors == null)
            {
                return Problem("Entity set 'AppDbContext.Colaboradors'  is null.");
            }
            _context.Colaboradors.Add(colaborador);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetColaborador", new { id = colaborador.CodCol }, colaborador);
        }

        /// <summary>
        /// Deleta o cadastro do colaborador desejado
        /// </summary>
        // DELETE: api/Colaborador/5
        [HttpDelete("{id}")]
        [Authorize("Admin")]
        public async Task<IActionResult> DeleteColaborador(int id)
        {
            if (_context.Colaboradors == null)
            {
                return NotFound();
            }
            var colaborador = await _context.Colaboradors.FindAsync(id);
            if (colaborador == null)
            {
                return NotFound();
            }

            _context.Colaboradors.Remove(colaborador);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ColaboradorExists(int id)
        {
            return (_context.Colaboradors?.Any(e => e.CodCol == id)).GetValueOrDefault();
        }
    }
}

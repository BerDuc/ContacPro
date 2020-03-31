using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ContacPro_Serveur.DAL;
using ContacPro_Serveur.Models;

namespace ContacPro_Serveur.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntentesController : ControllerBase
    {
        private readonly ContacProDBContext _context;

        public EntentesController(ContacProDBContext context)
        {
            _context = context;
        }

        // GET: api/Ententes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Entente>>> GetEntentes()
        {
            return await _context.Ententes.ToListAsync();
        }

        // GET: api/Ententes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Entente>> GetEntente(int id)
        {
            var entente = await _context.Ententes.FindAsync(id);

            if (entente == null)
            {
                return NotFound();
            }

            return entente;
        }

        // PUT: api/Ententes/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEntente(int id, Entente entente)
        {
            if (id != entente.EntenteID)
            {
                return BadRequest();
            }

            _context.Entry(entente).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EntenteExists(id))
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

        // POST: api/Ententes
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Entente>> PostEntente(Entente entente)
        {
            _context.Ententes.Add(entente);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEntente", new { id = entente.EntenteID }, entente);
        }

        // DELETE: api/Ententes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Entente>> DeleteEntente(int id)
        {
            var entente = await _context.Ententes.FindAsync(id);
            if (entente == null)
            {
                return NotFound();
            }

            _context.Ententes.Remove(entente);
            await _context.SaveChangesAsync();

            return entente;
        }

        private bool EntenteExists(int id)
        {
            return _context.Ententes.Any(e => e.EntenteID == id);
        }
    }
}

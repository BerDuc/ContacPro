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
    public class PrestationsController : ControllerBase
    {
        private readonly ContacProDBContext _context;

        public PrestationsController(ContacProDBContext context)
        {
            _context = context;
        }

        // GET: api/Prestations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Prestation>>> GetPrestations()
        {
            return await _context.Prestations.ToListAsync();
        }

        // GET: api/Prestations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Prestation>> GetPrestation(int id)
        {
            var prestation = await _context.Prestations.FindAsync(id);

            if (prestation == null)
            {
                return NotFound();
            }

            return prestation;
        }

        // PUT: api/Prestations/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPrestation(int id, Prestation prestation)
        {
            if (id != prestation.PrestationID)
            {
                return BadRequest();
            }

            _context.Entry(prestation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PrestationExists(id))
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

        // POST: api/Prestations
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Prestation>> PostPrestation(Prestation prestation)
        {
            _context.Prestations.Add(prestation);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPrestation", new { id = prestation.PrestationID }, prestation);
        }

        // DELETE: api/Prestations/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Prestation>> DeletePrestation(int id)
        {
            var prestation = await _context.Prestations.FindAsync(id);
            if (prestation == null)
            {
                return NotFound();
            }

            _context.Prestations.Remove(prestation);
            await _context.SaveChangesAsync();

            return prestation;
        }

        private bool PrestationExists(int id)
        {
            return _context.Prestations.Any(e => e.PrestationID == id);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ContacPro_Serveur.DAL;
using ContacPro_Serveur.Models;
using ContacPro_Serveur.DataProviders;
using Newtonsoft.Json.Linq;

namespace ContacPro_Serveur.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfessionnelsController : ControllerBase
    {
        private readonly ContacProDBContext _context;

        public ProfessionnelsController(ContacProDBContext context)
        {
            _context = context;
        }

        // GET: api/Professionnels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Professionnel>>> GetProfessionnels()
        {
            return await _context.Professionnels.ToListAsync();
        }

        // GET: api/Professionnels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Professionnel>> GetProfessionnel(int id)
        {
            var professionnel = await _context.Professionnels.FindAsync(id);

            if (professionnel == null)
            {
                return NotFound();
            }

            return professionnel;
        }

        //c'est pas conforme aux conventions RESTful, mais ça marche, alors que HttpClient ne permet pas d'envoyer un body dans un GET :/ 
        [HttpPost("Identification")]
        public async Task<ActionResult<IEnumerable<Professionnel>>> GetProfessionnel(Professionnel proRecherche)
        {

            var professionnel = await (from p in _context.Professionnels
                                       where p.Courriel == proRecherche.Courriel
                                       select p).ToListAsync();


            if (professionnel == null || professionnel.Count < 1)
            {
                return NotFound();
            }

            if (professionnel[0].Mdp != proRecherche.Mdp)
            {
                Professionnel echec = new Professionnel();
                echec.Nom = "mauvaise identification";
                professionnel.Clear();
                professionnel.Add(echec);
            }

            return professionnel;
        }


        // PUT: api/Professionnels/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProfessionnel(int id, Professionnel professionnel)
        {
            if (id != professionnel.UtilisateurID)
            {
                return BadRequest();
            }

            _context.Entry(professionnel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProfessionnelExists(id))
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

        // POST: api/Professionnels
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Professionnel>> PostProfessionnel(Professionnel professionnel)
        {
            _context.Professionnels.Add(professionnel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProfessionnel", new { id = professionnel.UtilisateurID }, professionnel);
        }

        // DELETE: api/Professionnels/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Professionnel>> DeleteProfessionnel(int id)
        {
            var professionnel = await _context.Professionnels.FindAsync(id);
            if (professionnel == null)
            {
                return NotFound();
            }

            _context.Professionnels.Remove(professionnel);
            await _context.SaveChangesAsync();

            return professionnel;
        }

        private bool ProfessionnelExists(int id)
        {
            return _context.Professionnels.Any(e => e.UtilisateurID == id);
        }
    }
}

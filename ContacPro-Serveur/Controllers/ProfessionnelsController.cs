using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ContacPro_Serveur.DAL;
using ContacPro_Serveur.Models;
using Newtonsoft.Json.Linq;
using ContacPro_Serveur.Providers;
using Microsoft.Extensions.Configuration;
using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace ContacPro_Serveur.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfessionnelsController : ControllerBase
    {
        private readonly ContacProDBContext _context;
        private readonly IConfiguration configuration;


        public ProfessionnelsController(ContacProDBContext context, IConfiguration iConfig)
        {
            _context = context;
            configuration = iConfig;

        }

        // GET: api/Professionnels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Professionnel>>> GetProfessionnels()
        {

            return await _context.Professionnels.ToListAsync();

            /* 
            var professionnels =  _context.Professionnels.Include(p => p.Expertises);
            foreach(Professionnel p in professionnels)
            {
               foreach(Expertise e in p.Expertises)
                {
                    
                }
        }

            return await professionnels.ToListAsync();
            */
        }


        /*
         public async Task<ActionResult<IEnumerable<Professionnel>>> GetProfessionnels()
        {
            return await _context.Professionnels.ToListAsync();
        }
             */


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

        [HttpGet("{recherche}/{motscles}")]
        public async Task<ActionResult<IEnumerable<Professionnel>>> GetProfessionnel(string recherche, string motscles)
        {

            List<string> liste_motscles = motscles.Split(";").ToList<string>();

            IQueryable<Professionnel> query;

            switch (recherche)
            {
                case ("Nom"):
                    query = (from p in _context.Professionnels
                             where p.Nom == liste_motscles[0] || p.Prenom == liste_motscles[0]
                             select p);
                    break;
                case ("Spécialité"):
                    query = (from p in _context.Professionnels
                           where p.Specialisation == liste_motscles[0]
                           select p);
                    break;
                default: return NotFound();
            }

            var professionnels = await query.ToListAsync<Professionnel>();

            
            /*            var professionnel = await (from p in _context.Professionnels
                                                   join expertise in _context.ProExps on p.UtilisateurID equals expertise.UtilisateurID
                                                   join exp in _context.Expertises on expertise.ExpertiseID equals exp.ExpertiseID
                                                   where exp.Valeur == motcle
                                                   select p).ToListAsync();
             */
            if (professionnels == null)
            {
                return NotFound();
            }

            return professionnels;
        }


   
       
        [HttpGet("Identification")]
        public async Task<ActionResult<Professionnel>> GetProfessionnel()
        {
            try
            {
                string email = Request.Headers["email"].First();
                string password = Request.Headers["password"].ToString();

                var response = await LoginProvider.GetToken(_context, configuration, email, password);
                if (response.StatusCode == 200) // to be modified
                {
                    return Ok(response); // to be modified not authentifed code
                }
                else if (response.StatusCode == 400)
                {
                    return Unauthorized("UnAuthorized");
                }
                else
                {
                    return NotFound("User not found");
                }

            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                return NotFound("catch clause");
            }
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

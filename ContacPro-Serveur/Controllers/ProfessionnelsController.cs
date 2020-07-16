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
            var professionnels =  _context.Professionnels.Include(p => p.Expertises);
            foreach(Professionnel p in professionnels)
            {
               /* foreach(Expertise e in p.Expertises)
                {
                    
                }*/
            }

            return await professionnels.ToListAsync();
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

        

        //pour chercher un professionnel par son expertise
        [HttpGet("expertise/{motcle}")]
        public async Task<ActionResult<IEnumerable<Professionnel>>> GetProfessionnel(string motcle)
        {

            var professionnel = await (from p in _context.Professionnels
                                       join expertise in _context.ProExps on p.UtilisateurID equals expertise.UtilisateurID
                                       join exp in _context.Expertises on expertise.ExpertiseID equals exp.ExpertiseID
                                       where exp.Valeur == motcle
                                       select p).ToListAsync();
            if (professionnel == null)
            {
                return NotFound();
            }

            return professionnel;
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

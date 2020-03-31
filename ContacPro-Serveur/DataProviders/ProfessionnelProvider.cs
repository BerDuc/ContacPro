using ContacPro_Serveur.DAL;
using ContacPro_Serveur.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContacPro_Serveur.DataProviders
{
    public class ProfessionnelProvider
    {
        public async Task<ActionResult<Professionnel>> ChercherParCourriel(string courriel, ContacProDBContext context)
        {
            Professionnel pro = new Professionnel();
            
            using (context)
            {
                var query = from professionnel in context.Professionnels
                            where professionnel.Courriel == courriel
                            select professionnel;

                pro = query.FirstOrDefault<Professionnel>();
            }
            return pro;
        }

    }
}

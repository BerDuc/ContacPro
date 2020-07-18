using ContacPro_Serveur.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContacPro_Serveur.DAL
{
    public class DBInitializer
    {
        public static void Initialize(ContacProDBContext context)
        {
            context.Database.EnsureCreated();

            if (context.Professionnels.Any())
            {
                return;
            }

            var professionnels = new Professionnel[]
            {
                new Professionnel{Nom="Ducharme", Prenom="Bernard", Specialisation="Historien", Courriel="1234@56.com", Mdp="1234"},
                new Professionnel{Nom="Ducharme", Prenom="Emilie", Specialisation="Analyse", Courriel="2345@78.com", Mdp="1234"},
                new Professionnel{Nom="Perron", Prenom="Mathieu", Specialisation="Historien", Courriel="1234@43.com", Mdp="1234"},
                new Professionnel{Nom="Monette", Prenom="Gabriel", Specialisation="Philosophe", Courriel="4321@45.com", Mdp="1234"},
                new Professionnel{Nom="Blecourt", Prenom="Manon", Specialisation="Sociologue", Courriel="12345@56.com", Mdp="1234"}
            };
            foreach(Professionnel p in professionnels)
            {
                context.Professionnels.Add(p);
            }
            context.SaveChanges();
        }
    }
}

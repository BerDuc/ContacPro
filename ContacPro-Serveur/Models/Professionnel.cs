using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContacPro_Serveur.Models
{
    public class Professionnel: Utilisateur
    {   

        public string AddrCv { get; set; }

        public virtual ICollection<Entente> Ententes { get; set; }
        public virtual ICollection<Prestation> Prestations { get; set; }
        public virtual ICollection<ProExp> Expertises { get; set; }
    }
}

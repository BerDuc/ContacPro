using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContacPro_Serveur.Models
{
    public class ProExp
    {
        public int UtilisateurID { get; set; }
        public Professionnel Professionnel { get; set; }

        public int ExpertiseID { get; set; }
        public Expertise Expertise { get; set; }
    }
}

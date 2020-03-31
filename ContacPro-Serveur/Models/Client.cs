using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContacPro_Serveur.Models
{
    public partial class Client : Utilisateur
    {

        public string Institution { get; set; }
        public virtual ICollection<Entente> Ententes { get; set; }
        public virtual ICollection<Prestation> Prestations { get; set; }
    }
}

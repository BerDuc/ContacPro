using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ContacPro_Serveur.Models
{
    public abstract partial class Utilisateur
    {
        
        [Key]
        public int UtilisateurID { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string AddrPhoto { get; set; }
        public string Mdp { get; set; }
        public string Courriel { get; set; }

        public virtual ICollection<Message> Messages_Envoyes { get; set; }
        public virtual ICollection<Message> Messages_Recus { get; set; }

    }
}

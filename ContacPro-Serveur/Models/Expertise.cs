using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ContacPro_Serveur.Models
{
    public class Expertise
    {
     
        [Key]
        public int ExpertiseID { get; set; }
        public string Valeur { get; set; }
        public virtual ICollection<ProExp> Professionnels { get; set; }

    }
}
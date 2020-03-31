using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ContacPro_Serveur.Models
{
    public class Tag
    {
        [Key]
        public int TagID { get; set; }
        public string Valeur { get; set; }
        public virtual ICollection<Tag_Prestation> Prestations { get; set; }
    }
}
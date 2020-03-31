using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContacPro_Serveur.Models
{
    public class Prestation
    {
        [Key]
        public int PrestationID { get; set; }
        public string Titre { get; set; }
        public string Lieu { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public decimal? Retribution { get; set; }
        public DateTime? Date { get; set; }
        public int Client_Id { get; set; }
        [ForeignKey("Client_Id")]
        public virtual Client Beneficiaire{ get; set; }
        public int Pro_Id { get; set; }

        [ForeignKey("Pro_Id")]
        public virtual Professionnel Prestataire{ get; set; }
        
        public virtual ICollection<Tag_Prestation> Tags{ get; set; }

    }

}
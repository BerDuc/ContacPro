using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContacPro_Serveur.Models
{
    public class Entente
    {
        [Key]
        public int EntenteID { get; set; }
        public int Pro_ID { get; set; }
        [ForeignKey("Pro_ID")]
        public virtual Professionnel Pro { get; set; }
        public int Client_ID { get; set; }
        [ForeignKey("Client_ID")]
        public virtual Client Client { get; set; }
        
        public bool ApprouvePro { get; set; }
        public bool ApprouveClient { get; set; }
        
        public virtual ICollection<Message> Messages { get; set; }
    }
}
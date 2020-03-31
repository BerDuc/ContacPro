using System.ComponentModel.DataAnnotations.Schema;

namespace ContacPro_Serveur.Models
{
    public partial class Message
    {
        public int MessageID { get; set; }
        public int Auteur_Id { get; set; }
        [ForeignKey("Auteur_Id")]
        public virtual Utilisateur Auteur { get; set; }
        public int Destinataire_Id { get; set; }
        [ForeignKey("Destinataire_Id")]
        public virtual Utilisateur Destinataire { get; set; }
        public string Titre { get; set; }
        public string Contenu { get; set; }
        public bool Lu { get; set; }

        public int? Entente_ID { get; set; }
        [ForeignKey("Entente_ID")]
        public virtual Entente Entente_Associee { get; set; }

    }
}
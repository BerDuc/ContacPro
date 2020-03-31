using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContacPro_Serveur.Models
{
    public class Tag_Prestation
    {
        public int PrestationID { get; set; }
        public Prestation Prestation { get; set; }
        public int TagID { get; set; }
        public Tag Tag { get; set; }

    }
}

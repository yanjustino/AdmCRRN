using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdmCRRN.Models
{
    public class PlanoConta
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public string TipoConta { get; set; } // Despesa ou receita
        public string Codigo { get; set; } // 99.99

        public virtual Centro Centro { get; set; }
    }
}
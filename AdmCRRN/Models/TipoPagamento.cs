using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdmCRRN.Models
{
    public class TipoPagamento
    {
        public int Id { get; set; }
        public string Descricao { get; set; }

        public virtual Centro Centro { get; set; }
    }
}
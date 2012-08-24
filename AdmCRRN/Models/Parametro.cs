using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdmCRRN.Models
{
    public class Parametro
    {
        public int id { get; set; }
        public string Descricao { get; set; }
        public string valor { get; set; }
        public virtual Entidade Entidade { get; set; }
    }
}
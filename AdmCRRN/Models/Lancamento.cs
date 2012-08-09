using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AdmCRRN.Models
{
    public class Lancamento
    {
        public int Id { get; set; }

        [Display(Name = "Preço de Compra")]
        public double Valor { get; set; }

        public string Historico { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Data Inválida")]
        public DateTime Data { get; set; }
        public string Status { get; set; }

        public virtual Entidade Entidade { get; set; }
        public virtual PlanoConta Conta { get; set; }
        public virtual TipoPagamento Pagamento { get; set; }
    }
}
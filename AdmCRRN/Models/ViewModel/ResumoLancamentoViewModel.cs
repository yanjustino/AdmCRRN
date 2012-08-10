using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdmCRRN.Models.ViewModel
{
    public class ResumoLancamentoViewModel
    {
        public int QuantidadeMembros { get; set; }
        public int QuantidadeCongregados { get; set; }
        public int QuantidadeCriancas { get; set; }
        public double SaldoAnterior { get; set; }
        public List<ItemResumoViewModel> Receitas { get; set; }
        public List<ItemResumoViewModel> Despesas { get; set; }
        public double TotalReceitas { get; set; }
        public double TotalDespesas { get; set; }
        public double SaldoFinal { get; set; }
    }
}
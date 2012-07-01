using AdmCRRN.Models.Agregados;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace AdmCRRN.Models
{
    public enum TipoEntidade : int
    {
        Pregacao = 0,
        Congregacao = 1,
        Igreja = 2
    }

    [Table("Entidades")]
    public class Entidade : Instituicao
    {
        public int Tipo { get; set; }
        public string CaminhoImagem { get; set; }
        public virtual Centro Centro { get; set; }
        public DateTime DataFundacao { get; set; }
        public DateTime DataEmancipacao { get; set; }
        public virtual Membro Dirigente { get; set; }
        public virtual Membro Secretario { get; set; }
        public virtual Membro Tesoureiro { get; set; }
        public virtual List<Membro> Membros { get; set; }
        public virtual string NomeTipoEntidade { get { return ParseNomeEntidade(); } }

        public SelectList ListaTiposEntidade()
        {
            return new SelectList(new[] { 
                new { Value=((int)TipoEntidade.Pregacao).ToString(), Text="Ponto de Pregação", Selected=true },
                new { Value=((int)TipoEntidade.Congregacao).ToString(), Text="Congregação", Selected=false },
                new { Value=((int)TipoEntidade.Igreja).ToString(), Text="Igreja", Selected=true } },
                "Value", "Text", "Selected");
        }

        private string ParseNomeEntidade()
        {
            var tipo = (TipoEntidade)this.Tipo;
            switch (tipo)
            {
                case TipoEntidade.Pregacao: return "Ponto de Pregação";
                case TipoEntidade.Congregacao: return "Congregação";
                case TipoEntidade.Igreja: return "Igreja";
                default: return "";
            }
        }
    }
}
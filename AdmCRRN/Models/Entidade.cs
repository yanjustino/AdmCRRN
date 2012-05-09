using AdmCRRN.Models.Agregados;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace AdmCRRN.Models
{
    public enum TipoEntidade: int
    {
        Pregacao = 0,
        Congregacao = 1,
        Igreja = 2
    }

    [Table("Entidades")]
    public class Entidade: Instituicao
    {
        public int Tipo { get; set; }

        public virtual Centro Centro { get; set; }
        public virtual List<Membro> Membros { get; set; }

        public virtual TipoEntidade NomeTipoEntidade { get { return (TipoEntidade)this.Tipo; } }
    }
}
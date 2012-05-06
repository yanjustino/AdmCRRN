using AdmCRRN.Models.Agregados;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace AdmCRRN.Models
{
    public enum TipoEntidade: int
    {
        Pregacao,
        Congregacao,
        Igreja
    }

    [Table("Entidades")]
    public class Entidade: Instituicao
    {
        public int Tipo { get; set; }
        public virtual Centro Centro { get; set; }
        public virtual TipoEntidade NomeTipoEntidade { get { return (TipoEntidade)this.Tipo; } }
        public virtual ICollection<Membro> Membros { get; set; }
    }
}
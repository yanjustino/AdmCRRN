using AdmCRRN.Models.Agregados;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace AdmCRRN.Models
{
    [Table("Centros")]
    public class Centro: Instituicao
    {
        public virtual ICollection<Entidade> Entidades { get; set; }
    }
}
using AdmCRRN.Models.Agregados;
using System.ComponentModel.DataAnnotations;
using System;
using System.Linq;
using System.Collections.Generic;

namespace AdmCRRN.Models
{
    [Table("Centros")]
    public class Centro : Instituicao
    {
        public virtual ICollection<Entidade> Entidades { get; set; }


        public bool EntidadePertenceAoCentro(Entidade entidade)
        {
            return this.Entidades.Where(e => e.Id == entidade.Id).FirstOrDefault() != null;
        }
    }
}
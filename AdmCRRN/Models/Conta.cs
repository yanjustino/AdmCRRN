using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Security;
using AdmCRRN.Models.ViewModel;

namespace AdmCRRN.Models
{
    [Table("Contas")]
    public class Conta
    {
        public int Id { get; set; }
        public Guid IdUsuario { get; set; }
        public virtual Instituicao Instituicao { get; set; }
    }
}
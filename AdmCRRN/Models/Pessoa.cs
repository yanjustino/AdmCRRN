using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AdmCRRN.Models.Atributos;
using System.ComponentModel.DataAnnotations;
using AdmCRRN.Models.Agregados;

namespace AdmCRRN.Models
{
    [Table("Pessoas")]
    public  abstract class Pessoa
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }

        [ValidacaoCNPJCPF]
        [Required(ErrorMessage = "Informe o CPF")]
        public string CPF { get; set; }

        
        public virtual Entidade Entidade { get; set; }
        public virtual Endereco Endereco { get; set; }

    }
}
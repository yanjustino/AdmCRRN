using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using AdmCRRN.Models.Agregados;
using AdmCRRN.Models.Atributos;

namespace AdmCRRN.Models
{
    [Table("Instituicoes")]
    public abstract class Instituicao
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe o nome do Centro Administrativos")]
        public string Nome { get; set; }

        //[ValidacaoCNPJCPF]
        //[Required(ErrorMessage = "Informe o CNPJ")]
        public string CNPJ { get; set; }

        public virtual Endereco Endereco { get; set; }
        public virtual ICollection<Conta> Contas { get; set; }
    }
}
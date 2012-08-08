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
        public string Telefone { get; set; }

        [Required(ErrorMessage = "Informe o nome do Centro Administrativos")]
        public string Nome { get; set; }
        public string CNPJ { get; set; }

        public virtual Endereco Endereco { get; set; }
        public virtual ICollection<Conta> Contas { get; set; }

        public bool IsCentro()
        {
            return this.GetType().BaseType == typeof(Centro);
        }

        public bool IsEntidade()
        {
            return this.GetType().BaseType == typeof(Entidade);
        }
    }
}
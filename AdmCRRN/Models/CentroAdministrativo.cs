using AdmCRRN.Models.Agregados;
using System.ComponentModel.DataAnnotations;
using System;

namespace AdmCRRN.Models
{
    [Table("CentrosAdministrativos")]
    public class CentroAdministrativo
    {
        public int Id { get; set; }

        [Required(ErrorMessage="Informe o Cnpj")]
        public string Cnpj { get; set; }

        [Required(ErrorMessage="Informe o nome do Centro Administrativos")]
        public string Nome { get; set; }

        public virtual Endereco Endereco { get; set; }

        public Guid UserId { get; set; }
    }
}
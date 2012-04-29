using AdmCRRN.Models.Agregados;
using System.ComponentModel.DataAnnotations;
using System;

namespace AdmCRRN.Models
{
    public enum TipoEntidade: int
    {
        Pregacao,
        Congregacao,
        Igreja
    }

    [Table("Entidades")]
    public class Entidade
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe o Cnpj")]
        public string Cnpj { get; set; }

        [Required(ErrorMessage = "Informe o nome da Entidade")]
        public string Nome { get; set; }

        public int Tipo { get; set; }
        public virtual CentroAdministrativo CentroAdministrativo { get; set; }
        public virtual Endereco Endereco { get; set; }
        public Guid UserId { get; set; }
    }
}
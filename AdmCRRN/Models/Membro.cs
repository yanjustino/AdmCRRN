using System.ComponentModel.DataAnnotations;
using AdmCRRN.Models.Agregados;

namespace AdmCRRN.Models
{
    [Table("Membros")]
    public class Membro
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe o Cpf")]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "Informe o nome do Membro")]
        public string Nome { get; set; }

        public virtual Endereco Endereco { get; set; }
        public virtual Entidade Entidade { get; set; }
    }
}
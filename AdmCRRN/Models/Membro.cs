using System.ComponentModel.DataAnnotations;
using AdmCRRN.Models.Agregados;

namespace AdmCRRN.Models
{
    [Table("Membros")]
    public class Membro
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe o CPF")]
        public string CPF { get; set; }
        public string Nome { get; set; }

        public virtual Entidade Entidade { get; set; }
        public virtual Endereco Endereco { get; set; }
    }
}
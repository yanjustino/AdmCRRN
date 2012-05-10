using System.ComponentModel.DataAnnotations;
using AdmCRRN.Models.Agregados;
using AdmCRRN.Models.Atributos;

namespace AdmCRRN.Models
{
    public enum TipoMembro : int
    {
        Membro = 0,
        Congregado = 1,
        Criança = 2
    }

    [Table("Membros")]
    public class Membro
    {
        public int Id { get; set; }

        [ValidacaoCNPJCPF]
        [Required(ErrorMessage = "Informe o CPF")]
        public string CPF { get; set; }

        public string Nome { get; set; }

        public virtual Entidade Entidade { get; set; }
        public virtual Endereco Endereco { get; set; }
        public int Tipo { get; set; }
        public virtual TipoMembro NomeTipoMembro { get { return (TipoMembro)this.Tipo; } }
    }
}
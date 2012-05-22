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

    public enum TipoCargo : int
    {
        Dirigente = 0,
        Secretario = 1,
        Tesoureiro = 2
    }


    [Table("Membros")]
    public class Membro 
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }

        [ValidacaoCNPJCPF]
        [Required(ErrorMessage = "Informe o CPF")]
        public string CPF { get; set; }


        public virtual Entidade Entidade { get; set; }
        public virtual Endereco Endereco { get; set; }

        public int Tipo { get; set; }
        public int Cargo { get; set; }
        public virtual TipoMembro NomeTipoMembro { get { return (TipoMembro)this.Tipo; } }
        public virtual TipoCargo NomeCargo { get { return (TipoCargo)this.Cargo; } }
    }
}
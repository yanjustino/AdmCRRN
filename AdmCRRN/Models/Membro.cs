using System.ComponentModel.DataAnnotations;
using AdmCRRN.Models.Agregados;
using AdmCRRN.Models.Atributos;
using System;

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

    public enum TipoEstadoCivil : int
    {
        Solteiro = 0,
        Casado = 1,
        Separado = 2,
        Divorciado = 3,
        Viuvo = 4
    }

    [Table("Membros")]
    public class Membro 
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Celular { get; set; }
        public string Filiacao { get; set; }
        public DateTime? DataNascimento { get; set; }
        public string Rg { get; set; }
        public DateTime? DataBatismo { get; set; }
        public DateTime? DataTornouSeMembro { get; set; }
        public string OficianteBatismo { get; set; }
        //[ValidacaoCNPJCPF]
        //[Required(ErrorMessage = "Informe o CPF")]
        public string CPF { get; set; }

        public int Tipo { get; set; }
        public int Cargo { get; set; }
        public int EstadoCivil { get; set; }
        public virtual TipoMembro NomeTipoMembro { get { return (TipoMembro)this.Tipo; } }
        public virtual TipoCargo NomeCargo { get { return (TipoCargo)this.Cargo; } }
        public virtual TipoEstadoCivil NomeEstadoCivil { get { return (TipoEstadoCivil)this.EstadoCivil; } }

        public virtual Entidade Entidade { get; set; }
        public virtual Endereco Endereco { get; set; }
    }
}
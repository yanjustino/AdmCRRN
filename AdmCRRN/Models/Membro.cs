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
    public class Membro : Pessoa
    {
        public int Tipo { get; set; }
        public virtual TipoMembro NomeTipoMembro { get { return (TipoMembro)this.Tipo; } }
    }
}
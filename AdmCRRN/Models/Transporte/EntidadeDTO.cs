using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace AdmCRRN.Models.Transporte
{
    [NotMapped]
    public class EntidadeDTO: Entidade
    {
        
        public int QuantidadeMembros() 
        {
            return this.Membros.Where(l => ((TipoMembro)l.Tipo == TipoMembro.Membro &&
                                            l.Entidade.Id == this.Id)).Count();

        }

        public int QuantidadeCongregados()
        {
            return this.Membros.Where(l => ((TipoMembro)l.Tipo == TipoMembro.Congregado &&
                                            l.Entidade.Id == this.Id)).Count();

        }

        public int QuantidadeCriancas()
        {
            return this.Membros.Where(l => ((TipoMembro)l.Tipo == TipoMembro.Criança &&
                                            l.Entidade.Id == this.Id)).Count();

        }
    }
}
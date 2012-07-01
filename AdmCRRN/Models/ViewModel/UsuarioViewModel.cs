using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using AdmCRRN.Models.Transporte;

namespace AdmCRRN.Models.ViewModel
{
    public class UsuarioViewModel
    {
        public string TipoInstituicao(Conta conta)
        {
            return conta.Instituicao.GetType().BaseType == typeof(Entidade) ?
                "Entidade" :
                "Centro";
        }
        
        public IEnumerable<Usuario> CriarListaUsuarios(List<Conta> contas)
        {
            foreach (var conta in contas)
                yield return new Usuario(conta);
        }
    }
}
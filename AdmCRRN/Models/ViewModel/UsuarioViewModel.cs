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
            if (conta == null)
                return "centro";

            return conta.Instituicao.GetType().BaseType == typeof(Entidade) ?
                "entidade" :
                "centro";
        }
        
        public IEnumerable<Usuario> CriarListaUsuarios(List<Conta> contas)
        {
            foreach (var conta in contas)
                yield return new Usuario(conta);
        }
    }
}
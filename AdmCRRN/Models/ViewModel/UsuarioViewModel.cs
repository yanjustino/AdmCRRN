using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace AdmCRRN.Models.ViewModel
{
    public class UsuarioViewModel
    {
        public string Nome { get; set; }
        public string Email { get; set; }

        public UsuarioViewModel()
        {

        }

        public UsuarioViewModel(Conta conta)
        {
            var usuario = Membership.GetUser(conta.IdUsuario);
            this.Nome = usuario.UserName;
            this.Email = usuario.Email;
        }

        public IEnumerable<UsuarioViewModel> CriarListaUsuarios(List<Conta> contas)
        {
            foreach (var conta in contas)
                yield return new UsuarioViewModel(conta);
        }
    }
}
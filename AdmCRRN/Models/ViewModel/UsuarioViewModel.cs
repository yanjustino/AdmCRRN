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
        public string Nome { get; set; }
        public string Email { get; set; }
        public string NomeInstituicao { get; set; }
        public string Senha { get; private set; }
        public string TipoInstituicao { get; private set; }
        public int IdInstituicao { get; set; }
        public int IdConta { get; set; }
        public ChangePasswordModel ChangePassword { get; set; }


        public UsuarioViewModel()
        {

        }

        public UsuarioViewModel(Conta conta)
        {
            var usuario = Membership.GetUser(conta.IdUsuario);
            this.Nome = usuario.UserName;
            this.Email = usuario.Email;
            this.NomeInstituicao = conta.Instituicao.Nome;
            this.IdInstituicao = conta.Instituicao.Id;

            this.TipoInstituicao = "Centro";
            if (conta.Instituicao.GetType().BaseType == typeof(Entidade))
                this.TipoInstituicao = "Entidade";

            this.Senha = usuario.ProviderUserKey.ToString();

            this.ChangePassword = new ChangePasswordModel();
        }

        public IEnumerable<UsuarioViewModel> CriarListaUsuarios(List<Conta> contas)
        {
            foreach (var conta in contas)
                yield return new UsuarioViewModel(conta);
        }
    }
}
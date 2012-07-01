using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AdmCRRN.Models.Transporte;
using System.Web.Security;

namespace AdmCRRN.Models
{
    public class Usuario
    {
        public int IdConta { get; set; }
        public int IdInstituicao { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string NomeInstituicao { get; set; }
        public string Senha { get; set; }
        public ChangePasswordModel ChangePassword { get; set; }

        public Usuario(Conta conta)
        {
            var membership = Membership.GetUser(conta.IdUsuario);

            Nome = membership.UserName;
            Email = membership.Email;
            NomeInstituicao = conta.Instituicao.Nome;
            IdInstituicao = conta.Instituicao.Id;
            Senha = membership.ProviderUserKey.ToString();
            ChangePassword = new ChangePasswordModel();
        }

    }
}
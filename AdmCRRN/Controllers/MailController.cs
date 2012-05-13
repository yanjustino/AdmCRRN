using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ActionMailer.Net.Mvc;
using System.Web.Security;
using System.Security.Cryptography;
using System.Text;
using AdmCRRN.Models;
using AdmCRRN.Models.ViewModel;

namespace AdmCRRN.Controllers
{
    public class MailController : MailerBase
    {
        public EmailResult NovoUsuario(UsuarioViewModel usuario)
        {
            To.Add( usuario.Email );
            Subject = "Adm CRRN: Cadastro de Conta do Usuário";
            return Email("NovoUsuario", usuario);
        }
    }
}
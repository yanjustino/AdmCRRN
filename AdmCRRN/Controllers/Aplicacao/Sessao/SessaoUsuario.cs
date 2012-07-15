using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using AdmCRRN.Models;

namespace AdmCRRN.Controllers.Aplicacao.Sessao
{
    public class SessaoUsuario
    {
        public static void Iniciar(Conta conta)
        {
            HttpContext.Current.Session["conta"] = conta;
        }

        public static void Encerrar()
        {
            if (HttpContext.Current.Session["conta"] != null)
                HttpContext.Current.Session.Remove("conta");
        }

        public static Conta Conta()
        {
            if (HttpContext.Current.Session["conta"] != null)
                return (Conta)HttpContext.Current.Session["conta"];
            return null;
        }

        public static bool UsuarioSuperAdmin()
        {
            return Roles.GetRolesForUser(GetUserMemberShip().UserName)
                        .Where(r => r.ToLower().Equals("super")).Count() > 0;
        }

        private static MembershipUser GetUserMemberShip()
        {
            return Membership.GetUser(HttpContext.Current.User.Identity.Name);
        }



    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace AdmCRRN.Models.Sessoes
{
    public class ContaSession
    {
        public static void Adicionar(Conta conta)
        {
            HttpContext.Current.Session["conta"] = conta;
        }

        public static void Encerrar()
        {
            if (ExisteUmaContaNaSessao())
                HttpContext.Current.Session.Remove("conta");
        }

        public static Conta ContaNaSessao()
        {
            if (ExisteUmaContaNaSessao())
                return (Conta)HttpContext.Current.Session["conta"];

            return null;
        }

        private static bool ExisteUmaContaNaSessao()
        {
            return HttpContext.Current.Session["conta"] != null;
        }

        public static bool ContaDoTipoCentro()
        {
            if (!ExisteUmaContaNaSessao()) return false;
            return ContaNaSessao().Instituicao.GetType().BaseType == typeof(Centro);
        }

        public static bool ContaDoTipoEntidade()
        {
            if (!ExisteUmaContaNaSessao()) return false;
            return ContaNaSessao().Instituicao.GetType().BaseType == typeof(Entidade);
        }

        public static bool UsuarioDoPerfilSuper()
        {
            return Roles.GetRolesForUser(UsuarioMemberShip().UserName)
                        .Where(r => r.ToLower().Equals("super")).Count() > 0;
        }

        public static Instituicao InstituicaoDaConta()
        {
            return ContaNaSessao().Instituicao;
        }

        public static MembershipUser UsuarioMemberShip()
        {
            return Membership.GetUser(HttpContext.Current.User.Identity.Name);
        }
    }
}
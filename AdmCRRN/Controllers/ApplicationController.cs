using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AdmCRRN.Controllers.Aplicacao.Sessao;
using AdmCRRN.Models;

namespace AdmCRRN.Controllers
{
    public class ApplicationController : Controller
    {
        public static bool AcessoPermitido(Instituicao instituicao)
        {
            if (SessaoUsuario.UsuarioSuperAdmin())
                return true;
            else
            {
                return (InstituicaoDaSessao().Id == instituicao.Id) ||
                       (instituicao.IsEntidade() && AcessoPermitido((Entidade)instituicao));
            }
        }

        public static bool AcessoPermitido(Entidade entidade)
        {
            if (InstituicaoDaSessao().IsCentro())
            {
                var centro = (Centro)InstituicaoDaSessao();
                return centro.EntidadePertenceAoCentro(entidade);
            }

            if (InstituicaoDaSessao().IsEntidade())
            {
                var id = ((Entidade)InstituicaoDaSessao()).Id;
                return entidade.Id == id;
            }

            return false;
        }

        private static Instituicao InstituicaoDaSessao()
        {
            return SessaoUsuario.Conta().Instituicao;
        }
    }
}

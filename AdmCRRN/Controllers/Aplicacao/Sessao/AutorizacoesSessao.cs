using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AdmCRRN.Models;

namespace AdmCRRN.Controllers.Aplicacao.Sessao
{
    public class AutorizacoesSessao
    {
        public static bool InstituicaoAutorizada(Instituicao instituicao)
        {
            var instituicao_sessao = SessaoUsuario.Conta().Instituicao;

            return (instituicao_sessao.Id == instituicao.Id) ||
                   (instituicao.IsEntidade() && EntidadeAutorizada((Entidade)instituicao));
        }

        public static bool EntidadeAutorizada(Entidade entidade)
        {
            var instituicao_sessao = SessaoUsuario.Conta().Instituicao;

            if (instituicao_sessao.IsCentro())
            {
                var centro = (Centro)instituicao_sessao;
                return centro.EntidadePertenceAoCentro(entidade);
            }
            return false;
        }


        public static bool MembroAutorizado(Membro membro)
        {
            var instituicao_sessao = SessaoUsuario.Conta().Instituicao;

            if (instituicao_sessao.IsCentro())
            {
                var centro = (Centro)instituicao_sessao;
                var entidades = centro.Entidades.Where(e => e.Membros.Where(m => m.Id == membro.Id).Count() > 0).FirstOrDefault();
                return entidades != null;
            }

            if (instituicao_sessao.IsEntidade())
            {
                var centro = (Entidade)instituicao_sessao;
                var membros = centro.Membros.Where(m => m.Id == membro.Id).FirstOrDefault();
                return membros != null;
            }           
            
            return false;
        }

    }
}
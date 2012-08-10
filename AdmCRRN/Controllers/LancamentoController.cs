using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AdmCRRN.Models;
using AdmCRRN.Models.Context;
using AdmCRRN.Models.ViewModel;
using AdmCRRN.Controllers.Aplicacao.Sessao;
using AdmCRRN.Models.Transporte;

namespace AdmCRRN.Controllers
{
    public class LancamentoController : Controller
    {
        DataContext contexto = new DataContext();

        public ActionResult Index()
        {
            var entidadeId = SessaoUsuario.Conta().Instituicao.Id;

            ResumoLancamentoViewModel resumo = new ResumoLancamentoViewModel();
            resumo.SaldoAnterior = 0;
            resumo.Receitas = (from lancamentos in contexto.Lancamentos
                               where lancamentos.Entidade.Id == entidadeId &&
                                     lancamentos.Conta.TipoConta.Equals("Receitas")
                               group lancamentos by lancamentos.Conta into g
                               select new ItemResumoViewModel()
                               {
                                   Descricao = g.Key.Descricao,
                                   Valor = g.Sum(l => l.Valor)
                               }).ToList();
            resumo.TotalReceitas = resumo.Receitas.Sum(l => l.Valor);
            resumo.Despesas = (from lancamentos in contexto.Lancamentos
                               where lancamentos.Entidade.Id == entidadeId &&
                                     lancamentos.Conta.TipoConta.Equals("Despesas")
                               group lancamentos by lancamentos.Conta into g
                               select new ItemResumoViewModel()
                               {
                                   Descricao = g.Key.Descricao,
                                   Valor = g.Sum(l => l.Valor)
                               }).ToList();
            resumo.TotalDespesas = resumo.Despesas.Sum(l => l.Valor);
            resumo.SaldoFinal = resumo.SaldoAnterior + resumo.TotalReceitas - resumo.TotalDespesas;
            
            resumo.QuantidadeCongregados = 0;
            resumo.QuantidadeCriancas = 0;
            resumo.QuantidadeMembros = 0;

            return View(resumo);
        }

        public ActionResult Create()
        {
            ViewBag.pagamentos = contexto.TiposPagamento.ToList();
            ViewBag.contas = contexto.PlanosConta.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult Create(Lancamento model)
        {
            model.Pagamento = contexto.TiposPagamento.Find(model.Pagamento.Id);


            contexto.Lancamentos.Add(model);
            contexto.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}

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
using AdmCRRN.Controllers.Aplicacao.Sessao;

namespace AdmCRRN.Controllers
{
    public class LancamentoController : Controller
    {
        private DataContext contexto = new DataContext();
        private Entidade entidade_sessao;

        public LancamentoController()
        {
            if (SessaoUsuario.Conta().Instituicao.IsEntidade())
                entidade_sessao = (Entidade)SessaoUsuario.Conta().Instituicao;
        }

        public ActionResult Index()
        {
            var entidadeId = SessaoUsuario.Conta().Instituicao.Id;

            ResumoLancamentoViewModel resumo = new ResumoLancamentoViewModel();
            resumo.SaldoAnterior = 0;
            resumo.Receitas = (from lancamentos in contexto.Lancamentos
                               where lancamentos.Entidade.Id == entidadeId &&
                                     lancamentos.PlanoConta.TipoConta.Equals("Receitas")
                               group lancamentos by lancamentos.PlanoConta into grupo
                               select new ItemResumoViewModel()
                               {
                                   Descricao = grupo.Key.Descricao,
                                   Valor = grupo.Sum(l => l.Valor)
                               }).ToList();
            resumo.TotalReceitas = resumo.Receitas.Sum(l => l.Valor);
            resumo.Despesas = (from lancamentos in contexto.Lancamentos
                               where lancamentos.Entidade.Id == entidadeId &&
                                     lancamentos.PlanoConta.TipoConta.Equals("Despesas")
                               group lancamentos by lancamentos.PlanoConta into grupo
                               select new ItemResumoViewModel()
                               {
                                   Descricao = grupo.Key.Descricao,
                                   Valor = grupo.Sum(l => l.Valor)
                               }).ToList();
            resumo.TotalDespesas = resumo.Despesas.Sum(l => l.Valor);
            resumo.SaldoFinal = resumo.SaldoAnterior + resumo.TotalReceitas - resumo.TotalDespesas;
            
            resumo.QuantidadeCongregados = 0;
            resumo.QuantidadeCriancas = 0;
            resumo.QuantidadeMembros = 0;

            return View(resumo);
        }

        [Authorize(Roles = "Usuario")]
        public ActionResult Create()
        {
            GerarViewBags();
            return View();
        }

        [HttpPost]
        public ActionResult Create(Lancamento model)
        {
            model.Entidade = contexto.Entidades.Find(entidade_sessao.Id);
            model.Pagamento = contexto.TiposPagamento.Find(model.Pagamento.Id);
            model.PlanoConta = contexto.PlanosConta.Find(model.PlanoConta.Id);
            model.Status = "A";

            contexto.Lancamentos.Add(model);
            contexto.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Usuario")]
        public ActionResult Edit(int id)
        {
            GerarViewBags();
            var lancamento = contexto.Lancamentos.Where(l => l.Id == id && l.Entidade.Id == entidade_sessao.Id).First();
            return View(lancamento);
        }

        [HttpPost]
        public ActionResult Edit(Lancamento model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Lancamento LancamentoAtual = contexto.Set<Lancamento>().Find(model.Id);
                    contexto.Entry(LancamentoAtual).CurrentValues.SetValues(model);
                    LancamentoAtual.Entidade = contexto.Entidades.Find(entidade_sessao.Id);
                    LancamentoAtual.Pagamento = contexto.TiposPagamento.Find(model.Pagamento.Id);
                    LancamentoAtual.PlanoConta = contexto.PlanosConta.Find(model.PlanoConta.Id);
                    contexto.SaveChanges();

                    return RedirectToAction("Index");
                }
                GerarViewBags();
                return View(model);
            }
            catch
            {
                GerarViewBags();
                return View(model);
            }
        }

        [Authorize(Roles = "Usuario")]
        public ActionResult Delete(int id)
        {
            var lancamento = contexto.Lancamentos.Where(l => l.Id == id && l.Entidade.Id == entidade_sessao.Id).First();
            return View(lancamento);
        }

        [HttpPost]
        public ActionResult Delete(Lancamento model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Lancamento modelAtual = contexto.Lancamentos.Find(model.Id);
                    contexto.Lancamentos.Remove(modelAtual);
                    contexto.SaveChanges();

                    return RedirectToAction("Index");
                }
                GerarViewBags();
                return View(model);
            }
            catch
            {
                GerarViewBags();
                return View(model);
            }
        }

        private void GerarViewBags()
        {
            var id = entidade_sessao.Centro.Id;
            ViewBag.pagamentos = contexto.TiposPagamento.Where(p => p.Centro.Id == id).ToList();
            ViewBag.contas = contexto.PlanosConta.Where(p => p.Centro.Id == id).ToList();
        }

    }
}

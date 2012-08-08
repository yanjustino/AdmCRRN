using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AdmCRRN.Models;
using AdmCRRN.Controllers.Aplicacao.Sessao;
using AdmCRRN.Models.Context;

namespace AdmCRRN.Controllers
{
    [Authorize()]
    public class TipoPagamentoController : Controller
    {
        DataContext contexto = new DataContext();

        public ActionResult Index()
        {
            var id = SessaoUsuario.Conta().Instituicao.Id;
            var tiposPagamento = contexto.TiposPagamento.Where(t => t.Centro.Id == id);
            return View(tiposPagamento);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(TipoPagamento model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int id_entidade = SessaoUsuario.Conta().Instituicao.Id;

                    model.Centro = contexto.Centros.Find(id_entidade);
                    contexto.TiposPagamento.Add(model);
                    contexto.SaveChanges();

                    return RedirectToAction("Index", new { id = id_entidade });
                }
                return View(model);
            }
            catch
            {
                return View(model);
            }
        }

        public ActionResult Edit(int id)
        {
            var tipoPagamento = contexto.TiposPagamento.Find(id);
            return View(tipoPagamento);
        }

        [HttpPost]
        public ActionResult Edit(TipoPagamento model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int id = SessaoUsuario.Conta().Instituicao.Id;

                    contexto.Entry(model).State = System.Data.EntityState.Modified;
                    contexto.SaveChanges();

                    return RedirectToAction("Index", new { id = id });
                }
                return View(model);
            }
            catch
            {
                return View(model);
            }
        }

        public ActionResult Delete(int id)
        {
            var model = contexto.TiposPagamento.Find(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(TipoPagamento model)
        {
            try
            {
                model = contexto.TiposPagamento.Find(model.Id);
                int id = model.Centro.Id;

                contexto.TiposPagamento.Remove(model);
                contexto.SaveChanges();

                return RedirectToAction("Index", new { id = id });
            }
            catch
            {
                return View();
            }
        }
    }
}

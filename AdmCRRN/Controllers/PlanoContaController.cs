using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AdmCRRN.Controllers.Aplicacao.Sessao;
using AdmCRRN.Models.Context;
using AdmCRRN.Models;

namespace AdmCRRN.Controllers
{
    public class PlanoContaController : Controller
    {
        DataContext contexto = new DataContext();

        public ActionResult Index()
        {
            var id = SessaoUsuario.Conta().Instituicao.Id;

            var plano_conta = contexto.PlanosConta.Where(t => t.Centro.Id == id);
            return View(plano_conta);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(PlanoConta model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int id_entidade = SessaoUsuario.Conta().Instituicao.Id;

                    model.Centro = contexto.Centros.Find(id_entidade);
                    contexto.PlanosConta.Add(model);
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
            var plano_conta = contexto.PlanosConta.Find(id);
            return View(plano_conta);
        }

        [HttpPost]
        public ActionResult Edit(PlanoConta model)
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
            var model = contexto.PlanosConta.Find(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(PlanoConta model)
        {
            try
            {
                model = contexto.PlanosConta.Find(model.Id);
                int id = model.Centro.Id;

                contexto.PlanosConta.Remove(model);
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

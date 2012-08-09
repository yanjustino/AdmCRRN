using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AdmCRRN.Models;
using AdmCRRN.Models.Context;

namespace AdmCRRN.Controllers
{
    public class LancamentoController : Controller
    {
       DataContext contexto = new DataContext();

        public ActionResult Index()
        {
           var lancamentos = contexto.Lancamentos.ToList();
           return View(lancamentos);
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

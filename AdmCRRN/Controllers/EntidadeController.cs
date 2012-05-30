using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AdmCRRN.Context;
using AdmCRRN.Models;
using AdmCRRN.Models.Agregados;
using AdmCRRN.Models.Sessoes;
using AdmCRRN.Models.Transporte;
using System;

namespace AdmCRRN.Controllers
{
    [Authorize(Roles = "Admin")]
    public class EntidadeController : Controller
    {
        DataContext contexto = new DataContext();

        public ActionResult Index()
        {
            var centro = (Centro)ContaSession.InstituicaoDaConta();
            var entidades = contexto.Entidades.Where(e => e.Centro.Id == centro.Id).ToList();

            var entidadesDTO = new List<EntidadeDTO>();
            foreach (var item in entidades)
            {
                entidadesDTO.Add(new EntidadeDTO()
                                 {
                                     Centro = item.Centro,
                                     CNPJ = item.CNPJ,
                                     Contas = item.Contas,
                                     Endereco = item.Endereco,
                                     Id = item.Id,
                                     Membros = item.Membros,
                                     Nome = item.Nome,
                                     Tipo = item.Tipo
                                 });

            }

            return View(entidadesDTO);
        }

        public ActionResult Details(int id)
        {
            return View(contexto.Entidades.Find(id));
        }

        public ActionResult Create()
        {
            Entidade model = new Entidade();
            model.Endereco = new Endereco();

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(Entidade model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int idCentro = ContaSession.InstituicaoDaConta().Id;
                    model.Centro = contexto.Centros.Find(idCentro);
                    contexto.Entidades.Add(model);
                    contexto.SaveChanges();

                    model.Dirigente.Entidade = contexto.Entidades.Find(model.Id);
                    model.Secretario.Entidade = contexto.Entidades.Find(model.Id);
                    model.Tesoureiro.Entidade = contexto.Entidades.Find(model.Id);

                    contexto.Entry(model).State = System.Data.EntityState.Modified;
                    contexto.SaveChanges();

                    return RedirectToAction("Index");
                }

                return View(model);
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
        }

        public ActionResult Edit(int id)
        {
            var entidade = contexto.Entidades.Where(e => e.Id == id).FirstOrDefault();
            var membros = contexto.Membros.Where(x => x.Entidade.Id == id);

            ViewBag.Dirigentes = new SelectList(membros, "Id", "Nome", entidade.Dirigente.Id);
            ViewBag.Tesoureiros = new SelectList(membros, "Id", "Nome", entidade.Tesoureiro.Id);
            ViewBag.Secretarios = new SelectList(membros, "Id", "Nome", entidade.Secretario.Id);
     
            return View(entidade);
        }

        [HttpPost]
        public ActionResult Edit(Entidade model)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var entidade = contexto.Entidades.Find(model.Id);
                    var secretario = contexto.Membros.Where(d => d.Id == model.Secretario.Id).First();
                    var dirigente = contexto.Membros.Where(s => s.Id == model.Dirigente.Id).First();
                    var tesoureiro = contexto.Membros.Where(t => t.Id == model.Tesoureiro.Id).First();

                    contexto.Entry(entidade).CurrentValues.SetValues(model);
                    contexto.Entry(entidade.Endereco).CurrentValues.SetValues(model.Endereco);
                    entidade.Secretario = secretario;
                    entidade.Dirigente = dirigente;
                    entidade.Tesoureiro = tesoureiro;

                    contexto.SaveChanges();

                    return RedirectToAction("Index");
                }

                return View(model);
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
        }

        public ActionResult Delete(int id)
        {
            return View(contexto.Entidades.Find(id));
        }

        [HttpPost]
        public ActionResult Delete(Entidade model)
        {
            try
            {
                model = contexto.Entidades.Find(model.Id);

                if (model.Membros.Count > 0)
                {
                    ModelState.AddModelError("", "Não é possível excluir uma entidade com membros cadastrados.");
                    return View(model);
                }

                if (model.Contas.Count > 0)
                {
                    ModelState.AddModelError("", "Não é possível excluir uma entidade com usuários associadas a ela.");
                    return View(model);
                }


                contexto.Entidades.Remove(model);
                contexto.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View(model);
            }
        }
    }
}

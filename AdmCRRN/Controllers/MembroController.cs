﻿using System.Web.Mvc;
using System.Linq;
using AdmCRRN.Context;
using AdmCRRN.Models;
using AdmCRRN.Models.Agregados;
using AdmCRRN.Models.Sessoes;

namespace AdmCRRN.Controllers
{
    [Authorize(Roles = "Admin, Usuario")]
    public class MembroController : Controller
    {
        DataContext contexto = new DataContext();

        public ActionResult Index(int id)
        {
            var membros = contexto.Membros.Where(e => e.Entidade.Id == id);
            return View(membros);
        }

        [HttpPost]
        public ActionResult Pesquisar(int id)
        {
            var text = Request.Form["txtPesquisar"].ToString().Trim();
            var membros = contexto.Membros.Where(e => e.Entidade.Id == id && e.Nome.Contains(text));

            if (membros.Count() == 0)
                return RedirectToAction("Index", new { id = id });
            else
                return View("Index", membros);
        }

        public ActionResult Details(int id)
        {
            return View(contexto.Membros.Find(id));
        }

        [Authorize(Roles = "Usuario")]
        public ActionResult Create()
        {
            ViewBag.TipoMembro = new SelectList(new[] { new { Value=((int)TipoMembro.Membro).ToString(), Text=TipoMembro.Membro.ToString(), Selected=true },
                                                          new { Value=((int)TipoMembro.Congregado).ToString(), Text=TipoMembro.Congregado.ToString(), Selected=false },
                                                          new { Value=((int)TipoMembro.Criança).ToString(), Text=TipoMembro.Criança.ToString(), Selected=true } },
                                                  "Value", "Text", "Selected");

            int idEntidade = ContaSession.InstituicaoDaConta().Id;

            Membro model = new Membro();
            model.Endereco = new Endereco();
            model.Entidade = contexto.Entidades.Find(idEntidade);
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(Membro model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int idEntidade = ContaSession.InstituicaoDaConta().Id;

                    model.Entidade = contexto.Entidades.Find(idEntidade);
                    contexto.Membros.Add(model);
                    contexto.SaveChanges();

                    return RedirectToAction("Index", new { id = idEntidade });
                }

                ViewBag.TipoMembro = new SelectList(new[] { new { Value=((int)TipoMembro.Membro).ToString(), Text=TipoMembro.Membro.ToString(), Selected=true },
                                                          new { Value=((int)TipoMembro.Congregado).ToString(), Text=TipoMembro.Congregado.ToString(), Selected=false },
                                                          new { Value=((int)TipoMembro.Criança).ToString(), Text=TipoMembro.Criança.ToString(), Selected=true } },
                                                      "Value", "Text", "Selected");


                return View(model);
            }
            catch
            {
                return View(model);
            }
        }

        public ActionResult Edit(int id)
        {
            Membro model = contexto.Membros.Find(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(Membro model)
        {
            try
            {
                int idEntidade = ContaSession.InstituicaoDaConta().Id;

                if (ModelState.IsValid)
                {
                    contexto.Entry(model).State = System.Data.EntityState.Modified;
                    contexto.Entry(model.Endereco).State = System.Data.EntityState.Modified;
                    contexto.SaveChanges();

                    return RedirectToAction("Index", new { id = idEntidade });
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
            return View(contexto.Membros.Find(id));
        }

        [HttpPost]
        public ActionResult Delete(Membro model)
        {
            try
            {
                int idEntidade = model.Id;

                model = contexto.Membros.Find(model.Id);
                contexto.Membros.Remove(model);
                contexto.SaveChanges();

                return RedirectToAction("Index", new { id = idEntidade });
            }
            catch
            {
                return View(model);
            }
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AdmCRRN.Context;
using AdmCRRN.Models;
using AdmCRRN.Models.Agregados;
using AdmCRRN.Models.Sessoes;
using AdmCRRN.Models.Transporte;

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

                    return RedirectToAction("Index");
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
            return View(contexto.Entidades.Find(id));
        }

        [HttpPost]
        public ActionResult Edit(Entidade model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    contexto.Entry(model).State = System.Data.EntityState.Modified;
                    contexto.Entry(model.Endereco).State = System.Data.EntityState.Modified;
                    contexto.SaveChanges();

                    return RedirectToAction("Index");
                }

                return View();
            }
            catch
            {
                return View();
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

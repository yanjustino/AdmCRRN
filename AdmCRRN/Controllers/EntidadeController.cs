using System.Web.Mvc;
using AdmCRRN.Context;
using AdmCRRN.Models;
using AdmCRRN.Models.Agregados;

namespace AdmCRRN.Controllers
{
    [Authorize(Roles="Admin")]
    public class EntidadeController : Controller
    {
        DataContext contexto = new DataContext();

        public ActionResult Index()
        {
            return View(contexto.Entidades);
        }

        public ActionResult Details(int id)
        {
            return View(contexto.Entidades.Find(id));
        }

        public ActionResult Create()
        {
            Entidade model = new Entidade();
            model.Endereco = new Endereco();
            //model.CentroAdministrativo = null;
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(Entidade model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    contexto.Entidades.Add(model);
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
                contexto.Entidades.Remove(model);
                contexto.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}

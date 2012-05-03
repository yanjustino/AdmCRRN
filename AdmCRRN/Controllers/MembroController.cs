using System.Web.Mvc;
using AdmCRRN.Context;
using AdmCRRN.Models;
using AdmCRRN.Models.Agregados;

namespace AdmCRRN.Controllers
{
    [Authorize(Roles="Usuario")]
    public class MembroController : Controller
    {
        DataContext contexto = new DataContext();

        public ActionResult Index()
        {
            return View(contexto.Membros);
        }

        public ActionResult Details(int id)
        {
            return View(contexto.Membros.Find(id));
        }

        public ActionResult Create()
        {
            Membro model = new Membro();
            model.Endereco = new Endereco();
            //model.Entidade = null;
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(Membro model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    contexto.Membros.Add(model);
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
            Membro model = contexto.Membros.Find(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(Membro model)
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
            return View(contexto.Membros.Find(id));
        }

        [HttpPost]
        public ActionResult Delete(Membro model)
        {
            try
            {
                model = contexto.Membros.Find(model.Id);
                contexto.Membros.Remove(model);
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

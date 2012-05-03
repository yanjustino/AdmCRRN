using System.Web.Mvc;
using AdmCRRN.Context;
using AdmCRRN.Models;
using AdmCRRN.Models.Agregados;

namespace AdmCRRN.Controllers
{
    [Authorize(Roles="Super")]
    public class CentroAdministrativoController : Controller
    {
        DataContext contexto = new DataContext();
        
        public ActionResult Index()
        {
            return View(contexto.CentrosAdministrativos);
        }

        public ActionResult Details(int id)
        {
            return View(contexto.CentrosAdministrativos.Find(id));
        }

        public ActionResult Create()
        {
            CentroAdministrativo model = new CentroAdministrativo();
            model.Endereco = new Endereco();
            return View(model);
        } 

        [HttpPost]
        public ActionResult Create(CentroAdministrativo model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    contexto.CentrosAdministrativos.Add(model);
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
            return View(contexto.CentrosAdministrativos.Find(id));
        }

        [HttpPost]
        public ActionResult Edit(CentroAdministrativo model)
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
            return View(contexto.CentrosAdministrativos.Find(id));
        }

        [HttpPost]
        public ActionResult Delete(CentroAdministrativo model)
        {
            try
            {
                model = contexto.CentrosAdministrativos.Find(model.Id);
                contexto.CentrosAdministrativos.Remove(model);
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

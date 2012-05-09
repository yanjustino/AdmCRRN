using System.Web.Mvc;
using AdmCRRN.Context;
using AdmCRRN.Models;
using AdmCRRN.Models.Agregados;
using System.Collections.Generic;
using System.Collections;

namespace AdmCRRN.Controllers
{
    [Authorize(Roles="Super")]
    public class CentroController : Controller
    {
        DataContext contexto = new DataContext();
        
        public ActionResult Index()
        {
            return View(contexto.Centros);
        }

        public ActionResult Details(int id)
        {
            return View(contexto.Centros.Find(id));
        }

        public ActionResult Create()
        {
            Centro model = new Centro();
            model.Endereco = new Endereco();
            return View(model);
        } 

        [HttpPost]
        public ActionResult Create(Centro model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    contexto.Centros.Add(model);
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
            return View(contexto.Centros.Find(id));
        }

        [HttpPost]
        public ActionResult Edit(Centro model)
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
            return View(contexto.Centros.Find(id));
        }

        [HttpPost]
        public ActionResult Delete(Centro model)
        {
            try
            {
                model = contexto.Centros.Find(model.Id);
                contexto.Centros.Remove(model);
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

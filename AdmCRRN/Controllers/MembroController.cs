using System.Web.Mvc;
using System.Linq;
using AdmCRRN.Context;
using AdmCRRN.Models;
using AdmCRRN.Models.Agregados;
using AdmCRRN.Models.Sessoes;

namespace AdmCRRN.Controllers
{
    [Authorize(Roles="Admin, Usuario")]
    public class MembroController : Controller
    {
        DataContext contexto = new DataContext();

        public ActionResult Index(int id)
        {
            var entidades = contexto.Membros.Where(e => e.Entidade.Id == id );
            return View(entidades);
        }

        public ActionResult Details(int id)
        {
            return View(contexto.Membros.Find(id));
        }

        public ActionResult Create()
        {
            ViewBag.TipoMembro = new SelectList( new [] { new { Value=((int)TipoMembro.Membro).ToString(), Text=TipoMembro.Membro.ToString(), Selected=true },
                                                          new { Value=((int)TipoMembro.Congregado).ToString(), Text=TipoMembro.Congregado.ToString(), Selected=false },
                                                          new { Value=((int)TipoMembro.Criança).ToString(), Text=TipoMembro.Criança.ToString(), Selected=true } }, 
                                                  "Value", "Text", "Selected" );

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
                    int idEntidade = ContaSession.InstituicaoDaConta().Id;
                    model.Entidade = contexto.Entidades.Find(idEntidade);
                    contexto.Membros.Add(model);
                    contexto.SaveChanges();

                    return RedirectToAction("Index");
                }

                return View(model);
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

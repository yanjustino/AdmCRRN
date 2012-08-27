using System.Web.Mvc;
using System.Linq;
using AdmCRRN.Models;
using AdmCRRN.Models.Agregados;
using AdmCRRN.Controllers.Aplicacao.Sessao;
using AdmCRRN.Models.Context;

namespace AdmCRRN.Controllers
{
    [Authorize(Roles = "Admin, Usuario")]
    public class MembroController : ApplicationController
    {
        DataContext contexto = new DataContext();

        public ActionResult Index(int id)
        {
            var entidade = contexto.Entidades.Find(id);
            if (!AcessoPermitido(entidade))
                return RedirectToAction("Index", "Home");

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
            var membro = contexto.Membros.Find(id);
            if(!UsuarioSessaoPodeManipularMembro(membro))
                return RedirectToAction("Index", "Home");

            return View(membro);
        }

        [Authorize(Roles = "Usuario")]
        public ActionResult Create()
        {
            ViewBag.TipoMembro = new SelectList(new[] { new { Value=((int)TipoMembro.Membro).ToString(), Text=TipoMembro.Membro.ToString(), Selected=true },
                                                        new { Value=((int)TipoMembro.Congregado).ToString(), Text=TipoMembro.Congregado.ToString(), Selected=false },
                                                        new { Value=((int)TipoMembro.Criança).ToString(), Text=TipoMembro.Criança.ToString(), Selected=false } },
                                                 "Value", "Text", "Selected");

            ViewBag.EstadoCivil = new SelectList(new[] { new { Value=((int)TipoEstadoCivil.Solteiro).ToString(), Text=TipoEstadoCivil.Solteiro.ToString(), Selected=true },
                                                         new { Value=((int)TipoEstadoCivil.Casado).ToString(), Text=TipoEstadoCivil.Casado.ToString(), Selected=false },
                                                         new { Value=((int)TipoEstadoCivil.Separado).ToString(), Text=TipoEstadoCivil.Separado.ToString(), Selected=false },
                                                         new { Value=((int)TipoEstadoCivil.Divorciado).ToString(), Text=TipoEstadoCivil.Divorciado.ToString(), Selected=false },
                                                         new { Value=((int)TipoEstadoCivil.Viuvo).ToString(), Text=TipoEstadoCivil.Viuvo.ToString(), Selected=false } },
                                                 "Value", "Text", "Selected");

            int id_entidade = SessaoUsuario.Conta().Instituicao.Id;

            Membro model = new Membro();
            model.Endereco = new Endereco();
            model.Entidade = contexto.Entidades.Find(id_entidade);
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(Membro model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int idEntidade = SessaoUsuario.Conta().Instituicao.Id;

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
            var membro = contexto.Membros.Find(id);
            if (!UsuarioSessaoPodeManipularMembro(membro))
                return RedirectToAction("Index", "Home");


            ViewBag.TipoMembro = new SelectList(new[] { new { Value=((int)TipoMembro.Membro).ToString(), Text=TipoMembro.Membro.ToString() },
                                                        new { Value=((int)TipoMembro.Congregado).ToString(), Text=TipoMembro.Congregado.ToString() },
                                                        new { Value=((int)TipoMembro.Criança).ToString(), Text=TipoMembro.Criança.ToString() } },
                                                 "Value", "Text", membro.Tipo);

            ViewBag.EstadoCivil = new SelectList(new[] { new { Value=((int)TipoEstadoCivil.Solteiro), Text=TipoEstadoCivil.Solteiro.ToString(),  },
                                                         new { Value=((int)TipoEstadoCivil.Casado), Text=TipoEstadoCivil.Casado.ToString() },
                                                         new { Value=((int)TipoEstadoCivil.Separado), Text=TipoEstadoCivil.Separado.ToString() },
                                                         new { Value=((int)TipoEstadoCivil.Divorciado), Text=TipoEstadoCivil.Divorciado.ToString() },
                                                         new { Value=((int)TipoEstadoCivil.Viuvo), Text=TipoEstadoCivil.Viuvo.ToString() } },
                                                 "Value", "Text", membro.EstadoCivil);
            return View(membro);
        }

        [HttpPost]
        public ActionResult Edit(Membro model)
        {
            try
            {
                int idEntidade = SessaoUsuario.Conta().Instituicao.Id;

                if (ModelState.IsValid)
                {
                    var membro_atual = contexto.Membros.Find(model.Id);
                    contexto.Entry(membro_atual).CurrentValues.SetValues(model);
                    contexto.Entry(membro_atual).State = System.Data.EntityState.Modified;
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
            var membro = contexto.Membros.Find(id);
            if (!UsuarioSessaoPodeManipularMembro(membro))
                return RedirectToAction("Index", "Home");

            return View(membro);
        }

        [HttpPost]
        public ActionResult Delete(Membro model)
        {
            try
            {
                model = contexto.Membros.Find(model.Id);
                int idEntidade = model.Entidade.Id;

                contexto.Membros.Remove(model);
                contexto.SaveChanges();

                return RedirectToAction("Index", new { id = idEntidade });
            }
            catch
            {
                return View(model);
            }
        }

        private bool UsuarioSessaoPodeManipularMembro(Membro membro)
        {
            var instituicao_sessao = SessaoUsuario.Conta().Instituicao;

            if (instituicao_sessao.IsCentro())
            {
                var centro = (Centro)instituicao_sessao;
                var entidades = centro.Entidades.Where(e => e.Membros.Where(m => m.Id == membro.Id).Count() > 0).FirstOrDefault();
                return entidades != null;
            }

            if (instituicao_sessao.IsEntidade())
            {
                var entidade = (Entidade)instituicao_sessao;
                var membros = contexto.Membros.Where(m => m.Id == membro.Id && m.Entidade.Id == entidade.Id).ToList().FirstOrDefault();
                return membros != null;
            }

            return false;
        }

    }
}

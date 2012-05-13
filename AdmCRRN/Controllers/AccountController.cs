using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using AdmCRRN.Models.Transporte;
using AdmCRRN.Models;
using AdmCRRN.Context;
using AdmCRRN.Models.Sessoes;
using AdmCRRN.Models.ViewModel;

namespace AdmCRRN.Controllers
{
    public class AccountController : Controller
    {
        DataContext contexto = new DataContext();


        public ActionResult Index()
        {
            var entidade = ContaSession.ContaNaSessao();

            List<Conta> contas = null;
            if (entidade == null)
                contas = contexto.Contas.ToList();
            else
                contas = contexto.Contas.Where(e => e.Instituicao.Id == entidade.Instituicao.Id).ToList();

            var viewModel = new UsuarioViewModel();
            var usuarios = viewModel.CriarListaUsuarios(contas).ToList();
            return View(usuarios);
        }

        public ActionResult LogOn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(model.UserName, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                    var chave = (Guid)Membership.GetUser(model.UserName).ProviderUserKey;
                    var conta = contexto.Contas.Where(c => c.IdUsuario == chave).FirstOrDefault();
                    ContaSession.Adicionar(conta);

                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            ContaSession.Encerrar();

            return RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = "Super, Admin")]
        public ActionResult Register(int id)
        {
            RegisterModel model = new RegisterModel()
            {
                IdInstituicao = id,
                AccountType = AccountsType.Usuario
            };

            return View(model);
        }

        [Authorize(Roles = "Super")]
        public ActionResult RegisterSuper()
        {

            RegisterModel model = new RegisterModel();
            model.AccountType = AccountsType.Super;
            return View("Register", model);
        }

        [Authorize(Roles = "Super, Admin")]
        public ActionResult RegisterAdmin()
        {
            RegisterModel model = new RegisterModel()
            {
                IdInstituicao = ContaSession.InstituicaoDaConta().Id,
                AccountType = AccountsType.Admin
            };

            return View("Register", model);
        }

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                MembershipCreateStatus createStatus;
                Membership.CreateUser(model.UserName, model.Password, model.Email, null, null, true, null, out createStatus);

                if (createStatus != MembershipCreateStatus.Success)
                    ModelState.AddModelError("", ErrorCodeToString(createStatus));
                else
                {
                    if (model.AccountType == AccountsType.Super)
                        Roles.AddUserToRole(model.UserName, RegisterModel.SUPER);
                    else if (model.AccountType == AccountsType.Admin)
                        Roles.AddUserToRole(model.UserName, RegisterModel.ADMIN);
                    else
                        Roles.AddUserToRole(model.UserName, RegisterModel.USUARIO);

                    Conta conta = new Conta();
                    conta.IdUsuario = (Guid)Membership.GetUser(model.UserName).ProviderUserKey;
                    conta.Instituicao = contexto.Instituicoes.Find(model.IdInstituicao);

                    contexto.Contas.Add(conta);
                    contexto.SaveChanges();

                    var mail = new MailController();
                    mail.NovoUsuario(conta);

                    return RedirectToAction("Index", "Home");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [Authorize]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {

                // ChangePassword will throw an exception rather
                // than return false in certain failure scenarios.
                bool changePasswordSucceeded;
                try
                {
                    MembershipUser currentUser = Membership.GetUser(User.Identity.Name, true /* userIsOnline */);
                    changePasswordSucceeded = currentUser.ChangePassword(model.OldPassword, model.NewPassword);
                }
                catch (Exception)
                {
                    changePasswordSucceeded = false;
                }

                if (changePasswordSucceeded)
                    return RedirectToAction("ChangePasswordSuccess");
                else
                    ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }

        #region Status Codes
        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        #endregion
    }
}

using ASOL.HireThings.Core;
using ASOL.HireThings.Model;
using ASOL.HireThings.Service;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Threading;

namespace ASOL.HireThings.Portal
{
   
    public class AccountController : BaseController<IAccountService>
    {
        IUserService _service = null;
        UserManager<IHireThingsUser, string> _userManager = null;

        private ApplicationSignInManager _signInManager;

        #region Constructor

        private AccountController(UserManager<IHireThingsUser> userManager)
        {
            _userManager = userManager;
            // Create user token provider 
            var provider = new Microsoft.Owin.Security.DataProtection.DpapiDataProtectionProvider("HireThingsPortal");
            _userManager.UserTokenProvider = new Microsoft.AspNet.Identity.Owin.DataProtectorTokenProvider<IHireThingsUser>(provider.Create("EmailConfirmation"))
            {
                //TokenLifespan = TimeSpan.FromMinutes(1)
            };

            _userManager.UserValidator = new UserValidator<IHireThingsUser>(userManager) { AllowOnlyAlphanumericUserNames = false };

            _userManager.PasswordValidator = new PasswordValidator { };
        }

        public AccountController(IUserService service)
            : this(new UserManager<IHireThingsUser>(new CustomUserStore()))
        {
            _service = service;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        #endregion

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", ""));
            return View();
        }

        /// <summary>
        /// Login View: 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(AccountViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Require the user to have a confirmed email before they can log on.
            var user = await _userManager.FindAsync(model.UserName, model.Password);
            if (user != null)
            {
                if (!user.IsEmailConfirmed)
                {
                    string callbackUrl = await SendEmailConfirmationTokenAsync(user.Id, "Confirm your account-Resend");

                    ViewBag.errorMessage = "You must have a confirmed email to log on.";
                    model.UserId = user.Id;
                    model.EmailId = user.EmailId;
                    model.UserName = user.UserName;

                    ModelState.Clear();
                    return View("EmailNotVerified", model);
                }
                await SignInAsync(user, model.RememberMe);

                Session[Constant.EmailId] = user.EmailId;
                //Session[Constant.TimeZoneId] = user.TimeZoneId;
                Session[Constant.UserId] = user.Id;
                Session[Constant.RoleId] = user.RoleId;



                IUserLogActvityModel accModel = new UserLogActvityModel();
                AccountDAL _dal = new AccountDAL();

                accModel.UserId = Convert.ToInt64(Session[Constant.UserId]);
                accModel.LoginType = 1;
                accModel.ActivityForm = "LogIn";
                accModel.ModuleId = 1;
                accModel.SessionId = Session.SessionID;
                string hostName = Dns.GetHostName();
                accModel.IPAddress = Dns.GetHostEntry(hostName).AddressList[1].ToString();
                _dal.LogUserActvity(accModel);

                if (user.RoleId != null && user.RoleId == 70043) {
                    return RedirectToLocal(TempConstants.DefaultPublicPage);
                }
                return RedirectToLocal(returnUrl);
            }
            else
            {
                ViewData["Error"] = true;
                ModelState.AddModelError("", "Invalid username or password.");
                return View(model);
            }

        }

        //
        // GET: /Account/Register
        /*Discuss Authorization in ASOL Temp - 2015-05-18*/
        //[AuthorizeUserControls("urlUserPreference")]
        public async Task<ActionResult> Register(CancellationToken cancellationToken)
        {
            ViewData[Constant.HeaderTitle] = "User";
            ViewData[Constant.FormTitle] = "ADD User";

            IHireThingsUser model = await  _service.IndexAsync(this.HttpContext.ApplicationInstance.Context,GetCanellationToken(cancellationToken));
            return View(model);
        }

        //
        // POST: /Account/Register
        [HttpPost]
        //[AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(HireThingsUser model, long ddlCountry, long ddlLocation, long[] groupListbox, CancellationToken cancellationToken)
        {
            model.CountryId = ddlCountry;
            model.LocationId = ddlLocation;
            ModelState.Remove("CountryId");
            ModelState.Add("CountryId", new ModelState());
            ModelState.SetModelValue("CountryId", new ValueProviderResult(model.CountryId, model.CountryId.ToString(), null));
            ModelState.Remove("LocationId");
            ModelState.Add("LocationId", new ModelState());
            ModelState.SetModelValue("LocationId", new ValueProviderResult(model.LocationId, model.LocationId.ToString(), null));

            model.UserId = Convert.ToInt64(System.Web.HttpContext.Current.User.Identity.GetUserId());
            ViewData[Constant.FormTitle] = "ADD User";

            if (ModelState.IsValid)
            {
                var user = model; ;
                user.Pin = model.Pin;
                user.UserName = model.EmailId;

                var result = await _userManager.CreateAsync(user, model.Pin);                
                if (result.Succeeded && user.Id!="0")
                {
                    //  Comment the following line to prevent log in until the user is confirmed.
                    //  await SignInManager.SignInAsync(user, isPersistent:false, rememberBrowser:false);
                    string callbackUrl = await SendEmailConfirmationTokenAsync(user.Id, "Confirm your account");

                    string mName = "";
                    if (model.MiddleName != null)
                        mName = model.MiddleName.Trim();
                    string Username = string.Format("{0}{1} {2}", model.FirstName.Trim(), (" " + mName).TrimEnd(), model.LastName.Trim());
                    try
                    {
                        long userId = Convert.ToInt64(user.Id);
                        if (_service.sendEmail(this.HttpContext.ApplicationInstance.Context, model.EmailId, callbackUrl, new EmailServerModel() { UserName = Username }, Constant.EmailType.UserSignup, userId, user.CountryId))
                        {
                            ViewData[Constant.QuerySuccess] = HttpContext.Items[Constant.QuerySuccess];
                            ViewData[Constant.CustomSuccessMessage] = "User has been created successfully. An email has been sent to given email address";
                            return RedirectToAction("UserSignUpConfirmation");
                        }
                        else
                        {
                            ViewData[Constant.QuerySuccess] = false;
                            ViewData[Constant.CustomSuccessMessage] = "User has been created successfully. Email cannot be sent.";
                        }
                    }
                    catch (Exception ex)
                    {
                        ViewData[Constant.QuerySuccess] = false;
                        ViewData[Constant.CustomSuccessMessage] = "User has been created successfully. Email cannot be sent.";
                    }




                }
                else
                {
                    ViewData[Constant.QuerySuccess] = false;
                    ViewData[Constant.CustomSuccessMessage] = "Error : Email is already associated with another user.";
                    model.Id = string.Empty;
                    AddErrors(result);
                }
            }
            else
            {
                ViewData[Constant.QuerySuccess] = false;
                ViewData[Constant.CustomSuccessMessage] = "Error : Some validations failed. Please check the error message for details.";

            }
         
            model = (HireThingsUser) await _service.IndexAsync(this.HttpContext.ApplicationInstance.Context, model,GetCanellationToken(cancellationToken));
            
            return View(model);
        }



        //
        // GET: /Account/Register
        /*Discuss Authorization in ASOL Temp - 2015-05-18*/
        //[AuthorizeUserControls("urlUserPreference")]
        [AllowAnonymous]
        public async Task<ActionResult> PublicRegister(CancellationToken cancellationToken)
        {            
            IHireThingsUser model = await _service.IndexAsync(this.HttpContext.ApplicationInstance.Context, GetCanellationToken(cancellationToken));
            return View(model);
        }
        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> PublicRegister(HireThingsUser model, long ddlCountry, long ddlLocation, long[] groupListbox, CancellationToken cancellationToken)
        {
            model.CountryId = ddlCountry;
            model.LocationId = ddlLocation;
            ModelState.Remove("CountryId");
            ModelState.Add("CountryId", new ModelState());
            ModelState.SetModelValue("CountryId", new ValueProviderResult(model.CountryId, model.CountryId.ToString(), null));
            ModelState.Remove("LocationId");
            ModelState.Add("LocationId", new ModelState());
            ModelState.SetModelValue("LocationId", new ValueProviderResult(model.LocationId, model.LocationId.ToString(), null));

            ModelState.Remove("IsActive");
            ModelState.Remove("RoleId");

            if (ModelState.IsValid)
            {
                var user = model; ;
                user.Pin = model.Pin;
                user.UserName = model.EmailId;
               // model.Id = "0";
                var result = await _userManager.CreateAsync(user, model.Pin);
                if (result.Succeeded && user.Id != "0")
                {
                    //temporary code

                    return Redirect("~/Account/Login");


                    //  Comment the following line to prevent log in until the user is confirmed.
                    //  await SignInManager.SignInAsync(user, isPersistent:false, rememberBrowser:false);
                    string callbackUrl = await SendEmailConfirmationTokenAsync(user.Id, "Confirm your account");

                    string mName = "";
                    if (model.MiddleName != null)
                        mName = model.MiddleName.Trim();
                    string Username = string.Format("{0}{1} {2}", model.FirstName.Trim(), (" " + mName).TrimEnd(), model.LastName.Trim());
                    try
                    {
                        long userId = Convert.ToInt64(user.Id);
                        if (_service.sendEmail(this.HttpContext.ApplicationInstance.Context, model.EmailId, callbackUrl, new EmailServerModel() { UserName = Username }, Constant.EmailType.UserSignup, userId, user.CountryId))
                        {
                            ViewData[Constant.QuerySuccess] = HttpContext.Items[Constant.QuerySuccess];
                            ViewData[Constant.CustomSuccessMessage] = "User has been created successfully. An email has been sent to given email address";
                            return RedirectToAction("PublicUserSignUpConfirmation");
                        }
                        else
                        {
                            ViewData[Constant.QuerySuccess] = false;
                            ViewData[Constant.CustomSuccessMessage] = "User has been created successfully. Email cannot be sent.";
                        }
                    }
                    catch (Exception ex)
                    {
                        ViewData[Constant.QuerySuccess] = false;
                        ViewData[Constant.CustomSuccessMessage] = "User has been created successfully. Email cannot be sent.";
                    }




                }
                else
                {
                    ViewData[Constant.QuerySuccess] = false;
                    ViewData[Constant.CustomSuccessMessage] = "Error : Email is already associated with another user.";
                    model.Id = string.Empty;
                    AddErrors(result);
                }
            }
            else
            {
                ViewData[Constant.QuerySuccess] = false;
                ViewData[Constant.CustomSuccessMessage] = "Error : Some validations failed. Please check the error message for details.";

            }

            model = (HireThingsUser)await _service.IndexAsync(this.HttpContext.ApplicationInstance.Context, model, GetCanellationToken(cancellationToken));

            return View(model);
        }


        //
        // POST: /Account/LogOff
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            Session.Clear();
            Session[Sessions.LogoutType] = 1;

            IUserLogActvityModel accModel = new UserLogActvityModel();
            AccountDAL _dal = new AccountDAL();

            accModel.UserId = Convert.ToInt64(Session[Constant.UserId]);
            accModel.LogoutType = 1;
            accModel.ActivityForm = "Logout";
            accModel.ModuleId = 1;
            accModel.SessionId = Session.SessionID;
            string hostName = Dns.GetHostName();
            accModel.IPAddress = Dns.GetHostEntry(hostName).AddressList[1].ToString();
            _dal.LogUserActvity(accModel);


            Session.Abandon();
            Response.Cookies.Clear();
            Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", ""));

            return RedirectToAction(Constant.LoginAction, Constant.LoginController);
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return Redirect(TempConstants.DefaultPage);
            }
        }

        #region Helpers
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private async Task SignInAsync(IHireThingsUser user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            var identity = await _userManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
                if (error.Contains("already taken"))
                    ModelState.AddModelError("EmailId", "Email already exists.");
                if (error.Contains("Invalid token"))
                    ModelState.AddModelError("EmailId", "Provided information is incorrect");
            }
        }
        #endregion

        //
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }

            var provider = new Microsoft.Owin.Security.DataProtection.DpapiDataProtectionProvider("HireThingsPortal");
            _userManager.UserTokenProvider = new Microsoft.AspNet.Identity.Owin.DataProtectorTokenProvider<IHireThingsUser>(provider.Create("EmailConfirmation"))
            {
                //TokenLifespan = TimeSpan.FromMinutes(1)
            };

            var result = await _userManager.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            ViewData[Constant.HeaderTitle] = "Forgot Password";
            if (TempData["Error"] != null) {
                ViewBag.Error = "Error";
                ForgotPasswordViewModel model = new ForgotPasswordViewModel();
                if (TempData["EmailId"] != null)
                {
                    model.EmailId = TempData["EmailId"].ToString();
                    return View(model);
                }

            }
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            IAccountService service = new AccountService();
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.EmailId);
                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordConfirmation");
                }

                if (!(service.VerifySecurityAnswer(model.UserId, model.Answer)))
                {
                    return View("SecurityInfoError");
                }

                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                // Create user token provider 
                var provider = new Microsoft.Owin.Security.DataProtection.DpapiDataProtectionProvider("HireThingsPortal");
                _userManager.UserTokenProvider = new Microsoft.AspNet.Identity.Owin.DataProtectorTokenProvider<IHireThingsUser>(provider.Create("EmailConfirmation"))
                {
                    TokenLifespan = TimeSpan.FromMinutes(1)
                };

                string code = await _userManager.GeneratePasswordResetTokenAsync(user.Id);
                var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);

                await _userManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");

                long userId = Convert.ToInt64(user.Id);
                if (_service.sendEmail(this.HttpContext.ApplicationInstance.Context, model.EmailId, callbackUrl, new EmailServerModel() { UserId = user.UserId, UserName = user.UserName }, Constant.EmailType.ForgotPassword, userId, user.CountryId))
                    return RedirectToAction("PasswordChanged", "Account");

            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            ViewBag.Url = TempData["callbackUrl"];
            return View();
        }

        //
        // GET: /Account/SecurityInfoError
        [AllowAnonymous]
        public ActionResult SecurityInfoError()
        {
            return View();
        }

        //
        // GET: /Account/SecurityInfoError
        [AllowAnonymous]
        public ActionResult PasswordChanged()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            ViewData[Constant.HeaderTitle] = "Reset Password";
            return code == null ? View("Error") : View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByNameAsync(model.EmailId);

            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            user.Pin = model.Password;
            var result = await _userManager.ResetPasswordAsync(user.Id, model.Code, model.Password);

            if (result.Succeeded)
            {
                ChangePasswordViewModel cpModel = new ChangePasswordViewModel();
                cpModel.UserId = Convert.ToInt64(user.Id);
                cpModel.UserName = user.UserName;
                cpModel.EmailId = model.EmailId;
                cpModel.NewPassword = model.Password;

                if (_service.ChangePassword(this.HttpContext.ApplicationInstance.Context, cpModel))
                    return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            else
            {
                ViewData[Constant.QuerySuccess] = false;
            }

            AddErrors(result);
            return View(model);
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            ViewData[Constant.HeaderTitle] = "Reset Password Confirmation";
            return View();
        }

        //
        // GET: /Account/ChangePasswordConfirmation
        [AllowAnonymous]
        public ActionResult ChangePasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ChangePasswordConfirmation
        [AllowAnonymous]
        public ActionResult SecurityInfoConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/EmailNotVerified
        [AllowAnonymous]
        public ActionResult EmailNotVerified()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult PublicEmailNotVerified()
        {
            return View();
        }
        //
        // GET: /Account/UserSignUpConfirmation
        [AllowAnonymous]
        public ActionResult UserSignUpConfirmation()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult PublicUserSignUpConfirmation()
        {
            return View();
        }
        //
        // GET: /Account/EmailResent
        [AllowAnonymous]
        public ActionResult EmailResent()
        {
            return View();
        }

        //
        // GET: /Account/ChangePassword
        //[AllowAnonymous]
        public async Task<ActionResult> ChangePassword(CancellationToken cancellationToken)
        {
            ViewData[Constant.HeaderTitle] = "Change Password";

            if (Session[Constant.ObjectModel] == null)
            {
                MenuDAL dal = new MenuDAL();
                IMenuModel model = await dal.SelectMenuAsync(Convert.ToInt64(User.Identity.GetUserId()), GetCanellationToken(cancellationToken));
                Session[Constant.ObjectModel] = model;
            }

            return View();
        }

        //
        // POST: /Account/ChangePassword
        [HttpPost]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            bool hasPassword = HasPassword();
            ViewBag.HasLocalPassword = hasPassword;
            ViewBag.ReturnUrl = Url.Action("Manage");

            if (hasPassword)
            {
                model.UserId = Convert.ToInt64(User.Identity.GetUserId());
                model.UserName = User.Identity.GetUserName();
                model.EmailId = Convert.ToString(Session[Constant.EmailId]);

                IdentityResult result = await _userManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
                if (result.Succeeded)
                {
                    if (_service.ChangePassword(this.HttpContext.ApplicationInstance.Context, model))
                        return RedirectToAction("ChangePasswordConfirmation", "Account");
                    else
                    {
                        ViewData[Constant.QuerySuccess] = false;
                    }
                }
                else
                {
                    ViewData[Constant.QuerySuccess] = false;
                    ViewData[Constant.CustomSuccessMessage] = "Error: Old Password is Incorrect.";
                }
            }
            else
            {
                // User does not have a password so remove any validation errors caused by a missing OldPassword field
                ModelState state = ModelState["OldPassword"];
                if (state != null)
                {
                    state.Errors.Clear();
                }

                if (ModelState.IsValid)
                {
                    IdentityResult result = await _userManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
                    if (result.Succeeded)
                    {
                        //return RedirectToAction("Manage", new { Message = ManageMessageId.SetPasswordSuccess });
                    }
                    else
                    {
                        AddErrors(result);
                    }
                }
            }

            return View(model);
        }

        private bool HasPassword()
        {
            var user = _userManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PINHash != null;
            }
            return false;
        }

        private async Task<string> SendEmailConfirmationTokenAsync(string userID, string subject)
        {
            string code = await _userManager.GenerateEmailConfirmationTokenAsync(userID);
            var callbackUrl = Url.Action("ConfirmEmail", "Account",
               new { userId = userID, code = code }, protocol: Request.Url.Scheme);
            await _userManager.SendEmailAsync(userID, subject,
               "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

            return callbackUrl;
        }

        /// <summary>
        /// Get User Security Question against user Email id and poulate view
        /// </summary>
        /// <param name="userEmail">User Email Id</param>
        /// <returns>Return forgot password model to Security Question View</returns>
        [HttpPost]
        [AllowAnonymous]
        public ActionResult SecurityQuestion(ForgotPasswordViewModel model)
        {
            Int32 retVal = 0;

            model = _service.IsValidateUser(model, ref retVal);

            if (retVal == 0)
            {
                TempData["Error"] = "Error";
                TempData["EmailId"] = model.EmailId;
                return RedirectToAction("ForgotPassword");
            }

            if (retVal == 1)
            {
                return RedirectToAction("SecurityInformationError");
            }

            ViewBag.Message = "";

            ModelState.Clear();
            return View(model);
        }

        //
        // GET: /Account/ResendEmail
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> ResendEmail(AccountViewModel model)
        {
            string callbackUrl = await SendEmailConfirmationTokenAsync(model.UserId, "Confirm your account");
            if (_service.sendEmail(this.HttpContext.ApplicationInstance.Context, model.EmailId, callbackUrl, new EmailServerModel() { UserName = model.UserName }, Constant.EmailType.UserSignup))
            {
                return Redirect("EmailResent");
            }
            return View();
        }

    }
}
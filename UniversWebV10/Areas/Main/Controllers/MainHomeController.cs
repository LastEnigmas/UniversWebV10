using Core.Convertor;
using Core.DTOs;
using Core.Generator;
using Core.Security;
using Core.Sender;
using Core.Services.UserSer;
using Data.Model;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static Core.Convertor.ViewToString;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Authorization;

namespace UniversWebV10.Areas.Main.Controllers
{
    [Area("Main")]
    public class MainHomeController : Controller
    {
        private readonly IUserService _userService;
        private readonly IViewRenderService _render;
        public MainHomeController( IUserService service , IViewRenderService render)
        {   
            _render = render;
            _userService = service;
        }


        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Hello() => View();

        #region SignUp
        [Route("SignUp")]
        public IActionResult SignUp() => View();

        [Route("SignUp")]
        [HttpPost]
        public IActionResult SignUp(SignUpViewModel signUp)
        {
            if (!ModelState.IsValid)
            {
                return View(signUp);
            }

            if (_userService.IsUsername(FixText.FixTexts(signUp.Username)))
            {
                ModelState.AddModelError("Username", "Is Exist");
                return View(signUp);
            }

            if (_userService.IsEmail(FixText.FixTexts(signUp.Email)))
            {
                ModelState.AddModelError("Email", "Is Exist");
                return View(signUp);
            }

            User user = new User()
            {
                Username = signUp.Username,
                Email = signUp.Email,
                Password = PasswordHashC.EncodePasswordMd5(signUp.Password),
                IsActive = false,
                ActiveCode = ActiveCodeGen.GenerateCode(),
                Picture = "",
                PictureTitle = "",
            };

            _userService.Add(user);

            string Body = _render.RenderToStringAsync("registerView", user);
            EmailSenders.Send(user.Email, "Register", Body);

            return View();
        }

        #endregion
         
        #region Register

        public IActionResult Register(string id )
        {
            ViewBag.IsRegister =  _userService.RegisterUser(id);
            return View();
        }
        #endregion

        #region SignIn

        [Route("SignIn")]
        public IActionResult SignIn() => View();

        [Route("SignIn")]
        [HttpPost]
        public IActionResult SignIn(SignInViewModel signIn)
        {
            if (!ModelState.IsValid)
            {
                return View(signIn);
            }

            User user = _userService.SignInUser(signIn);
            if(user != null)
            {
                if (user.IsActive)
                {
                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(ClaimTypes.Name , user.Username),
                        new Claim(ClaimTypes.Email, user.Email),
                    };
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);

                    var properties = new AuthenticationProperties()
                    {
                        IsPersistent = signIn.RememberMe
                    };

                    HttpContext.SignInAsync(principal, properties);

                    ViewBag.IsSignIn = true;
                    return View();
                }
                else
                {
                    ModelState.AddModelError("UsernaemOrEmail", "Please Active Your Account First");
                }
            }
            else
            {
                ModelState.AddModelError("UsernaemOrEmail", "Username Or Email Or Password Invalid");
                return View(signIn);
            }


            return View();
        }

        #endregion
         
        #region ForgotPasword

        [Route("ForgotPassword")]
        public IActionResult ForgotPassword() => View();

        [Route("ForgotPassword")]
        [HttpPost]
        public IActionResult ForgotPassword(ForgotPasswordViewModel forgot)
        {
            if (!ModelState.IsValid)
            {
                return View(forgot);
            }
            forgot.EmailOrUsername = FixText.FixTexts(forgot.EmailOrUsername);
            User user = _userService.FindUserByEmailOrUsername(forgot);
            if(user != null)
            {

                string Body = _render.RenderToStringAsync("ForgotView", user);
                EmailSenders.Send(user.Email, "Forgot!", Body);
                ViewBag.IsForgot = true;

                return View();
            }
            else
            {
                ModelState.AddModelError("EmailOrUsername", "Email is Not Valid");
                return View(forgot);
            }
        }

        #endregion

        #region ResetPasword

        public IActionResult ResetPassword(string id)
        {
            return View(new ResetPasswordViewModel()
            {
                ActiveCode = id,
            });
        }

        [HttpPost]
        public IActionResult ResetPassword(ResetPasswordViewModel reset)
        {
            if (!ModelState.IsValid)
            {
                return View(reset);
            }

            User user = _userService.GetUserByActiveCode(reset.ActiveCode);
            if( user != null)
            {
                user.ActiveCode = ActiveCodeGen.GenerateCode();
                user.Password = PasswordHashC.EncodePasswordMd5(reset.Password);

                _userService.Update(user);
                return RedirectToAction("SignIn");
            }
            else
            {
                return NotFound();
            }

        }
        #endregion
    }
}

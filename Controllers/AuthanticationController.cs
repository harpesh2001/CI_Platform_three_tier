using CI_Platform_three_tier.DataModels;
using CI_Platform_three_tier.DataModels.DataModels;
using CI_Platform_three_tier.DataModels.ViewModel;
using CI_Platform_three_tier.Models;
using CI_Platform_three_tier.Repository.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
/*using CI_Platform_three_tier.Repository.Interface;
*/


namespace CI_Platform_three_tier.Controllers
{



    public class AuthanticationController : Controller
    {

        private readonly IUserRepository _userRepository;
        private readonly ILogger<AuthanticationController> _logger;
        private readonly EmailSender _emailSender;
        private readonly PlatformDbContext _platformDbContext;
        public AuthanticationController(ILogger<AuthanticationController> logger, IUserRepository userRepository, EmailSender emailSender)
        {
            _logger = logger;
            _userRepository = userRepository;
            _emailSender = emailSender;
            _platformDbContext = new PlatformDbContext();
        }




        [HttpGet]
        [AllowAnonymous]
        [Route("/Authantication/Login", Name = "Login")]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        [Route("", Name = "Default")]
        [Route("/Authentication/Login", Name = "LoginPost")]
        public IActionResult Login(User model)
        {
            /*if (ModelState.IsValid)
            {
               */
            var validity = _userRepository.VerifyUserAsync(model);
            if (validity == 0)
            {
                ViewBag.msg = 0;

            }
            else
            {
                return Redirect("/Home/Index");
            }
            return View(model);
            /* }
             ViewData["ModelState"] = "Model state invalid.";
             return View(model);*/

        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [Route("/Authantication/Register", Name = "CreateUserPost")]
        public async Task<IActionResult> Register(User model)
        {
            /*if(ModelState.IsValid) {*/
            await _userRepository.RegisterUserAsync(model);
            return RedirectToRoute("Login");
            /* }
             else
             {
                 return RedirectToRoute("Login");
             }*/
        }


        /* [HttpGet]
         [Route("/Authantication/ResetPassword", Name = "ResetPassword")]
         public IActionResult ResetPassword()
         {
             return View();
         }


         [HttpPost]
         [Route("/Authantication/ResetPassword", Name = "ResetPasswordParameter")]
         public IActionResult ResetPassword(User model)
         {
             string token = HttpContext.Session.GetString("token");
             var validate = _platformDbContext.PasswordResets.Where(a => a.Token == token).FirstOrDefault();
             if (validate == null)
             {
                 var user1 = _platformDbContext.Users.FirstOrDefault(u => u.Email == validate.Email);
                 user1.Password = model.Password;
                 _platformDbContext.Update(user1);
                 _platformDbContext.SaveChanges();
                 TempData["Error"] = "PassWord changhed";
                 HttpContext.Session.Remove("token_session");
             }
             return View();
         }
 */

        [HttpGet]
        [Route("Authantication/ResetPassword", Name = "ResetPassword")]
        public IActionResult ResetPassword(string Email, string Token)
        {
            PasswordReset rp = new PasswordReset()
            {
                Email = Email,
                Token = Token,
            };
            return View();

        }
        [HttpPost]

        [Route("Authantication/ResetPassword", Name = "ResetPassword")]
        public IActionResult ResetPassword(ResetViewModel model)
        {
            if (ModelState.IsValid)
            {
                var checkemail = _platformDbContext.PasswordResets.Where(u => u.Email == model.Email && u.Token == model.Token).OrderBy(a=>a.Id).LastOrDefault();
                if (checkemail != null)
                {
                    var updatepass = _platformDbContext.Users.Where(u => u.Email == model.Email).FirstOrDefault();

                    if (updatepass != null)
                    {
                        updatepass.Password = model.Password;
                        updatepass.UpdatedAt = DateTime.Now;

                        _platformDbContext.Users.Update(updatepass);
                        _platformDbContext.SaveChanges();
                        return RedirectToAction("login");
                    }
                    else
                    {
                        return View(model);
                    }

                }
                else
                {
                    return View(model);
                }

            }
            return View(model);
        }







        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [Route("/Authantication/ForgotPassword", Name = "SendEmail")]

        public async Task<IActionResult> ForgotPasswordAsync(User user)
        {
            var existance = await _userRepository.CheckUserAsync(user.Email);
            if (existance == 1)
            {
                var token = Guid.NewGuid().ToString();
                _emailSender.SendEmail(user.Email, "Hear is your password reset link...", "https://localhost:7179/Authantication/ResetPassword?Token=" + token + "&Email=" + user.Email);
                await _userRepository.AddToken(user.Email, token);
                HttpContext.Session.SetString("token", token);
            }
            //Redirect 

            return RedirectToRoute("Default");
        }


    }
}

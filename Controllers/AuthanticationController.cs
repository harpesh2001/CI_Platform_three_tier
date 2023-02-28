using CI_Platform_three_tier.DataModels.DataModels;
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

        public AuthanticationController( ILogger<AuthanticationController> logger, IUserRepository userRepository )
        {
            _logger= logger;    
            _userRepository= userRepository;

        }




        [HttpGet]
        [AllowAnonymous]
        [Route("/Authantication/Login", Name = "Login")]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        [Route("", Name ="Default")]
        [Route("/Authentication/Login", Name ="LoginPost")]
        public IActionResult Login(User model)
        {
            /*if (ModelState.IsValid)
            {
               */ var validity = _userRepository.VerifyUserAsync(model);
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
            if(ModelState.IsValid) { 
                await _userRepository.RegisterUserAsync(model);
                return RedirectToRoute("Login");
            }
            else
            {
                return RedirectToRoute("Login");
            }
        }


        [HttpGet]
        public IActionResult ResetPassword() {
            return View();
        }
        [HttpGet]
        public IActionResult ForgotPassword() 
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        
        public IActionResult ForgotPassword(User user)
        {
            var validity = _userRepository.VerifyUserAsync(user);
            if (validity == 0)
            {
                ViewBag.msg = 0;
            }
            else
            {
                var otp = generateOtp();

            }
            return View();
        }

        private string generateOtp()
        {

            Random r = new Random();
            string otp = (r.Next(100000, 999999)).ToString();
            return otp;
        }
    }
}

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
            try {
                await _userRepository.RegisterUserAsync(model);
                return RedirectToRoute("Login");
            }
            catch(Exception ex) {
                //asdfa
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
    }
}

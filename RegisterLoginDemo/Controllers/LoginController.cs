using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using RegisterLoginDemo.Helpers;
using RegisterLoginDemo.ViewModels;

namespace RegisterLoginDemo.Controllers
{
    public class LoginController : Controller
    {
        private IConfiguration _config;
        private CommonHelper _commonHelper;
        private IHttpContextAccessor _contextAccessor;
        public LoginController(IConfiguration configuration, IHttpContextAccessor contextAccessor)
        {
            _config = configuration;
            _commonHelper = new CommonHelper(configuration);
            _contextAccessor = contextAccessor;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Index(LoginViewModel vm) 
        {
            if (string.IsNullOrEmpty(vm.Username) || string.IsNullOrEmpty(vm.Password))
            {
                ViewBag.Error = "Username or password empty!";
                return View();
            }
            else 
            {
                if (SignInMethod(vm.Username, vm.Password)) 
                {
                    return RedirectToAction("Welcome");
                }
            }
            return View();
        }

        public IActionResult Welcome() 
        {
            return View();
        }

        private void EntryIntoSession(string Username)
        {
            HttpContext.Session.SetString("Username", Username);
            
        }

        private bool SignInMethod(string Username, string Password) 
        {
            string query = $"Select * from [UserInfo] where Username = '{Username}' AND Password = '{Password}'";
            var userDetails = _commonHelper.GetUserByUsername(query);
            if (userDetails.Username != null) 
            {
                _contextAccessor.HttpContext.Session.SetString("Username", userDetails.Username);
                return true;
                
            }
            ViewBag.Error = "Username and password wrong";
            return false;
        }
    }
}

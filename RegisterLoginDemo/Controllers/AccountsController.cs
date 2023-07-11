using Microsoft.AspNetCore.Mvc;
using RegisterLoginDemo.Helpers;
using RegisterLoginDemo.ViewModels;

namespace RegisterLoginDemo.Controllers
{
    public class AccountsController : Controller
    {

        private IConfiguration _config;
        CommonHelper _commonHelper;

        public AccountsController(IConfiguration configuration)
        {
            _config = configuration;
            _commonHelper = new CommonHelper(configuration);
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel vm) {

            string userExistQuery = $"Select * from [UserInfo] where Username = '{vm.Username}' OR Email = '{vm.Email}'";
            bool userExists = _commonHelper.UserAlreadyExists(userExistQuery);
            if (userExists)
            {
                ViewBag.Error = "User already exists!";
                return View("Register", "Accounts");
            }
            string query = $"Insert into [UserInfo](Username, FirstName, LastName, Email, Password) values('{vm.Username}', '{vm.FirstName}'," +
                $"'{vm.LastName}', '{vm.Email}', '{vm.Password}')";

            int result = _commonHelper.DMLTransaction(query);
            if (result > 0) {
                EntryIntoSession(vm.Username);
                ViewBag.Success = "Successful registration";
              
            }

            return View();
        }

        private void EntryIntoSession(string Username)
        {
            HttpContext.Session.SetString("Username", Username);
        }
    }
}

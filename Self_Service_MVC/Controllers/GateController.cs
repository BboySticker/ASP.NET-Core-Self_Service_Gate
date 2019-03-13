using System.Threading.Tasks;
using Self_Service_MVC.Services;
using Microsoft.AspNetCore.Mvc;
using Self_Service_MVC.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Session;

namespace Self_Service_MVC.Services
{
    public class GateController : Controller
    {
        private readonly IUserService _userService;
        private readonly PhoneValidator _validator;

        // 构造函数 依赖注入
        public GateController(IUserService userService, PhoneValidator pv)
        {
            _userService = userService;
            _validator = pv;
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Forgot()
        {
            return View();
        }

        public IActionResult ConfirmByEmail(UserInfo user)
        {
            /*查询函数连接AD接口，查找输入的UserEmail是否存在
             if GetUserEmailFunc(uesr.UserEmail)
             {
                return View();
             }
             else
             {
                return RedirectToAction();
             }
             */
            return View();
        }

        public IActionResult ConfirmByPhone(UserInfo user)
        {
            /*
             同上
             */
            return View();
        }

        public IActionResult Navagation()
        {
            return View();
        }

        public IActionResult CheckAccount()
        {
            return RedirectToAction("ResetPage", "Reset");
        }
        
    }
}
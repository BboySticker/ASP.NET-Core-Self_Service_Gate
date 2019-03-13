using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Self_Service_MVC.Models;
using Self_Service_MVC.Services;

namespace Self_Service_MVC.Services
{
    public class ResetController : Controller
    {

        private readonly PhoneValidator _validator;
        PhoneMemoryService sendphone = new PhoneMemoryService();
        private string phone;
        private int usercode;
        private int officalcode;
        private Task<string> returnData;

        // 构造函数 依赖注入
        public ResetController(PhoneValidator pv)
        {
            _validator = pv;
        }

        public IActionResult Reset()
        {
            return View();
        }

        public IActionResult ResetPage(Code code)
        {
            usercode = code.MobileCode; //用户填入的code
            officalcode = code.OfficalCode;
            if (usercode == officalcode)
            {
                return View();
            }
            else
            {
                return RedirectToAction("ConfirmByPhone", "Gate");
            }
        }

        [HttpPost]
        public IActionResult InputPhoneCode(UserInfo user)
        {
            phone = user.UserPhone;
            bool accept = _validator.IsPhone(ref phone);
            if (accept)
            {
                Random rad = new Random();
                //int mobile_code = rad.Next(100000, 1000000);
                int mobile_code = 123456;
                string content = "您的验证码是：" + mobile_code.ToString() + " 。请不要把验证码泄露给其他人。";

                //returnData = sendphone.SendRequest("C68578069", "2c9301f6638e4ab2d7416ed88167bfc6", phone, content);
                //sendphone.SendRequestByTencent();

                ViewData["Phone"] = phone.Substring(0, 3) + "****" + phone.Substring(7, 4);
                ViewData["OfficalCode"] = mobile_code;
                return View();
            }
            else
            {
                return RedirectToAction("ConfirmByPhone", "Gate");
            }
        }

        public IActionResult InputEmailCode(UserInfo user)
        {
            byte[] b = new byte[4];
            new System.Security.Cryptography.RNGCryptoServiceProvider().GetBytes(b);
            Random r = new Random(BitConverter.ToInt32(b, 0));
            string s = null, str = "";
            if (1 == 1) { str += "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ"; }
            for (int i = 0; i < 6; i++)
            {
                s += str.Substring(r.Next(0, str.Length - 1), 1);
            }

            string[] sArray = user.UserEmail.Split('@');
            ViewData["email"] = user.UserEmail.Substring(0, 4) + "****@"+ sArray[1];
            string senderServerIp = "smtp.163.com";             //使用163代理邮箱服务器（也可是使用qq的代理邮箱服务器，但需要与具体邮箱对相应）
            string toMailAddress = user.UserEmail;              //要发送对象的邮箱
            string fromMailAddress = "13718613841@163.com";     //你的邮箱
            string subjectInfo = "验证码";                      //主题
            string bodyInfo = "<p>" + s + "</p>";               //以Html格式发送的邮件
            string mailUsername = "13718613841@163.com";        //登录邮箱的用户名
            string mailPassword = "1a2b3c4d";                   //对应的登录邮箱的第三方密码（你的邮箱不论是163还是qq邮箱，都需要自行开通stmp服务）
            string mailPort = "25";                             //发送邮箱的端口号
                                                                //string attachPath = "E:\\123123.txt; E:\\haha.pdf";

            //创建发送邮箱的对象
            MailMemoryService myEmail = new MailMemoryService(senderServerIp, toMailAddress, fromMailAddress, subjectInfo, bodyInfo, mailUsername, mailPassword, mailPort, false, false);

            //添加附件
            //email.AddAttachments(attachPath);
            if (myEmail.Send())
            {
                return View();
            }
            else
            {
                return Content("<script>alert('邮件发送失败!')</script>");
            }
        }

    }
}
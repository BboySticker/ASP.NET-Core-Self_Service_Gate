using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Text;
using Microsoft.IdentityModel.Protocols;
using System.Collections.Generic;

using qcloudsms_csharp;
using qcloudsms_csharp.json;
using qcloudsms_csharp.httpclient;


namespace Self_Service_MVC.Services
{
    public class PhoneMemoryService: IPhoneService
    {
        private static readonly HttpClient client = new HttpClient();

        public async Task<string> SendRequest(string account, string password, string mobile, string content)
        {
            //腾讯云https://yun.tim.qq.com/v5/tlssmssvr/sendsms?sdkappid=1400193053&random={1}
            var values = new Dictionary<string, string>
            {
                {"account", account },
                {"password", password },
                {"mobile", mobile },
                {"content", content }
            };
            var httpcontent = new FormUrlEncodedContent(values);
            var response = await client.PostAsync("http://106.ihuyi.com/webservice/sms.php?method=Submit", httpcontent);
            var result = await response.Content.ReadAsStringAsync();
            return result;
        }

        public void SendRequestByTencent()
        {
            int appid = 1400193053;
            string appkey = "7735847c74e7d4ce98d309fcddd78743";
            string phonenum = "18510311192";

            SmsSingleSender ssender = new SmsSingleSender(appid, appkey);
            var result = ssender.send(0, "86", phonenum, "【腾讯云】您的验证码是: 666666", "", "");
        }
    }

}

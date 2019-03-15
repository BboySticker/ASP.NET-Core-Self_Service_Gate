using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Threading.Tasks;
using Aliyun.Acs.Sms;
using Aliyun.Acs.Sms.Model.V20160927;
using Aliyun.Acs.Core;
using Aliyun.Acs.Core.Profile;
using Aliyun.Acs.Core.Exceptions;


namespace Self_Service_MVC.Services
{
    public class PhoneMemoryService: IPhoneService
    {
        private static readonly HttpClient client = new HttpClient();

        public async Task<string> SendRequest(string account, string password, string mobile, string content)
        {
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

        public void SendRequestByAli(string code)
        {
            IClientProfile profile = DefaultProfile.GetProfile(
            "<your-region-id>",
            "<your-access-key-id>",
            "<your-access-key-secret>");
            DefaultAcsClient client = new DefaultAcsClient(profile);
            try
            {
                // 构造请求
                SingleSendSmsRequest request = new SingleSendSmsRequest();
                request.SignName = "切尔思";
                request.TemplateCode = "SMS_160570919";
                request.ParamString = code;
                // 发起请求，并得到 Response
                SingleSendSmsResponse response = client.GetAcsResponse(request);
                System.Console.WriteLine(response.HttpResponse.Content);
            }
            catch (ServerException ex)
            {
                System.Console.WriteLine(ex.ToString());
            }
            catch (ClientException ex)
            {
                System.Console.WriteLine(ex.ToString());
            }
        }
    }

}

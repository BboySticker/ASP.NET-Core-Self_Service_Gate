using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Threading.Tasks;
using Aliyun.Acs.Core;
using Aliyun.Acs.Core.Profile;
using Aliyun.Acs.Core.Exceptions;
using Aliyun.Acs.Sms;
using Aliyun.Acs.Sms.Model.V20160927;
using Newtonsoft.Json;

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

        public void SendRequestByAli(string phone, string code)
        {
            IClientProfile profile = DefaultProfile.GetProfile("cn-beijing", "=", "=");
            //DefaultProfile.AddEndpoint("cn-hangzhou", "cn-hangzhou", "Dysmsapi", "dysmsapi.aliyuncs.com");
            DefaultAcsClient client = new DefaultAcsClient(profile);
            try
            {
                // 构造请求
                SingleSendSmsRequest request = new SingleSendSmsRequest();
                request.ResourceOwnerAccount = "Chunfeng.Zhang@1266439463042087.onaliyun.com";
                request.RecNum = phone;
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

        public void SendRequestByAliApi(string mobile, string code)
        {
            var values = new Dictionary<string, string>
            {
                {"PhoneNumbers", mobile },
                {"SignName", "切尔思" },
                {"TemplateCode", "SMS_160570919"  },
                {"AccessKeyId", "=" },
                {"Action", "SendSms" },
                {"TemplateParam", JsonConvert.ToString("code:123456") }
            };
            var httpcontent = new FormUrlEncodedContent(values);
            var response = client.PostAsync("https://dysmsapi.aliyuncs.com/", httpcontent);
            System.Console.WriteLine(response.Status); //WaitingForActivation
            System.Console.WriteLine(response.Result);
            /*
             WaitingForActivation
                StatusCode: 400, ReasonPhrase: 'Bad Request', Version: 1.1, Content: System.Net.Http.HttpConnection+HttpConnectionResponseContent, Headers:
                {
                  Date: Mon, 18 Mar 2019 09:21:25 GMT
                  Connection: close
                  Access-Control-Allow-Origin: *
                  Access-Control-Allow-Methods: POST, GET, OPTIONS
                  Access-Control-Allow-Headers: X-Requested-With, X-Sequence, _aop_secret, _aop_signature
                  Access-Control-Max-Age: 172800
                  Server: Jetty(7.2.2.v20101205)
                  Content-Type: text/xml; charset=UTF-8
                  Content-Length: 353
                }
             */

        }
    }

}

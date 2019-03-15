using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Aliyun.Acs.Core;
using Aliyun.Acs.Core.Profile;
using Aliyun.Acs.Core.Exceptions;
using Aliyun.Acs.Ecs.Model.V20140526;


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

        public void SendRequestByAli()
        {
            IClientProfile profile = DefaultProfile.GetProfile(
            "<your-region-id>",
            "<your-access-key-id>",
            "<your-access-key-secret>");
            DefaultAcsClient client = new DefaultAcsClient(profile);
            try
            {
                // 构造请求
                DescribeInstancesRequest request = new DescribeInstancesRequest();
                request.PageSize = 10;
                // 发起请求，并得到 Response
                DescribeInstancesResponse response = client.GetAcsResponse(request);
                System.Console.WriteLine(response.TotalCount);
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

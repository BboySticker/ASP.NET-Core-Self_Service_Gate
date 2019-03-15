using System.Threading.Tasks;

namespace Self_Service_MVC.Services
{
    public interface IPhoneService
    {
        Task<string> SendRequest(string account, string password, string mobile, string content);
        void SendRequestByAli(string code);
    }
}

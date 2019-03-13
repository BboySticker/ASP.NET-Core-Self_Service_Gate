namespace Self_Service_MVC.Services
{
    public interface IMailService
    {
        void AddAttachments(string attachmentsPath);
        bool Send();

    }
}

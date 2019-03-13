namespace Self_Service_MVC.Models
{
    public class UserInfo
    {
        public int ID { get; set; }
        public string Username { get; set; }
        private string Password { get; set; }
        public string UserPhone { get; set; }
        public string UserEmail { get; set; }

    }
}

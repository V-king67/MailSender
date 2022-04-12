namespace MailSender.lib.Interfaces.Models
{
    public interface IServer
    {
        string Name { get; set; }
        string Address { get; set; }
        int Port { get; set; }
        bool UseSSL { get; set; }
        string Description { get; set; }
        string Login { get; set; }
        string Password { get; set; }

    }
}

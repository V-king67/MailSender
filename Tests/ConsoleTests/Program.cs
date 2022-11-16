using MailSender.lib.Interfaces;
using MailSender.lib.Service;

namespace ConsoleTests
{
    class Program
    {
        static void Main(string[] args)
        {
            IEncryptorService cryptor = new Rfc2898Encryptor();
            var str = "Hello world!";
            const string password = "MailSender!";

            var cryptedStr = cryptor.Encrypt(str, password);
            var decrypredStr = cryptor.Decrypt(cryptedStr, password);
        }
    }
}

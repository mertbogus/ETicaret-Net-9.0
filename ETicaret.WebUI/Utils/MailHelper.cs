using ETicaret.Core.Entities;
using System.Net;
using System.Net.Mail;

namespace ETicaret.WebUI.Utils
{
    public class MailHelper
    {
        public static async Task SendMailAsync (Contact contact)
        {
            SmtpClient smtpClient = new SmtpClient ("mail.siteadi.com",587);
            smtpClient.Credentials = new NetworkCredential("siteadi.com", "mailsifre");
            smtpClient.EnableSsl = false;
            MailMessage mailMessage = new MailMessage ();
            mailMessage.From = new MailAddress("mail.siteadi.com");
            mailMessage.To.Add("bilgi.siteadi.com");
            mailMessage.Subject="Siteden Mesaj Var!";
            mailMessage.Body = $"İsim {contact.Name} - Soyisim {contact.SurName} - Mail {contact.Email} - Telefon {contact.Phone}" +
                $"- Mesaj {contact.Message} ";
            mailMessage.IsBodyHtml = true;
            await smtpClient.SendMailAsync (mailMessage);
            smtpClient.Dispose();
        }
    }
}

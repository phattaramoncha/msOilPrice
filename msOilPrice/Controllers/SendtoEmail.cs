using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;

namespace msOilPrice.Controllers
{
    public class SendtoEmail
    {
        private string userName, password;

        public SendtoEmail()
        {
            GetUserName();
            GetPassword();
        }

        public void GetUserName()
        {
            var appSettings = ConfigurationManager.AppSettings;
            userName = appSettings["userName"];
        }

        public void GetPassword()
        {
            var appSettings = ConfigurationManager.AppSettings;
            password = appSettings["password"];
        }

        public void PostMail(string strBody)
        {
            try
            {
                MailMessage mailMessage = new MailMessage();
                // mailMessage.From = new MailAddress("splsys02@supalai.com", "Supalai IT (getOilPriceAPI)");
                mailMessage.From = new MailAddress(userName, "Supalai IT (getOilPriceAPI)");
                mailMessage.To.Add(new MailAddress("it@supalai.com"));
                mailMessage.Subject = "Err: API get oil price from PTT";
                mailMessage.IsBodyHtml = true;
                mailMessage.Body = strBody;

                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
                System.Net.NetworkCredential networkCredential =
                    // new NetworkCredential("splsys02@supalai.com", "pewcosiaeedagpxf");
                    new NetworkCredential(userName, password);
                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = networkCredential;
                smtpClient.Send(mailMessage);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
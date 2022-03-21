using System;
using System.Net;
using System.Net.Mail;

namespace msOilPrice.Controllers
{
    public class SendtoEmail
    {
        public void postMail(string strBody)
        {
            try
            {
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress("erp@supalai.com", "Supalai IT (getOilPriceAPI)");
                mailMessage.To.Add(new MailAddress("it@supalai.com"));
                mailMessage.Subject = "Err: API get oil price from PTT";
                mailMessage.IsBodyHtml = true;
                mailMessage.Body = strBody;

                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
                System.Net.NetworkCredential networkCredential =
                    new NetworkCredential("system@supalai.com", "SPL@cm#01!");
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
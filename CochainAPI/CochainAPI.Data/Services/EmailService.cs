using CochainAPI.Data.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Net.Mail;

namespace CochainAPI.Data.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;
        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public void SendEmail(string body, string email, string subject)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");//gmailSmtpClient
                SmtpServer.Port = 587; //- 465 ssl gmail -587
                SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;

                SmtpServer.EnableSsl = true;
                SmtpServer.UseDefaultCredentials = false;
                SmtpServer.Credentials = new System.Net.NetworkCredential("cochain2025@gmail.com", Environment.GetEnvironmentVariable("emailinapppassword"));

                mail.Sender = new MailAddress("info@cochain.eu");
                mail.From = new MailAddress("info@cochain.eu");
                try
                {
                    if (!email.Equals("info@cochain.eu"))
                    {
                        mail.To.Add(email);
                    }
                }
                catch
                {

                }

                mail.To.Add("info@cochain.eu");
                mail.Subject = subject;
                mail.IsBodyHtml = true;
                mail.Body = body;

                SmtpServer.Send(mail);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void SendTemporaryPassword(string email, string tempPassword)
        {
            string subject = "La tua password temporanea";
            string body = "<html><body style=\"max-width: 605px;\"><table><tr><td style=\"text-align: center;\">";
            body += "<tr><td><p>Gentile utente,</p></td></tr>";
            body += "<tr><td><p>Di seguito è riportata la tua password temporanea per accedere al nostro sistema:</p></td></tr>";
            body += "<tr><td><h2 style=\"text-align: center;\">" + tempPassword + "</h2></td></tr>";
            body += "<tr><td><br></td></tr><tr><td>Cordiali saluti,</td></tr><tr><td>Il Team di Cochain</td></tr></table></body></html>";

            SendEmail(body, email, subject);
        }

    }
}
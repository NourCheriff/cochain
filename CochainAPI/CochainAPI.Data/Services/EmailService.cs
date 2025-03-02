using CochainAPI.Data.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Net.Mail;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Services;
using MimeKit;
using GmailMessage = Google.Apis.Gmail.v1.Data.Message;
using System.Text;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Util.Store;
using Google.Apis.Auth.AspNetCore3;
using Microsoft.AspNetCore.Mvc;

namespace CochainAPI.Data.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;
        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public void SendEmail(string testo, string email, string oggetto)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");//gmailSmtpClient
                SmtpServer.Port = 587; //- 465 ssl gmail -587
                SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;

                SmtpServer.EnableSsl = true;
                SmtpServer.UseDefaultCredentials = false;
                SmtpServer.Credentials = new System.Net.NetworkCredential("cochain2025@gmail.com", _config["Email:InAppPassword"]);

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
                mail.Subject = oggetto;
                mail.IsBodyHtml = true;
                mail.Body = testo;

                SmtpServer.Send(mail);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void EmailPasswordTemporanea(string email, string tempPassword)
        {
            string oggetto = "La tua password temporanea";
            string testo = "<html><body style=\"max-width: 605px;\"><table><tr><td style=\"text-align: center;\">";
            testo += "<img style=\"width: 50%; height: 66px; display: block; margin-left: auto; margin-right: auto;\" src=\"https://www.cochain.com/logo.png\"></img></td></tr>";
            testo += "<tr><td><p>Gentile utente,</p></td></tr>";
            testo += "<tr><td><p>Di seguito è riportata la tua password temporanea per accedere al nostro sistema:</p></td></tr>";
            testo += "<tr><td><h2 style=\"text-align: center;\">" + tempPassword + "</h2></td></tr>";
            testo += "<tr><td><br></td></tr><tr><td>Cordiali saluti,</td></tr><tr><td>Il Team di Cochain</td></tr></table></body></html>";

            SendEmail(testo, email, oggetto);
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using MVC_CookBook_PSSC.Models.EmailComponents;

namespace MVC_CookBook_PSSC.Models
{
    public class EmailSystemService
    {
        public static EmailSystemService service = new EmailSystemService();
        private EmailSystemService() { }

        //ConnectionString = @"Server=127.0.0.1;Database=angajati;Uid=root;Pwd=";
        private void SendConfirmationEmail(EmailAddress emailAddress, User user)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("cookbook.noreply@gmail.com");
                mail.To.Add(emailAddress.GetEmailAddress.GetText);
                mail.Subject = "CookBook Email Confirmation";
                mail.Body = "Hello "+user.GetName+ " " + user.GetSurename+Environment.NewLine + Environment.NewLine + Environment.NewLine +
                     @"Thank you for your registration at CookBook, to ensure maximum security we would like for you to activate your account using the link below.
Please click the link below to activate your account!" +Environment.NewLine;

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("cookbook.noreply", "Q1werty2");
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);

            }
            catch (Exception ex)
            {
               
            }
        }

    }
}

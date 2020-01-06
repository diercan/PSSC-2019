using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;


namespace EmailSystem
{
    public class EmailSystemService
    {
        public static EmailSystemService service = new EmailSystemService();
        private EmailSystemService() { }

        //ConnectionString = @"Server=127.0.0.1;Database=angajati;Uid=root;Pwd=";
        public static void SendConfirmationEmail(String emailAddress, String username)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("cookbook.noreply@gmail.com");
                mail.To.Add(emailAddress);
                mail.Subject = "CookBook Email Confirmation";
                mail.Body = "Hello " + username  + Environment.NewLine + Environment.NewLine + Environment.NewLine +
                     @"Thank you for your registration at CookBook, We hope you will find our service useful and have a good time finding out known/unknown recipes or even uploading you own!" + Environment.NewLine;

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

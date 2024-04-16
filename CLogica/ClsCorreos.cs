using System;
using System.Net;
using System.Net.Mail;

namespace proyectoFinal.CLogica
{
    public class ClsCorreos
    {
        public void EnviarCorreo(string subject, string body, string toEmail)
        {
            try
            {
                string fromEmail = "proyectofinal.grupo4.2024@gmail.com";
                string fromPassword = "owtgosmnadffkegq";

                MailMessage message = new MailMessage();
                message.From = new MailAddress(fromEmail);
                message.Subject = subject;
                message.To.Add(new MailAddress(toEmail));
                message.Body = body;
                message.IsBodyHtml = false;

                var smtpClient = new SmtpClient("smtp.gmail.com") {
                Port = 587,
                Credentials = new NetworkCredential(fromEmail, fromPassword),
                EnableSsl = true
                };
                smtpClient.Send(message);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al enviar el correo electrónico: " + ex.Message);
            }
        }
    }
}

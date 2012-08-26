using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Mail;
using WebProgramAR.Entidades;
using WebProgramAR.Negocio;

namespace WebProgramAR.MailSender
{
    public static class MailManager
    {

        /// <summary>
        /// Default SMTP Port.
        /// </summary>
        public static int SmtpPort = 6666;

        public static bool Enviar(string to, string subject, string body)
        {
            try
            {
                Mail mail = new Mail();
                mail.FechaCreacion = DateTime.Now;
                mail.From = "noreply@program-ar.com.ar";
                mail.Subject = subject;
                mail.Enviado = false;
                mail.FechaEnvio = null;
                //Set IsBodyHtml to true means you can send HTML email.

                mail.Body = body;
                mail.To = to;
            
                MailNegocio.Alta(mail);
                return true;
            }
            catch
            {
                //Error, could not send the message
                throw;
            }
            return false;

        }

        //public static bool Enviar(string to, string subject, string body)
        //{
        //    SmtpClient smtpClient = new SmtpClient("smtp.apgconsulting.com.ar", SmtpPort);

        //    MailMessage message = new MailMessage();
        //    MailAddress fromAddress = new MailAddress("noreply@program-ar.com.ar");


        //    //smtpClient.Host = "216.59.32.145";
        //    smtpClient.UseDefaultCredentials = true;
        //    smtpClient.Credentials = new NetworkCredential("programar", "Pr0gr4m4r");
        //    smtpClient.EnableSsl = false;

        //    message.From = fromAddress;
        //    message.Subject = subject;
        //    //Set IsBodyHtml to true means you can send HTML email.
        //    message.IsBodyHtml = true;
        //    message.Body = body;
        //    message.To.Add(to);
        //    message.BodyEncoding = Encoding.Unicode;

        //    try
        //    {
        //        smtpClient.Send(message);
        //        return true;
        //    }
        //    catch
        //    {
        //        //Error, could not send the message
        //        throw;
        //    }
        //    return false;
            
        //}

       
    }
}

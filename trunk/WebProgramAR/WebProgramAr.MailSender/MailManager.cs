﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Mail;

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
            SmtpClient smtpClient = new SmtpClient("smtp.apgconsulting.com.ar", SmtpPort);

            MailMessage message = new MailMessage();
            MailAddress fromAddress = new MailAddress("admin@program-ar.com.ar");


            //smtpClient.Host = "216.59.32.145";
            smtpClient.UseDefaultCredentials = true;
            smtpClient.Credentials = new NetworkCredential("programar", "Pr0gr4m4r");
            smtpClient.EnableSsl = false;

            message.From = fromAddress;
            message.Subject = subject;
            //Set IsBodyHtml to true means you can send HTML email.
            message.IsBodyHtml = true;
            message.Body = body;
            message.To.Add(to);
            message.BodyEncoding = Encoding.Unicode;

            try
            {
                smtpClient.Send(message);
                return true;
            }
            catch
            {
                //Error, could not send the message
                throw;
            }
            return false;
            
        }

        //public static bool Enviar(MailMessage mailMessage)
        //{
        //    string domainName = GetDomainName(mailMessage.To[0].Address);
        //    IPAddress[] servers = GetMailExchangeServer(domainName);
        //    foreach (IPAddress server in servers)
        //    {
        //        try
        //        {
        //            SmtpClient client = new SmtpClient(server.ToString(), SmtpPort);
        //            client.Send(mailMessage);
        //            return true;
        //        }
        //        catch
        //        {
        //            continue;
        //        }
        //    }
        //    return false;
        //}

        //public static string GetDomainName(string emailAddress)
        //{
        //    int atIndex = emailAddress.IndexOf('@');
        //    if (atIndex == -1)
        //    {
        //        throw new ArgumentException("Not a valid email address",
        //                                    "emailAddress");
        //    }
        //    if (emailAddress.IndexOf('<') > -1 &&
        //        emailAddress.IndexOf('>') > -1)
        //    {
        //        return emailAddress.Substring(atIndex + 1,
        //               emailAddress.IndexOf('>') - atIndex);
        //    }
        //    else
        //    {
        //        return emailAddress.Substring(atIndex + 1);
        //    }
        //}

        //public static IPAddress[] GetMailExchangeServer(string domainName)
        //{
        //    IPHostEntry hostEntry =
        //      DomainNameUtil.GetIPHostEntryForMailExchange(domainName);
        //    if (hostEntry.AddressList.Length > 0)
        //    {
        //        return hostEntry.AddressList;
        //    }
        //    else if (hostEntry.Aliases.Length > 0)
        //    {
        //        return System.Net.Dns.GetHostAddresses(hostEntry.Aliases[0]);
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}
    }
}

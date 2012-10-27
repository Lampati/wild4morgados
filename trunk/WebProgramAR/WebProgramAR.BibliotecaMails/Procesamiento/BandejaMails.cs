using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net.Mail;
using WebProgramAR.Entidades;
using BibliotecaMails.Logging;
using System.Net;

namespace BibliotecaMails.Procesamiento
{
    public class BandejaMails
    {
        private string host;
        private int puerto;
        private bool ssl;
        private int intervalo;
        private Thread th;
        private bool seguir;
        private Log log;
        private string userSmtp;
        private string passwordSmtp;

        public BandejaMails()
        {
            this.host = Config.Configuracion.HostSmtp;
            this.puerto = Config.Configuracion.PortSmtp;
            this.ssl = Config.Configuracion.UsarSsl;
            this.intervalo = Config.Configuracion.Intervalo;
            this.seguir = true;
            this.log = new Log(Config.Configuracion.NivelLog, this);
            this.userSmtp = Config.Configuracion.UserSmtp;
            this.passwordSmtp = Config.Configuracion.PasswordSmtp;

            this.th = new Thread(new ThreadStart(this.Procesar));
            this.th.Name = "TH_ServicioMails";
            this.th.IsBackground = true;

            this.log.Loguear("Comenzando a procesar mails pendientes...", NivelLog.Debug, ClaseLog.Informacion);
        }

        public void Comenzar()
        {
            this.th.Start();
        }

        public void Detener()
        {
            this.seguir = false;
            this.th.Join();
        }

        private void Procesar()
        {
            SmtpClient smtp = new SmtpClient(this.host, this.puerto);
            smtp.EnableSsl = this.ssl;
            smtp.Credentials = new NetworkCredential("programar", "Pr0gr4m4r");

            do
            {
                foreach (Mail mail in WebProgramAR.DataAccess.MailDA.MailsPendientesDeEnvio())
                {
                    string body = String.Empty;
                    try { body = Utilidades.Criptografia.Crypto.Desencriptar(mail.Body); }
                    catch (Exception ex)
                    {
                        //si hubo algun error al desencriptar, lo marcamos como enviado y continuamos con el siguiente mail.
                        this.log.Loguear("Error al desencriptar el cuerpo del mensaje --> " + ex.Message + Environment.NewLine + "Stack:" + Environment.NewLine + ex.StackTrace,
                            NivelLog.Normal, ClaseLog.Error);
                        WebProgramAR.DataAccess.MailDA.ActualizarEstadoMail(mail);
                        continue;
                    }
                    MailMessage msg = new MailMessage(mail.From, mail.To, mail.Subject, body);
                    try
                    {
                        this.log.Loguear(String.Format("Enviando mail {0}...", mail.id.ToString()), NivelLog.Debug, ClaseLog.Informacion);
                        smtp.Send(msg);
                        this.log.Loguear(String.Format("Enviado mail {0}", mail.id.ToString()), NivelLog.Debug, ClaseLog.Informacion);
                        WebProgramAR.DataAccess.MailDA.ActualizarEstadoMail(mail);
                        this.log.Loguear(String.Format("Actualizado estado mail {0}...", mail.id.ToString()), NivelLog.Debug, ClaseLog.Informacion);
                    }
                    catch (Exception ex)
                    {
                        this.log.Loguear(ex.Message + Environment.NewLine + "Stack:" + Environment.NewLine + ex.StackTrace,
                            NivelLog.Normal, ClaseLog.Error);                            
                    }
                }

                this.Dormir(intervalo);
            } while (this.seguir);
        }

        private void Dormir(int milisegundos)
        {
            int ms = 0;
            do
            {
                Thread.Sleep(500);
                ms += 500;
            } while (ms < milisegundos && this.seguir);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BibliotecaMails.Config
{
    internal class Configuracion
    {
        private static System.Configuration.AppSettingsReader appReader =
            new System.Configuration.AppSettingsReader();
        private static string hostSmtp;
        private static int portSmtp;
        private static int intervalo;
        private static string userSmtp;
        private static string passwordSmtp;

        internal static string HostSmtp
        {
            get
            {
                if (Object.Equals(hostSmtp, null))
                    hostSmtp = appReader.GetValue("HostSmtp", typeof(string)).ToString();

                return hostSmtp;
            }
        }

        internal static int PortSmtp
        {
            get
            {
                if (Object.Equals(portSmtp, 0))
                    portSmtp = (int)appReader.GetValue("PortSmtp", typeof(int));

                return portSmtp;
            }
        }

        internal static bool UsarSsl
        {
            get { return (bool)appReader.GetValue("UsarSsl", typeof(bool)); }
        }

        /// <summary>
        /// Intervalo de barrido de la base de datos (en milisegundos)
        /// </summary>
        internal static int Intervalo
        {
            get
            {
                if (Object.Equals(intervalo, 0))
                    intervalo = (int)appReader.GetValue("Intervalo", typeof(int));

                return intervalo;
            }
        }

        internal static BibliotecaMails.Logging.NivelLog NivelLog
        {
            get
            {
                string nivel = appReader.GetValue("NivelLog", typeof(string)).ToString();
                switch (nivel.ToLower())
                {
                    case "debug":
                        return Logging.NivelLog.Debug;
                    default:
                        return Logging.NivelLog.Normal;
                }
            }
        }

        internal static string UserSmtp
        {
            get
            {
                if (Object.Equals(userSmtp, null))
                    userSmtp = appReader.GetValue("UserSmtp", typeof(string)).ToString();

                return userSmtp;
            }
        }

        internal static string PasswordSmtp
        {
            get
            {
                if (Object.Equals(passwordSmtp, null))
                    passwordSmtp = appReader.GetValue("PasswordSmtp", typeof(string)).ToString();

                return passwordSmtp;
            }
        }
    }
}

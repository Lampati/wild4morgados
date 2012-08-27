using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BibliotecaMails.Procesamiento;
using System.Diagnostics;

namespace BibliotecaMails.Logging
{
    public enum NivelLog
    {
        Debug = 0,
        Normal = 1
    }

    public enum ClaseLog
    {
        Error = 0,
        Informacion = 1,
        Advertencia = 2
    }

    internal class Log
    {
        private NivelLog nivel;
        private BandejaMails bandeja;
        private const string sVisor = "ServicioMailsProgramAR";

        internal Log(NivelLog nivel, BandejaMails bandeja)
        {
            this.nivel = nivel;
            this.bandeja = bandeja;

            if (!EventLog.SourceExists(sVisor))
                EventLog.CreateEventSource(sVisor, sVisor);
        }

        internal void Loguear(string mensaje, NivelLog nivel, ClaseLog clase)
        {
            if (this.nivel > nivel)
                return;

            EventLogEntryType tipoLog = EventLogEntryType.Information;

            switch (clase)
            {
                case ClaseLog.Error:
                    tipoLog = EventLogEntryType.Error;
                    break;
                case ClaseLog.Informacion:
                    tipoLog = EventLogEntryType.Information;
                    break;
                case ClaseLog.Advertencia:
                    tipoLog = EventLogEntryType.Warning;
                    break;
            }

            EventLog.WriteEntry(sVisor, mensaje, tipoLog);
        }
    }
}

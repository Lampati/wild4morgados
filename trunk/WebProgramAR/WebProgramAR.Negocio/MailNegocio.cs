using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebProgramAR.Entidades;
using WebProgramAR.DataAccess;

namespace WebProgramAR.Negocio
{
    public class MailNegocio
    {
       

        public static void Alta(Mail mail)
        {

            MailDA.Alta(mail);
        }

    }
}

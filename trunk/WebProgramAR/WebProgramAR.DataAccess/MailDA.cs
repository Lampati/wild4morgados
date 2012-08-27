using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebProgramAR.Entidades;
using System.Data;

namespace WebProgramAR.DataAccess
{
    public class MailDA 
    {
        public static string _nombreTabla = "Mails";

        public static IEnumerable<Mail> MailsPendientesDeEnvio()
        {
            using (WebProgramAREntities db = new WebProgramAREntities())
            {
                return db.Mails.Where(m => !m.Enviado).ToList();
            }
        }

        public static void ActualizarEstadoMail(Mail ma)
        {
            using (WebProgramAREntities db = new WebProgramAREntities())
            {
                Mail mail = db.Mails.Single(m => m.id == ma.id);

                mail.FechaEnvio = DateTime.Now;
                mail.Enviado = true;

                db.ObjectStateManager.ChangeObjectState(mail, EntityState.Modified);
                db.SaveChanges();
            }
        }
        
        public static void Alta(Mail mail)
        {
            using (WebProgramAREntities db = new WebProgramAREntities())
            {
                db.Mails.AddObject(mail);
                db.SaveChanges();
            }
        }
      
    }
}

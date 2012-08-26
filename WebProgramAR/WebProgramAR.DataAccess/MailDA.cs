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

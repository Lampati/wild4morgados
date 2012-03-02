using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Excepciones
{
    public class ExcepcionHashNoConcuerda : Exception
    {
        public ExcepcionHashNoConcuerda(string hash1, string hash2)
            : base(String.Format("Los Hash no concuerdan \"{0}\" : \"{1}\"", hash1, hash2))
        {
        }
    }
}

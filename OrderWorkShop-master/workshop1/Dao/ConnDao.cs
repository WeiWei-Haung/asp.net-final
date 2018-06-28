using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Workshop1.Dao
{
    public class ConnDao
    {
        public string GetConnStr()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
        }
    }
}
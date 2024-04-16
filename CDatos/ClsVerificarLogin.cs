using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace proyectoFinal.CDatos
{
    public class ClsVerificarLogin
    {
        private static int loginStatus = 0;

        public static void SetLoginStatus(int status)
        {
            loginStatus = status;
        }

        public static int GetLoginStatus()
        {
            return loginStatus;
        }
    }
}

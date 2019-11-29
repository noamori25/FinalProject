using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagmentSystem.BLL
{
    public static class FlightCenterConfig
    {
        public const string ADMIN_USER = "admin";
        public const string ADMIN_PASSWORD = "9999";
        public static string ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionToSqlServer"].ConnectionString;
    }
}

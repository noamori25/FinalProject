using ProjectManagmentSystem.BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagmentSystem.DAO
{
    /// <summary>
    /// This class communicates with all tables in DB (MSSQL)
    /// </summary>
    public class DeleteDataDAO
    {
        /// <summary>
        /// Delete all tables in DB
        /// </summary>
        /// <param name="c"></param>
        public void RemoveAll()
        {
            using (SqlConnection con = new SqlConnection(FlightCenterConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("CLEAN_ALL_DB", con))
                {
                    cmd.Connection.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}

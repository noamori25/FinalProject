using ProjectManagmentSystem.BLL;
using ProjectManagmentSystem.DAO.Interfaces;
using ProjectManagmentSystem.POCO;
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
    /// This class communicates with Airline companies table in DB (MSSQL)
    /// </summary>
    public class AirlineDAOMSSQL : IAirlineDAO
    {
        /// <summary>
        /// Add new airline to DB and fill airline Id
        /// </summary>
        /// <param name="a"></param>
        /// <returns>airline comapny Id</returns>
        public long Add(AirlineCompany a)
        {
            using (SqlConnection con = new SqlConnection(FlightCenterConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("INSERT_AIRLINE_COMPANY", con))
                {
                    cmd.Parameters.Add(new SqlParameter("@AIRLINE_NAME", a.AirlineName));
                    cmd.Parameters.Add(new SqlParameter("@USER_NAME", a.UserName));
                    cmd.Parameters.Add(new SqlParameter("@PASSWORD", a.Password));
                    cmd.Parameters.Add(new SqlParameter("@COUNTRY_CODE", a.CountryCode));
                    cmd.Connection.Open();
                    cmd.CommandType = CommandType.StoredProcedure;

                    return a.Id = (long)cmd.ExecuteScalar();
                }
            }
        }

        /// <summary>
        /// Search airline in DB by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>airline company object</returns>
        public AirlineCompany Get(long id)
        {
            AirlineCompany AirlineById = new AirlineCompany();

            using (SqlConnection con = new SqlConnection(FlightCenterConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GET_AIRLINE_BY_ID", con))
                {
                    cmd.Parameters.Add(new SqlParameter("@ID", id));
                    cmd.Connection.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            AirlineById.Id = id;
                            AirlineById.AirlineName = (string)reader["AIRLINE_NAME"];
                            AirlineById.CountryCode = (long)reader["COUNTRY_CODE"];
                            AirlineById.UserName = (string)reader["USER_NAME"];
                            AirlineById.Password = (string)reader["PASSWORD"];
                            return AirlineById;
                        }
                    }
                }

            }
            return null;
        }

        /// <summary>
        /// Search airline in DB by user name
        /// </summary>
        /// <param name="userName"></param>
        /// <returns>airline company</returns>
        public AirlineCompany GetAirlineByUserName(string userName)
        {
            AirlineCompany AirlineByUserName = new AirlineCompany();

            using (SqlConnection con = new SqlConnection(FlightCenterConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GET_AIRLINE_BY_USER_NAME", con))
                {
                    cmd.Parameters.Add(new SqlParameter("@USER_NAME", userName));
                    cmd.Connection.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            AirlineByUserName.Id = (long)reader["ID"];
                            AirlineByUserName.AirlineName = (string)reader["AIRLINE_NAME"];
                            AirlineByUserName.CountryCode = (long)reader["COUNTRY_CODE"];
                            AirlineByUserName.UserName = userName;
                            AirlineByUserName.Password = (string)reader["PASSWORD"];
                            return AirlineByUserName;
                        }
                    }
                }

            }
            return null;
        }

        /// <summary>
        /// Search all countries in DB
        /// </summary>
        /// <returns>list of airline companies</returns>
        public IList<AirlineCompany> GetAll()
        {
            List<AirlineCompany> AllAirlineCompany = new List<AirlineCompany>();

            using (SqlConnection con = new SqlConnection(FlightCenterConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GET_ALL_AIRLINE_COMPANIES", con))
                {
                    cmd.Connection.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader read = cmd.ExecuteReader())
                    {
                        while (read.Read())
                        {
                            AirlineCompany AirlineCompany = new AirlineCompany();
                            AirlineCompany.Id = (long)read["ID"];
                            AirlineCompany.AirlineName = (string)read["AIRLINE_NAME"];
                            AirlineCompany.UserName = (string)read["USER_NAME"];
                            AirlineCompany.Password = (string)read["PASSWORD"];
                            AirlineCompany.CountryCode = (long)read["COUNTRY_CODE"];
                            AllAirlineCompany.Add(AirlineCompany);
                        }
                    }
                }
            }
            return AllAirlineCompany;
        }

        /// <summary>
        /// Search airline comapnies in DB by country
        /// </summary>
        /// <param name="countryId"></param>
        /// <returns>list of airline companies by country</returns>
        public IList<AirlineCompany> GetAllAirlinesByCountry(long countryId)
        {
            List<AirlineCompany> AllAirlineByCountry = new List<AirlineCompany>();

            using (SqlConnection con = new SqlConnection(FlightCenterConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GET_AIRLINE_BY_COUNTRY_ID", con))
                {
                    cmd.Parameters.Add(new SqlParameter("@ID", countryId));
                    cmd.Connection.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader read = cmd.ExecuteReader())
                    {
                        while (read.Read())
                        {
                            AirlineCompany AirlineCompany = new AirlineCompany();
                            AirlineCompany.Id = (long)read["ID"];
                            AirlineCompany.AirlineName = (string)read["AIRLINE_NAME"];
                            AirlineCompany.UserName = (string)read["USER_NAME"];
                            AirlineCompany.Password = (string)read["PASSWORD"];
                            AirlineCompany.CountryCode = (long)read["COUNTRY_CODE"];
                            AllAirlineByCountry.Add(AirlineCompany);
                        }
                    }
                }
            }
            return AllAirlineByCountry;
        }

        /// <summary>
        /// Delete airline company in DB
        /// </summary>
        /// <param name="a"></param>
        public void Remove(AirlineCompany a)
        {
            using (SqlConnection con = new SqlConnection(FlightCenterConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("DELETE_AIRLINE", con))
                {
                    cmd.Parameters.Add(new SqlParameter("@ID", a.Id));
                    cmd.Connection.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Update airline company in DB
        /// </summary>
        /// <param name="a"></param>
        public void Update(AirlineCompany a)
        {
            using (SqlConnection con = new SqlConnection(FlightCenterConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("UPDATE_AIRLINE", con))
                {
                    cmd.Parameters.Add(new SqlParameter("@ID", a.Id));
                    cmd.Parameters.Add(new SqlParameter("@AIRLINE_NAME", a.AirlineName));
                    cmd.Parameters.Add(new SqlParameter("@UAER_NAME", a.UserName));
                    cmd.Parameters.Add(new SqlParameter("@PASSWORD", a.Password));
                    cmd.Parameters.Add(new SqlParameter("@COUNTRY_CODE", a.CountryCode));
                    cmd.Connection.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }

            }
        }
    }
}

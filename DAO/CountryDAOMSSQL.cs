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
    /// This class communicates with Countries table in DB (MSSQL)
    /// </summary>
    public class CountryDAOMSSQL : ICountryDAO
    {
        /// <summary>
        /// Add new counry to DB and fill country Id
        /// </summary>
        /// <param name="c"></param>
        /// <returns>country Id</returns>
        public long Add(Country c)
        {
            using (SqlConnection con = new SqlConnection(FlightCenterConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("INSERT_COUNTRY", con))
                {
                    cmd.Parameters.Add(new SqlParameter("@COUNRTY_NAME", c.CountryName));
                    cmd.Connection.Open();
                    cmd.CommandType = CommandType.StoredProcedure;

                    c.Id = (long)cmd.ExecuteScalar();
                    return c.Id;
                }

            }
        }

        /// <summary>
        /// Search country in DB by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>country object</returns>
        public Country Get(long id)
        {
            Country CountryById = new Country();

            using (SqlConnection con = new SqlConnection(FlightCenterConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GET_COUNTRY_BY_ID", con))
                {
                    cmd.Parameters.Add(new SqlParameter("@ID", id));
                    cmd.Connection.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            CountryById.Id = id;
                            CountryById.CountryName = (string)reader["COUNTRY_NAME"];
                            return CountryById;
                        }
                    }
                }

            }
            return null;
        }

        /// <summary>
        /// Search country in DB by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns>country object</returns>
        public Country GetCountryByName(string name)
        {
            Country countryByName = new Country();

            using (SqlConnection con = new SqlConnection(FlightCenterConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GET_COUNTRY_BY_NAME", con))
                {
                    cmd.Parameters.Add(new SqlParameter("@NAME", name));
                    cmd.Connection.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            countryByName.Id = (long)reader["ID"];
                            countryByName.CountryName = name;
                            return countryByName;
                        }
                    }
                }

            }
            return null;
        }

        /// <summary>
        /// Search all countries in DB
        /// </summary>
        /// <returns>list of all countries</returns>
        public IList<Country> GetAll()
        {
            List<Country> AllCountries = new List<Country>();

            using (SqlConnection con = new SqlConnection(FlightCenterConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GET_ALL_COUNTRIES", con))
                {
                    cmd.Connection.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader read = cmd.ExecuteReader())
                    {
                        while (read.Read())
                        {
                            Country Country = new Country();
                            Country.Id = (long)read["ID"];
                            Country.CountryName = (string)read["COUNTRY_NAME"];
                            AllCountries.Add(Country);
                        }
                    }
                }
            }
            return AllCountries;
        }

        /// <summary>
        /// Delete country in DB
        /// </summary>
        /// <param name="c"></param>
        public void Remove(Country c)
        {
            using (SqlConnection con = new SqlConnection(FlightCenterConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("DELETE_COUNTRY", con))
                {
                    cmd.Parameters.Add(new SqlParameter("@COUNTRY_CODE", c.Id));
                    cmd.Connection.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Update country in DB
        /// </summary>
        /// <param name="c"></param>
        public void Update(Country c)
        {
            //i dont think we need to let the users update countries..
            using (SqlConnection con = new SqlConnection(FlightCenterConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("UPDATE_COUNTRY", con))
                {
                    cmd.Parameters.Add(new SqlParameter("@ID", c.Id));
                    cmd.Parameters.Add(new SqlParameter("@COUNTRY_NAME", c.CountryName));
                    cmd.Connection.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }

            }
        }
    }
}

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
    /// This class communicates with Customers table in DB (MSSQL)
    /// </summary>
    public class CustomerDAOMSSQL : ICustomerDAO
    {
        /// <summary>
        /// Add nes customer and fill customer Id
        /// </summary>
        /// <param name="c"></param>
        /// <returns>customer Id</returns>
        public long Add(Customer c)
        {
            using (SqlConnection con = new SqlConnection(FlightCenterConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("INSERT_CUSTOMER", con))
                {
                    cmd.Parameters.Add(new SqlParameter("@FIRST_NAME", c.FirstName));
                    cmd.Parameters.Add(new SqlParameter("@LAST_NAME", c.LastName));
                    cmd.Parameters.Add(new SqlParameter("@USER_NAME", c.UserName));
                    cmd.Parameters.Add(new SqlParameter("@PASSWORD", c.Password));
                    cmd.Parameters.Add(new SqlParameter("@ADDRESS", c.Address));
                    cmd.Parameters.Add(new SqlParameter("@PHONE_NO", c.PhoneNo));
                    cmd.Parameters.Add(new SqlParameter("@CREDIT_CARD_NUMBER", c.CreditCardNumber));
                    cmd.Connection.Open();
                    cmd.CommandType = CommandType.StoredProcedure;

                    return c.Id = (long)cmd.ExecuteScalar();
                }

            }
        }

        /// <summary>
        /// Search customer in DB by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>customer object</returns>
        public Customer Get(long id)
        {
            Customer CustomerById = new Customer();

            using (SqlConnection con = new SqlConnection(FlightCenterConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GET_CUSTOMER_BY_ID", con))
                {
                    cmd.Parameters.Add(new SqlParameter("@ID", id));
                    cmd.Connection.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            CustomerById.Id = id;
                            CustomerById.FirstName = (string)reader["FIRST_NAME"];
                            CustomerById.LastName = (string)reader["LAST_NAME"];
                            CustomerById.UserName = (string)reader["USER_NAME"];
                            CustomerById.Password = (string)reader["PASSWORD"];
                            CustomerById.Address = (string)reader["ADDRESS"];
                            CustomerById.PhoneNo = (string)reader["PHONE_NO"];
                            CustomerById.CreditCardNumber = (string)reader["CREDIT_CARD_NUMBER"];
                            return CustomerById;
                        }
                    }
                }

            }
            return null;
        }

        /// <summary>
        /// Search all customers from DB
        /// </summary>
        /// <returns>list of all customers</returns>
        public IList<Customer> GetAll()
        {
            List<Customer> AllCustomers = new List<Customer>();

            using (SqlConnection con = new SqlConnection(FlightCenterConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GET_ALL_CUSTOMERS", con))
                {
                    cmd.Connection.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader read = cmd.ExecuteReader())
                    {
                        while (read.Read())
                        {
                            Customer Customer = new Customer();
                            Customer.Id = (long)read["ID"];
                            Customer.FirstName = (string)read["FIRST_NAME"];
                            Customer.LastName = (string)read["LAST_NAME"];
                            Customer.UserName = (string)read["USER_NAME"];
                            Customer.Password = (string)read["PASSWORD"];
                            Customer.Address = (string)read["ADDRESS"];
                            Customer.PhoneNo = (string)read["PHONE_NO"];
                            Customer.CreditCardNumber = (string)read["CREDIT_CARD_NUMBER"];

                            AllCustomers.Add(Customer);
                        }
                    }
                }
            }
            return AllCustomers;
        }

        /// <summary>
        /// Search customer in DB by user name
        /// </summary>
        /// <param name="name"></param>
        /// <returns>customer object</returns>
        public Customer GetCustomerByUserName(string name)
        {
            Customer CustomerByUserName = new Customer();

            using (SqlConnection con = new SqlConnection(FlightCenterConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GET_CUSTOMER_BY_USER_NAME", con))
                {
                    cmd.Parameters.Add(new SqlParameter("@USER_NAME", name));
                    cmd.Connection.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            CustomerByUserName.Id = (long)reader["ID"];
                            CustomerByUserName.FirstName = (string)reader["FIRST_NAME"];
                            CustomerByUserName.LastName = (string)reader["LAST_NAME"];
                            CustomerByUserName.UserName = name;
                            CustomerByUserName.Password = (string)reader["PASSWORD"];
                            CustomerByUserName.Address = (string)reader["ADDRESS"];
                            CustomerByUserName.PhoneNo = (string)reader["PHONE_NO"];
                            CustomerByUserName.CreditCardNumber = (string)reader["CREDIT_CARD_NUMBER"];
                            return CustomerByUserName;
                        }
                    }
                }

            }
            return null;
        }

        /// <summary>
        /// Delete customer in DB
        /// </summary>
        /// <param name="c"></param>
        public void Remove(Customer c)
        {
            using (SqlConnection con = new SqlConnection(FlightCenterConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("DELETE_TICKETS_BY_CUSTOMER", con))
                {
                    cmd.Parameters.Add(new SqlParameter("@CUSTOMER_ID", c.Id));
                    cmd.Connection.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
                using (SqlCommand cmd = new SqlCommand("DELETE_CUSTOMER", con))
                {
                    cmd.Parameters.Add(new SqlParameter("@ID", c.Id));
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Update customer in DB
        /// </summary>
        /// <param name="c"></param>
        public void Update(Customer c)
        {
            using (SqlConnection con = new SqlConnection(FlightCenterConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("UPDATE_CUSTOMER", con))
                {
                    cmd.Parameters.Add(new SqlParameter("@ID", c.Id));
                    cmd.Parameters.Add(new SqlParameter("@FIRST_NAME", c.FirstName));
                    cmd.Parameters.Add(new SqlParameter("@LAST_NAME", c.LastName));
                    cmd.Parameters.Add(new SqlParameter("@USER_NAME", c.UserName));
                    cmd.Parameters.Add(new SqlParameter("@PASSWORD", c.Password));
                    cmd.Parameters.Add(new SqlParameter("@ADDRESS", c.Address));
                    cmd.Parameters.Add(new SqlParameter("@PHONE_NO", c.PhoneNo));
                    cmd.Parameters.Add(new SqlParameter("@CREDIT_CARD_NUMBER", c.CreditCardNumber));
                    cmd.Connection.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }

            }
        }
    }
}

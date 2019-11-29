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
    /// This class communicates with tickets table in DB (MSSQL)
    /// </summary>
    public class TicketDAOMSSQL : ITicketDAO
    {
        /// <summary>
        /// Add new ticket to DB and fill tickets Id
        /// </summary>
        /// <param name="t"></param>
        /// <returns>ticket Id</returns>
        public long Add(Ticket t)
        {
            using (SqlConnection con = new SqlConnection(FlightCenterConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("INSERT_TICKET", con))
                {
                    cmd.Parameters.Add(new SqlParameter("@FLIGHT_ID", t.FlightId));
                    cmd.Parameters.Add(new SqlParameter("@CUSTOMER_ID", t.CustomerId));
                    cmd.Connection.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    t.Id = (long)cmd.ExecuteScalar();
                }
                using (SqlCommand cmd = new SqlCommand("UPDATE_REMAINING_TICKET_MINUS1_BY_FLIGHT_ID", con))
                {
                    cmd.Parameters.Add(new SqlParameter("@FLIGHT_ID", t.FlightId));
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
                return t.Id;

            }
        }

        /// <summary>
        /// Search ticket in DB by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>ticket object</returns>
        public Ticket Get(long id)
        {
            Ticket TicketById = new Ticket();
            using (SqlConnection con = new SqlConnection(FlightCenterConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GET_TICKET_BY_ID", con))
                {
                    cmd.Parameters.Add(new SqlParameter("@ID", id));
                    cmd.Connection.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            TicketById.Id = id;
                            TicketById.CustomerId = (long)reader["CUSTOMER_ID"];
                            TicketById.FlightId = (long)reader["FLIGHT_ID"];
                            return TicketById;
                        }
                    }
                }

            }
            return null;
        }

        /// <summary>
        /// Search tickets in DB by customer
        /// </summary>
        /// <param name="customer"></param>
        /// <returns>list of tickets by customer</returns>
        public IList<Ticket> GetTicketsByCustomer(Customer customer)
        {
            List<Ticket> AllTicketsByCustomer = new List<Ticket>();
            using (SqlConnection con = new SqlConnection(FlightCenterConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GET_TICKETS_BY_CUSTOMER", con))
                {
                    cmd.Parameters.Add(new SqlParameter("@CUSTOMER_ID", customer.Id));
                    cmd.Connection.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Ticket TicketByCustomer = new Ticket();
                            TicketByCustomer.Id = (long)reader["ID"];
                            TicketByCustomer.CustomerId = customer.Id;
                            TicketByCustomer.FlightId = (long)reader["FLIGHT_ID"];
                            AllTicketsByCustomer.Add(TicketByCustomer);
                        }
                    }
                }

            }
            return AllTicketsByCustomer;
        }

        /// <summary>
        /// Search tickets in DB 
        /// </summary>
        /// <returns>list of all tickets</returns>
        public IList<Ticket> GetAll()
        {
            List<Ticket> AllTickets = new List<Ticket>();
            using (SqlConnection con = new SqlConnection(FlightCenterConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GET_ALL_TICKETS", con))
                {
                    cmd.Connection.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader read = cmd.ExecuteReader())
                    {
                        while (read.Read())
                        {
                            Ticket Ticket = new Ticket();
                            Ticket.Id = (long)read["ID"];
                            Ticket.FlightId = (long)read["FLIGHT_ID"];
                            Ticket.CustomerId = (long)read["CUSTOMER_ID"];
                            AllTickets.Add(Ticket);
                        }
                    }
                }
            }
            return AllTickets;

        }

        /// <summary>
        /// Delete ticket in DB 
        /// </summary>
        /// <param name="t"></param>
        public void Remove(Ticket t)
        {
            using (SqlConnection con = new SqlConnection(FlightCenterConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("UPDATE_REMAINING_TICKET_PLUS1_BY_FLIGHT_ID", con))
                {
                    cmd.Parameters.Add(new SqlParameter("@FLIGHT_ID", t.FlightId));
                    cmd.Connection.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
                using (SqlCommand cmd = new SqlCommand("DELETE_TICKET", con))
                {
                    cmd.Parameters.Add(new SqlParameter("@ID", t.Id));
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }

            }
        }

        /// <summary>
        /// Update ticket in DB
        /// </summary>
        /// <param name="t"></param>
        public void Update(Ticket t)
        {
            using (SqlConnection con = new SqlConnection(FlightCenterConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("UPDATE_TICKET", con))
                {
                    cmd.Parameters.Add(new SqlParameter("@ID", t.Id));
                    cmd.Parameters.Add(new SqlParameter("@FLIGHT_ID", t.FlightId));
                    cmd.Parameters.Add(new SqlParameter("@CUSTOMER_ID", t.CustomerId));

                    cmd.Connection.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }

            }
        }

        /// <summary>
        //Transferring all flight's tickets that landed three hours
        ///ago and more from Tickets table to Tickets history table in DB
        /// </summary>
        public void MoveToTicketsHistory()
        {
            using (SqlConnection con = new SqlConnection(FlightCenterConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("MOVE_TO_TICKETS_HISTORY", con))
                {
                    cmd.Connection.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }

            }
        }

        /// <summary>
        /// Search tickets in DB by flight Id
        /// </summary>
        /// <param name="flightId"></param>
        /// <returns>list of tickets by flight</returns>
        public IList<Ticket> GetAllTicketsByFlight(long flightId)
        {
            List<Ticket> AllTicketsByFlight = new List<Ticket>();
            using (SqlConnection con = new SqlConnection(FlightCenterConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GET_ALL_TICKETS_BY_FLIGHT", con))
                {
                    cmd.Parameters.Add(new SqlParameter("@FLIGHT_ID", flightId));
                    cmd.Connection.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader read = cmd.ExecuteReader())
                    {
                        while (read.Read())
                        {
                            Ticket Ticket = new Ticket();
                            Ticket.Id = (long)read["ID"];
                            Ticket.FlightId = (long)read["FLIGHT_ID"];
                            Ticket.CustomerId = (long)read["CUSTOMER_ID"];
                            AllTicketsByFlight.Add(Ticket);
                        }
                    }
                }
            }
            return AllTicketsByFlight;

        }

        /// <summary>
        /// Serach tickets in DB by airline company Id
        /// </summary>
        /// <param name="airlineId"></param>
        /// <returns>list of tickets by airline company</returns>
        public IList<Ticket> GetAllTicketsByAirlineCompany(long airlineId)
        {
            List<Ticket> AllTicketsByAirline = new List<Ticket>();
            using (SqlConnection con = new SqlConnection(FlightCenterConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GET_TICKETS_BY_AIRLINE", con))
                {
                    cmd.Parameters.Add(new SqlParameter("@ID", airlineId));
                    cmd.Connection.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader read = cmd.ExecuteReader())
                    {
                        while (read.Read())
                        {
                            Ticket Ticket = new Ticket();
                            Ticket.Id = (long)read["ID"];
                            Ticket.FlightId = (long)read["FLIGHT_ID"];
                            Ticket.CustomerId = (long)read["CUSTOMER_ID"];
                            AllTicketsByAirline.Add(Ticket);
                        }
                    }
                }
            }
            return AllTicketsByAirline;
        }
    }
}

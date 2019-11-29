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
    /// This class communicates with Flights table in DB (MSSQL)
    /// </summary>
    public class FlightDAOMSSQL : IFlightDAO
    {
        /// <summary>
        /// Add nes flight to DB and fill flight Id
        /// </summary>
        /// <param name="f"></param>
        /// <returns>flight Id</returns>
        public long Add(Flight f)
        {
            using (SqlConnection con = new SqlConnection(FlightCenterConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("INSERT_FLIGHT", con))
                {
                    cmd.Parameters.Add(new SqlParameter("@AIRLINECOMPANY_ID", f.AirlineCompanyId));
                    cmd.Parameters.Add(new SqlParameter("@ORIGIN_COUNTRY_CODE", f.OriginCountryCode));
                    cmd.Parameters.Add(new SqlParameter("@DESTINATION_COUNTRY_CODE", f.DestinationCountryCode));
                    cmd.Parameters.Add(new SqlParameter("@DEPARTURE_TIME", f.DepartureTime));
                    cmd.Parameters.Add(new SqlParameter("@LANDING_TIME", f.LandingTime));
                    cmd.Parameters.Add(new SqlParameter("@REMAINING_TICKETS", f.RemainingTickets));
                    cmd.Connection.Open();
                    cmd.CommandType = CommandType.StoredProcedure;

                    return f.Id = (long)cmd.ExecuteScalar();
                }
            }
        }

        /// <summary>
        /// Serach flight in DB by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>flight object</returns>
        public Flight Get(long id)
        {
            Flight FlightById = new Flight();
            using (SqlConnection con = new SqlConnection(FlightCenterConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GET_FLIGHT_BY_ID", con))
                {
                    cmd.Parameters.Add(new SqlParameter("@ID", id));
                    cmd.Connection.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            FlightById.Id = id;
                            FlightById.AirlineCompanyId = (long)reader["AIRLINECOMPANY_ID"];
                            FlightById.OriginCountryCode = (long)reader["ORIGIN_COUNTRY_CODE"];
                            FlightById.DestinationCountryCode = (long)reader["DESTINATION_COUNTRY_CODE"];
                            FlightById.DepartureTime = (DateTime)reader["DEPARTURE_TIME"];
                            FlightById.LandingTime = (DateTime)reader["LANDING_TIME"];
                            FlightById.RemainingTickets = (int)reader["REMAINING_TICKETS"];
                            return FlightById;
                        }
                    }
                }

            }
            return null;
        }

        /// <summary>
        /// Search all flights in DB
        /// </summary>
        /// <returns>list of 
        ///all flights in DB</returns>
        public IList<Flight> GetAll()
        {
            List<Flight> AllFlights = new List<Flight>();
            using (SqlConnection con = new SqlConnection(FlightCenterConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GET_ALL_FLIGHTS", con))
                {
                    cmd.Connection.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader read = cmd.ExecuteReader())
                    {
                        while (read.Read())
                        {
                            Flight Flight = new Flight();
                            Flight.Id = (long)read["ID"];
                            Flight.AirlineCompanyId = (long)read["AIRLINECOMPANY_ID"];
                            Flight.OriginCountryCode = (long)read["ORIGIN_COUNTRY_CODE"];
                            Flight.DestinationCountryCode = (long)read["DESTINATION_COUNTRY_CODE"];
                            Flight.DepartureTime = (DateTime)read["DEPARTURE_TIME"];
                            Flight.LandingTime = (DateTime)read["LANDING_TIME"];
                            Flight.RemainingTickets = (int)read["REMAINING_TICKETS"];

                            AllFlights.Add(Flight);
                        }
                    }
                }
            }
            return AllFlights;
        }

        /// <summary>
        /// Search all vacancy flights in DB
        /// </summary>
        /// <returns>dictionary key = flight, value = int of remaining tickets</returns>
        public Dictionary<Flight, int> GetAllVacancyFlights()
        {
            Dictionary<Flight, int> VacancyFlights = new Dictionary<Flight, int>();
            using (SqlConnection con = new SqlConnection(FlightCenterConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GET_VACANCY_FLIGHTS", con))
                {
                    cmd.Connection.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader read = cmd.ExecuteReader())
                    {
                        while (read.Read())
                        {
                            Flight Flight = new Flight();
                            Flight.Id = (long)read["ID"];
                            Flight.AirlineCompanyId = (long)read["AIRLINECOMPANY_ID"];
                            Flight.OriginCountryCode = (long)read["ORIGIN_COUNTRY_CODE"];
                            Flight.DestinationCountryCode = (long)read["DESTINATION_COUNTRY_CODE"];
                            Flight.DepartureTime = (DateTime)read["DEPARTURE_TIME"];
                            Flight.LandingTime = (DateTime)read["LANDING_TIME"];
                            Flight.RemainingTickets = (int)read["REMAINING_TICKETS"];
                            VacancyFlights.Add(Flight, Flight.RemainingTickets);

                        }
                    }
                }
            }
            return VacancyFlights;
        }

        /// <summary>
        /// Search flight in DB by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>flight object</returns>
        public Flight GetFlightById(long id)
        {
            Flight FlightById = new Flight();
            using (SqlConnection con = new SqlConnection(FlightCenterConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GET_FLIGHT_BY_ID", con))
                {
                    cmd.Parameters.Add(new SqlParameter("@ID", id));
                    cmd.Connection.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            FlightById.Id = id;
                            FlightById.AirlineCompanyId = (long)reader["AIRLINECOMPANY_ID"];
                            FlightById.OriginCountryCode = (long)reader["ORIGIN_COUNTRY_CODE"];
                            FlightById.DestinationCountryCode = (long)reader["DESTINATION_COUNTRY_CODE"];
                            FlightById.DepartureTime = (DateTime)reader["DEPARTURE_TIME"];
                            FlightById.LandingTime = (DateTime)reader["LANDING_TIME"];
                            FlightById.RemainingTickets = (int)reader["REMAINING_TICKETS"];
                            return FlightById;
                        }
                    }
                }

            }
            return null;
        }

        /// <summary>
        /// Search flights in DB by origin country
        /// </summary>
        /// <param name="countryCode"></param>
        /// <returns>list of all flights by origin country</returns>
        public IList<Flight> GetFlightByOriginCountry(long countryCode)
        {
            List<Flight> AllFlightsByOriginCountry = new List<Flight>();
            using (SqlConnection con = new SqlConnection(FlightCenterConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GET_FLIGHTS_BY_ORIGIN_COUNTRY", con))
                {
                    cmd.Parameters.Add(new SqlParameter("@COUNTRY_CODE", countryCode));
                    cmd.Connection.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader read = cmd.ExecuteReader())
                    {
                        while (read.Read())
                        {
                            Flight Flight = new Flight();
                            Flight.Id = (long)read["ID"];
                            Flight.AirlineCompanyId = (long)read["AIRLINECOMPANY_ID"];
                            Flight.OriginCountryCode = (long)read["ORIGIN_COUNTRY_CODE"];
                            Flight.DestinationCountryCode = (long)read["DESTINATION_COUNTRY_CODE"];
                            Flight.DepartureTime = (DateTime)read["DEPARTURE_TIME"];
                            Flight.LandingTime = (DateTime)read["LANDING_TIME"];
                            Flight.RemainingTickets = (int)read["REMAINING_TICKETS"];

                            AllFlightsByOriginCountry.Add(Flight);
                        }
                    }
                }
            }
            return AllFlightsByOriginCountry;
        }

        /// <summary>
        /// Search flights in DB by customer 
        /// </summary>
        /// <param name="customer"></param>
        /// <returns>list of flights by customer</returns>
        public IList<Flight> GetFlightsByCustomer(Customer customer)
        {
            List<Flight> FlightsByCustomer = new List<Flight>();
            using (SqlConnection con = new SqlConnection(FlightCenterConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GET_FLIGHTS_BY_CUSTOMER", con))
                {
                    cmd.Parameters.Add(new SqlParameter("@CUSTOMER_ID", customer.Id));
                    cmd.Connection.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader read = cmd.ExecuteReader())
                    {
                        while (read.Read())
                        {
                            Flight FlightByCustomer = new Flight();
                            FlightByCustomer.Id = (long)read["ID"];
                            FlightByCustomer.AirlineCompanyId = (long)read["AIRLINECOMPANY_ID"];
                            FlightByCustomer.OriginCountryCode = (long)read["ORIGIN_COUNTRY_CODE"];
                            FlightByCustomer.DestinationCountryCode = (long)read["DESTINATION_COUNTRY_CODE"];
                            FlightByCustomer.DepartureTime = (DateTime)read["DEPARTURE_TIME"];
                            FlightByCustomer.LandingTime = (DateTime)read["LANDING_TIME"];
                            FlightByCustomer.RemainingTickets = (int)read["REMAINING_TICKETS"];
                            FlightsByCustomer.Add(FlightByCustomer);
                        }
                    }
                }
            }
            return FlightsByCustomer;
        }

        /// <summary>
        /// Serch flights in DB by departure date
        /// </summary>
        /// <param name="departureDate"></param>
        /// <returns>list of airlines by departure date</returns>
        public IList<Flight> GetFlightsByDepartureDate(DateTime departureDate)
        {

            List<Flight> FlightsByDeparture = new List<Flight>();
            using (SqlConnection con = new SqlConnection(FlightCenterConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GET_FLIGHTS_BY_DEPARTURE", con))
                {
                    cmd.Parameters.Add(new SqlParameter("@DEPARTURE", departureDate));
                    cmd.Connection.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader read = cmd.ExecuteReader())
                    {
                        while (read.Read())
                        {
                            Flight FlightByDeparture = new Flight();
                            FlightByDeparture.Id = (long)read["ID"];
                            FlightByDeparture.AirlineCompanyId = (long)read["AIRLINECOMPANY_ID"];
                            FlightByDeparture.OriginCountryCode = (long)read["ORIGIN_COUNTRY_CODE"];
                            FlightByDeparture.DestinationCountryCode = (long)read["DESTINATION_COUNTRY_CODE"];
                            FlightByDeparture.DepartureTime = (DateTime)read["DEPARTURE_TIME"];
                            FlightByDeparture.LandingTime = (DateTime)read["LANDING_TIME"];
                            FlightByDeparture.RemainingTickets = (int)read["REMAINING_TICKETS"];
                            FlightsByDeparture.Add(FlightByDeparture);
                        }
                    }
                }
            }
            return FlightsByDeparture;
        }

        /// <summary>
        /// Search flights in DB by destination country
        /// </summary>
        /// <param name="countryCode"></param>
        /// <returns>list of fligts by destination country</returns>
        public IList<Flight> GetFlightsByDestinationCountry(long countryCode)
        {
            List<Flight> FlightsByDestination = new List<Flight>();
            using (SqlConnection con = new SqlConnection(FlightCenterConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GET_FLIGHTS_BY_DESTINATION", con))
                {
                    cmd.Parameters.Add(new SqlParameter("@COUNTRY_CODE", countryCode));
                    cmd.Connection.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader read = cmd.ExecuteReader())
                    {
                        while (read.Read())
                        {
                            Flight FlightByDestination = new Flight();
                            FlightByDestination.Id = (long)read["ID"];
                            FlightByDestination.AirlineCompanyId = (long)read["AIRLINECOMPANY_ID"];
                            FlightByDestination.OriginCountryCode = (long)read["ORIGIN_COUNTRY_CODE"];
                            FlightByDestination.DestinationCountryCode = (long)read["DESTINATION_COUNTRY_CODE"];
                            FlightByDestination.DepartureTime = (DateTime)read["DEPARTURE_TIME"];
                            FlightByDestination.LandingTime = (DateTime)read["LANDING_TIME"];
                            FlightByDestination.RemainingTickets = (int)read["REMAINING_TICKETS"];

                            FlightsByDestination.Add(FlightByDestination);
                        }
                    }
                }
            }
            return FlightsByDestination;
        }

        /// <summary>
        /// Search flights in DB by landing date
        /// </summary>
        /// <param name="LandingDate"></param>
        /// <returns>list of flights by landing date</returns>
        public IList<Flight> GetFlightsByLandingDate(DateTime LandingDate)
        {

            List<Flight> FlightsByLanding = new List<Flight>();
            using (SqlConnection con = new SqlConnection(FlightCenterConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GET_FLIGHTS_BY_LANDING", con))
                {
                    cmd.Parameters.Add(new SqlParameter("@LANDING", LandingDate));
                    cmd.Connection.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader read = cmd.ExecuteReader())
                    {
                        while (read.Read())
                        {
                            Flight FlightByLanding = new Flight();
                            FlightByLanding.Id = (long)read["ID"];
                            FlightByLanding.AirlineCompanyId = (long)read["AIRLINECOMPANY_ID"];
                            FlightByLanding.OriginCountryCode = (long)read["ORIGIN_COUNTRY_CODE"];
                            FlightByLanding.DestinationCountryCode = (long)read["DESTINATION_COUNTRY_CODE"];
                            FlightByLanding.DepartureTime = (DateTime)read["DEPARTURE_TIME"];
                            FlightByLanding.LandingTime = (DateTime)read["LANDING_TIME"];
                            FlightByLanding.RemainingTickets = (int)read["REMAINING_TICKETS"];

                            FlightsByLanding.Add(FlightByLanding);
                        }
                    }
                }
            }
            return FlightsByLanding;
        }

        /// <summary>
        /// Delete flight in DB
        /// </summary>
        /// <param name="f"></param>
        public void Remove(Flight f)
        {
            using (SqlConnection con = new SqlConnection(FlightCenterConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("DELTE_TICKETS_BY_FLIGHT", con))
                {
                    cmd.Parameters.Add(new SqlParameter("@FLIGHT_ID", f.Id));
                    cmd.Connection.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
                using (SqlCommand cmd = new SqlCommand("DELETE_FLIGHT", con))
                {
                    cmd.Parameters.Add(new SqlParameter("@ID", f.Id));
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Update flight in DB
        /// </summary>
        /// <param name="f"></param>
        public void Update(Flight f)
        {
            using (SqlConnection con = new SqlConnection(FlightCenterConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("UPDATE_FLIGHT", con))
                {
                    cmd.Parameters.Add(new SqlParameter("@ID", f.Id));
                    cmd.Parameters.Add(new SqlParameter("@AIRLINE_ID", f.AirlineCompanyId));
                    cmd.Parameters.Add(new SqlParameter("@ORIGIN_COUNTRY_CODE", f.OriginCountryCode));
                    cmd.Parameters.Add(new SqlParameter("@DESTINATION_COUNTRY_CODE", f.DestinationCountryCode));
                    cmd.Parameters.Add(new SqlParameter("@DEPARTUE_TIME", f.DepartureTime));
                    cmd.Parameters.Add(new SqlParameter("@LANDING_TIME", f.LandingTime));
                    cmd.Parameters.Add(new SqlParameter("@REMAINING_TICKETS", f.RemainingTickets));

                    cmd.Connection.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Search flights in DB by airline company
        /// </summary>
        /// <param name="airlineCompany"></param>
        /// <returns>list of flights by airline company</returns>
        public IList<Flight> GetFlightsByAirlineCompany(AirlineCompany airlineCompany)
        {
            List<Flight> FlightsByAirlineCompany = new List<Flight>();
            using (SqlConnection con = new SqlConnection(FlightCenterConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GET_FLIGHTS_BY_AIRLINE_COMPANY", con))
                {
                    cmd.Parameters.Add(new SqlParameter("@AIRLINE_COMPANY_ID", airlineCompany.Id));
                    cmd.Connection.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader read = cmd.ExecuteReader())
                    {
                        while (read.Read())
                        {
                            Flight FlightByAirline = new Flight();
                            FlightByAirline.Id = (long)read["ID"];
                            FlightByAirline.AirlineCompanyId = airlineCompany.Id;
                            FlightByAirline.OriginCountryCode = (long)read["ORIGIN_COUNTRY_CODE"];
                            FlightByAirline.DestinationCountryCode = (long)read["DESTINATION_COUNTRY_CODE"];
                            FlightByAirline.DepartureTime = (DateTime)read["DEPARTURE_TIME"];
                            FlightByAirline.LandingTime = (DateTime)read["LANDING_TIME"];
                            FlightByAirline.RemainingTickets = (int)read["REMAINING_TICKETS"];

                            FlightsByAirlineCompany.Add(FlightByAirline);
                        }
                    }
                }
            }
            return FlightsByAirlineCompany;
        }

        /// <summary>
        /// Transferring all flights that landed three
        /// hours ago and more from Flights table to Flight history table in DB
        /// </summary>
        public void MoveToFlightsHistory()
        {
            using (SqlConnection con = new SqlConnection(FlightCenterConfig.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("MOVE_TO_FLIGHTS_HISTORY", con))
                {
                    cmd.Connection.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }

            }
        }
    }
}

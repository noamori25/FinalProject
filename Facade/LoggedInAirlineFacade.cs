using ProjectManagmentSystem.BLL;
using ProjectManagmentSystem.Exceptions;
using ProjectManagmentSystem.Facade.Interfaces;
using ProjectManagmentSystem.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagmentSystem.Facade
{
    /// <summary>
    /// This class represents options of airline company user
    /// </summary>
    public class LoggedInAirlineFacade : AnonymousUserFacade, ILoggedInAirlineFacade
    {
        /// <summary>
        /// Cnacel specific flight of this current airline comapny user
        /// </summary>
        /// <param name="token"></param>
        /// <param name="flight"></param>
        public void CancelFlight(LoginToken<AirlineCompany> token, Flight flight)
        {
            if (token != null && token.User != null)
            {
                _flightDAO.Remove(flight);
            }
        }

        /// <summary>
        /// Change password to this current airline company user
        /// </summary>
        /// <param name="token"></param>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        public void ChangeMyPassword(LoginToken<AirlineCompany> token, string oldPassword, string newPassword)
        {
            if (token != null && token.User != null)
            {
                if (token.User.Password == oldPassword)
                {
                    token.User.Password = newPassword;
                    _airlineDAO.Update(token.User);
                }
                else
                {
                    throw new WrongPasswordException($"wrong old password ({oldPassword})");
                }

            }

        }

        /// <summary>
        /// Add new flight of this current airline company user
        /// </summary>
        /// <param name="token"></param>
        /// <param name="flight"></param>
        /// <returns>flight Id</returns>
        public long CreateFlight(LoginToken<AirlineCompany> token, Flight flight)
        {
            if (token != null && token.User != null)
            {
                return flight.Id = _flightDAO.Add(flight);
            }
            return 0;
        }

        /// <summary>
        /// Search all flights of this
        /// current airline company user
        /// </summary>
        /// <param name="token"></param>
        /// <returns>list of flights</returns>
        public IList<Flight> GetAllFlights(LoginToken<AirlineCompany> token)
        {
            if (token != null && token.User != null)
            {
                List<Flight> AllFlights = _flightDAO.GetAll().ToList();
                return AllFlights;
            }
            return null;
        }

        /// <summary>
        /// Search all tickets of this current airline company user
        /// </summary>
        /// <param name="token"></param>
        /// <returns>list of tickets</returns>
        public IList<Ticket> GetAllMyTickets(LoginToken<AirlineCompany> token)
        {
            if (token != null && token.User != null)
            {
                List<Ticket> AllTickets = _ticketDAO.GetAllTicketsByAirlineCompany(token.User.Id).ToList();
                return AllTickets;
            }
            return null;
        }

        /// <summary>
        /// Update details of this current airline company user
        /// </summary>
        /// <param name="token"></param>
        /// <param name="airline"></param>
        public void ModifyAirlineDetails(LoginToken<AirlineCompany> token, AirlineCompany airline)
        {

            if (token != null && token.User != null)
            {
                _airlineDAO.Update(airline);
            }
        }

        /// <summary>
        /// Update specific flight of this current airline company user
        /// </summary>
        /// <param name="token"></param>
        /// <param name="flight"></param>
        public void UpdateFlight(LoginToken<AirlineCompany> token, Flight flight)
        {
            if (token != null && token.User != null)
            {
                Flight fli = GetFlightById(flight.Id);
                if (fli != null)
                    _flightDAO.Update(flight);
                else
                {
                    throw new IncorrectIdException($"{flight.Id} does not exist");
                }
            }
        }

        /// <summary>
        /// Search tickets by specific flight of this current airline company user
        /// </summary>
        /// <param name="token"></param>
        /// <param name="flightId"></param>
        /// <returns>list of tickets</returns>
        public IList<Ticket> GetAllTicketsByFlight(LoginToken<AirlineCompany> token, long flightId)
        {
            if (token != null && token.User != null)
            {
                Flight flight = _flightDAO.Get(flightId);
                if (flight != null)
                {
                    List<Ticket> TicketsByFlight = _ticketDAO.GetAllTicketsByFlight(flightId).ToList();
                    return TicketsByFlight;
                }
                else
                {
                    throw new IncorrectIdException($"{flightId} does not exist");
                }

            }
            return null;
        }
    }
}

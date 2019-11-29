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
    /// This class represents options of customer user
    /// </summary>
    public class LoggedInCustomerFacade : AnonymousUserFacade, ILoggedInCustomerFacade
    {
        /// <summary>
        /// Delete a ticket that bought by this current customer user
        /// </summary>
        /// <param name="token"></param>
        /// <param name="ticket"></param>
        public void CancelTicket(LoginToken<Customer> token, Ticket ticket)
        {
            if (token != null && token.User != null)
            {
                _ticketDAO.Remove(ticket);
            }
        }

        /// <summary>
        /// Search flights of this current customer user
        /// </summary>
        /// <param name="token"></param>
        /// <returns>list of flights</returns>
        public IList<Flight> GetAllMyFlights(LoginToken<Customer> token)
        {
            if (token != null && token.User != null)
            {
                IList<Flight> AllMyFlights = _flightDAO.GetFlightsByCustomer(token.User);
                return AllMyFlights;
            }
            return null;
        }

        /// <summary>
        /// Purcase ticket by this current customer user
        /// </summary>
        /// <param name="token"></param>
        /// <param name="flight"></param>
        /// <returns>ticket object</returns>
        public Ticket PurchaseTicket(LoginToken<Customer> token, Flight flight)
        {
            if (flight.RemainingTickets > 0)
            {
                Ticket ticket = new Ticket(flight.Id, token.User.Id);
                if (token != null && token.User != null)
                {
                    try
                    {
                        ticket.Id = _ticketDAO.Add(ticket);
                    }
                    catch (Exception e)
                    {
                        throw new UserAlreadyExistException($"{token.User.FirstName} {token.User.LastName} already has ticket from this flight");
                    }

                    return ticket;
                }
            }

            throw new TicketsSoldOutException($"All the tickets of {flight} sold out");
        }

        /// <summary>
        /// Delete the account of this current customer user
        /// </summary>
        /// <param name="token"></param>
        /// <param name="customer"></param>
        public void DeleteMyAccount(LoginToken<Customer> token, Customer customer)
        {
            if (token != null && token.User != null)
            {
                _customerDAO.Remove(customer);
            }
        }

        /// <summary>
        /// Get all tickets of this current customer user
        /// </summary>
        /// <param name="token"></param>
        /// <param name="customer"></param>
        /// /// <returns>list of ticket object</returns>
        public IList<Ticket> GetAllMyTickets(LoginToken<Customer> token, Customer customer)
        {
            if (token != null && token.User != null)
            {
                return _ticketDAO.GetTicketsByCustomer(customer);
            }
            return null;
        }
    }
}

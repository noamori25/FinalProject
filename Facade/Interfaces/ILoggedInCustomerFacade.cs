using ProjectManagmentSystem.BLL;
using ProjectManagmentSystem.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagmentSystem.Facade.Interfaces
{
    interface ILoggedInCustomerFacade
    {
        IList<Flight> GetAllMyFlights(LoginToken<Customer> token);
        Ticket PurchaseTicket(LoginToken<Customer> token, Flight flight);
        void CancelTicket(LoginToken<Customer> token, Ticket ticket);
        void DeleteMyAccount(LoginToken<Customer> token, Customer customer);
        IList<Ticket> GetAllMyTickets(LoginToken<Customer> token, Customer customer);
    }
}

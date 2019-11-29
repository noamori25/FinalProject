using ProjectManagmentSystem.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagmentSystem.DAO.Interfaces
{
    public interface ITicketDAO : IBasicDB<Ticket>
    {
        IList<Ticket> GetTicketsByCustomer(Customer customer);
        void MoveToTicketsHistory();
        IList<Ticket> GetAllTicketsByFlight(long flightId);
        IList<Ticket> GetAllTicketsByAirlineCompany(long airlineId);
    }
}

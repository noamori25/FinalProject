using ProjectManagmentSystem.BLL;
using ProjectManagmentSystem.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagmentSystem.Facade.Interfaces
{
    interface ILoggedInAirlineFacade
    {
        IList<Ticket> GetAllMyTickets(LoginToken<AirlineCompany> token);
        IList<Flight> GetAllFlights(LoginToken<AirlineCompany> token);
        void CancelFlight(LoginToken<AirlineCompany> token, Flight flight);
        long CreateFlight(LoginToken<AirlineCompany> token, Flight flight);
        void UpdateFlight(LoginToken<AirlineCompany> token, Flight flight);
        void ChangeMyPassword(LoginToken<AirlineCompany> token, string oldPassword, string newPassword);
        void ModifyAirlineDetails(LoginToken<AirlineCompany> token, AirlineCompany airline);
        IList<Ticket> GetAllTicketsByFlight(LoginToken<AirlineCompany> token, long flightId);
    }
}

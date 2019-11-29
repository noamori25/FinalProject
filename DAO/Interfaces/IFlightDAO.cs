using ProjectManagmentSystem.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagmentSystem.DAO.Interfaces
{
    public interface IFlightDAO : IBasicDB<Flight>
    {
        Dictionary<Flight, int> GetAllVacancyFlights();
        Flight GetFlightById(long id);
        IList<Flight> GetFlightByOriginCountry(long countryCode);
        IList<Flight> GetFlightsByDestinationCountry(long countryCode);
        IList<Flight> GetFlightsByDepartureDate(DateTime departureDate);
        IList<Flight> GetFlightsByLandingDate(DateTime LandingDate);
        IList<Flight> GetFlightsByCustomer(Customer customer);
        IList<Flight> GetFlightsByAirlineCompany(AirlineCompany airlineCompany);
        void MoveToFlightsHistory();
    }
}

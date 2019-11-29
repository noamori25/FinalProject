using ProjectManagmentSystem.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagmentSystem.Facade.Interfaces
{
    interface IAnonymousUserFacade
    {
        IList<AirlineCompany> GetAllAirlineCompanies();
        IList<Flight> GetAllFlights();
        Dictionary<Flight, int> GetAllVacancyFlights();
        Flight GetFlightById(long id);
        IList<Flight> GetFlightsByDepartureDate(DateTime departureDate);
        IList<Flight> GetFlightsByDestinationCountry(long countryCode);
        IList<Flight> GetFlightsByLandingDate(DateTime departureDate);
        IList<Flight> GetFlightsByOriginCountry(long countryCode);
    }
}

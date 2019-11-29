using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManagmentSystem.POCO;

namespace ProjectManagmentSystem.DAO.Interfaces
{
    public interface IAirlineDAO : IBasicDB<AirlineCompany>
    {
        AirlineCompany GetAirlineByUserName(string name);
        IList<AirlineCompany> GetAllAirlinesByCountry(long countryId);
        void Update(AirlineCompany airline);
    }
}

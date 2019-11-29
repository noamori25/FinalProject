using ProjectManagmentSystem.BLL;
using ProjectManagmentSystem.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagmentSystem.Facade.Interfaces
{
    interface ILoggedInAdministratorFacade
    {
        long CreateNewAirLine(LoginToken<Administrator> token, AirlineCompany airline);
        void UpdateAirlineDetails(LoginToken<Administrator> token, AirlineCompany airline);
        void RemoveAirline(LoginToken<Administrator> token, AirlineCompany airline);
        long CreateNewCustomer(LoginToken<Administrator> token, Customer customer);
        void UpdateCustomerDetails(LoginToken<Administrator> token, Customer customer);
        void RemoveCustomer(LoginToken<Administrator> token, Customer customer);
        Country GetCountryByName(LoginToken<Administrator> token, string name);
        long CreateNewCountry(LoginToken<Administrator> token, Country c);
        AirlineCompany GetAirlineByUserName(LoginToken<Administrator> token, string userName);
        void DeleteCountry(LoginToken<Administrator> token, Country c);
        List<Country> GetAllCountries(LoginToken<Administrator> token);
        List<Customer> GetAllCustomers(LoginToken<Administrator> token);
    }
}

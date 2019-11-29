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
    /// This class represents options of administrator user (with login)
    /// </summary>
    public class LoggedInAdministratorFacade : AnonymousUserFacade, ILoggedInAdministratorFacade
    {
        /// <summary>
        /// Create new airline
        /// </summary>
        /// <param name="token"></param>
        /// <param name="airline"></param>
        /// <returns>airline Id</returns>
        public long CreateNewAirLine(LoginToken<Administrator> token, AirlineCompany airline)
        {
            if (token != null && token.User != null)
            {
                return airline.Id = _airlineDAO.Add(airline);
            }
            return 0;
        }

        /// <summary>
        /// Create new customer
        /// </summary>
        /// <param name="token"></param>
        /// <param name="customer"></param>
        /// <returns>customer Id</returns>
        public long CreateNewCustomer(LoginToken<Administrator> token, Customer customer)
        {
            if (token != null && token.User != null)
            {
                return customer.Id = _customerDAO.Add(customer);
            }
            return 0;
        }

        /// <summary>
        /// Delete specific airline
        /// </summary>
        /// <param name="token"></param>
        /// <param name="airline"></param>
        public void RemoveAirline(LoginToken<Administrator> token, AirlineCompany airline)
        {
            if (token != null && token.User != null)
            {
                _airlineDAO.Remove(airline);
            }
        }

        /// <summary>
        /// Delete specific customer
        /// </summary>
        /// <param name="token"></param>
        /// <param name="customer"></param>
        public void RemoveCustomer(LoginToken<Administrator> token, Customer customer)
        {
            if (token != null && token.User != null)
            {
                _customerDAO.Remove(customer);
            }
        }

        /// <summary>
        /// Update dettaild of specific airline
        /// </summary>
        /// <param name="token"></param>
        /// <param name="airline"></param>
        public void UpdateAirlineDetails(LoginToken<Administrator> token, AirlineCompany airline)
        {
            if (token != null && token.User != null)
            {
                AirlineCompany air = _airlineDAO.Get(airline.Id);
                if (air != null)
                    _airlineDAO.Update(airline);
                else
                {
                    throw new IncorrectIdException($"{airline.Id} does not exsit");
                }

            }
        }

        /// <summary>
        /// Update details of specific customer
        /// </summary>
        /// <param name="token"></param>
        /// <param name="customer"></param>
        public void UpdateCustomerDetails(LoginToken<Administrator> token, Customer customer)
        {
            if (token != null && token.User != null)
            {
                Customer cus = _customerDAO.Get(customer.Id);
                if (cus != null)
                    _customerDAO.Update(customer);
                else
                {
                    throw new IncorrectIdException($"{cus.Id} does not exsit");
                }
            }
        }

        /// <summary>
        /// Search country by name
        /// </summary>
        /// <param name="token"></param>
        /// <param name="name"></param>
        /// <returns>country object</returns>
        public Country GetCountryByName(LoginToken<Administrator> token, string name)
        {
            if (token != null && token.User != null)
            {
                Country c = _countryDAO.GetCountryByName(name);
                return c;
            }
            return null;
        }

        /// <summary>
        /// Create new country
        /// </summary>
        /// <param name="token"></param>
        /// <param name="c"></param>
        /// <returns>country Id</returns>
        public long CreateNewCountry(LoginToken<Administrator> token, Country c)
        {
            if (token != null && token.User != null)
            {
                return c.Id = _countryDAO.Add(c);
            }
            return 0;
        }

        /// <summary>
        /// Search airline by user name
        /// </summary>
        /// <param name="token"></param>
        /// <param name="userName"></param>
        /// <returns>airline company object</returns>
        public AirlineCompany GetAirlineByUserName(LoginToken<Administrator> token, string userName)
        {
            if (token != null && token.User != null)
            {
                AirlineCompany a = _airlineDAO.GetAirlineByUserName(userName);
                return a;
            }
            return null;
        }

        /// <summary>
        /// Delete country if needed throw exception
        /// </summary>
        /// <param name="token"></param>
        /// <param name="c"></param>
        public void DeleteCountry(LoginToken<Administrator> token, Country c)
        {
            if (token != null && token.User != null)
            {
                if (_airlineDAO.GetAllAirlinesByCountry(c.Id).Count == 0 && _flightDAO.GetFlightByOriginCountry(c.Id).Count == 0
                    && _flightDAO.GetFlightsByDestinationCountry(c.Id).Count == 0)
                {
                    _countryDAO.Remove(c);
                }
                else
                {
                    throw new CountryInUseException($"There are flights and airline are relevant to {c}");
                }
            }
        }

        /// <summary>
        /// Get All countries
        /// </summary>
        /// <param name="token"></param>
        /// <returns>list of countries</returns>
        public List<Country> GetAllCountries(LoginToken<Administrator> token)
        {
            if (token != null && token.User != null)
            {
                return (List<Country>)_countryDAO.GetAll();
            }
            return null;
        }

        /// <summary>
        /// Get All customers
        /// </summary>
        /// <param name="token"></param>
        /// <returns>list of customers</returns>
        public List<Customer> GetAllCustomers(LoginToken<Administrator> token)
        {
            if (token != null && token.User != null)
            {
                return (List<Customer>)_customerDAO.GetAll();
            }
            return null;
        }
    }
}

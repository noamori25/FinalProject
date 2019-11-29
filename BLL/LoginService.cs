using ProjectManagmentSystem.BLL.Intefaces;
using ProjectManagmentSystem.DAO;
using ProjectManagmentSystem.DAO.Interfaces;
using ProjectManagmentSystem.Exceptions;
using ProjectManagmentSystem.POCO;
using ProjectManagmentSystem.POCO.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagmentSystem.BLL
{
    public class LoginService : ILoginService
    {
        private IAirlineDAO _airlineDAO;
        private ICustomerDAO _customerDAO;
        private Dictionary<Type, Func<string, string, ILoginToken>> mapTypeLoginFunction;


        public LoginService()
        {
            _airlineDAO = new AirlineDAOMSSQL();
            _customerDAO = new CustomerDAOMSSQL();
            mapTypeLoginFunction = new Dictionary<Type, Func<string, string, ILoginToken>>();
            AddToDictionary();
        }

        /// <summary>
        /// Generic function to check if the user exist
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="loginToken"></param>
        /// <returns>true or false if login succeed</returns>
        public bool TryLogin<T>(string userName, string password, out ILoginToken loginToken) where T : IUser
        {
            loginToken = mapTypeLoginFunction[typeof(T)].Invoke(userName, password);
            if (loginToken != null)
            {
                return true;
            }
            return false;
        }


        private bool TryAdminLogin(string userName, string password, out LoginToken<Administrator> token)
        {
            if (userName == FlightCenterConfig.ADMIN_USER && password == FlightCenterConfig.ADMIN_PASSWORD)
            {
                token = new LoginToken<Administrator>();
                token.User = new Administrator() { UserName = userName, Password = password };
                return true;

            }
            token = null;
            return false;
        }

        private bool TryAirlineLogin(string userName, string password, out LoginToken<AirlineCompany> token)
        {
            AirlineCompany Airline_User = _airlineDAO.GetAirlineByUserName(userName);

            if (Airline_User != null)
            {
                if (Airline_User.Password == password)
                {
                    token = new LoginToken<AirlineCompany>();
                    token.User = Airline_User;
                    return true;
                }
                throw new WrongPasswordException($"{password} does not match");
            }
            token = null;
            return false;
        }

        private bool TryCustomerLogin(string userName, string password, out LoginToken<Customer> token)
        {
            Customer Customer_User = _customerDAO.GetCustomerByUserName(userName);

            if (Customer_User != null)
            {
                if (Customer_User.Password == password)
                {
                    token = new LoginToken<Customer>();
                    token.User = Customer_User;
                    return true;
                }
                throw new WrongPasswordException($"{password} does not match");
            }

            token = null;
            return false;
        }

        private void AddToDictionary()
        {
            mapTypeLoginFunction.Add(typeof(Administrator),
                (string userName, string password) =>
                {
                    if (TryAdminLogin(userName, password, out LoginToken<Administrator> token))
                    {
                        return token;
                    }
                    return null;
                });
            mapTypeLoginFunction.Add(typeof(Customer),
                (string userName, string password) =>
                {
                    if (TryCustomerLogin(userName, password, out LoginToken<Customer> token))
                    {
                        return token;
                    }
                    return null;
                });
            mapTypeLoginFunction.Add(typeof(AirlineCompany),
                (string userName, string password) =>
                {
                    if (TryAirlineLogin(userName, password, out LoginToken<AirlineCompany> token))
                    {
                        return token;
                    }
                    return null;
                });
        }
    }
}

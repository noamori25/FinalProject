using ProjectManagmentSystem.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Configuration;
using ProjectManagmentSystem.DAO;
using ProjectManagmentSystem.Facade;
using ProjectManagmentSystem.BLL.Intefaces;
using ProjectManagmentSystem.POCO;

namespace ProjectManagmentSystem.BLL
{
    public sealed class FlyingCenterSystem
    {
        private static FlyingCenterSystem _instance;
        private static object _key = new object();
        private LoginService login;

        private Timer _timer;


        private FlyingCenterSystem()
        {
            login = new LoginService();
            _timer = new Timer(new TimerCallback(timer_Elapsed), null, 5000, 60 * 60 * 1000);
        }

        private void timer_Elapsed(object sender)
        {
            string time = ConfigurationManager.AppSettings["ClearHistoryTime"].ToString();
            int hour = Convert.ToInt32(time);
            int now = DateTime.Now.Hour;
            if (now >= hour)
            {
                TicketDAOMSSQL t = new TicketDAOMSSQL();
                FlightDAOMSSQL f = new FlightDAOMSSQL();
                t.MoveToTicketsHistory();
                f.MoveToFlightsHistory();
            }
        }

        /// <summary>
        /// Create one instance only if it null 
        /// </summary>
        /// <returns></returns>
        public static FlyingCenterSystem GetInstance()
        {
            if (_instance == null)
            {
                lock (_key)
                {
                    if (_instance == null)
                    {
                        _instance = new FlyingCenterSystem();
                    }
                }
            }
            return _instance;
        }

        /// <summary>
        /// Try to login 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>ILoginToken</returns>
        public ILoginToken Login(string username, string password)
        {
            if (login.TryLogin<Administrator>(username, password, out ILoginToken loginTokenAd))
            {
                return loginTokenAd;
            }
            if (login.TryLogin<Customer>(username, password, out ILoginToken loginTokenC))
            {
                return loginTokenC;
            }
            if (login.TryLogin<AirlineCompany>(username, password, out ILoginToken loginTokenAi))
            {
                return loginTokenAi;
            }
            throw new UserNotFoundException($"user name:{username} and password:{password} Incompatible");
        }

        /// <summary>
        /// Create facade by checikng the type
        /// </summary>
        /// <param name="token"></param>
        /// <returns>Facde with options</returns>
        public FacadeBase GetFacade(ILoginToken token)
        {
            if (token == null)
            {
                return new AnonymousUserFacade();
            }
            if (token.GetType() == typeof(LoginToken<Administrator>))
            {
                return new LoggedInAdministratorFacade();
            }
            if (token.GetType() == typeof(LoginToken<Customer>))
            {
                return new LoggedInCustomerFacade();
            }
            if (token.GetType() == typeof(LoginToken<AirlineCompany>))
            {
                return new LoggedInAirlineFacade();
            }
            return new AnonymousUserFacade();
        }

    }
}

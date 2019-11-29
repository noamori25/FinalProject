using ProjectManagmentSystem.POCO.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagmentSystem.POCO
{
    /// <summary>
    /// This class represents Airline company object
    /// </summary>
    public class AirlineCompany : IPoco, IUser
    {
        public long Id;
        public string AirlineName;
        public string UserName;
        public string Password;
        public long CountryCode;

        public AirlineCompany()
        {

        }

        public AirlineCompany(string airlineName, string userName, string password, long countryCode)
        {
            this.AirlineName = airlineName;
            this.UserName = userName;
            this.Password = password;
            this.CountryCode = countryCode;
        }

        /// <summary>
        /// Override the real == operator
        /// compare between two objects of Airline company type 
        /// </summary>
        /// <param name="a1"></param>
        /// <param name="a2"></param>
        /// <returns>true or false</returns>
        public static bool operator ==(AirlineCompany a1, AirlineCompany a2)
        {
            if (ReferenceEquals(a1, null) && ReferenceEquals(a2, null))
            {
                return true;
            }
            if (ReferenceEquals(a1, null) || ReferenceEquals(a2, null))
            {
                return false;
            }
            if (a1.Id == a2.Id)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Override the real != operator
        /// compare between two objects of Airline company type
        /// </summary>
        /// <param name="a1"></param>
        /// <param name="a2"></param>
        /// <returns>true or false</returns>
        public static bool operator !=(AirlineCompany a1, AirlineCompany a2)
        {
            return !(a1 == a2);

        }

        /// <summary>
        /// Override the real Equals function
        /// compare between two objects of Airline company type by Id (this and other)
        /// </summary>
        /// <param name="a1"></param>
        /// <param name="a2"></param>
        /// <returns>true or false</returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (obj is AirlineCompany)
            {
                AirlineCompany a = obj as AirlineCompany;
                return this.Id == a.Id;
            }
            return false;
        }

        /// <summary>
        /// Override the real function Get hash code
        ///return the Id of this current airline company object
        /// </summary>
        /// <returns>this current airline company Id</returns>
        public override int GetHashCode()
        {
            int id = (int)this.Id;
            return id;
        }

        /// <summary>
        /// Override the real function To string
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            return $"Airline id {Id}, Airline name {AirlineName}," +
                $" Airline user name {UserName}, Airline password {Password}," +
                $" Airline country code {CountryCode}";
        }
    }
}

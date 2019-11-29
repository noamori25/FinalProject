using ProjectManagmentSystem.POCO.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagmentSystem.POCO
{
    /// <summary>
    /// This class represents Country object
    /// </summary>
    public class Country : IPoco
    {
        public long Id;
        public string CountryName;

        public Country()
        {

        }
        public Country(string countryName)
        {
            this.CountryName = countryName;
        }

        /// <summary>
        /// Override the real == operator
        /// compare between two objects of Country type
        /// </summary>
        /// <param name="c1"></param>
        /// <param name="c2"></param>
        /// <returns>true or false</returns>
        public static bool operator ==(Country c1, Country c2)
        {
            if (ReferenceEquals(c1, null) && ReferenceEquals(c2, null))
            {
                return true;
            }
            if (ReferenceEquals(c1, null) || ReferenceEquals(c2, null))
            {
                return false;
            }
            if (c1.Id == c2.Id)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Override the real != operator
        /// compare between two objects of Country type
        /// </summary>
        /// <param name="c1"></param>
        /// <param name="c2"></param>
        /// <returns>true or false</returns>
        public static bool operator !=(Country c1, Country c2)
        {
            return !(c1 == c2);

        }

        /// <summary>
        /// Override the real Equals function
        /// compare between two objects of Country type by Id (this and other)
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>true or false</returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (obj is Country)
            {
                Country c = obj as Country;
                return this.Id == c.Id;
            }
            return false;
        }

        /// <summary>
        /// Override the real function Get hash code
        ///return the Id of this current Country object
        /// </summary>
        /// <returns>this current country Id</returns>
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
            return $"Id {Id}, Country Name {CountryName}";
        }
    }
}

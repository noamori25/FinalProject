using ProjectManagmentSystem.POCO.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagmentSystem.POCO
{
    /// <summary>
    /// This class represents Flight object
    /// </summary>
    public class Flight : IPoco
    {
        public long Id;
        public long AirlineCompanyId;
        public long OriginCountryCode;
        public long DestinationCountryCode;
        public DateTime DepartureTime;
        public DateTime LandingTime;
        public int RemainingTickets;

        public Flight()
        {

        }

        public Flight(long airlineCompanyId, long originCountryCode, long destinationCountryCode,
            DateTime departureTime, DateTime landingTime, int remainingTickets)
        {
            this.AirlineCompanyId = airlineCompanyId;
            this.OriginCountryCode = originCountryCode;
            this.DestinationCountryCode = destinationCountryCode;
            this.DepartureTime = departureTime;
            this.LandingTime = landingTime;
            this.RemainingTickets = remainingTickets;
        }

        /// <summary>
        /// Override the real == operator
        /// compare between two objects of Flight type
        /// </summary>
        /// <param name="f1"></param>
        /// <param name="f2"></param>
        /// <returns>true or false</returns>
        public static bool operator ==(Flight f1, Flight f2)
        {
            if (ReferenceEquals(f1, null) && ReferenceEquals(f2, null))
            {
                return true;
            }
            if (ReferenceEquals(f1, null) || ReferenceEquals(f2, null))
            {
                return false;
            }
            if (f1 == f2)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Override the real != operator 
        /// compare between two objects of Flight type
        /// </summary>
        /// <param name="f1"></param>
        /// <param name="f2"></param>
        /// <returns>true or false</returns>
        public static bool operator !=(Flight f1, Flight f2)
        {
            return !(f1 == f2);

        }
        /// <summary>
        /// Override the real Equals function
        /// compare between two objects of Flight type by Id (this and other)
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>true or false</returns>
        public override bool Equals(object obj)
        {

            if (obj == null)
                return false;
            if (obj is Flight)
            {
                Flight f = obj as Flight;
                return this.Id == f.Id;
            }
            return false;
        }

        /// <summary>
        /// Override the real function Get hash code
        ///return the Id of this current Flight object
        /// </summary>
        /// <returns>this current Flight Id</returns>
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
            return $"Id {Id}, Airline company id {AirlineCompanyId}, " +
                $"Origin country code {OriginCountryCode}, " +
                $"Destination country code {DestinationCountryCode}," +
                $" Departure time {DepartureTime.ToShortDateString()}, " +
                $"Landing time {LandingTime.ToShortDateString()}," +
                $" remaining tickets {RemainingTickets}";
        }

    }
}

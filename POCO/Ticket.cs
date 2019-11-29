using ProjectManagmentSystem.POCO.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagmentSystem.POCO
{
    /// <summary>
    /// This class represents Ticket object
    /// </summary>
    public class Ticket : IPoco
    {
        public long Id;
        public long FlightId;
        public long CustomerId;

        public Ticket()
        {

        }

        public Ticket(long flightId, long customerId)
        {
            this.FlightId = flightId;
            this.CustomerId = customerId;
        }
        /// <summary>
        /// Override the real == operator
        /// compare between two objects of Ticket type
        /// </summary>
        /// <param name="f1"></param>
        /// <param name="f2"></param>
        /// <returns>true or false</returns>
        public static bool operator ==(Ticket t1, Ticket t2)
        {
            if (ReferenceEquals(t1, null) || ReferenceEquals(t2, null))
            {
                return false;
            }
            if (t1 == t2)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// Override the real != operator 
        /// compare between two objects of Ticket type
        /// </summary>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        /// <returns>true or false</returns>
        public static bool operator !=(Ticket t1, Ticket t2)
        {
            return !(t1 == t2);

        }
        /// <summary>
        /// Override the real Equals function
        /// compare between two objects of Ticket type by Id (this and other)
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>true or false</returns>
        public override bool Equals(object obj)
        {

            if (obj == null)
                return false;
            if (obj is Ticket)
            {
                Ticket t = obj as Ticket;
                return this.Id == t.Id;
            }
            return false;
        }

        /// <summary>
        /// Override the real function Get hash code
        ///return the Id of this current Ticket object
        /// </summary>
        /// <returns>this current Ticket Id</returns>
        public override int GetHashCode()
        {
            return (int)this.Id;
        }
        /// <summary>
        /// Override the real function To string
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            return $"Ticket Id = {Id}, Flight Id = {FlightId}, Customer Id = {CustomerId}";
        }
    }
}

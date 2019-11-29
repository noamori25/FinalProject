using ProjectManagmentSystem.POCO.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagmentSystem.POCO
{
    /// <summary>
    /// This class represents Customer object
    /// </summary>
    public class Customer : IPoco, IUser
    {
        public long Id;
        public string FirstName;
        public string LastName;
        public string UserName;
        public string Password;
        public string Address;
        public string PhoneNo;
        public string CreditCardNumber;

        public Customer()
        {

        }

        public Customer(string firstName, string lastName, string userName, string password, string address, string phoneNo, string creditCardNumber)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.UserName = userName;
            this.Password = password;
            this.Address = address;
            this.PhoneNo = phoneNo;
            this.CreditCardNumber = creditCardNumber;
        }

        /// <summary>
        /// Override the real == operator
        /// compare between two objects of Customer type 
        /// </summary>
        /// <param name="c1"></param>
        /// <param name="c2"></param>
        /// <returns>true or false</returns>
        public static bool operator ==(Customer c1, Customer c2)
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
        /// compare between teo objects of Customer type
        /// </summary>
        /// <param name="c1"></param>
        /// <param name="c2"></param>
        /// <returns>true or false</returns>
        public static bool operator !=(Customer c1, Customer c2)
        {
            return !(c1 == c2);

        }

        /// <summary>
        /// Override the real Equals function
        /// compare between two objects of Customer type by Id (this and other)
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>true or false</returns>
        public override bool Equals(object obj)
        {

            if (obj == null)
                return false;
            if (obj is Customer)
            {
                Customer c = obj as Customer;
                return this.Id == c.Id;
            }
            return false;
        }

        /// <summary>
        /// Override the real function Get hash code
        ///return the Id of this current Customer object
        /// </summary>
        /// <returns>this current customer Id</returns>
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
            return $"Id {Id}, First Name {FirstName}, Last Name {LastName}, User Name {UserName}" +
                $"Password {Password}, Address {Address}, Phone Number {PhoneNo}," +
                $" Credit Card {CreditCardNumber}";
        }
    }
}

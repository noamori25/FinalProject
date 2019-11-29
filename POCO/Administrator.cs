using ProjectManagmentSystem.POCO.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagmentSystem.POCO
{
    /// <summary>
    /// This class represents Administrator object
    /// </summary>
    public class Administrator : IUser
    {
        public string UserName;
        public string Password;

        public Administrator()
        {

        }


        public Administrator(string userName, string password)
        {
            this.UserName = userName;
            this.Password = password;
        }


        /// <summary>
        /// Override the real To string function
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            return $"Administrator User: {UserName} {Password}";
        }

    }
}

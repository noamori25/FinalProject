using ProjectManagmentSystem.BLL.Intefaces;
using ProjectManagmentSystem.POCO.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagmentSystem.BLL
{
    public class LoginToken<T> : ILoginToken where T : IUser
    {
        public T User { get; set; }
    }
}

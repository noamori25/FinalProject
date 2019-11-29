using ProjectManagmentSystem.POCO.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagmentSystem.BLL.Intefaces
{
    public interface ILoginService
    {
        bool TryLogin<T>(string userName, string password, out ILoginToken token) where T : IUser;
    }
}

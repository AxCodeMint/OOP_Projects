using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;
using Administracao_Utilizadores.Models;

namespace Administracao_Utilizadores.Interfaces
{
    internal interface ISession
    {
        bool IsLogged { get; }
        User User { get; }

        Session Login(CredentialsModel credentials);

        void Logout();
    }
}

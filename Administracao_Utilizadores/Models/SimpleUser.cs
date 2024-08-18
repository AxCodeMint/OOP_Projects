using Administracao_Utilizadores.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administracao_Utilizadores.Models
{
    internal class SimpleUser : User
    {
        public SimpleUser(string firstName, string lastName, DateTime birthDate, string email, string phone, string username, string password)
            : base(EnumRole.SimpleUser, firstName, lastName, birthDate, email, phone, username, password)
        {
        }

        public SimpleUser()
        {

        }
    }
}

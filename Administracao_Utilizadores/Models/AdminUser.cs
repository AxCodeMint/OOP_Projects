using Administracao_Utilizadores.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administracao_Utilizadores.Models
{
    internal class AdminUser : User
    {
        public AdminUser(string firstName, string lastName, DateTime birthDate, string email, string phone, string username, string password)
            : base(EnumRole.Admin, firstName, lastName, birthDate, email, phone, username, password)
        {
        }

        public AdminUser()
        {

        }
    }
}

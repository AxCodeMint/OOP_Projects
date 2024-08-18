using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administracao_Utilizadores.Models
{
    public class CredentialsModel
    {
        public string Username { get; private set; }
        public string Password { get; private set; }

        public CredentialsModel(string username, string pass)
        {
            Username = username;
            Password = pass;
        }
    }
}

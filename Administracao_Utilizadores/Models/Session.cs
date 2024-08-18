using Administracao_Utilizadores.Data.Interfaces;
using Administracao_Utilizadores.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administracao_Utilizadores.Models
{
    public class Session : ISession
    {
        private IUserRepository _userRepository;

        public bool IsLogged { get; private set; }
        public User User { get; private set; }

        public Session(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Session Login(CredentialsModel credentials)
        {
            List<User> users = _userRepository.GetAll();

            User userFound = users.FirstOrDefault(x => x.Username == credentials.Username && x.Password == credentials.Password);
            User = userFound;
            IsLogged = userFound != null;
            return this;
        }

        public void Logout()
        {
            IsLogged = false;
            User = null;
        }
    }
}


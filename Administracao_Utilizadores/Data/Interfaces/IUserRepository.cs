using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Administracao_Utilizadores.Models;

namespace Administracao_Utilizadores.Data.Interfaces
{
    public interface IUserRepository
    {
        void SeedUsers();
        List<User> GetAll();
        User GetById(int id);
        int GetLastId();
        List<User> GetUsersByName(string name);
        User GetByUsername(string username);
        User GetByEmail(string email);
        void AddUser(User user);
        void UpdateUser(User user);
    }
}

using Administracao_Utilizadores.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Administracao_Utilizadores.Models;

namespace Administracao_Utilizadores.Data
{
    public class UserRepository : IUserRepository
    {
        private List<User> users = new List<User>();
        private static int nextId = 1;

        public void SeedUsers()
        {
            var user01 = new PowerUser("Joao","Artur", new DateTime(1994, 05, 15), "jartur@gmail.com", "+351939567335", "jartur", "passpass");

            var user02 = new SimpleUser("Joana", "Teresa", new DateTime(1990, 08, 03), "jtersa@gmail.com", "+351969544765", "jteresa", "passpass");

            var user03 = new AdminUser("Milena","Reis", new DateTime(1985, 12, 25), "mreis@gmail.com", "+351929465613", "milena", "milena00");

            AddUser(user01);
            AddUser(user02);
            AddUser(user03);
        }

        public List<User> GetAll()
        {
            return users;
        }

        public User GetById(int id)
        {
            return users.FirstOrDefault(x => x.Id == id);
        }

        public int GetLastId()
        {
            return users.Max(x => x.Id);
        }

        public List<User> GetUsersByName(string name)
        {
            return users.Where(x => x.FirstName.Equals(name, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public User GetByUsername(string username)
        {
            return users.Where(x => x.Username.Equals(username, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
        }

        public User GetByEmail(string email)
        {
            return users.Where(x => x.Email.Equals(email, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
        }

        public void AddUser(User user)
        {
            user.SetId(nextId++);
            users.Add(user);
        }

        public void UpdateUser(User user)
        {
            User userToUpdate = users.Where(x => x.Id == user.Id).First();
            userToUpdate.Update(user.Role, user.FirstName,user.LastName, user.BirthDate, user.Email, user.Phone, user.Username, user.Password);
        }
    }
}

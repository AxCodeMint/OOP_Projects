using Administracao_Utilizadores.Enums;
using Administracao_Utilizadores.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Administracao_Utilizadores.Models
{
    public abstract class User
    {
        private int _id;
        private EnumRole _role;
        private string _firstName;
        private string _lastName;
        private DateTime _birthDate;
        private string _email;
        private string _phone;
        private string _username;
        private string _password;

        public int Id { get; private set; }

        public virtual EnumRole Role
        {
            get => _role;
            private set
            {
                if (!Enum.IsDefined(typeof(EnumRole), value))
                {
                    throw new ArgumentException("Invalid Role value.");
                }
                _role = value;
            }
        }

        public virtual string FirstName
        {
            get => _firstName;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("First Name cannot be null or empty.");
                }
                if (value.Length > 20)
                {
                    throw new ArgumentException("First Name cant have more than 20 chars.");
                }
                _firstName = value;
            }
        }

        public virtual string LastName
        {
            get => _lastName;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("last Name cannot be null or empty.");
                }
                if (value.Length > 20)
                {
                    throw new ArgumentException("Last Name cant have more than 20 chars.");
                }
                _lastName = value;
            }
        }

        public virtual DateTime BirthDate
        {
            get => _birthDate;
            private set
            {
                if (value >= DateTime.Now)
                {
                    throw new ArgumentException("BirthDate must be a past date.");
                }
                _birthDate = value;
            }
        }

        public virtual string Email
        {
            get => _email;
            private set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Contains("@") == false)
                {
                    throw new ArgumentException("Invalid email format.");
                }
                
                if (value.Length > 256)
                {
                    throw new ArgumentException("Email cant have more than 256 chars.");
                }
                _email = value;
            }
        }

        public virtual string Phone
        {
            get => _phone;
            private set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length != 9)
                {
                    throw new ArgumentException("Phone number must have 9 digits.");
                }

                _phone = value;
            }
        }

        public virtual string Username
        {
            get => _username;
            private set
            {
                if (value.Length > 20)
                {
                    throw new ArgumentException("Username must have less than 20 characters.");
                }
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Username cannot be null or empty.");
                }
                _username = value;
            }
        }

        public virtual string Password
        {
            get => _password;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Password cannot be null or empty.");
                }
                if (value.Length < 8)
                {
                    throw new ArgumentException("Password must have minimum 8 characters.");
                }
                _password = value;
            }
        }

        public User()
        {

        }

        public User(EnumRole role, string firstName, string lastName, DateTime birthDate, string email, string phone, string username, string password)
        {
            Role = role;
            FirstName = firstName;
            LastName = lastName;
            BirthDate = birthDate;
            Email = email;
            Phone = phone;
            Username = username;
            Password = password;
        }

        public void SetId(int id)
        {
            Id = id;
        }

        public void Update(EnumRole role, string firstName, string lastName, DateTime birthDate, string email, string phone, string username, string password)
        {
            Role = role;
            FirstName = firstName;
            LastName = lastName;
            BirthDate = birthDate;
            Email = email;
            Phone = phone;
            Username = username;
            Password = password;
        }
    }
}

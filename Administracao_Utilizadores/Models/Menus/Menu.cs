using Administracao_Utilizadores.Data.Interfaces;
using Administracao_Utilizadores.Interfaces;
using Administracao_Utilizadores.Utilities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administracao_Utilizadores.Models.Menus
{
    internal abstract class Menu
    {
        private readonly IUserRepository _userRepository;
        private readonly ISession _session;
        private readonly (ConsoleColor background, ConsoleColor foreground) _color;

        public Menu(IUserRepository userRepository, ISession session, (ConsoleColor background, ConsoleColor foreground) color)
        {
            _userRepository = userRepository;
            _session = session;
            _color = color;
        }

        public abstract ConsoleKeyInfo MainMenu();

        protected void ShowList()
        {
            Console.Clear();
            ConsoleUtility.WriteTitle("List of users", "ROLE: " + _session.User.Role.ToString(), fontColor: _color.foreground);
            var listQuery = _userRepository.GetAll().OrderBy(u => u.Id);
            ConsoleUtility.ShowUserList(listQuery);
        }

        #region steps

        protected virtual ReadLineInfo AskRole()
        {
            bool exit = false;
            ReadLineInfo rInfoEnum;
            do
            {
                rInfoEnum = ConsoleUtility.ReadInput(
               "Type of users\n\t 1. Admin\n\t 2. PowerUser\n\t 3. SimpleUser\n\n\n What user do you pretend to create?",
                $"\n[{_session.User.Username}] -> ");

                if (rInfoEnum.Exit)
                {
                    return rInfoEnum;
                }

                if (rInfoEnum.Text != "1" && rInfoEnum.Text != "2" && rInfoEnum.Text != "3")
                {
                    ConsoleUtility.WriteError("Invalid choice input.");
                }
                else
                {
                    exit = true;
                }

            } while (exit == false);
            return rInfoEnum;
        }

        protected virtual ReadLineInfo AskFirstName()
        {

            bool exit = false;
            ReadLineInfo rInfoName;
            do
            {
                rInfoName = ConsoleUtility.ReadInput(
                "Insert the first name:",
                 $"\n[{_session.User.Username}] -> ");

                if (rInfoName.Exit)
                {
                    return rInfoName;
                }

                if (string.IsNullOrWhiteSpace(rInfoName.Text) || rInfoName.Text.Length > 20)
                {
                    ConsoleUtility.WriteError("First name cant have more than 20 chars.");
                }
                else
                {
                    exit = true;
                }

            } while (exit == false);
            return rInfoName;
        }

        protected virtual ReadLineInfo AskLastName()
        {

            bool exit = false;
            ReadLineInfo rInfoName;
            do
            {
                rInfoName = ConsoleUtility.ReadInput(
                "Insert the last name:",
                 $"\n[{_session.User.Username}] -> ");

                if (rInfoName.Exit)
                {
                    return rInfoName;
                }

                if (string.IsNullOrWhiteSpace(rInfoName.Text) || rInfoName.Text.Length > 20)
                {
                    ConsoleUtility.WriteError("Last name cant have more than 20 chars.");
                }
                else
                {
                    exit = true;
                }

            } while (exit == false);
            return rInfoName;
        }

        protected virtual ReadLineInfo AskBirthDate()
        {

            bool exit = false;
            ReadLineInfo rInfoBirthDate;
            do
            {
                rInfoBirthDate = ConsoleUtility.ReadInput(
                "Insert the birth date (yyyy-MM-dd):",
                 $"\n[{_session.User.Username}] -> ");

                if (rInfoBirthDate.Exit)
                {
                    return rInfoBirthDate;
                }

                if (!(DateTime.TryParseExact(rInfoBirthDate.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var _)))
                {
                    ConsoleUtility.WriteError("Invalid date format.");
                }
                else
                {
                    exit = true;
                }

            } while (exit == false);
            return rInfoBirthDate;
        }

        protected virtual ReadLineInfo AskEmail()
        {
            bool exit = false;
            ReadLineInfo rInfoEmail;
            do
            {
                rInfoEmail = ConsoleUtility.ReadInput(
               "Insert the email:",
                $"\n[{_session.User.Username}] -> ");

                if (rInfoEmail.Exit)
                {
                    return rInfoEmail;
                }

                if (string.IsNullOrWhiteSpace(rInfoEmail.Text) || rInfoEmail.Text.Contains("@") == false)
                {
                    ConsoleUtility.WriteError("Invalid email format.");
                }
                else if (_userRepository.GetByEmail(rInfoEmail.Text) != null)
                {
                    ConsoleUtility.WriteError("Email already exists.");
                }
                else if (rInfoEmail.Text.Length > 256)
                {
                    ConsoleUtility.WriteError("Email cant have more than 256 chars.");
                }
                else
                {
                    exit = true;
                }

            } while (exit == false);
            return rInfoEmail;
        }

        protected virtual ReadLineInfo AskPhone()
        {
            bool exit = false;
            ReadLineInfo rInfoPhone;
            do
            {
                rInfoPhone = ConsoleUtility.ReadInput(
               "Insert the phone number:",
                $"\n[{_session.User.Username}] -> ");

                if (rInfoPhone.Exit)
                {
                    return rInfoPhone;
                }

                if (string.IsNullOrWhiteSpace(rInfoPhone.Text) || rInfoPhone.Text.Length != 9)
                {
                    ConsoleUtility.WriteError("Phone number must have 9 digits.");
                }
                else
                {
                    exit = true;
                }

            } while (exit == false);
            return rInfoPhone;
        }

        protected virtual ReadLineInfo AskUsername()
        {
            bool exit = false;
            ReadLineInfo rInfoUserName;
            do
            {
                rInfoUserName = ConsoleUtility.ReadInput(
               "Insert the username:",
                $"\n[{_session.User.Username}] -> ");

                if (rInfoUserName.Exit)
                {
                    return rInfoUserName;
                }

                if (string.IsNullOrWhiteSpace(rInfoUserName.Text) || rInfoUserName.Text.Length > 20)
                {
                    ConsoleUtility.WriteError("Username must have less than 20 characters.");
                }
                else if (_userRepository.GetByUsername(rInfoUserName.Text) != null)
                {
                    ConsoleUtility.WriteError("Username already exists.");
                }
                else
                {
                    exit = true;
                }

            } while (exit == false);
            return rInfoUserName;
        }

        protected virtual ReadLineInfo AskPassword()
        {

            bool exit = false;
            ReadLineInfo rInfoPassword;
            do
            {
                rInfoPassword = ConsoleUtility.ReadInputPasswordForm(
                "Insert the password:",
               $"\n[{_session.User.Username}] -> ");

                if (rInfoPassword.Exit)
                {
                    return rInfoPassword;
                }

                if (string.IsNullOrWhiteSpace(rInfoPassword.Text) || rInfoPassword.Text.Length > 8)
                {
                    ConsoleUtility.WriteError("Password must have 8 characters minimum.");
                }
                else
                {
                    exit = true;
                }

            } while (exit == false);
            return rInfoPassword;
        }


        #endregion

    }
}

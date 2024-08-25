using Administracao_Utilizadores.Data.Interfaces;
using Administracao_Utilizadores.Interfaces;
using Administracao_Utilizadores.Utilities;
using General_Utility;
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
            Utility.WriteTitle("List of users", "ROLE: " + _session.User.Role.ToString(), fontColor: _color.foreground);
            var listQuery = _userRepository.GetAll().OrderBy(u => u.Id);
            ConsoleUtility.ShowUserList(listQuery);
        }

        #region steps

        protected virtual ReadLineInfo AskRole()
        {
            bool exit = false;
            ReadLineInfo readInfoEnum;
            do
            {
                readInfoEnum = ConsoleUtility.ReadInput(
               "Type of users\n\t 1. Admin\n\t 2. PowerUser\n\t 3. SimpleUser\n\n\n What user do you pretend to create? Or press [ESC] to return to Menu.",
                $"\n[{_session.User.Username}] -> ");

                if (readInfoEnum.Exit)
                {
                    return readInfoEnum;
                }

                if (readInfoEnum.Text != "1" && readInfoEnum.Text != "2" && readInfoEnum.Text != "3")
                {
                    Utility.WriteError("Invalid choice input.");
                }
                else
                {
                    exit = true;
                }

            } while (exit == false);
            return readInfoEnum;
        }

        protected virtual ReadLineInfo AskFirstName()
        {
            bool exit = false;
            ReadLineInfo readInfoName;
            do
            {
                readInfoName = ConsoleUtility.ReadInput(
                "Insert the first name or press [ESC] to return to Menu:",
                 $"\n[{_session.User.Username}] -> ");

                if (readInfoName.Exit)
                {
                    return readInfoName;
                }

                if (string.IsNullOrWhiteSpace(readInfoName.Text) || readInfoName.Text.Length > 20)
                {
                    Utility.WriteError("First name cant have more than 20 chars.");
                }
                else
                {
                    exit = true;
                }

            } while (exit == false);
            return readInfoName;
        }

        protected virtual ReadLineInfo AskLastName()
        {
            bool exit = false;
            ReadLineInfo readInfoName;
            do
            {
                readInfoName = ConsoleUtility.ReadInput(
                "Insert the last name or press [ESC] to return to Menu:",
                 $"\n[{_session.User.Username}] -> ");

                if (readInfoName.Exit)
                {
                    return readInfoName;
                }

                if (string.IsNullOrWhiteSpace(readInfoName.Text) || readInfoName.Text.Length > 20)
                {
                    Utility.WriteError("Last name cant have more than 20 chars.");
                }
                else
                {
                    exit = true;
                }

            } while (exit == false);
            return readInfoName;
        }

        protected virtual ReadLineInfo AskBirthDate()
        {
            bool exit = false;
            ReadLineInfo readInfoBirthDate;
            do
            {
                readInfoBirthDate = ConsoleUtility.ReadInput(
                "Insert the birth date (yyyy-MM-dd) or press [ESC] to return to Menu:",
                 $"\n[{_session.User.Username}] -> ");

                if (readInfoBirthDate.Exit)
                {
                    return readInfoBirthDate;
                }

                if (!(DateTime.TryParseExact(readInfoBirthDate.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var _)))
                {
                    Utility.WriteError("Invalid date format.");
                }
                else
                {
                    exit = true;
                }

            } while (exit == false);
            return readInfoBirthDate;
        }

        protected virtual ReadLineInfo AskEmail()
        {
            bool exit = false;
            ReadLineInfo readInfoEmail;
            do
            {
                readInfoEmail = ConsoleUtility.ReadInput(
               "Insert the email or press [ESC] to return to Menu:",
                $"\n[{_session.User.Username}] -> ");

                if (readInfoEmail.Exit)
                {
                    return readInfoEmail;
                }

                if (string.IsNullOrWhiteSpace(readInfoEmail.Text) || readInfoEmail.Text.Contains("@") == false)
                {
                    Utility.WriteError("Invalid email format.");
                }
                else if (_userRepository.GetByEmail(readInfoEmail.Text) != null)
                {
                    Utility.WriteError("Email already exists.");
                }
                else if (readInfoEmail.Text.Length > 256)
                {
                    Utility.WriteError("Email cant have more than 256 chars.");
                }
                else
                {
                    exit = true;
                }

            } while (exit == false);
            return readInfoEmail;
        }

        protected virtual ReadLineInfo AskPhone()
        {
            bool exit = false;
            ReadLineInfo readInfoPhone;
            do
            {
                readInfoPhone = ConsoleUtility.ReadInput(
               "Insert the phone number (+XXXYYYYYYYYY) or press [ESC] to return to Menu:",
                $"\n[{_session.User.Username}] -> ");

                if (readInfoPhone.Exit)
                {
                    return readInfoPhone;
                }

                if (string.IsNullOrWhiteSpace(readInfoPhone.Text) || readInfoPhone.Text.Length != 13)
                {
                    Utility.WriteError("Phone number must have 13 digits.");
                }
                else
                {
                    exit = true;
                }

            } while (exit == false);
            return readInfoPhone;
        }

        protected virtual ReadLineInfo AskUsername()
        {
            bool exit = false;
            ReadLineInfo readInfoUserName;
            do
            {
                readInfoUserName = ConsoleUtility.ReadInput(
               "Insert the username or press [ESC] to return to Menu:",
                $"\n[{_session.User.Username}] -> ");

                if (readInfoUserName.Exit)
                {
                    return readInfoUserName;
                }

                if (string.IsNullOrWhiteSpace(readInfoUserName.Text) || readInfoUserName.Text.Length > 20)
                {
                    Utility.WriteError("Username must have less than 20 characters.");
                }
                else if (_userRepository.GetByUsername(readInfoUserName.Text) != null)
                {
                    Utility.WriteError("Username already exists.");
                }
                else
                {
                    exit = true;
                }

            } while (exit == false);
            return readInfoUserName;
        }

        protected virtual ReadLineInfo AskPassword()
        {

            bool exit = false;
            ReadLineInfo readInfoPassword;
            do
            {
                readInfoPassword = ConsoleUtility.ReadInputPasswordForm(
                "Insert the password or press [ESC] to return to Menu:",
               $"\n[{_session.User.Username}] -> ");

                if (readInfoPassword.Exit)
                {
                    return readInfoPassword;
                }

                if (string.IsNullOrWhiteSpace(readInfoPassword.Text) || readInfoPassword.Text.Length < 8)
                {
                    Utility.WriteError("Password must have 8 characters minimum.");
                }
                else
                {
                    exit = true;
                }

            } while (exit == false);
            return readInfoPassword;
        }

        #endregion
    }
}

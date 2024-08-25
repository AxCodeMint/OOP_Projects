using Administracao_Utilizadores.Data.Interfaces;
using Administracao_Utilizadores.Enums;
using Administracao_Utilizadores.Interfaces;
using Administracao_Utilizadores.Utilities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using General_Utility;

namespace Administracao_Utilizadores.Models.Menus
{
    internal class AdminUserMenu : PowerUserMenu
    {
        private readonly IUserRepository _userRepository;
        private readonly ISession _session;
        private readonly (ConsoleColor background, ConsoleColor foreground) _color;

        public AdminUserMenu(IUserRepository userRepository, ISession session, (ConsoleColor background, ConsoleColor foreground) color)
            : base(userRepository, session, color)
        {
            _userRepository = userRepository;
            _session = session;
            _color = color;
        }

        public override ConsoleKeyInfo MainMenu()
        {
            bool exit = false;
            ConsoleKeyInfo key;

            do
            {
                Console.Clear();
                Utility.WriteTitle("Administrator Menu", "ROLE: " + _session.User.Role.ToString(), fontColor: _color.foreground);
                Console.ForegroundColor = _color.foreground;
                Console.WriteLine("[1] - Create users");
                Console.WriteLine("[2] - Change data");
                Console.WriteLine("[3] - Search by Id");
                Console.WriteLine("[4] - Search by name");
                Console.WriteLine("[5] - List");
                Console.WriteLine("[Esc] - Logout");
                Console.Write($"\n[{_session.User.Username}] -> ");

                key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.Escape)
                {
                    ConsoleKeyInfo answer = Utility.WriteQuestionAndChoice("Do you really want to logout?", $"\n[{_session.User.Username}] -> ");

                    if (answer.KeyChar == '1')
                    {
                        _session.Logout();
                        Utility.WriteSucess("Logout sucessfully.");
                        exit = true;
                    }
                }
                else
                {
                    switch (key.KeyChar)
                    {
                        case '1':
                            try
                            {
                                User createUser = CreateUser();
                                if (createUser != null)
                                {
                                    _userRepository.AddUser(createUser);
                                    Utility.WriteSucess("User created successfully.");
                                }
                            }
                            catch (Exception ex)
                            {
                                Utility.WriteError(ex.Message);
                                Console.Clear();
                            }

                            break;
                        case '2':
                            try
                            {
                                User editUser = EditProfile();
                                if (editUser != null)
                                {
                                    _userRepository.UpdateUser(editUser);
                                    Utility.WriteSucess("User updated successfully.");
                                }
                            }
                            catch (Exception ex)
                            {
                                Utility.WriteError(ex.Message);
                                Console.Clear();
                            }

                            break;
                        case '3':
                            SearchById();
                            break;
                        case '4':
                            SearchByName();
                            break;
                        case '5':
                            ShowList();
                            Utility.WriteInformation();
                            break;
                        default:
                            Utility.WriteError("Invalid option.");
                            break;
                    }
                }
            }
            while (exit == false);

            Console.ResetColor();

            return key;
        }

        private User CreateUser()
        {
            Console.Clear();
            Utility.WriteTitle("Create User", "ROLE: " + _session.User.Role.ToString(), fontColor: _color.foreground);
            bool next = false;

            ReadLineInfo readInfoEnum;
            do
            {
                readInfoEnum = AskRole();
                if (readInfoEnum.Exit)
                {

                    ConsoleKeyInfo keyTypedQuestion = Utility.WriteQuestionAndChoice("Do you really want to go back to menu? Unsaved changes will be lost.", $"\n[{_session.User.Username}] -> ");
                    Console.Clear();
                    if (keyTypedQuestion.KeyChar == '1')
                    {
                        return default;
                    }
                }
                else
                {
                    next = true;
                }

            } while (next == false);

            Console.WriteLine();
            next = false;

            ReadLineInfo readInfoFirstName;
            do
            {
                readInfoFirstName = AskFirstName();
                if (readInfoFirstName.Exit)
                {

                    ConsoleKeyInfo keyTypedQuestion = Utility.WriteQuestionAndChoice("Do you really want to go back to menu? Unsaved changes will be lost.", $"\n[{_session.User.Username}] -> ");
                    Console.Clear();
                    if (keyTypedQuestion.KeyChar == '1')
                    {
                        return default;
                    }
                }
                else
                {
                    next = true;
                }
            } while (next == false);
            Console.WriteLine();
            next = false;

            ReadLineInfo readInfoLastName;
            do
            {
                readInfoLastName = AskLastName();
                if (readInfoLastName.Exit)
                {

                    ConsoleKeyInfo keyTypedQuestion = Utility.WriteQuestionAndChoice("Do you really want to go back to menu? Unsaved changes will be lost.", $"\n[{_session.User.Username}] -> ");
                    Console.Clear();
                    if (keyTypedQuestion.KeyChar == '1')
                    {
                        return default;
                    }
                }
                else
                {
                    next = true;
                }
            } while (next == false);
            Console.WriteLine();
            next = false;

            ReadLineInfo readInfoBirthDate;
            do
            {
                readInfoBirthDate = AskBirthDate();
                if (readInfoBirthDate.Exit)
                {

                    ConsoleKeyInfo keyTypedQuestion = Utility.WriteQuestionAndChoice("Do you really want to go back to menu? Unsaved changes will be lost.", $"\n[{_session.User.Username}] -> ");
                    Console.Clear();
                    if (keyTypedQuestion.KeyChar == '1')
                    {
                        return default;
                    }
                }
                else
                {
                    next = true;
                }
            } while (next == false);
            DateTime birthDate = DateTime.ParseExact(readInfoBirthDate.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            Console.WriteLine();
            next = false;

            ReadLineInfo readInfoEmail;
            do
            {
                readInfoEmail = AskEmail();
                if (readInfoEmail.Exit)
                {

                    ConsoleKeyInfo keyTypedQuestion = Utility.WriteQuestionAndChoice("Do you really want to go back to menu? Unsaved changes will be lost.", $"\n[{_session.User.Username}] -> ");
                    Console.Clear();
                    if (keyTypedQuestion.KeyChar == '1')
                    {
                        return default;
                    }
                }
                else
                {
                    next = true;
                }
            } while (next == false);
            Console.WriteLine();
            next = false;

            ReadLineInfo readInfoPhone;
            do
            {
                readInfoPhone = AskPhone();
                if (readInfoPhone.Exit)
                {

                    ConsoleKeyInfo keyTypedQuestion = Utility.WriteQuestionAndChoice("Do you really want to go back to menu? Unsaved changes will be lost.", $"\n[{_session.User.Username}] -> ");
                    Console.Clear();
                    if (keyTypedQuestion.KeyChar == '1')
                    {
                        return default;
                    }
                }
                else
                {
                    next = true;
                }
            } while (next == false);
            Console.WriteLine();
            next = false;

            ReadLineInfo readInfoUserName;
            do
            {
                readInfoUserName = AskUsername();
                if (readInfoUserName.Exit)
                {

                    ConsoleKeyInfo keyTypedQuestion = Utility.WriteQuestionAndChoice("Do you really want to go back to menu? Unsaved changes will be lost.", $"\n[{_session.User.Username}] -> ");
                    Console.Clear();
                    if (keyTypedQuestion.KeyChar == '1')
                    {
                        return default;
                    }
                }
                else
                {
                    next = true;
                }
            } while (next == false);
            Console.WriteLine();
            next = false;

            ReadLineInfo readInfoPassword;
            do
            {
                readInfoPassword = AskPassword();
                if (readInfoPassword.Exit)
                {

                    ConsoleKeyInfo keyTypedQuestion = Utility.WriteQuestionAndChoice("Do you really want to go back to menu? Unsaved changes will be lost.", $"\n[{_session.User.Username}] -> ");
                    Console.Clear();
                    if (keyTypedQuestion.KeyChar == '1')
                    {
                        return default;
                    }
                }
                else
                {
                    next = true;
                }
            } while (next == false);

            switch ((EnumRole)int.Parse(readInfoEnum.Text))
            {
                case EnumRole.Admin:
                    User userAdmin = new AdminUser(readInfoFirstName.Text, readInfoLastName.Text, birthDate, readInfoEmail.Text, readInfoPhone.Text, readInfoUserName.Text, readInfoPassword.Text);
                    return userAdmin;

                case EnumRole.PowerUser:
                    User userPowerUser = new PowerUser(readInfoFirstName.Text, readInfoLastName.Text, birthDate, readInfoEmail.Text, readInfoPhone.Text, readInfoUserName.Text, readInfoPassword.Text);
                    return userPowerUser;


                case EnumRole.SimpleUser:
                    User userSimpleUser = new SimpleUser(readInfoFirstName.Text, readInfoLastName.Text, birthDate, readInfoEmail.Text, readInfoPhone.Text, readInfoUserName.Text, readInfoPassword.Text);
                    return userSimpleUser;

                default:
                    throw new Exception("Operation not possible");
            }
        }

        private User EditProfile()
        {
            Console.Clear();
            bool exit = false;
            ConsoleKeyInfo key;
            ReadLineInfo readInfoProfileId;

            do
            {
                Console.Clear();
                Utility.WriteTitle("Edit User", "ROLE: " + _session.User.Role.ToString(), fontColor: _color.foreground);

                var listQuery = _userRepository.GetAll().OrderBy(u => u.Id);
                ConsoleUtility.ShowUserList(listQuery);
                Console.WriteLine();

                readInfoProfileId = ConsoleUtility.ReadInput(
                "Insert the id that you want to edit or press [ESC] to go back to Menu.",
                    $"\n[{_session.User.Username}] -> ");

                if (readInfoProfileId.Exit)
                {
                    return default;
                }

                if (_userRepository.GetById(int.TryParse(readInfoProfileId.Text, out var id) ? id : 0) == null)
                {
                    Utility.WriteError("Please enter a valid Id");
                }
                else
                {
                    exit = true;
                }

            } while (exit == false);

            exit = false;
            ShowList();
            User user = _userRepository.GetById(int.Parse(readInfoProfileId.Text));

            EnumRole role = user.Role;
            string firstName = user.FirstName;
            string lastName = user.LastName;
            DateTime birthDate = user.BirthDate;
            string email = user.Email;
            string phone = user.Phone;
            string username = user.Username;
            string password = user.Password;

            do
            {
                Console.Clear();
                Utility.WriteTitle("Edit User", "ROLE: " + _session.User.Role.ToString(), fontColor: _color.foreground);

                ConsoleUtility.ShowEditUser(user,
                    role != user.Role ? role.ToString() : null,
                    firstName != user.FirstName ? firstName : null,
                    lastName != user.LastName ? lastName : null,
                    birthDate != user.BirthDate ? birthDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture) : null,
                    email != user.Email ? email : null,
                    phone != user.Phone ? phone : null,
                    username != user.Username ? username : null
                    );

                Console.WriteLine("[1] - Update Role");
                Console.WriteLine("[2] - Update First Name");
                Console.WriteLine("[3] - Update Last Name");
                Console.WriteLine("[4] - Update BirthDate");
                Console.WriteLine("[5] - Update Email");
                Console.WriteLine("[6] - Update Phone");
                Console.WriteLine("[7] - Update Password");
                Console.WriteLine("[Enter] - Save");
                Console.WriteLine("[Esc] - Back to Menu");
                Console.Write($"\n[{_session.User.Username}] -> ");

                key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.Escape)
                {
                    ConsoleKeyInfo keyTypedQuestion = Utility.WriteQuestionAndChoice("Do you really want to go back to menu? Unsaved changes will be lost.", $"\n[{_session.User.Username}] -> ");

                    if (keyTypedQuestion.KeyChar == '1')
                    {
                        exit = true;
                    }
                }
                else if (key.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine();
                    Utility.WriteInformation("User about to be saved ...");
                    user.Update(role, firstName,lastName, birthDate, email, phone, username, password);
                    return user;
                }
                else
                {
                    switch (key.KeyChar)
                    {
                        case '1':

                            Console.Clear();
                            Utility.WriteTitle("Edit User", "ROLE: " + _session.User.Role.ToString(), fontColor: _color.foreground);
                            ReadLineInfo readInfoEnumRole = AskRole();
                            if (readInfoEnumRole.Exit == false)
                            {
                                role = (EnumRole)int.Parse(readInfoEnumRole.Text);
                            }
                            break;

                        case '2':

                            Console.Clear();
                            Utility.WriteTitle("Edit User", "ROLE: " + _session.User.Role.ToString(), fontColor: _color.foreground);
                            ReadLineInfo readInfoFirstName = AskFirstName();
                            if (readInfoFirstName.Exit == false)
                            {
                                firstName = readInfoFirstName.Text;
                            }
                            break;
                        
                        case '3':

                            Console.Clear();
                            Utility.WriteTitle("Edit User", "ROLE: " + _session.User.Role.ToString(), fontColor: _color.foreground);
                            ReadLineInfo readInfoLastName = AskLastName();
                            if (readInfoLastName.Exit == false)
                            {
                                lastName = readInfoLastName.Text;
                            }
                            break;

                        case '4':

                            Console.Clear();
                            Utility.WriteTitle("Edit User", "ROLE: " + _session.User.Role.ToString(), fontColor: _color.foreground);
                            ReadLineInfo readInfoBirthDate = AskBirthDate();
                            if (readInfoBirthDate.Exit == false)
                            {
                                birthDate = DateTime.ParseExact(readInfoBirthDate.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                            }
                            break;

                        case '5':

                            Console.Clear();
                            Utility.WriteTitle("Edit User", "ROLE: " + _session.User.Role.ToString(), fontColor: _color.foreground);
                            ReadLineInfo readInfoEmail = AskEmail();
                            if (readInfoEmail.Exit == false)
                            {
                                email = readInfoEmail.Text;
                            }
                            break;

                        case '6':

                            Console.Clear();
                            Utility.WriteTitle("Edit User", "ROLE: " + _session.User.Role.ToString(), fontColor: _color.foreground);
                            ReadLineInfo readInfoPhone = AskPhone();
                            if (readInfoPhone.Exit == false)
                            {
                                phone = readInfoPhone.Text;
                            }
                            break;

                        case '7':

                            Console.Clear();
                            Utility.WriteTitle("Edit User", "ROLE: " + _session.User.Role.ToString(), fontColor: _color.foreground);
                            ReadLineInfo readInfoPassword = AskPassword();
                            if (readInfoPassword.Exit == false)
                            {
                                password = readInfoPassword.Text;
                            }

                            break;

                        default:
                            Utility.WriteError("Invalid option.");
                            break;
                    }
                }
            } while (exit == false);

            return user;
        }

        private void SearchById()
        {
            bool exit = false;
            ReadLineInfo readInfo;
            do
            {
                do
                {
                    Console.Clear();
                    Utility.WriteTitle("Search by Id", "ROLE: " + _session.User.Role.ToString(), fontColor: _color.foreground);
                    readInfo = ConsoleUtility.ReadInput(
                    "Type the id that you want to search or press [ESC] to go back to Menu.",
                    $"\n[{_session.User.Username}] -> ");

                    if (readInfo.Exit)
                    {
                        return;
                    }

                    if (int.TryParse(readInfo.Text, out var _) == false)
                    {
                        Utility.WriteError("Please enter a valid integer.");
                    }
                    else
                    {
                        exit = true;
                    }

                } while (exit == false);
                exit = false;

                User user = _userRepository.GetById(int.Parse(readInfo.Text));

                if (user != null)
                {
                    ConsoleUtility.ShowUserDetails(user);
                    Utility.WriteInformation();
                    exit = true;
                }
                else
                {
                    Utility.WriteError("User not found.");
                }

            } while (exit == false);
        }
    }
}

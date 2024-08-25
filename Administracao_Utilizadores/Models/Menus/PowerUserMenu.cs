using Administracao_Utilizadores.Data.Interfaces;
using Administracao_Utilizadores.Interfaces;
using Administracao_Utilizadores.Utilities;
using General_Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administracao_Utilizadores.Models.Menus
{
    internal class PowerUserMenu : Menu
    {
        private readonly IUserRepository _userRepository;
        private readonly ISession _session;
        private readonly (ConsoleColor background, ConsoleColor foreground) _color;

        public PowerUserMenu(IUserRepository userRepository, ISession session, (ConsoleColor background, ConsoleColor foreground) color)
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
                Utility.WriteTitle("Power User Menu", "ROLE: " + _session.User.Role.ToString(), fontColor: _color.foreground);
                Console.ForegroundColor = _color.foreground;
                Console.WriteLine("[1] - Search by name");
                Console.WriteLine("[2] - List");
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
                            SearchByName();
                            break;
                        case '2':
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

        protected void SearchByName()
        {
            bool exit = false;

            do
            {
                Console.Clear();
                Utility.WriteTitle("Search By Name", "ROLE: " + _session.User.Role.ToString(), fontColor: _color.foreground);
                ReadLineInfo readInfo = ConsoleUtility.ReadInput("Type the name you want to search or press [ESC] to return to Menu.", $"\n[{_session.User.Username}] -> ");

                if (readInfo.Exit)
                {
                    break;
                }

                List<User> user = _userRepository.GetUsersByName(readInfo.Text);

                if (user != null && user.Any() == true)
                {
                    Console.WriteLine();
                    ConsoleUtility.ShowUserList(user);
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

using Administracao_Utilizadores.Data.Interfaces;
using Administracao_Utilizadores.Interfaces;
using Administracao_Utilizadores.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administracao_Utilizadores.Models.Menus
{
    internal class SimpleUserMenu : Menu
    {
        private readonly IUserRepository _userRepository;
        private readonly ISession _session;
        private readonly (ConsoleColor background, ConsoleColor foreground) _color;
        public SimpleUserMenu(IUserRepository userRepository, ISession session, (ConsoleColor background, ConsoleColor foreground) color)
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
                ConsoleUtility.WriteTitle("Simple User Menu", "ROLE: " + _session.User.Role.ToString(), fontColor: _color.foreground);
                Console.ForegroundColor = _color.foreground;
                Console.WriteLine("[1] - List");
                Console.WriteLine("[Esc] - Logout");
                Console.Write($"\n[{_session.User.Username}] -> ");

                key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.Escape)
                {
                    ConsoleKeyInfo answer = ConsoleUtility.WriteQuestionAndChoice("Do you really want to logout?", $"\n[{_session.User.Username}] -> ");

                    if (answer.KeyChar == '1')
                    {
                        _session.Logout();
                        ConsoleUtility.WriteSucess("Logout sucessfully.");
                        exit = true;
                    }
                }
                else
                {
                    switch (key.KeyChar)
                    {
                        case '1':
                            ShowList();
                            ConsoleUtility.WriteInformation();
                            break;
                        default:
                            ConsoleUtility.WriteError("Invalid option.");
                            break;
                    }
                }
            }
            while (exit == false);

            Console.ResetColor();

            return key;
        }

    }
}

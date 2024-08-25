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
    internal class LoginMenu
    {
        private readonly ISession _session;

        public LoginMenu(ISession session)
        {
            _session = session;
        }

        public ConsoleKeyInfo ShowMenu()
        {
            bool exit = false;
            ConsoleKeyInfo key;
            do
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.White;
                Utility.WriteTitle("RSGymPT - Main Menu");
                Console.WriteLine("[1] - Login");
                Console.WriteLine("[Esc] - Exit", "\n\n");

                key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.Escape)
                {
                    exit = true;
                }
                else
                {
                    switch (key.KeyChar)
                    {
                        case '1':
                            Login();
                            if (_session.IsLogged)
                            {
                                exit = true;
                            }
                            break;
                        default:
                            Utility.WriteError("Invalid option.");
                            break;
                    }
                }
            } while (exit == false);

            Console.ResetColor();

            return key;
        }

        private void Login()
        {
            Utility.WriteInformation("For better experience extend the console width.");
            Utility.WriteInformation("Default User - Username: 'milena' | Password: 'milena00'");
            do
            {
                Console.Clear();
                Utility.WriteTitle("Login Menu");
                Console.WriteLine("Insert username and password or press [ESC] to return to Main Menu.\n");
                ReadLineInfo rInfoUsername = ConsoleUtility.ReadInput("Username: ");
                if (rInfoUsername.Exit)
                {
                    break;
                }
                ReadLineInfo rInfoPassword = ConsoleUtility.ReadInputPasswordForm("Password: ");
                if (rInfoPassword.Exit)
                {
                    break;
                }

                CredentialsModel credentials = new CredentialsModel(rInfoUsername.Text, rInfoPassword.Text);
                _session.Login(credentials);

                if (_session.IsLogged)
                {
                    Utility.WriteSucess("Login successful.");
                    break;
                }
                else
                {
                    Utility.WriteError("Invalid username or password.");
                }
            } while (true);

            Console.ResetColor();
        }
    }
}


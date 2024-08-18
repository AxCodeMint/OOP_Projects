using Administracao_Utilizadores.Interfaces;
using Administracao_Utilizadores.Utilities;
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
                ConsoleUtility.WriteTitle("Main Menu");
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
                            ConsoleUtility.WriteError("Invalid option.");
                            break;
                    }
                }
            } while (exit == false);

            Console.ResetColor();

            return key;
        }

        private void Login()
        {
            ConsoleUtility.WriteInformation("For better experieance extend the console width.");
            ConsoleUtility.WriteInformation("Default User, Username: 'milena' | Password: 'milena00'");
            do
            {
                Console.Clear();
                ConsoleUtility.WriteTitle("Login Menu");
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
                    ConsoleUtility.WriteSucess("Login successful.");
                    break;
                }
                else
                {
                    ConsoleUtility.WriteError("Invalid username or password.");
                }
            } while (true);

            Console.ResetColor();
        }
    }
}


using Administracao_Utilizadores.Data;
using Administracao_Utilizadores.Data.Interfaces;
using Administracao_Utilizadores.Interfaces;
using Administracao_Utilizadores.Models.Menus;
using Administracao_Utilizadores.Models;
using Administracao_Utilizadores.Utilities;
using System;
using General_Utility;


namespace Administracao_Utilizadores
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Utility.SetUnicodeConsole();
            IUserRepository userRepository = new UserRepository();
            userRepository.SeedUsers();
            ISession session = new Session(userRepository);
            LoginMenu menuLogin = new LoginMenu(session);

            bool exit = false;
            ConsoleKeyInfo key;

            try
            {
                do
                {
                    if (session.IsLogged == false)
                    {
                        key = menuLogin.ShowMenu();

                        if (key.Key == ConsoleKey.Escape)
                        {
                            exit = true;
                        }
                        else
                        {
                            exit = false;
                        }
                    }
                    else
                    {
                        Console.Clear();
                        switch (session.User.Role)
                        {
                            case Enums.EnumRole.Admin:
                                Menu adminUserMenu = new AdminUserMenu(userRepository, session, (ConsoleColor.Black, ConsoleColor.Magenta));
                                adminUserMenu.MainMenu();
                                break;

                            case Enums.EnumRole.PowerUser:
                                Menu powerUserMenu = new PowerUserMenu(userRepository, session, (ConsoleColor.Black, ConsoleColor.Blue));
                                powerUserMenu.MainMenu();
                                break;

                            case Enums.EnumRole.SimpleUser:
                                Menu simpleUserMenu = new SimpleUserMenu(userRepository, session, (ConsoleColor.Black, ConsoleColor.DarkGreen));
                                simpleUserMenu.MainMenu();
                                break;

                            default:
                                break;
                        }
                    }

                } while (exit == false);

                Utility.WriteInformation("Bye!");
            }
            catch (Exception ex)
            {
                Utility.WriteError(ex.Message);
            }

        }
    }
}

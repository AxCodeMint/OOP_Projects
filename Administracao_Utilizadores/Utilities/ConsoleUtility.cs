using Administracao_Utilizadores.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administracao_Utilizadores.Utilities
{
    public static class ConsoleUtility
    {
        public static void SetUnicodeConsole()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

        }
        public static void WriteTitle(string title, string profile = "", ConsoleColor backgroundColor = ConsoleColor.Black, ConsoleColor fontColor = ConsoleColor.White)
        {
            ConsoleColor oldBackgroundColor = Console.BackgroundColor;
            ConsoleColor oldForegroundColor = Console.ForegroundColor;

            Console.BackgroundColor = backgroundColor;
            Console.ForegroundColor = fontColor;
            Console.WriteLine("┌" + new string('─', 100) + "┐");
            Console.WriteLine($"│{title.ToUpper(),-50}{profile,-50}│");
            Console.WriteLine("└" + new string('─', 100) + "┘");
            Console.WriteLine();

            Console.BackgroundColor = oldBackgroundColor;
            Console.ForegroundColor = oldForegroundColor;

        }

        public static ConsoleKeyInfo WriteQuestionAndChoice(string question, string prompt = "")
        {
            bool exit = false;
            ConsoleKeyInfo key;

            ConsoleColor oldBackgroundColor = Console.BackgroundColor;
            ConsoleColor oldForegroundColor = Console.ForegroundColor;

            do
            {
                Console.Clear();
                Console.BackgroundColor = ConsoleColor.DarkYellow;
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($"{question}");
                Console.WriteLine($"\n[1] Yes \n[2]/[ESC] No");
                if (!string.IsNullOrEmpty(prompt))
                {
                    Console.Write(prompt);
                }

                key = Console.ReadKey(true);
                Console.WriteLine();

                if (key.Key == ConsoleKey.Escape)
                {
                    exit = true;
                }
                else
                {
                    switch (key.KeyChar)
                    {
                        case '1':
                            exit = true;
                            break;
                        case '2':
                            exit = true;
                            break;
                        default:
                            Console.BackgroundColor = oldBackgroundColor;
                            Console.ForegroundColor = oldForegroundColor;
                            WriteError("Invalid option.");
                            break;
                    }
                }
            } while (exit == false);

            Console.BackgroundColor = oldBackgroundColor;
            Console.ForegroundColor = oldForegroundColor;

            return key;
        }

        public static void WriteError(string text)
        {
            ConsoleColor oldBackgroundColor = Console.BackgroundColor;
            ConsoleColor oldForegroundColor = Console.ForegroundColor;

            Console.WriteLine();
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"Error: {text}");
            Console.WriteLine($"Please press any key to continue.");
            Console.BackgroundColor = oldBackgroundColor;
            Console.ForegroundColor = oldForegroundColor;
            Console.ReadKey(true);
            Console.WriteLine();
        }

        public static void WriteInformation(string text = "")
        {
            ConsoleColor oldBackgroundColor = Console.BackgroundColor;
            ConsoleColor oldForegroundColor = Console.ForegroundColor;


            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
            if (!string.IsNullOrEmpty(text))
            {
                Console.WriteLine($"Info: {text}");
            }
            Console.WriteLine($"Please press any key to continue.");
            Console.BackgroundColor = oldBackgroundColor;
            Console.ForegroundColor = oldForegroundColor;
            Console.WriteLine();
            Console.ReadKey(true);

        }

        public static void WriteSucess(string text)
        {
            ConsoleColor oldBackgroundColor = Console.BackgroundColor;
            ConsoleColor oldForegroundColor = Console.ForegroundColor;

            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine();
            Console.WriteLine($"Message: {text}");
            Console.WriteLine($"Please press any key to continue.");
            Console.BackgroundColor = oldBackgroundColor;
            Console.ForegroundColor = oldForegroundColor;
            Console.WriteLine();
            Console.ReadKey(true);
            
        }

        public static ReadLineInfo ReadInput(string initialText = "", string promptName = "")
        {
            ReadLineInfo readInfo = new ReadLineInfo();
            string input = ""; 

            Console.Write($"{initialText}{promptName}");

            while (true)
            {
                var key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.Escape)  
                {
                    readInfo.Exit = true;   
                    break;
                }

                if (key.Key == ConsoleKey.Enter) 
                {
                    Console.WriteLine();
                    readInfo.Text = input;
                    break;
                }

                if (key.Key == ConsoleKey.Backspace)
                {
                    if (input.Length > 0)
                    {
                        input = input.Substring(0, input.Length - 1);
                        ClearCurrentLine();
                        if (!string.IsNullOrEmpty(promptName))
                        {
                            ClearCurrentLine(1);
                        }
                        Console.Write($"{initialText}{promptName}");
                        Console.Write(input);
                    }
                }
                else
                {
                    input += key.KeyChar; 
                    Console.Write(key.KeyChar);
                }
            }

            return readInfo;
        }

        public static ReadLineInfo ReadInputPasswordForm(string initText, string promptPrefix = "")
        {
            var rInfo = new ReadLineInfo();
            string password = string.Empty;         
            string mask = string.Empty;           

            Console.Write($"{initText}{promptPrefix}");

            do
            {
                
                ConsoleKeyInfo key = Console.ReadKey(true);  

                if (key.Key == ConsoleKey.Enter)        
                {
                    break;
                }

                if (key.Key == ConsoleKey.Escape)        
                {
                    rInfo.Exit = true;
                    return rInfo;
                }

                if (key.Key == ConsoleKey.Backspace && password.Length > 0) 
                {
                    password = password.Substring(0, password.Length - 1);  
                    mask = mask.Substring(0, mask.Length - 1);
                    ClearCurrentLine();
                    if (!string.IsNullOrEmpty(promptPrefix))
                    {
                        ClearCurrentLine(1);
                    }
                    Console.Write($"{initText}{promptPrefix}");                    
                    Console.Write(mask);
                }
                else if (key.Key != ConsoleKey.Backspace)                   
                {
                    password = password + key.KeyChar;      
                    mask = mask + '*';                      
                    Console.Write("*");              
                }

            } while (true);

            Console.WriteLine();

            rInfo.Text = password;
            return rInfo;
        }

        public static void ShowUserDetails(User user)
        {
            ConsoleColor oldBackgroundColor = Console.BackgroundColor;
            ConsoleColor oldForegroundColor = Console.ForegroundColor;
            var userBirhday = user.BirthDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);

            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine($"┌──────────────────────────────────────────────┐");
            Console.WriteLine($"│                     USER                     │");
            Console.WriteLine($"├──────────────────────────────────────────────│");
            Console.WriteLine($"│ > Id: {user.Id,-13}                          │");
            Console.WriteLine($"│ > Role: {user.Role,-15}                      │");
            Console.WriteLine($"│ > First Name: {user.FirstName,-20}           │");
            Console.WriteLine($"│ > Last Name: {user.LastName,-19}             │");
            Console.WriteLine($"│ > BirthDate: {userBirhday,-17}               │");
            Console.WriteLine($"│ > Email:{user.Email,-16}                     │");
            Console.WriteLine($"│ > Phone:{user.Phone,-16}                     │");
            Console.WriteLine($"│ > Username:{user.Username,-19}               │");
            Console.WriteLine($"└──────────────────────────────────────────────┘");

            Console.BackgroundColor = oldBackgroundColor;
            Console.ForegroundColor = oldForegroundColor;

        }

        public static void ShowEditUser(User user, string role = null, string firstName = null, string lastName = null, string birthday = null, string email = null, string phone = null, string username = null)
        {
            ConsoleColor oldBackgroundColor = Console.BackgroundColor;
            ConsoleColor oldForegroundColor = Console.ForegroundColor;

            var userBirhday = user.BirthDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine($"┌─────────────────────────────────────────────────────────────┬─────────────────────────────────────────────────────────────┐");
            Console.WriteLine($"│                           USER                              │                     UPDATED PROPERTIES                      │");
            Console.WriteLine($"├─────────────────────────────────────────────────────────────│─────────────────────────────────────────────────────────────┤");
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($"│ > Id: {user.Id,-13}                                         │                                                             │");
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.WriteLine($"│ > Role: {user.Role,-15}                                     │ {(role != null ? "* Role:" + role : ""),-44}                │");
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($"│ > First Name: {user.FirstName,-20}                          │ {(firstName != null ? "* First Name:" + firstName : ""),-60}│");
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.WriteLine($"│ > Last Name: {user.LastName,-19}                            │ {(lastName != null ? "* Last Name:" + lastName : ""),-57}   │");
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($"│ > BirthDate: {userBirhday,-17}                              │ {(birthday != null ? "* BirthDate:" + birthday : ""),-57}   │");
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.WriteLine($"│ > Email:{user.Email,-16}                                    │ {(email != null ? "* Email:" + email : ""),-47}             │");
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($"│ > Phone:{user.Phone,-16}                                    │ {(phone != null ? "* Phone:" + phone : ""),-47}             │");
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.WriteLine($"│ > Username:{user.Username,-19}                              │ {(username != null ? "* Username:" + username : ""),-56}    │");
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($"└─────────────────────────────────────────────────────────────┴─────────────────────────────────────────────────────────────┘");
            Console.BackgroundColor = oldBackgroundColor;
            Console.ForegroundColor = oldForegroundColor;
            Console.WriteLine();
        }

        public static void ShowUserList(IEnumerable<User> users)
        {
            ConsoleColor oldBackgroundColor = Console.BackgroundColor;
            ConsoleColor oldForegroundColor = Console.ForegroundColor;

            // Cabeçalho da tabela com separadores
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Black;

            Console.WriteLine("┌─────┬──────────────┬─────────────────┬─────────────────┬──────────────┬──────────────────────────────┬───────────────┬─────────────────┐");
            Console.WriteLine("│ Id  │ Role         │ First Name      │ Last Name       │ BirthDate    │ Email                        │ Phone         │ Username        │");
            Console.WriteLine("├─────┼──────────────┼─────────────────┼─────────────────┼──────────────┼──────────────────────────────┼───────────────┼─────────────────┤");

            
            int index = 0;
            foreach (var user in users)
            {
                
                if (index % 2 == 0)
                {
                    Console.BackgroundColor = ConsoleColor.DarkGray; 
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.Gray; 
                }

                Console.WriteLine($"│ {user.Id,-3} │ {user.Role,-12} │ {user.FirstName,-15} │ {user.LastName,-15} │ {user.BirthDate:yyyy-MM-dd}   │ {user.Email,-28} │ {user.Phone,-13} │ {user.Username,-15} │");
                index++;
            }

            Console.BackgroundColor = ConsoleColor.Gray;
            Console.WriteLine("└─────┴──────────────┴─────────────────┴─────────────────┴──────────────┴──────────────────────────────┴───────────────┴─────────────────┘");

            Console.BackgroundColor = oldBackgroundColor;
            Console.ForegroundColor = oldForegroundColor;
        }

        public static void ClearCurrentLine(int position = 0)
        {
            Console.SetCursorPosition(0, Console.CursorTop - position);
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }

    }
}

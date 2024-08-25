using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace General_Utility
{
    public class Utility
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

        public static void WriteMessage(string message, string beginMessage = "", string endMessage = "", string promptUsername = null)
        {
            Console.Write($"{beginMessage} > {message}{endMessage}");
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
            PauseConsole();
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
            PauseConsole();
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
            PauseConsole();
            Console.BackgroundColor = oldBackgroundColor;
            Console.ForegroundColor = oldForegroundColor;
            Console.WriteLine();
            Console.ReadKey(true);

        }

        public static void PauseConsole()
        {
            Console.WriteLine($"Please press any key to continue.");
        }

        public static void TerminateConsole()
        {
            WriteInformation("Bye!");
        }
    }
}

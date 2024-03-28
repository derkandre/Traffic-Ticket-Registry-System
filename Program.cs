using System;
using System.Text;
using System.Threading;

namespace ttrs
{
    class Program
    {
        static void Main(string[] args)
        {
            string user, pass;
            bool loggedIn = false;
            bool tryAgain = true;

            do
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Clear();



                while (loggedIn == false)
                {
                    WriteCenter("▀█▀ █▀▄ ▄▀▄ █▀ █▀ █ ▄▀▀   ▀█▀ █ ▄▀▀ █▄▀ ██▀ ▀█▀   █▀▄ ██▀ ▄▀  █ ▄▀▀ ▀█▀ █▀▄ ▀▄▀   ▄▀▀ ▀▄▀ ▄▀▀ ▀█▀ ██▀ █▄ ▄█", 5);
                    WriteCenter(" █  █▀▄ █▀█ █▀ █▀ █ ▀▄▄    █  █ ▀▄▄ █ █ █▄▄  █    █▀▄ █▄▄ ▀▄█ █ ▄██  █  █▀▄  █    ▄██  █  ▄██  █  █▄▄ █ ▀ █", 0);

                    SetColor("yellow", "black");
                    WriteCenter("+-------------------------------------+", 4);
                    WriteCenter("LOG IN", -1);
                    WriteCenter("|                                     |", 0);
                    WriteCenter("|                 USER                |", 0);
                    WriteCenter("|                                     |", 0);
                    WriteCenter("|                                     |", 0);
                    WriteCenter("|                 PASS                |", 0);
                    WriteCenter("|                                     |", 0);
                    WriteCenter("|                                     |", 0);
                    WriteCenter("|                                     |", 0);
                    WriteCenter("|                                     |", 0);
                    WriteCenter("+-------------------------------------+", 0);

                    Console.ResetColor();

                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;

                    user = ReadSecure(-8).ToString();
                    WriteCenter("\n", 0);
                    Console.ResetColor();
                    pass = ReadSecure(1).ToString();

                    bool authentication = Authenticate(user, pass);

                    if (authentication == true)
                        loggedIn = true;

                    ResetToDefaultColor();
                    Console.Clear();
                }

                Console.CursorVisible = false;

                hr(0);
                WriteCenter("TRAFFIC TICKET REGISTRY SYSTEM", 6);

                SetColor("yellow", "black");
                WriteCenter("+-------------------------------------+", 2);
                WriteCenter("MAIN MENU", -1);
                WriteCenter("|                                     |", 0);
                WriteCenter("|    [1] Register Ticket Violation    |", 0);
                WriteCenter("|    [2] Retrieve Ticket Records      |", 0);
                WriteCenter("|    [3] Modify   Ticket Records      |", 0);
                WriteCenter("|    [4] About Software               |", 0);
                WriteCenter("|    [5] Log Out                      |", 0);
                WriteCenter("|                                     |", 0);
                WriteCenter("+-------------------------------------+", 0);
                ResetToDefaultColor();

                WriteCenter("TTRS v0.02UIb", 7);

                hr(1);

                ConsoleKeyInfo menuChoice = Console.ReadKey(true);

                SetColor("black", "yellow");
                if (menuChoice.Key == ConsoleKey.D5)
                {
                    int i = -12;

                    // WT's buffer height is 30 while CMDs is 9001. FIXES: Cursor positioning is off.
                    // Ref: https://github.com/microsoft/terminal/issues/8312
                    if (Console.BufferHeight != 30)
                        i = -13;

                    tryAgain = false;
                    WriteCenter("[5] Log Out                   ", i);
                    Console.SetCursorPosition(40, Console.CursorTop + 4);
                    ResetToDefaultColor();
                    Thread.Sleep(1500);
                }
                else
                {
                    UnderConstruction();
                    ResetToDefaultColor();
                }
            } while (tryAgain == true);

        }
        static bool Authenticate(string username, string password)
        {
            int cWidth = Console.WindowWidth;
            Console.BackgroundColor = ConsoleColor.Yellow;

            if (username == "enforcer" && password == "imissyou")
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Thread.Sleep(250);
                WriteCenter("LOGGING IN", 2);
                Console.SetCursorPosition(65, Console.CursorTop - 1);
                Thread.Sleep(3500);

                return true;
            }
            else if (username == null || password == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Thread.Sleep(250);
                WriteCenter("MISSING USERNAME/PASSWORD", 2);
                Console.SetCursorPosition(65, Console.CursorTop - 1);
                Thread.Sleep(3500);

                return false;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Thread.Sleep(250);
                WriteCenter("INCORRECT USERNAME/PASSWORD", 2);
                Console.SetCursorPosition(73, Console.CursorTop - 1);
                Thread.Sleep(3500);
            }

            Console.ResetColor();
            return false;
        }
        static void hr(int cPosition)
        {
            int cWidth = Console.WindowWidth;

            Console.SetCursorPosition(0, Console.CursorTop + cPosition);

            for (int i = 0; i < cWidth; i++)
            {
                Console.Write("=");
            }
        }
        static void WriteCenter(string text, int cPosition)
        {
            int cWidth = Console.WindowWidth;

            Console.SetCursorPosition((cWidth - text.Length) / 2, Console.CursorTop + cPosition);

            Console.WriteLine(text);
        }
        static string ReadCenter(int cPosition)
        {
            int cWidth = Console.WindowWidth;

            Console.SetCursorPosition((cWidth - "********".Length) / 2, Console.CursorTop + cPosition);
            Console.BackgroundColor = ConsoleColor.Cyan;
            Console.WriteLine("        ");

            Console.SetCursorPosition((cWidth - "********".Length) / 2, Console.CursorTop - 1);

            return Console.ReadLine();
        }
        static string ReadSecure(int cPosition)
        {
            int cWidth = Console.WindowWidth;

            Console.SetCursorPosition((cWidth - "********".Length) / 2, Console.CursorTop + cPosition);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("        ");

            Console.SetCursorPosition((cWidth - "********".Length) / 2, Console.CursorTop - 1);

            StringBuilder input = new StringBuilder();

            int i = 0;

            while (i < 8)
            {
                ConsoleKeyInfo inputtedChar = Console.ReadKey();
                input.Append(inputtedChar.KeyChar);
                i++;

                if (inputtedChar.Key == ConsoleKey.Enter)
                    break;

                // Intervention for the backspace key to not be appended to our string; for deleting characters purpose
                if (inputtedChar.Key == ConsoleKey.Backspace)
                {
                    /* Intervention for backspace to not go beyond the black field box.
                       Also because in line 219, when there is only 1 character left it produces
                       an exception: ArgumentOutOfRangeException: StartIndex cannot be less than zero. */
                    if (input.Length < 2)
                    {
                        /* Mostly went to trial and error until the backspace problem is solved.
                           The backspace no longer goes beyond the black field box. This is fixed by
                           adding 1 to the cursor position; basically just goes back to the position
                           where a backspace was used */
                        Console.SetCursorPosition(Console.CursorLeft + 1, Console.CursorTop);
                        input.Remove(0, 1);
                        i--;
                        continue; // Skips our next group of code that would remove another character
                    }
                    {
                        input.Remove(i - 1, 1);
                        i -= 2;
                        // Line 219 does not remove the text in display, only in the background
                        // As such writing \b on the console replicates the backspace visually
                        Console.Write(" \b"); // matches a backspace, \u0008 Ref: Micorosft Learn Docs.
                        input.Remove(i, 1);
                    }
                }
            }

            return input.ToString();
        }
        static void ResetToDefaultColor()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.BackgroundColor = ConsoleColor.Black;
        }
        static void SetColor(string bg, string fg)
        {
            switch (bg)
            {
                case "gray":
                    Console.BackgroundColor = ConsoleColor.Gray;
                    break;
                case "white":
                    Console.BackgroundColor = ConsoleColor.White;
                    break;
                case "yellow":
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    break;
            }
            switch (fg)
            {
                case "white":
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case "black":
                    Console.ForegroundColor = ConsoleColor.Black;
                    break;
                case "yellow":
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
            }
        }
        static void UnderConstruction()
        {
            Console.Clear();

            hr(0);
            WriteCenter("TRAFFIC TICKET REGISTRY SYSTEM", 8);

            SetColor("yellow", "black");
            WriteCenter("+-------------------------------------+", 2);
            WriteCenter("UNDER CONSTRUCTION", -1);
            WriteCenter("|                                     |", 0);
            WriteCenter("|    [1] Go Back                      |", 0);
            WriteCenter("|                                     |", 0);
            WriteCenter("+-------------------------------------+", 0);
            ResetToDefaultColor();

            WriteCenter("TTRS v0.02UIb", 9);

            hr(1);

            ConsoleKeyInfo choice = Console.ReadKey(true);

            SetColor("black", "yellow");
            if (choice.Key == ConsoleKey.D1)
            {
                int i = -14;

                if (Console.BufferHeight != 30)
                    i = -15;

                WriteCenter("[1] Go Back                   ", i);
                Thread.Sleep(1500);
                return;
            }
        }
    }
}
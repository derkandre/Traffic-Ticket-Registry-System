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

            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Clear();

            WriteCenter("▀█▀ █▀▄ ▄▀▄ █▀ █▀ █ ▄▀▀   ▀█▀ █ ▄▀▀ █▄▀ ██▀ ▀█▀   █▀▄ ██▀ ▄▀  █ ▄▀▀ ▀█▀ █▀▄ ▀▄▀   ▄▀▀ ▀▄▀ ▄▀▀ ▀█▀ ██▀ █▄ ▄█", 4);
            WriteCenter(" █  █▀▄ █▀█ █▀ █▀ █ ▀▄▄    █  █ ▀▄▄ █ █ █▄▄  █    █▀▄ █▄▄ ▀▄█ █ ▄██  █  █▀▄  █    ▄██  █  ▄██  █  █▄▄ █ ▀ █", 0);

            SetBoxColor("yellow", "black");
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

            Authenticate(user, pass);

            ResetToDefaultColor();
            Console.Clear();

            hr(0);
            WriteCenter("TRAFFIC TICKET REGISTRY SYSTEM", 8);

            SetBoxColor("yellow", "black");
            WriteCenter("+-------------------------------------+", 2);
            WriteCenter("MAIN MENU", -1);
            WriteCenter("|    [1] Register Ticket Violation    |", 0);
            WriteCenter("|    [2] Retrieve Ticket Records      |", 0);
            WriteCenter("|    [3] Modify   Ticket Records      |", 0);
            WriteCenter("|    [4] Log Out                      |", 0);
            WriteCenter("+-------------------------------------+", 0);
            ResetToDefaultColor();

            WriteCenter("TTRS v0.01UIb", 8);

            hr(1);
        }
        static void Authenticate(string username, string password)
        {
            Console.BackgroundColor = ConsoleColor.Yellow;

            if (username == "enforcer" && password == "imissyou")
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                WriteCenter("LOGGING IN", 2);
                Thread.Sleep(3000);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                WriteCenter("INCORRECT USERNAME/PASSWORD", 2);
                Thread.Sleep(3000);
            }

            Console.ResetColor();

            return;
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
            }

            return input.ToString();
        }
        static void ResetToDefaultColor()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.BackgroundColor = ConsoleColor.Black;
        }
        static void SetBoxColor(string bg, string fg)
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
    }
}
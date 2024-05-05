using System;
using System.Linq;
using System.Text;
using System.Threading;

namespace ttrs
{
    class Program
    {
        static string[,] noHelmet = new string[999, 5];
        static string[,] noLicense = new string[999, 5];
        static string[,] dui = new string[999, 5];
        static string[,] noInsurance = new string[999, 5];
        static string[,] noRegistration = new string[999, 5];

        static int noLicenseCount = 0;
        static int noRegistrationCount = 0;
        static int noInsuranceCount = 0;
        static int noHelmetCount = 0;
        static int duiCount = 0;

        static int ticketNum = 0;

        static void Main(string[] args)
        {
            Console.Clear();
            bool tryAgain = true;
            LoginPage(false);

            do
            {
                try
                {
                    Console.SetWindowSize(120, 30);
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Clear();

                    Console.CursorVisible = false;

                    hr(0);
                    WriteCenter("TRAFFIC TICKET REGISTRY SYSTEM", 6);

                    SetColor("yellow", "black");
                    WriteCenter("+-------------------------------------+", 2);
                    WriteCenter("MAIN MENU", -1);
                    WriteCenter("|                                     |", 0);
                    WriteCenter("|    [1] Register Ticket Violation    |", 0);
                    WriteCenter("|    [2] Search   Ticket Entries      |", 0);
                    WriteCenter("|    [3] View Violations Summary      |", 0);
                    WriteCenter("|    [4] About Software               |", 0);
                    WriteCenter("|    [5] Log Out and Exit             |", 0);
                    WriteCenter("|                                     |", 0);
                    WriteCenter("+-------------------------------------+", 0);
                    ResetToDefaultColor();

                    WriteCenter("TTRS v0.07a", 7);

                    hr(1);

                    ConsoleKeyInfo menuChoice = Console.ReadKey(true);

                    SetColor("black", "yellow");

                    if (menuChoice.Key == ConsoleKey.D1)
                    {
                        RegisterTicketViolation();
                    }
                    else if (menuChoice.Key == ConsoleKey.D2)
                    {
                        //SearchTicketEntries();
                    }
                    else if (menuChoice.Key == ConsoleKey.D3)
                    {
                        ViewTicketViolationSummary();
                    }
                    else if (menuChoice.Key == ConsoleKey.D5)
                    {
                        int i = -12;

                        // WT's buffer height is 30 while CMDs is 9001. FIXES: Cursor positioning is off.
                        // Ref: https://github.com/microsoft/terminal/issues/8312
                        if (Console.BufferHeight != 30)
                            i = -13;

                        tryAgain = false;
                        WriteCenter("[5] Log Out and Exit         ", i);
                        Console.SetCursorPosition(40, Console.CursorTop + 4);
                        ResetToDefaultColor();
                        Thread.Sleep(1000);
                        LoginPage(true);
                    }
                    else if (menuChoice.Key == ConsoleKey.D4)
                    {
                        Console.Clear();

                        SetColor("yellow", "black");
                        WriteCenter("+----------------------------------------------------------------------------------+", 4);
                        WriteCenter("|                                    ISC License                                   |", 0);
                        WriteCenter("|                                                                                  |", 0);
                        WriteCenter("| Copyright (c) 2024 Derick Andre, Gabriel Federick and Joseph Lindell             |", 0);
                        WriteCenter("|                                                                                  |", 0);
                        WriteCenter("| Permission to use, copy, modify, and/or distribute this software for any purpose |", 0);
                        WriteCenter("| with or without fee is hereby granted, provided that the above copyright notice  |", 0);
                        WriteCenter("| and this permission notice appear in all copies.                                 |", 0);
                        WriteCenter("|                                                                                  |", 0);
                        WriteCenter("| THE SOFTWARE IS PROVIDED  \"AS IS\"  AND THE AUTHOR DISCLAIMS ALL WARRANTIES       |", 0);
                        WriteCenter("| WITH REGARD TO THIS SOFTWARE INCLUDING ALL IMPLIED WARRANTIES OF MERCHANTABILITY |", 0);
                        WriteCenter("| AND FITNESS. IN NO EVENT SHALL THE AUTHOR BE LIABLE FOR ANY SPECIAL, DIRECT,     |", 0);
                        WriteCenter("| INDIRECT, OR CONSEQUENTIAL DAMAGES OR ANY DAMAGES WHATSOEVER RESULTING FROM LOSS |", 0);
                        WriteCenter("| OF USE, DATA OR PROFITS, WHETHER IN AN ACTION OF CONTRACT, NEGLIGENCE OR OTHER   |", 0);
                        WriteCenter("| TORTIOUS ACTION, ARISING OUT OF OR IN CONNECTION WITH THE USE OR PERFORMANCE OF  |", 0);
                        WriteCenter("| THIS SOFTWARE.                                                                   |", 0);
                        WriteCenter("|                                                                                  |", 0);
                        WriteCenter("| https://github.com/derkandre/ttrs/blob/main/LICENSE.md                           |", 0);
                        WriteCenter("+----------------------------------------------------------------------------------+", 0);
                        ResetToDefaultColor();

                        SetColor("yellow", "black");
                        WriteCenter("[1] Go Back", 3);
                        ResetToDefaultColor();

                        ConsoleKeyInfo choice = Console.ReadKey(true);

                        SetColor("black", "yellow");
                        if (choice.Key == ConsoleKey.D1)
                        {
                            int i = -1;

                            if (Console.BufferHeight != 30)
                                i = -1;

                            WriteCenter("[1] Go Back", i);
                            Thread.Sleep(1000);
                        }
                    }
                    else
                    {
                        UnderConstruction();
                        ResetToDefaultColor();
                    }

                    tryAgain = true;

                }
                catch (Exception ex)
                {
                    SetColor("red", "white");
                    WriteCenter("+------------------------------------------+", 0);
                    WriteCenter(ex.Message, 0);
                    WriteCenter("+------------------------------------------+", 0);
                    ResetToDefaultColor();

                    Console.ReadKey();
                }
            } while (tryAgain == true);
        }
        static void RegisterTicketViolation()
        {
            string name;
            bool recordFound = false;

            Console.CursorVisible = true;
            Console.Clear();

            SetColor("yellow", "black");
            WriteCenter("+------------------------------------------+", 2);
            WriteCenter("List of Violation", -1);
            WriteCenter("|  (A) No License                          |", 0);
            WriteCenter("|  (B) No Registration                     |", 0);
            WriteCenter("|  (C) No Helmet                           |", 0);
            WriteCenter("|  (D) No Insurance                        |", 0);
            WriteCenter("|  (E) Driving Under the Influence (DUI)   |", 0);
            WriteCenter("+------------------------------------------+", 0);
            ResetToDefaultColor();

            Console.Write("\n\t\t\t\t\t\tType of Violation: ");
            string choice = Console.ReadLine().ToUpper();

            switch (choice)
            {
                case "A":
                    Console.Write("\n\t\t\t\t\t\tDate             : ");
                    noLicense[noLicenseCount, 0] = Console.ReadLine();

                    Console.Write("\t\t\t\t\t\tTicket Number    : ");

                    ticketNum++;
                    SetColor("yellow", "black");
                    Console.WriteLine("{0}", noLicense[noLicenseCount, 1] = ticketNum.ToString("TN-000"));
                    ResetToDefaultColor();

                    Console.Write("\t\t\t\t\t\tName             : ");
                    name = Console.ReadLine();

                    for (int i = 0; i < noLicenseCount; i++)
                    {
                        if (noLicense[i, 2] == name)
                        {
                            recordFound = true;

                            if (noLicense[i, 3] == "1st")
                            {
                                SetColor("yellow", "black");
                                WriteCenter("+-------------------------------+", 1);
                                WriteCenter("|    Existing Record(s) Found   |", 0);
                                WriteCenter("|    Offense Type     : 1st     |", 0);
                                WriteCenter("+-------------------------------+", 0);
                                ResetToDefaultColor();
                                noLicense[i + 1, 3] = "2nd";
                                Console.WriteLine("\n\t\t\t\t\t\tOffense Type     : 2nd");
                            }
                            else if (noLicense[i, 3] == "2nd")
                            {
                                SetColor("yellow", "black");
                                WriteCenter("+-------------------------------+", 1);
                                WriteCenter("|    Existing Record(s) Found   |", 0);
                                WriteCenter("|    Offense Type     : 2nd     |", 0);
                                WriteCenter("+-------------------------------+", 0);
                                ResetToDefaultColor();
                                noLicense[i + 1, 3] = "3rd";
                                Console.WriteLine("\n\t\t\t\t\t\tOffense Type     : 3rd");
                            }
                        }
                    }


                    if (recordFound == false)
                    {
                        SetColor("yellow", "black");
                        WriteCenter("+-------------------------------+", 1);
                        WriteCenter("|      No Record(s) Found       |", 0);
                        WriteCenter("+-------------------------------+", 0);
                        ResetToDefaultColor();
                        noLicense[noLicenseCount, 3] = "1st";
                        Console.WriteLine("\n\t\t\t\t\t\tOffense Type     : 1st ");
                    }
                    noLicense[noLicenseCount, 2] = name;
                    Console.Write("\t\t\t\t\t\tPayment          : ");
                    noLicense[noLicenseCount, 4] = Console.ReadLine();
                    noLicenseCount++;

                    break;

                case "B":
                    Console.Write("\n\t\t\t\t\t\tDate             : ");
                    noRegistration[noRegistrationCount, 0] = Console.ReadLine();

                    Console.Write("\t\t\t\t\t\tTicket Number    : ");

                    ticketNum++;
                    SetColor("yellow", "black");
                    Console.WriteLine("{0}", noRegistration[noRegistrationCount, 1] = ticketNum.ToString("TN-000"));
                    ResetToDefaultColor();

                    Console.Write("\t\t\t\t\t\tName             : ");
                    name = Console.ReadLine();

                    for (int i = 0; i < noRegistrationCount; i++)
                    {
                        if (noRegistration[i, 2] == name)
                        {
                            recordFound = true;
                            if (noRegistration[i, 3] == "1st")
                            {
                                SetColor("yellow", "black");
                                WriteCenter("+-------------------------------+", 1);
                                WriteCenter("|    Existing Record(s) Found   |", 0);
                                WriteCenter("|    Offense Type     : 1st     |", 0);
                                WriteCenter("+-------------------------------+", 0);
                                ResetToDefaultColor();
                                noRegistration[i + 1, 3] = "2nd";
                                Console.WriteLine("\n\t\t\t\t\t\tOffense Type     : 2nd");
                            }
                            else if (noRegistration[i, 3] == "2nd")
                            {
                                SetColor("yellow", "black");
                                WriteCenter("+-------------------------------+", 1);
                                WriteCenter("|    Existing Record(s) Found   |", 0);
                                WriteCenter("|    Offense Type     : 2nd     |", 0);
                                WriteCenter("+-------------------------------+", 0);
                                ResetToDefaultColor();
                                noRegistration[i + 1, 3] = "3rd";
                                Console.WriteLine("\n\t\t\t\t\t\tOffense Type     : 3rd");
                            }
                        }
                    }

                    if (recordFound == false)
                    {
                        SetColor("yellow", "black");
                        WriteCenter("+-------------------------------+", 1);
                        WriteCenter("|      No Record(s) Found       |", 0);
                        WriteCenter("+-------------------------------+", 0);
                        ResetToDefaultColor();
                        noRegistration[noRegistrationCount, 3] = "1st";
                        Console.WriteLine("\n\t\t\t\t\t\tOffense Type     : 1st ");
                    }
                    noRegistration[noRegistrationCount, 2] = name;
                    Console.Write("\t\t\t\t\t\tPayment          : ");
                    noRegistration[noRegistrationCount, 4] = Console.ReadLine();
                    noRegistrationCount++;

                    break;

                case "C":
                    Console.Write("\n\t\t\t\t\t\tDate             : ");
                    noHelmet[noHelmetCount, 0] = Console.ReadLine();

                    Console.Write("\t\t\t\t\t\tTicket Number    : ");

                    ticketNum++;
                    SetColor("yellow", "black");
                    Console.WriteLine("{0}", noHelmet[noHelmetCount, 1] = ticketNum.ToString("TN-000"));
                    ResetToDefaultColor();

                    Console.Write("\t\t\t\t\t\tName             : ");
                    name = Console.ReadLine();

                    for (int i = 0; i < noHelmetCount; i++)
                    {
                        if (noHelmet[i, 2] == name)
                        {
                            recordFound = true;
                            if (noHelmet[i, 3] == "1st")
                            {
                                SetColor("yellow", "black");
                                WriteCenter("+-------------------------------+", 1);
                                WriteCenter("|    Existing Record(s) Found   |", 0);
                                WriteCenter("|    Offense Type     : 1st     |", 0);
                                WriteCenter("+-------------------------------+", 0);
                                ResetToDefaultColor();
                                noHelmet[i + 1, 3] = "2nd";
                                Console.WriteLine("\n\t\t\t\t\t\tOffense Type     : 2nd");
                            }
                            else if (noHelmet[i, 3] == "2nd")
                            {
                                SetColor("yellow", "black");
                                WriteCenter("+-------------------------------+", 1);
                                WriteCenter("|    Existing Record(s) Found   |", 0);
                                WriteCenter("|    Offense Type     : 2nd     |", 0);
                                WriteCenter("+-------------------------------+", 0);
                                ResetToDefaultColor();
                                noHelmet[i + 1, 3] = "3rd";
                                Console.WriteLine("\n\t\t\t\t\t\tOffense Type     : 3rd");
                            }
                        }
                    }

                    if (recordFound == false)
                    {
                        SetColor("yellow", "black");
                        WriteCenter("+-------------------------------+", 1);
                        WriteCenter("|      No Record(s) Found       |", 0);
                        WriteCenter("+-------------------------------+", 0);
                        ResetToDefaultColor();
                        noHelmet[noHelmetCount, 3] = "1st";
                        Console.WriteLine("\n\t\t\t\t\t\tOffense Type     : 1st ");
                    }
                    noHelmet[noHelmetCount, 2] = name;
                    Console.Write("\t\t\t\t\t\tPayment          : ");
                    noHelmet[noHelmetCount, 4] = Console.ReadLine();
                    noHelmetCount++;

                    break;

                case "D":
                    Console.Write("\n\t\t\t\t\t\tDate             : ");
                    noInsurance[noInsuranceCount, 0] = Console.ReadLine();

                    Console.Write("\t\t\t\t\t\tTicket Number    : ");

                    ticketNum++;
                    SetColor("yellow", "black");
                    Console.WriteLine("{0}", noInsurance[noInsuranceCount, 1] = ticketNum.ToString("TN-000"));
                    ResetToDefaultColor();

                    Console.Write("\t\t\t\t\t\tName             : ");
                    name = Console.ReadLine();

                    for (int i = 0; i < noInsuranceCount; i++)
                    {
                        if (noInsurance[i, 2] == name)
                        {
                            recordFound = true;
                            if (noInsurance[i, 3] == "1st")
                            {
                                SetColor("yellow", "black");
                                WriteCenter("+-------------------------------+", 1);
                                WriteCenter("|    Existing Record(s) Found   |", 0);
                                WriteCenter("|    Offense Type     : 1st     |", 0);
                                WriteCenter("+-------------------------------+", 0);
                                ResetToDefaultColor();
                                noInsurance[i + 1, 3] = "2nd";
                                Console.WriteLine("\n\t\t\t\t\t\tOffense Type     : 2nd");
                            }
                            else if (noInsurance[i, 3] == "2nd")
                            {
                                SetColor("yellow", "black");
                                WriteCenter("+-------------------------------+", 1);
                                WriteCenter("|    Existing Record(s) Found   |", 0);
                                WriteCenter("|    Offense Type     : 2nd     |", 0);
                                WriteCenter("+-------------------------------+", 0);
                                ResetToDefaultColor();
                                noInsurance[i + 1, 3] = "3rd";
                                Console.WriteLine("\n\t\t\t\t\t\tOffense Type     : 3rd");
                            }
                        }
                    }


                    if (recordFound == false)
                    {
                        SetColor("yellow", "black");
                        WriteCenter("+-------------------------------+", 1);
                        WriteCenter("|      No Record(s) Found       |", 0);
                        WriteCenter("+-------------------------------+", 0);
                        ResetToDefaultColor();
                        noInsurance[noInsuranceCount, 3] = "1st";
                        Console.WriteLine("\n\t\t\t\t\t\tOffense Type     : 1st ");
                    }
                    noInsurance[noInsuranceCount, 2] = name;
                    Console.Write("\t\t\t\t\t\tPayment          : ");
                    noInsurance[noInsuranceCount, 4] = Console.ReadLine();
                    noInsuranceCount++;

                    break;

                case "E":
                    Console.Write("\n\t\t\t\t\t\tDate             : ");
                    dui[duiCount, 0] = Console.ReadLine();

                    Console.Write("\t\t\t\t\t\tTicket Number    : ");

                    ticketNum++;
                    SetColor("yellow", "black");
                    Console.WriteLine("{0}", dui[duiCount, 1] = ticketNum.ToString("TN-000"));
                    ResetToDefaultColor();

                    Console.Write("\t\t\t\t\t\tName             : ");
                    name = Console.ReadLine();

                    for (int i = 0; i < duiCount; i++)
                    {
                        if (dui[i, 2] == name)
                        {
                            recordFound = true;
                            if (dui[i, 3] == "1st")
                            {
                                SetColor("yellow", "black");
                                WriteCenter("+-------------------------------+", 1);
                                WriteCenter("|    Existing Record(s) Found   |", 0);
                                WriteCenter("|    Offense Type     : 1st     |", 0);
                                WriteCenter("+-------------------------------+", 0);
                                ResetToDefaultColor();
                                dui[i + 1, 3] = "2nd";
                                Console.WriteLine("\n\t\t\t\t\t\tOffense Type     : 2nd");
                            }
                            else if (dui[i, 3] == "2nd")
                            {
                                SetColor("yellow", "black");
                                WriteCenter("+-------------------------------+", 1);
                                WriteCenter("|    Existing Record(s) Found   |", 0);
                                WriteCenter("|    Offense Type     : 2nd     |", 0);
                                WriteCenter("+-------------------------------+", 0);
                                ResetToDefaultColor();
                                dui[i + 1, 3] = "3rd";
                                Console.WriteLine("\n\t\t\t\t\t\tOffense Type     : 3rd");
                            }
                        }
                    }

                    if (recordFound == false)
                    {
                        SetColor("yellow", "black");
                        WriteCenter("+-------------------------------+", 1);
                        WriteCenter("|      No Record(s) Found       |", 0);
                        WriteCenter("+-------------------------------+", 0);
                        ResetToDefaultColor();
                        dui[duiCount, 3] = "1st";
                        Console.WriteLine("\n\t\t\t\t\t\tOffense Type     : 1st ");
                    }

                    dui[duiCount, 2] = name;
                    Console.Write("\t\t\t\t\t\tPayment          : ");
                    dui[duiCount, 4] = Console.ReadLine();
                    duiCount++;

                    break;
            }
            return;
        }

        static void ViewTicketViolationSummary()
        {
            Console.Clear();

            SetColor("yellow", "yellow");
            hr(0);
            ResetToDefaultColor();

            Console.WriteLine(" Violation            | Date           | Ticket Number | Name                      " +
                "| Offense Type | Payment     |");

            SetColor("black", "black");
            hr(0);
            ResetToDefaultColor();

            for (int i = 0; i < duiCount; i++)
            {
                tableRow();

                Console.SetCursorPosition(0, Console.CursorTop);
                Console.WriteLine("  D U I");
                Console.SetCursorPosition(22, Console.CursorTop - 1);
                Console.WriteLine("| " + dui[i, 0]);
                Console.SetCursorPosition(39, Console.CursorTop - 1);
                Console.WriteLine("| " + dui[i, 1]);
                Console.SetCursorPosition(55, Console.CursorTop - 1);
                Console.WriteLine("| " + dui[i, 2]);
                Console.SetCursorPosition(83, Console.CursorTop - 1);
                Console.WriteLine("| " + dui[i, 3]);
                Console.SetCursorPosition(98, Console.CursorTop - 1);
                Console.WriteLine("| " + dui[i, 4]);
                Console.SetCursorPosition(112, Console.CursorTop - 1);
                Console.WriteLine("|");
            }

            for (int i = 0; i < noHelmetCount; i++)
            {
                tableRow();

                Console.SetCursorPosition(0, Console.CursorTop);
                Console.WriteLine("  No Helmet");
                Console.SetCursorPosition(22, Console.CursorTop - 1);
                Console.WriteLine("| " + noHelmet[i, 0]);
                Console.SetCursorPosition(39, Console.CursorTop - 1);
                Console.WriteLine("| " + noHelmet[i, 1]);
                Console.SetCursorPosition(55, Console.CursorTop - 1);
                Console.WriteLine("| " + noHelmet[i, 2]);
                Console.SetCursorPosition(83, Console.CursorTop - 1);
                Console.WriteLine("| " + noHelmet[i, 3]);
                Console.SetCursorPosition(98, Console.CursorTop - 1);
                Console.WriteLine("| " + noHelmet[i, 4]);
                Console.SetCursorPosition(112, Console.CursorTop - 1);
                Console.WriteLine("|");
            }

            for (int i = 0; i < noInsuranceCount; i++)
            {
                tableRow();

                Console.SetCursorPosition(0, Console.CursorTop);
                Console.WriteLine("  No Insurance");
                Console.SetCursorPosition(22, Console.CursorTop - 1);
                Console.WriteLine("| " + noInsurance[i, 0]);
                Console.SetCursorPosition(39, Console.CursorTop - 1);
                Console.WriteLine("| " + noInsurance[i, 1]);
                Console.SetCursorPosition(55, Console.CursorTop - 1);
                Console.WriteLine("| " + noInsurance[i, 2]);
                Console.SetCursorPosition(83, Console.CursorTop - 1);
                Console.WriteLine("| " + noInsurance[i, 3]);
                Console.SetCursorPosition(98, Console.CursorTop - 1);
                Console.WriteLine("| " + noInsurance[i, 4]);
                Console.SetCursorPosition(112, Console.CursorTop - 1);
                Console.WriteLine("|");
            }

            for (int i = 0; i < noLicenseCount; i++)
            {
                /* WriteCenter("---------NO LICENSE---------", 0);
                WriteCenter("Date    : " + noLicense[i, 0], 0);
                WriteCenter("Ticket #: " + noLicense[i, 1], 0);
                WriteCenter("Name    : " + noLicense[i, 2], 0);
                WriteCenter("Offense : " + noLicense[i, 3], 0);
                WriteCenter("Payment : " + noLicense[i, 4], 0);
                Console.WriteLine(); */

                /* Console.WriteLine("\tNo Registration | Date    : " + noRegistration[i, 1] + "Ticket #: " + noRegistration[i, 2] +
                    "Name    : " + noRegistration[i, 3] + "Offense : " + noRegistration[i, 4] + "Payment : " + noRegistration[i, 0], 0); */

                tableRow();

                Console.SetCursorPosition(0, Console.CursorTop);
                Console.WriteLine("  No License");
                Console.SetCursorPosition(22, Console.CursorTop - 1);
                Console.WriteLine("| " + noLicense[i, 0]);
                Console.SetCursorPosition(39, Console.CursorTop - 1);
                Console.WriteLine("| " + noLicense[i, 1]);
                Console.SetCursorPosition(55, Console.CursorTop - 1);
                Console.WriteLine("| " + noLicense[i, 2]);
                Console.SetCursorPosition(83, Console.CursorTop - 1);
                Console.WriteLine("| " + noLicense[i, 3]);
                Console.SetCursorPosition(98, Console.CursorTop - 1);
                Console.WriteLine("| " + noLicense[i, 4]);
                Console.SetCursorPosition(112, Console.CursorTop - 1);
                Console.WriteLine("|");
            }

            for (int i = 0; i < noRegistrationCount; i++)
            {
                tableRow();

                Console.SetCursorPosition(0, Console.CursorTop);
                Console.WriteLine("  No Registration");
                Console.SetCursorPosition(22, Console.CursorTop - 1);
                Console.WriteLine("| " + noRegistration[i, 0]);
                Console.SetCursorPosition(39, Console.CursorTop - 1);
                Console.WriteLine("| " + noRegistration[i, 1]);
                Console.SetCursorPosition(55, Console.CursorTop - 1);
                Console.WriteLine("| " + noRegistration[i, 2]);
                Console.SetCursorPosition(83, Console.CursorTop - 1);
                Console.WriteLine("| " + noRegistration[i, 3]);
                Console.SetCursorPosition(98, Console.CursorTop - 1);
                Console.WriteLine("| " + noRegistration[i, 4]);
                Console.SetCursorPosition(112, Console.CursorTop - 1);
                Console.WriteLine("|");
            }

            SetColor("yellow", "yellow");
            hr(0);
            ResetToDefaultColor();

            Console.ReadKey();
        }

        static void LoginPage(bool loggedIn)
        {
            string user, pass;
            ConsoleKeyInfo toExit;

            Console.Clear();
            Console.CursorVisible = true;

            if (loggedIn == true)
            {
                SetColor("yellow", "black");
                WriteCenter("+------------------------+", 12);
                WriteCenter("CONFIRMATION", -1);
                WriteCenter("|                        |", 0);
                WriteCenter("|                        |", 0);
                WriteCenter("[Y/N] Exit", -1);
                WriteCenter("|                        |", 0);
                WriteCenter("+------------------------+", 0);

                Console.CursorVisible = false;
                toExit = Console.ReadKey(true);

                if (toExit.Key == ConsoleKey.N)
                {
                    WriteCenter("  [N] Exit", -3);
                    Console.CursorVisible = true;
                    Console.SetCursorPosition((Console.WindowWidth - 3) / 2, Console.CursorTop - 1);
                    loggedIn = false;
                    Thread.Sleep(1000);
                }
                else if (toExit.Key == ConsoleKey.Y)
                {
                    WriteCenter("[Y] Exit  ", -3);
                    Console.CursorVisible = true;
                    Console.SetCursorPosition((Console.WindowWidth - 3) / 2, Console.CursorTop - 1);
                    loggedIn = true;
                    Thread.Sleep(1000);

                    Environment.Exit(0);
                }
            }

            while (loggedIn == false)
            {
                ResetToDefaultColor();
                Console.Clear();

                hr(0);
                hr(27);

                WriteCenter("▀█▀ █▀▄ ▄▀▄ █▀ █▀ █ ▄▀▀   ▀█▀ █ ▄▀▀ █▄▀ ██▀ ▀█▀   █▀▄ ██▀ ▄▀  █ ▄▀▀ ▀█▀ █▀▄ ▀▄▀   ▄▀▀ ▀▄▀ ▄▀▀ ▀█▀ ██▀ █▄ ▄█", -23);
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

                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;

                user = ReadField(-8, "user/pass").ToString();
                WriteCenter("\n", 0);
                Console.ResetColor();
                pass = ReadField(1, "user/pass").ToString();

                bool authentication = Authenticate(user, pass);

                if (authentication == true)
                {
                    loggedIn = true;
                    ResetToDefaultColor();
                    Console.Clear();
                    return;
                }

                ResetToDefaultColor();
                Console.Clear();
            }
        }
        static bool Authenticate(string username, string password)
        {
            int cWidth = Console.WindowWidth;
            Console.BackgroundColor = ConsoleColor.Yellow;

            if (username == "enforcer" && password == "imissyou")
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Thread.Sleep(150);
                WriteCenter("LOGGING IN", 2);

                int j = 1;

                for (int i = 0; i < 3; i++)
                {
                    if (i != 0)
                        j = 0;

                    Console.SetCursorPosition(65 + i, Console.CursorTop - j);
                    Console.Write(".");
                    Thread.Sleep(1000);
                }

                Console.SetCursorPosition(65, Console.CursorTop - 1);
                // Thread.Sleep(3000);

                return true;
            }
            else if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Thread.Sleep(150);
                WriteCenter("MISSING USERNAME/PASSWORD", 2);
                Console.SetCursorPosition(72, Console.CursorTop - 1);
                Thread.Sleep(3000);

                return false;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Thread.Sleep(150);
                WriteCenter("INCORRECT USERNAME/PASSWORD", 2);
                Console.SetCursorPosition(73, Console.CursorTop - 1);
                Thread.Sleep(3000);
            }

            Console.ResetColor();
            return false;
        }

        // Beyond this are methods for UI Design.

        static void hr(int cPosition) // Inspired by HTML's horizontal rule
        {
            int cWidth = Console.WindowWidth;

            Console.SetCursorPosition(0, Console.CursorTop + cPosition);

            SetColor("yellow", "yellow");
            for (int i = 0; i < cWidth; i++)
            {
                Console.Write(" ");
            }
            ResetToDefaultColor();
        }

        static void tableRow()
        {
            for (int i = 0; i < Console.WindowWidth; i++)
            {
                Console.Write(" ");
            }
        }
        static void WriteCenter(string text, int cPosition)
        {
            int cWidth = Console.WindowWidth;

            Console.SetCursorPosition((cWidth - text.Length) / 2, Console.CursorTop + cPosition);

            Console.WriteLine(text);
        }

        static string ReadField(int cPosition, string fieldType)
        {
            if (fieldType == "user/pass")
            {
                int cWidth = Console.WindowWidth;

                Console.SetCursorPosition((cWidth - "********".Length) / 2, Console.CursorTop + cPosition);
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("        ");

                Console.SetCursorPosition((cWidth - "********".Length) / 2, Console.CursorTop - 1);

                StringBuilder input = new StringBuilder();

                int i = 0;

                while (i < 8)
                {
                    ConsoleKeyInfo inputtedChar = Console.ReadKey();
                    input.Append(inputtedChar.KeyChar);
                    i++;

                    if (inputtedChar.Key == ConsoleKey.Enter) // Logic error, the append should be after this
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
            else
            {
                int cWidth = Console.WindowWidth;

                Console.SetCursorPosition((cWidth - 16) / 2, Console.CursorTop + cPosition);
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("                ");

                Console.SetCursorPosition((cWidth - 16) / 2, Console.CursorTop - 1);

                StringBuilder input = new StringBuilder();

                int i = 0;

                while (i < 16)
                {
                    ConsoleKeyInfo inputtedChar = Console.ReadKey();
                    i++;

                    if (inputtedChar.Key == ConsoleKey.Enter)
                        break;

                    input.Append(inputtedChar.KeyChar);

                    if (inputtedChar.Key == ConsoleKey.Backspace)
                    {
                        if (input.Length < 2)
                        {
                            Console.SetCursorPosition(Console.CursorLeft + 1, Console.CursorTop);
                            input.Remove(0, 1);
                            i--;
                            continue;
                        }
                        {
                            input.Remove(i - 1, 1);
                            i -= 2;
                            Console.Write(" \b");
                            input.Remove(i, 1);
                        }
                    }
                }
                return input.ToString();
            }
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
                case "red":
                    Console.BackgroundColor = ConsoleColor.Red;
                    break;
                case "darkyellow":
                    Console.BackgroundColor = ConsoleColor.DarkYellow;
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
                Thread.Sleep(1000);
                return;
            }
        }
    }
}
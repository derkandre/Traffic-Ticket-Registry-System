using System;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Dynamic;
using System.Linq;
using System.Net.Sockets;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading;
using System.Timers;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ttrs
{
    class Program
    {
        static string[,] dui = new string[999, 5];
        static string[,] noHelmet = new string[999, 5];
        static string[,] noLicense = new string[999, 5];
        static string[,] noInsurance = new string[999, 5];
        static string[,] noRegistration = new string[999, 5];

        static int noRegistrationCount = 0;
        static int noInsuranceCount = 0;
        static int noLicenseCount = 0;
        static int noHelmetCount = 0;
        static int duiCount = 0;

        static int ticketNum = 0;
        static int offenseTrack = 0;
        static int recordCounts = 0;

        static void Main(string[] args)
        {
            System.Console.OutputEncoding = System.Text.Encoding.Unicode;
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

                    WriteCenter("TTRS v0.08a", 7);

                    hr(1);

                    ConsoleKeyInfo menuChoice = Console.ReadKey(true);

                    SetColor("black", "yellow");

                    if (menuChoice.Key == ConsoleKey.D1)
                    {
                        RegisterTicketViolation();
                    }
                    else if (menuChoice.Key == ConsoleKey.D2)
                    {
                        SearchViolation();
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

                        Console.ReadKey();
                    }
                    else
                    {
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
            int result = 0;
            bool recordFound = false;

            Console.Clear();

            hr(0);
            hr(28);

            SetColor("yellow", "black");
            WriteCenter("+------------------------------------------+", -18);
            WriteCenter("List of Violations", -1);
            WriteCenter("|                                          |", 0);
            WriteCenter("|  [A] No License                          |", 0);
            WriteCenter("|  [B] No Registration                     |", 0);
            WriteCenter("|  [C] No Helmet                           |", 0);
            WriteCenter("|  [D] No Insurance                        |", 0);
            WriteCenter("|  [E] Driving Under the Influence (DUI)   |", 0);
            WriteCenter("|                                          |", 0);
            WriteCenter("+------------------------------------------+", 0);
            ResetToDefaultColor();

            ConsoleKeyInfo choice = Console.ReadKey(true);

            Console.Clear();
            Console.CursorVisible = true;

            hr(0);
            hr(28);

            Console.SetCursorPosition(0, Console.CursorTop - 28);

            switch (choice.Key)
            {
                case ConsoleKey.A:
                    int i;
                    result = 0;

                    SetColor("yellow", "black");
                    WriteCenter("            NO LICENSE           ", 5);
                    ResetToDefaultColor();

                    CheckDate("noLicense");

                    Console.Write("\n\t\t\t\t\t\tTicket Number    : ");

                    ticketNum++;
                    SetColor("yellow", "black");
                    Console.Write("{0}", noLicense[noLicenseCount, 1] = ticketNum.ToString("TN-000"));
                    ResetToDefaultColor();

                    Console.Write("\n\n\t\t\t\t\t\tName             : ");
                    name = Console.ReadLine();

                    for (i = 0; i < noLicenseCount; i++)
                    {
                        if (noLicense[i, 2] == name)
                        {
                            recordFound = true;

                            if (result == 0)
                            {
                                SetColor("yellow", "black");
                                WriteCenter("+-------------------------------+", 1);
                                WriteCenter("|    Existing Record(s) Found   |", 0);
                            }

                            if (noLicense[i, 3] == "1st")
                            {
                                result = 1;
                                noLicense[noLicenseCount, 3] = "2nd";
                                noLicense[noLicenseCount, 4] = 1250.ToString();
                            }
                            if (noLicense[i, 3] == "2nd")
                            {
                                noLicense[noLicenseCount, 3] = "3rd";
                                noLicense[noLicenseCount, 4] = 3000.ToString();
                                result = 2;
                            }
                            if (noLicense[i, 3] == "3rd")
                            {
                                result = 3;
                                noLicense[noLicenseCount, 3] = "Suspended";
                                noLicense[noLicenseCount, 4] = 10000.ToString();
                            }
                            if (noLicense[i, 3] == "Suspended")
                            {
                                noLicense[noLicenseCount, 3] = "Suspended";
                                result = 4;
                            }
                        }
                    }

                    if (result == 1)
                    {
                        WriteCenter("|    Offense Type     : 1st     |", 0);
                    }
                    if (result == 2)
                    {
                        WriteCenter("|    Offense Type     : 1st     |", 0);
                        WriteCenter("|    Offense Type     : 2nd     |", 0);
                    }
                    if (result == 3)
                    {
                        WriteCenter("|    Offense Type     : 1st     |", 0);
                        WriteCenter("|    Offense Type     : 2nd     |", 0);
                        WriteCenter("|    Offense Type     : 3rd     |", 0);
                    }
                    if (result == 4)
                    {
                        WriteCenter("+-------------------------------+", 0);
                        WriteCenter("|           ⚠  NOTICE           |", -2);
                        WriteCenter("|                               |", 0);
                        WriteCenter("|      DRIVER'S LICENSE IS      |", 0);
                        WriteCenter("|           SUSPENDED           |", 0);
                        WriteCenter("|                               |", 0);

                        ticketNum--;
                    }

                    if (result != 0)
                    {
                        WriteCenter("+-------------------------------+", 0);

                        ResetToDefaultColor();

                        if (result == 1)
                            Console.WriteLine("\n\t\t\t\t\t\tOffense Type     : 2nd ");
                        if (result == 2)
                            Console.WriteLine("\n\t\t\t\t\t\tOffense Type     : 3rd ");
                        if (result == 3)
                            Console.WriteLine("\n\t\t\t\t\t\tOffense Type     : Suspended");
                    }

                    ResetToDefaultColor();

                    if (recordFound == false)
                    {
                        SetColor("yellow", "black");
                        WriteCenter("+-------------------------------+", 1);
                        WriteCenter("|      No Record(s) Found       |", 0);
                        WriteCenter("+-------------------------------+", 0);
                        ResetToDefaultColor();
                        noLicense[noLicenseCount, 3] = "1st";
                        Console.WriteLine("\n\t\t\t\t\t\tOffense Type     : 1st ");
                        noLicense[noLicenseCount, 4] = 500.ToString();
                    }

                    noLicense[noLicenseCount, 2] = name;
                    Console.Write("\n\t\t\t\t\t\tPayment          : {0}", noLicense[noLicenseCount, 4]);

                    if (result != 4)
                    {
                        noLicenseCount++;
                        recordCounts++;
                    }

                    break;

                case ConsoleKey.B:
                    result = 0;

                    SetColor("yellow", "black");
                    WriteCenter("         NO REGISTRATION         ", 5);
                    ResetToDefaultColor();

                    CheckDate("noRegistration");

                    Console.Write("\n\t\t\t\t\t\tTicket Number    : ");

                    ticketNum++;
                    SetColor("yellow", "black");
                    Console.Write("{0}", noRegistration[noRegistrationCount, 1] = ticketNum.ToString("TN-000"));
                    ResetToDefaultColor();

                    Console.Write("\n\n\t\t\t\t\t\tName             : ");
                    name = Console.ReadLine();

                    for (i = 0; i < noRegistrationCount; i++)
                    {
                        if (noRegistration[i, 2] == name)
                        {
                            recordFound = true;

                            if (result == 0)
                            {
                                SetColor("yellow", "black");
                                WriteCenter("+-------------------------------+", 1);
                                WriteCenter("|    Existing Record(s) Found   |", 0);
                            }

                            if (noRegistration[i, 3] == "1st")
                            {
                                result = 1;
                                noRegistration[noRegistrationCount, 3] = "2nd";
                                noRegistration[noRegistrationCount, 4] = 1250.ToString();
                            }
                            if (noRegistration[i, 3] == "2nd")
                            {
                                noRegistration[noRegistrationCount, 3] = "3rd";
                                noRegistration[noRegistrationCount, 4] = 3000.ToString();
                                result = 2;
                            }
                            if (noRegistration[i, 3] == "3rd")
                            {
                                result = 3;
                                noRegistration[noRegistrationCount, 3] = "Suspended";
                                noRegistration[noRegistrationCount, 4] = 10000.ToString();
                            }
                            if (noRegistration[i, 3] == "Suspended")
                            {
                                noRegistration[noRegistrationCount, 3] = "Suspended";
                                result = 4;
                            }
                        }
                    }

                    if (result == 1)
                    {
                        WriteCenter("|    Offense Type     : 1st     |", 0);
                    }
                    if (result == 2)
                    {
                        WriteCenter("|    Offense Type     : 1st     |", 0);
                        WriteCenter("|    Offense Type     : 2nd     |", 0);
                    }
                    if (result == 3)
                    {
                        WriteCenter("|    Offense Type     : 1st     |", 0);
                        WriteCenter("|    Offense Type     : 2nd     |", 0);
                        WriteCenter("|    Offense Type     : 3rd     |", 0);
                    }
                    if (result == 4)
                    {
                        WriteCenter("+-------------------------------+", 0);
                        WriteCenter("|           ⚠  NOTICE           |", -2);
                        WriteCenter("|                               |", 0);
                        WriteCenter("|      DRIVER'S LICENSE IS      |", 0);
                        WriteCenter("|           SUSPENDED           |", 0);
                        WriteCenter("|                               |", 0);

                        ticketNum--;
                    }

                    if (result != 0)
                    {
                        WriteCenter("+-------------------------------+", 0);

                        ResetToDefaultColor();

                        if (result == 1)
                            Console.WriteLine("\n\t\t\t\t\t\tOffense Type     : 2nd ");
                        if (result == 2)
                            Console.WriteLine("\n\t\t\t\t\t\tOffense Type     : 3rd ");
                        if (result == 3)
                            Console.WriteLine("\n\t\t\t\t\t\tOffense Type     : Suspended");
                    }

                    ResetToDefaultColor();

                    if (recordFound == false)
                    {
                        SetColor("yellow", "black");
                        WriteCenter("+-------------------------------+", 1);
                        WriteCenter("|      No Record(s) Found       |", 0);
                        WriteCenter("+-------------------------------+", 0);
                        ResetToDefaultColor();
                        noRegistration[noRegistrationCount, 3] = "1st";
                        Console.WriteLine("\n\t\t\t\t\t\tOffense Type     : 1st ");
                        noRegistration[noRegistrationCount, 4] = 500.ToString();
                    }

                    noRegistration[noRegistrationCount, 2] = name;
                    Console.Write("\n\t\t\t\t\t\tPayment          : {0}", noRegistration[noRegistrationCount, 4]);

                    if (result != 4)
                    {
                        noRegistrationCount++;
                        recordCounts++;
                    }

                    break;

                case ConsoleKey.C:
                    result = 0;

                    SetColor("yellow", "black");
                    WriteCenter("            NO HELMET            ", 5);
                    ResetToDefaultColor();

                    CheckDate("noHelmet");

                    Console.Write("\n\t\t\t\t\t\tTicket Number    : ");

                    ticketNum++;
                    SetColor("yellow", "black");
                    Console.Write("{0}", noHelmet[noHelmetCount, 1] = ticketNum.ToString("TN-000"));
                    ResetToDefaultColor();

                    Console.Write("\n\n\t\t\t\t\t\tName             : ");
                    name = Console.ReadLine();

                    for (i = 0; i < noHelmetCount; i++)
                    {
                        if (noHelmet[i, 2] == name)
                        {
                            recordFound = true;

                            if (result == 0)
                            {
                                SetColor("yellow", "black");
                                WriteCenter("+-------------------------------+", 1);
                                WriteCenter("|    Existing Record(s) Found   |", 0);
                            }

                            if (noHelmet[i, 3] == "1st")
                            {
                                result = 1;
                                noHelmet[noHelmetCount, 3] = "2nd";
                                noHelmet[noHelmetCount, 4] = 1250.ToString();
                            }
                            if (noHelmet[i, 3] == "2nd")
                            {
                                noHelmet[noHelmetCount, 3] = "3rd";
                                noHelmet[noHelmetCount, 4] = 3000.ToString();
                                result = 2;
                            }
                            if (noHelmet[i, 3] == "3rd")
                            {
                                result = 3;
                                noHelmet[noHelmetCount, 3] = "Suspended";
                                noHelmet[noHelmetCount, 4] = 10000.ToString();
                            }
                            if (noHelmet[i, 3] == "Suspended")
                            {
                                noHelmet[noHelmetCount, 3] = "Suspended";
                                result = 4;
                            }
                        }
                    }

                    if (result == 1)
                    {
                        WriteCenter("|    Offense Type     : 1st     |", 0);
                    }
                    if (result == 2)
                    {
                        WriteCenter("|    Offense Type     : 1st     |", 0);
                        WriteCenter("|    Offense Type     : 2nd     |", 0);
                    }
                    if (result == 3)
                    {
                        WriteCenter("|    Offense Type     : 1st     |", 0);
                        WriteCenter("|    Offense Type     : 2nd     |", 0);
                        WriteCenter("|    Offense Type     : 3rd     |", 0);
                    }
                    if (result == 4)
                    {
                        WriteCenter("+-------------------------------+", 0);
                        WriteCenter("|           ⚠  NOTICE           |", -2);
                        WriteCenter("|                               |", 0);
                        WriteCenter("|      DRIVER'S LICENSE IS      |", 0);
                        WriteCenter("|           SUSPENDED           |", 0);
                        WriteCenter("|                               |", 0);

                        ticketNum--;
                    }

                    if (result != 0)
                    {
                        WriteCenter("+-------------------------------+", 0);

                        ResetToDefaultColor();

                        if (result == 1)
                            Console.WriteLine("\n\t\t\t\t\t\tOffense Type     : 2nd ");
                        if (result == 2)
                            Console.WriteLine("\n\t\t\t\t\t\tOffense Type     : 3rd ");
                        if (result == 3)
                            Console.WriteLine("\n\t\t\t\t\t\tOffense Type     : Suspended");
                    }

                    ResetToDefaultColor();

                    if (recordFound == false)
                    {
                        SetColor("yellow", "black");
                        WriteCenter("+-------------------------------+", 1);
                        WriteCenter("|      No Record(s) Found       |", 0);
                        WriteCenter("+-------------------------------+", 0);
                        ResetToDefaultColor();
                        noHelmet[noHelmetCount, 3] = "1st";
                        Console.WriteLine("\n\t\t\t\t\t\tOffense Type     : 1st ");
                        noHelmet[noHelmetCount, 4] = 500.ToString();
                    }

                    noHelmet[noHelmetCount, 2] = name;
                    Console.Write("\n\t\t\t\t\t\tPayment          : {0}", noHelmet[noHelmetCount, 4]);

                    if (result != 4)
                    {
                        noHelmetCount++;
                        recordCounts++;
                    }

                    break;

                case ConsoleKey.D:
                    result = 0;

                    SetColor("yellow", "black");
                    WriteCenter("          NO INSURANCE           ", 5);
                    ResetToDefaultColor();

                    CheckDate("noInsurance");

                    Console.Write("\n\t\t\t\t\t\tTicket Number    : ");

                    ticketNum++;
                    SetColor("yellow", "black");
                    Console.Write("{0}", noInsurance[noInsuranceCount, 1] = ticketNum.ToString("TN-000"));
                    ResetToDefaultColor();

                    Console.Write("\n\n\t\t\t\t\t\tName             : ");
                    name = Console.ReadLine();

                    for (i = 0; i < noInsuranceCount; i++)
                    {
                        if (noInsurance[i, 2] == name)
                        {
                            recordFound = true;

                            if (result == 0)
                            {
                                SetColor("yellow", "black");
                                WriteCenter("+-------------------------------+", 1);
                                WriteCenter("|    Existing Record(s) Found   |", 0);
                            }

                            if (noInsurance[i, 3] == "1st")
                            {
                                result = 1;
                                noInsurance[noInsuranceCount, 3] = "2nd";
                                noInsurance[noInsuranceCount, 4] = 1250.ToString();
                            }
                            if (noInsurance[i, 3] == "2nd")
                            {
                                noInsurance[noInsuranceCount, 3] = "3rd";
                                noInsurance[noInsuranceCount, 4] = 3000.ToString();
                                result = 2;
                            }
                            if (noInsurance[i, 3] == "3rd")
                            {
                                result = 3;
                                noInsurance[noInsuranceCount, 3] = "Suspended";
                                noInsurance[noInsuranceCount, 4] = 10000.ToString();
                            }
                            if (noInsurance[i, 3] == "Suspended")
                            {
                                noInsurance[noInsuranceCount, 3] = "Suspended";
                                result = 4;
                            }
                        }
                    }

                    if (result == 1)
                    {
                        WriteCenter("|    Offense Type     : 1st     |", 0);
                    }
                    if (result == 2)
                    {
                        WriteCenter("|    Offense Type     : 1st     |", 0);
                        WriteCenter("|    Offense Type     : 2nd     |", 0);
                    }
                    if (result == 3)
                    {
                        WriteCenter("|    Offense Type     : 1st     |", 0);
                        WriteCenter("|    Offense Type     : 2nd     |", 0);
                        WriteCenter("|    Offense Type     : 3rd     |", 0);
                    }
                    if (result == 4)
                    {
                        WriteCenter("+-------------------------------+", 0);
                        WriteCenter("|           ⚠  NOTICE           |", -2);
                        WriteCenter("|                               |", 0);
                        WriteCenter("|      DRIVER'S LICENSE IS      |", 0);
                        WriteCenter("|           SUSPENDED           |", 0);
                        WriteCenter("|                               |", 0);

                        ticketNum--;
                    }

                    if (result != 0)
                    {
                        WriteCenter("+-------------------------------+", 0);

                        ResetToDefaultColor();

                        if (result == 1)
                            Console.WriteLine("\n\t\t\t\t\t\tOffense Type     : 2nd ");
                        if (result == 2)
                            Console.WriteLine("\n\t\t\t\t\t\tOffense Type     : 3rd ");
                        if (result == 3)
                            Console.WriteLine("\n\t\t\t\t\t\tOffense Type     : Suspended");
                    }

                    ResetToDefaultColor();

                    if (recordFound == false)
                    {
                        SetColor("yellow", "black");
                        WriteCenter("+-------------------------------+", 1);
                        WriteCenter("|      No Record(s) Found       |", 0);
                        WriteCenter("+-------------------------------+", 0);
                        ResetToDefaultColor();
                        noInsurance[noInsuranceCount, 3] = "1st";
                        Console.WriteLine("\n\t\t\t\t\t\tOffense Type     : 1st ");
                        noInsurance[noInsuranceCount, 4] = 500.ToString();
                    }

                    noInsurance[noInsuranceCount, 2] = name;
                    Console.Write("\n\t\t\t\t\t\tPayment          : {0}", noInsurance[noInsuranceCount, 4]);

                    if (result != 4)
                    {
                        noInsuranceCount++;
                        recordCounts++;
                    }

                    break;

                case ConsoleKey.E:
                    result = 0;

                    SetColor("yellow", "black");
                    WriteCenter("   DRIVING UNDER THE INFLUENCE   ", 5);
                    ResetToDefaultColor();

                    CheckDate("dui");

                    Console.Write("\n\t\t\t\t\t\tTicket Number    : ");

                    ticketNum++;
                    SetColor("yellow", "black");
                    Console.Write("{0}", dui[duiCount, 1] = ticketNum.ToString("TN-000"));
                    ResetToDefaultColor();

                    Console.Write("\n\n\t\t\t\t\t\tName             : ");
                    name = Console.ReadLine();

                    for (i = 0; i < duiCount; i++)
                    {
                        if (dui[i, 2] == name)
                        {
                            recordFound = true;

                            if (result == 0)
                            {
                                SetColor("yellow", "black");
                                WriteCenter("+-------------------------------+", 1);
                                WriteCenter("|    Existing Record(s) Found   |", 0);
                            }

                            if (dui[i, 3] == "1st")
                            {
                                result = 1;
                                dui[duiCount, 3] = "2nd";
                                dui[duiCount, 4] = 1250.ToString();
                            }
                            if (dui[i, 3] == "2nd")
                            {
                                dui[duiCount, 3] = "3rd";
                                dui[duiCount, 4] = 3000.ToString();
                                result = 2;
                            }
                            if (dui[i, 3] == "3rd")
                            {
                                result = 3;
                                dui[duiCount, 3] = "Suspended";
                                dui[duiCount, 4] = 10000.ToString();
                            }
                            if (dui[i, 3] == "Suspended")
                            {
                                dui[duiCount, 3] = "Suspended";
                                result = 4;
                            }
                        }
                    }

                    if (result == 1)
                    {
                        WriteCenter("|    Offense Type     : 1st     |", 0);
                    }
                    if (result == 2)
                    {
                        WriteCenter("|    Offense Type     : 1st     |", 0);
                        WriteCenter("|    Offense Type     : 2nd     |", 0);
                    }
                    if (result == 3)
                    {
                        WriteCenter("|    Offense Type     : 1st     |", 0);
                        WriteCenter("|    Offense Type     : 2nd     |", 0);
                        WriteCenter("|    Offense Type     : 3rd     |", 0);
                    }
                    if (result == 4)
                    {
                        WriteCenter("+-------------------------------+", 0);
                        WriteCenter("|           ⚠  NOTICE           |", -2);
                        WriteCenter("|                               |", 0);
                        WriteCenter("|      DRIVER'S LICENSE IS      |", 0);
                        WriteCenter("|           SUSPENDED           |", 0);
                        WriteCenter("|                               |", 0);

                        ticketNum--;
                    }

                    if (result != 0)
                    {
                        WriteCenter("+-------------------------------+", 0);

                        ResetToDefaultColor();

                        if (result == 1)
                            Console.WriteLine("\n\t\t\t\t\t\tOffense Type     : 2nd ");
                        if (result == 2)
                            Console.WriteLine("\n\t\t\t\t\t\tOffense Type     : 3rd ");
                        if (result == 3)
                            Console.WriteLine("\n\t\t\t\t\t\tOffense Type     : Suspended");
                    }

                    ResetToDefaultColor();

                    if (recordFound == false)
                    {
                        SetColor("yellow", "black");
                        WriteCenter("+-------------------------------+", 1);
                        WriteCenter("|      No Record(s) Found       |", 0);
                        WriteCenter("+-------------------------------+", 0);
                        ResetToDefaultColor();
                        dui[duiCount, 3] = "1st";
                        Console.WriteLine("\n\t\t\t\t\t\tOffense Type     : 1st ");
                        dui[duiCount, 4] = 500.ToString();
                    }

                    dui[duiCount, 2] = name;
                    Console.Write("\n\t\t\t\t\t\tPayment          : {0}", dui[duiCount, 4]);

                    if (result != 4)
                    {
                        duiCount++;
                        recordCounts++;
                    }

                    break;
            }

            Console.ReadKey();
            return;
        }

        static void ViewTicketViolationSummary()
        {
            Console.Clear();

            hr(0);
            hr(28);

            SetColor("yellow", "black");
            WriteCenter("+------------------------------------------+", -18);
            WriteCenter("View Options", -1);
            WriteCenter("|                                          |", 0);
            WriteCenter("|  [A] View All                            |", 0);
            WriteCenter("|  [B] View by Category                    |", 0);
            WriteCenter("|                                          |", 0);
            WriteCenter("+------------------------------------------+", 0);
            ResetToDefaultColor();

            ConsoleKeyInfo choice = Console.ReadKey(true);

            switch (choice.Key)
            {
                case ConsoleKey.A:
                    ViewAll();
                    break;
                case ConsoleKey.B:
                    ViewByCategory();
                    break;
                default:
                    return;
            }
        }

        static void ViewAll()
        {
            Console.Clear();

            SetColor("yellow", "yellow");
            hr(0);
            ResetToDefaultColor();

            Console.WriteLine(" {0} Record(s) Found", recordCounts);

            SetColor("yellow", "yellow");
            hr(0);
            ResetToDefaultColor();

            Console.WriteLine(" Violation            | Date           | Ticket Number | Name                      " +
                "| Offense Type | Payment     |");

            SetColor("black", "black");
            hr(0);
            ResetToDefaultColor();

            Stopwatch time = new Stopwatch();
            time.Start();

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

            time.Stop();

            SetColor("yellow", "yellow");
            hr(0);
            WriteCenter("Search and Index Time (" + time.ElapsedMilliseconds + "ms)", 2);
            ResetToDefaultColor();

            Console.ReadKey();
        }

        static void ViewByCategory()
        {
            Console.Clear();

            int counter = 0;

            hr(0);
            hr(28);

            SetColor("yellow", "black");
            WriteCenter("+------------------------------------------+", -18);
            WriteCenter("List of Violations", -1);
            WriteCenter("|                                          |", 0);
            WriteCenter("|  [A] No License                          |", 0);
            WriteCenter("|  [B] No Registration                     |", 0);
            WriteCenter("|  [C] No Helmet                           |", 0);
            WriteCenter("|  [D] No Insurance                        |", 0);
            WriteCenter("|  [E] Driving Under the Influence (DUI)   |", 0);
            WriteCenter("|                                          |", 0);
            WriteCenter("+------------------------------------------+", 0);
            ResetToDefaultColor();

            ConsoleKeyInfo choice = Console.ReadKey(true);

            Console.Clear();

            SetColor("yellow", "yellow");
            hr(0);
            ResetToDefaultColor();

            WriteCenter("\n", 0);

            SetColor("yellow", "yellow");
            hr(0);
            ResetToDefaultColor();

            Console.WriteLine(" Violation            | Date           | Ticket Number | Name                      " +
                "| Offense Type | Payment     |");

            SetColor("black", "black");
            hr(0);
            ResetToDefaultColor();

            Stopwatch time = new Stopwatch();
            time.Start();

            switch (choice.Key)
            {
                case ConsoleKey.A:
                    for (int i = 0; i < noLicenseCount; i++)
                    {
                        counter++;

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
                    break;

                case ConsoleKey.B:
                    for (int i = 0; i < noRegistrationCount; i++)
                    {
                        counter++;

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
                    break;

                case ConsoleKey.C:
                    for (int i = 0; i < noHelmetCount; i++)
                    {
                        counter++;

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
                    break;

                case ConsoleKey.D:
                    for (int i = 0; i < noInsuranceCount; i++)
                    {
                        counter++;

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
                    break;

                case ConsoleKey.E:
                    for (int i = 0; i < duiCount; i++)
                    {
                        counter++;

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
                    break;
            }

            time.Stop();

            hr(0);

            if (counter != 0)
            {
                Console.SetCursorPosition(0, Console.CursorTop - counter - 4);
                Console.WriteLine(" {0} Record(s) Found", counter);
                WriteCenter("Search and Index Time (" + time.ElapsedMilliseconds + "ms)", counter + 5);
            }
            else
            {
                Console.SetCursorPosition(0, 1);
                Console.WriteLine(" {0} Record(s) Found", counter);
                WriteCenter("No Record(s) to Display", 4);
                Console.WriteLine();
                hr(0);
                WriteCenter("Search and Index Time (" + time.ElapsedMilliseconds + "ms)", 2);
            }

            Console.ReadKey();
        }

        static void SearchViolation()
        {
            Console.Clear();

            hr(0);
            hr(28);

            SetColor("yellow", "black");
            WriteCenter("+------------------------------------------+", -20);
            WriteCenter("Search Options", -1);
            WriteCenter("|                                          |", 0);
            WriteCenter("|  [A] Search by Name                      |", 0);
            WriteCenter("|  [B] Search by Ticket #                  |", 0);
            WriteCenter("|                                          |", 0);
            WriteCenter("+------------------------------------------+", 0);
            ResetToDefaultColor();

            ConsoleKeyInfo choice = Console.ReadKey(true);

            switch (choice.Key)
            {
                case ConsoleKey.A:
                    SearchByName();
                    break;
                case ConsoleKey.B:
                    SearchByTicket();
                    break;
            }

            return;
        }

        static void SearchByName()
        {
            string name;
            int numEntries = noLicenseCount + noInsuranceCount + noHelmetCount + noRegistrationCount + duiCount;
            int counter = 0;

            Console.Write("\n\n\t\t\t\t\tViolator's Name: ");
            name = Console.ReadLine();

            Console.Clear();
            hr(0);

            for (int i = 0; i < noLicenseCount; i++)
            {
                if (noLicense[i, 2] == name)
                    counter++;
            }
            for (int i = 0; i < duiCount; i++)
            {
                if (dui[i, 2] == name)
                    counter++;
            }
            for (int i = 0; i < noInsuranceCount; i++)
            {
                if (noInsurance[i, 2] == name)
                    counter++;
            }
            for (int i = 0; i < noRegistrationCount; i++)
            {
                if (noRegistration[i, 2] == name)
                    counter++;
            }
            for (int i = 0; i < noHelmetCount; i++)
            {
                if (noHelmet[i, 2] == name)
                    counter++;
            }

            Stopwatch time = new Stopwatch();
            time.Start();

            if (counter > 0)
            {
                Console.WriteLine(" {0} Record(s) Found (out of {1} Entries)", counter, numEntries);

                hr(0);

                Console.Write(" Violation            | Date           | Ticket Number | Name                      " +
                    "| Offense Type | Payment     |");

                hr(1);

                for (int i = 0; i < duiCount; i++)
                {
                    if (dui[i, 2] == name)
                    {
                        tableRow();

                        Console.SetCursorPosition(0, Console.CursorTop);
                        Console.WriteLine("  DUI");
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
                }
                for (int i = 0; i < noHelmetCount; i++)
                {
                    if (noHelmet[i, 2] == name)
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
                }
                for (int i = 0; i < noInsuranceCount; i++)
                {
                    if (noInsurance[i, 2] == name)
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
                }
                for (int i = 0; i < noLicenseCount; i++)
                {

                    if (noLicense[i, 2] == name)
                    {
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
                }
                for (int i = 0; i < noRegistrationCount; i++)
                {
                    if (noRegistration[i, 2] == name)
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
                }
            }
            else
            {
                hr(0);

                Console.WriteLine("  {0} Record(s) Found (out of {1} Entries)", counter, numEntries);

                hr(0);

                Console.WriteLine(" Violation            | Date           | Ticket Number | Name                      " +
                    "| Offense Type | Payment     |");
                hr(0);

                Console.WriteLine();
                WriteCenter("No Record(s) to Display", 1);
                Console.WriteLine();

                hr(0);
            }

            time.Stop();

            counter = 0;

            hr(0);
            WriteCenter("Search and Index Time (" + time.ElapsedMilliseconds + "ms)", 2);
            Console.ReadKey();
        }

        static void SearchByTicket()
        {

            string ticket;

            Console.Write("\n\n\t\t\t\t\tTicket Number: ");
            ticket = Console.ReadLine();
            Console.Clear();


            int counter = 0;

            for (int i = 0; i < noLicenseCount; i++)
            {
                if (noLicense[i, 1] == ticket)
                    counter++;
            }
            for (int i = 0; i < duiCount; i++)
            {
                if (dui[i, 1] == ticket)
                    counter++;
            }
            for (int i = 0; i < noInsuranceCount; i++)
            {
                if (noInsurance[i, 1] == ticket)
                    counter++;
            }
            for (int i = 0; i < noRegistrationCount; i++)
            {
                if (noRegistration[i, 1] == ticket)
                    counter++;
            }
            for (int i = 0; i < noHelmetCount; i++)
            {
                if (noHelmet[i, 1] == ticket)
                    counter++;
            }

            if (counter > 0)
            {
                hr(0);
                WriteCenter("Record Found", 6);
                hr(21);
                for (int i = 0; i < noLicenseCount; i++)
                {
                    if (noLicense[i, 1] == ticket)
                    {
                        SetColor("yellow", "black");                       
                        WriteCenter("                                    ", -20);
                        WriteCenter("No License", -1);
                        ResetToDefaultColor();
                        SetColor("yellow", "black");
                        WriteCenter("|                                  |", 1);
                        SetColor("yellow", "black");
                        WriteCenter("|                                  |", 0);
                        WriteCenter("Date    : " + noLicense[i, 0], -1);
                        WriteCenter("|                                  |", 0);
                        WriteCenter("Ticket #: " + noLicense[i, 1], -1);
                        WriteCenter("|                                  |", 0);
                        WriteCenter("Name    : " + noLicense[i, 2], -1);
                        WriteCenter("|                                  |", 0);
                        WriteCenter("Offense : " + noLicense[i, 3], -1);
                        WriteCenter("|                                  |", 0);
                        WriteCenter("Payment : " + noLicense[i, 4], -1);
                        WriteCenter("|                                  |", 0);
                        ResetToDefaultColor();
                        WriteCenter("                                    ", 0);
                        SetColor("yellow", "black");
                        WriteCenter("                                    ", 0);
                        ResetToDefaultColor();
                    }
                }
                for (int i = 0; i < duiCount; i++)
                {
                    if (dui[i, 1] == ticket)
                    {
                        SetColor("yellow", "black");
                        WriteCenter("                                    ", -20);
                        WriteCenter("No License", -1);
                        ResetToDefaultColor();
                        SetColor("yellow", "black");
                        WriteCenter("|                                  |", 1);
                        SetColor("yellow", "black");
                        WriteCenter("|                                  |", 0);
                        WriteCenter("Date    : " + dui[i, 0], -1);
                        WriteCenter("|                                  |", 0);
                        WriteCenter("Ticket #: " + dui[i, 1], -1);
                        WriteCenter("|                                  |", 0);
                        WriteCenter("Name    : " + dui[i, 2], -1);
                        WriteCenter("|                                  |", 0);
                        WriteCenter("Offense : " + dui[i, 3], -1);
                        WriteCenter("|                                  |", 0);
                        WriteCenter("Payment : " + dui[i, 4], -1);
                        WriteCenter("|                                  |", 0);
                        ResetToDefaultColor();
                        WriteCenter("                                    ", 0);
                        SetColor("yellow", "black");
                        WriteCenter("                                    ", 0);
                        ResetToDefaultColor();
                    }
                }
                for (int i = 0; i < noInsuranceCount; i++)
                {
                    if (noInsurance[i, 1] == ticket)
                    {
                        SetColor("yellow", "black");
                        WriteCenter("                                    ", -20);
                        WriteCenter("No License", -1);
                        ResetToDefaultColor();
                        SetColor("yellow", "black");
                        WriteCenter("|                                  |", 1);
                        SetColor("yellow", "black");
                        WriteCenter("|                                  |", 0);
                        WriteCenter("Date    : " + noInsurance[i, 0], -1);
                        WriteCenter("|                                  |", 0);
                        WriteCenter("Ticket #: " + noInsurance[i, 1], -1);
                        WriteCenter("|                                  |", 0);
                        WriteCenter("Name    : " + noInsurance[i, 2], -1);
                        WriteCenter("|                                  |", 0);
                        WriteCenter("Offense : " + noInsurance[i, 3], -1);
                        WriteCenter("|                                  |", 0);
                        WriteCenter("Payment : " + noInsurance[i, 4], -1);
                        WriteCenter("|                                  |", 0);
                        ResetToDefaultColor();
                        WriteCenter("                                    ", 0);
                        SetColor("yellow", "black");
                        WriteCenter("                                    ", 0);
                        ResetToDefaultColor();
                    }
                }
                for (int i = 0; i < noRegistrationCount; i++)
                {
                    if (noRegistration[i, 1] == ticket)
                    {
                        SetColor("yellow", "black");
                        WriteCenter("                                    ", -20);
                        WriteCenter("No License", -1);
                        ResetToDefaultColor();
                        SetColor("yellow", "black");
                        WriteCenter("|                                  |", 1);
                        SetColor("yellow", "black");
                        WriteCenter("|                                  |", 0);
                        WriteCenter("Date    : " + noRegistration[i, 0], -1);
                        WriteCenter("|                                  |", 0);
                        WriteCenter("Ticket #: " + noRegistration[i, 1], -1);
                        WriteCenter("|                                  |", 0);
                        WriteCenter("Name    : " + noRegistration[i, 2], -1);
                        WriteCenter("|                                  |", 0);
                        WriteCenter("Offense : " + noRegistration[i, 3], -1);
                        WriteCenter("|                                  |", 0);
                        WriteCenter("Payment : " + noRegistration[i, 4], -1);
                        WriteCenter("|                                  |", 0);
                        ResetToDefaultColor();
                        WriteCenter("                                    ", 0);
                        SetColor("yellow", "black");
                        WriteCenter("                                    ", 0);
                        ResetToDefaultColor();
                    }
                }
                for (int i = 0; i < noHelmetCount; i++)
                {
                    if (noHelmet[i, 1] == ticket)
                    {
                        SetColor("yellow", "black");
                        WriteCenter("                                    ", -20);
                        WriteCenter("No License", -1);
                        ResetToDefaultColor();
                        SetColor("yellow", "black");
                        WriteCenter("|                                  |", 1);
                        SetColor("yellow", "black");
                        WriteCenter("|                                  |", 0);
                        WriteCenter("Date    : " + noHelmet[i, 0], -1);
                        WriteCenter("|                                  |", 0);
                        WriteCenter("Ticket #: " + noHelmet[i, 1], -1);
                        WriteCenter("|                                  |", 0);
                        WriteCenter("Name    : " + noHelmet[i, 2], -1);
                        WriteCenter("|                                  |", 0);
                        WriteCenter("Offense : " + noHelmet[i, 3], -1);
                        WriteCenter("|                                  |", 0);
                        WriteCenter("Payment : " + noHelmet[i, 4], -1);
                        WriteCenter("|                                  |", 0);
                        ResetToDefaultColor();
                        WriteCenter("                                    ", 0);
                        SetColor("yellow", "black");
                        WriteCenter("                                    ", 0);
                        ResetToDefaultColor();
                    }
                }
            }
            else
                Console.WriteLine("\nNo Record Found \n");

            counter = 0;
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
                Console.ReadKey();

                return false;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Thread.Sleep(150);
                WriteCenter("INCORRECT USERNAME/PASSWORD", 2);
                Console.SetCursorPosition(73, Console.CursorTop - 1);
                Console.ReadKey();
            }

            Console.ResetColor();
            return false;
        }

        static void CheckDate(string violationType)
        {
            DateTime date;
            bool dateIsValid;
            int counter = 0;

            switch (violationType)
            {
                case "noLicense":
                    while (true)
                    {
                        try
                        {
                            if (counter != 0)
                            {
                                Console.SetCursorPosition(0, Console.CursorTop - 6);
                                Console.WriteLine("\t\t\t\t\t\t\t\t                                                           ");
                                Console.SetCursorPosition(0, Console.CursorTop - 4);
                                Console.Write("\n\n\t\t\t\t\t\tDate (MM/DD/YYYY): ");
                                noLicense[noLicenseCount, 0] = Console.ReadLine();
                            }
                            else
                            {
                                Console.Write("\n\n\t\t\t\t\t\tDate (MM/DD/YYYY): ");
                                noLicense[noLicenseCount, 0] = Console.ReadLine();
                            }

                            date = DateTime.ParseExact(noLicense[noLicenseCount, 0], "MM/dd/yyyy", null);
                            noLicense[noLicenseCount, 0] = date.ToString("MM/dd/yyyy");

                            dateIsValid = true;
                        }
                        catch (FormatException)
                        {
                            dateIsValid = false;
                            counter++;

                            SetColor("yellow", "black");
                            WriteCenter("+-------------------------------+", 1);
                            WriteCenter("|                               |", 0);
                            SetColor("", "red");
                            WriteCenter("⚠  ERROR", -1);
                            SetColor("yellow", "black");
                            WriteCenter("|      Invalid date format.     |", 0);
                            WriteCenter("+-------------------------------+", 0);
                            ResetToDefaultColor();
                        }
                        if (dateIsValid == true)
                            break;
                    }
                    break;

                case "noRegistration":
                    while (true)
                    {
                        try
                        {
                            if (counter != 0)
                            {
                                Console.SetCursorPosition(0, Console.CursorTop - 6);
                                Console.WriteLine("\t\t\t\t\t\t\t\t                                                           ");
                                Console.SetCursorPosition(0, Console.CursorTop - 4);
                                Console.Write("\n\n\t\t\t\t\t\tDate (MM/DD/YYYY): ");
                                noRegistration[noRegistrationCount, 0] = Console.ReadLine();
                            }
                            else
                            {
                                Console.Write("\n\n\t\t\t\t\t\tDate (MM/DD/YYYY): ");
                                noRegistration[noRegistrationCount, 0] = Console.ReadLine();
                            }

                            date = DateTime.ParseExact(noRegistration[noRegistrationCount, 0], "MM/dd/yyyy", null);
                            noRegistration[noRegistrationCount, 0] = date.ToString("MM/dd/yyyy");

                            dateIsValid = true;
                        }
                        catch (FormatException)
                        {
                            dateIsValid = false;
                            counter++;

                            SetColor("yellow", "black");
                            WriteCenter("+-------------------------------+", 1);
                            WriteCenter("|                               |", 0);
                            SetColor("", "red");
                            WriteCenter("⚠  ERROR", -1);
                            SetColor("yellow", "black");
                            WriteCenter("|      Invalid date format.     |", 0);
                            WriteCenter("+-------------------------------+", 0);
                            ResetToDefaultColor();
                        }
                        if (dateIsValid == true)
                            break;
                    }
                    break;

                case "noHelmet":
                    while (true)
                    {
                        try
                        {
                            if (counter != 0)
                            {
                                Console.SetCursorPosition(0, Console.CursorTop - 6);
                                Console.WriteLine("\t\t\t\t\t\t\t\t                                                           ");
                                Console.SetCursorPosition(0, Console.CursorTop - 4);
                                Console.Write("\n\n\t\t\t\t\t\tDate (MM/DD/YYYY): ");
                                noHelmet[noHelmetCount, 0] = Console.ReadLine();
                            }
                            else
                            {
                                Console.Write("\n\n\t\t\t\t\t\tDate (MM/DD/YYYY): ");
                                noHelmet[noHelmetCount, 0] = Console.ReadLine();
                            }

                            date = DateTime.ParseExact(noHelmet[noHelmetCount, 0], "MM/dd/yyyy", null);
                            noHelmet[noHelmetCount, 0] = date.ToString("MM/dd/yyyy");

                            dateIsValid = true;
                        }
                        catch (FormatException)
                        {
                            dateIsValid = false;
                            counter++;

                            SetColor("yellow", "black");
                            WriteCenter("+-------------------------------+", 1);
                            WriteCenter("|                               |", 0);
                            SetColor("", "red");
                            WriteCenter("⚠  ERROR", -1);
                            SetColor("yellow", "black");
                            WriteCenter("|      Invalid date format.     |", 0);
                            WriteCenter("+-------------------------------+", 0);
                            ResetToDefaultColor();
                        }
                        if (dateIsValid == true)
                            break;
                    }
                    break;

                case "noInsurance":
                    while (true)
                    {
                        try
                        {
                            if (counter != 0)
                            {
                                Console.SetCursorPosition(0, Console.CursorTop - 6);
                                Console.WriteLine("\t\t\t\t\t\t\t\t                                                           ");
                                Console.SetCursorPosition(0, Console.CursorTop - 4);
                                Console.Write("\n\n\t\t\t\t\t\tDate (MM/DD/YYYY): ");
                                noInsurance[noInsuranceCount, 0] = Console.ReadLine();
                            }
                            else
                            {
                                Console.Write("\n\n\t\t\t\t\t\tDate (MM/DD/YYYY): ");
                                noInsurance[noInsuranceCount, 0] = Console.ReadLine();
                            }

                            date = DateTime.ParseExact(noInsurance[noInsuranceCount, 0], "MM/dd/yyyy", null);
                            noInsurance[noInsuranceCount, 0] = date.ToString("MM/dd/yyyy");

                            dateIsValid = true;
                        }
                        catch (FormatException)
                        {
                            dateIsValid = false;
                            counter++;

                            SetColor("yellow", "black");
                            WriteCenter("+-------------------------------+", 1);
                            WriteCenter("|                               |", 0);
                            SetColor("", "red");
                            WriteCenter("⚠  ERROR", -1);
                            SetColor("yellow", "black");
                            WriteCenter("|      Invalid date format.     |", 0);
                            WriteCenter("+-------------------------------+", 0);
                            ResetToDefaultColor();
                        }
                        if (dateIsValid == true)
                            break;
                    }
                    break;

                case "dui":
                    while (true)
                    {
                        try
                        {
                            if (counter != 0)
                            {
                                Console.SetCursorPosition(0, Console.CursorTop - 6);
                                Console.WriteLine("\t\t\t\t\t\t\t\t                                                           ");
                                Console.SetCursorPosition(0, Console.CursorTop - 4);
                                Console.Write("\n\n\t\t\t\t\t\tDate (MM/DD/YYYY): ");
                                dui[duiCount, 0] = Console.ReadLine();
                            }
                            else
                            {
                                Console.Write("\n\n\t\t\t\t\t\tDate (MM/DD/YYYY): ");
                                dui[duiCount, 0] = Console.ReadLine();
                            }

                            date = DateTime.ParseExact(dui[duiCount, 0], "MM/dd/yyyy", null);
                            dui[duiCount, 0] = date.ToString("MM/dd/yyyy");

                            dateIsValid = true;
                        }
                        catch (FormatException)
                        {
                            dateIsValid = false;
                            counter++;

                            SetColor("yellow", "black");
                            WriteCenter("+-------------------------------+", 1);
                            WriteCenter("|                               |", 0);
                            SetColor("", "red");
                            WriteCenter("⚠  ERROR", -1);
                            SetColor("yellow", "black");
                            WriteCenter("|      Invalid date format.     |", 0);
                            WriteCenter("+-------------------------------+", 0);
                            ResetToDefaultColor();
                        }
                        if (dateIsValid == true)
                            break;
                    }
                    break;
            }

            if (counter != 0)
                Console.SetCursorPosition(0, Console.CursorTop + 5);
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
                case "red":
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
            }
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
                            // Line 1711 does not remove the text in display, only in the background
                            // As such writing \b on the console replicates the backspace visually
                            Console.Write(" \b"); // matches a backspace, \u0008 Ref: Micorosft Learn Docs.
                            input.Remove(i, 1);
                        }
                    }
                }
                return input.ToString();
            }
            else
                return "Invalid argument passed to parameter";
        }
    }
}
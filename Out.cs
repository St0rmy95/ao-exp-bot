using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace OsrExpBooster
{
    public static class Out
    {

        private const int MF_BYCOMMAND = 0x00000000;
        public const int SC_CLOSE = 0xF060;
        public const int SC_MINIMIZE = 0xF020;
        public const int SC_MAXIMIZE = 0xF030;
        public const int SC_SIZE = 0xF000;

        [DllImport("user32.dll")]
        public static extern int DeleteMenu(IntPtr hMenu, int nPosition, int wFlags);

        [DllImport("user32.dll")]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("kernel32.dll", ExactSpelling = true)]

        private static extern IntPtr GetConsoleWindow();
        public const string BOT = "Bot";
        public const string APP = "Application";
        public const string GAME = "Game";
        public const string ERROR = "Error";

        static object locker = new object();

        static bool init = false;
        static void Init()
        {
            init = true;
            IntPtr handle = GetConsoleWindow();
            IntPtr sysMenu = GetSystemMenu(handle, false);

            if (handle != IntPtr.Zero)
            {
                DeleteMenu(sysMenu, SC_MAXIMIZE, MF_BYCOMMAND);
                DeleteMenu(sysMenu, SC_SIZE, MF_BYCOMMAND);
            }
            Console.SetWindowSize(170, 50);
           // Console.BackgroundColor = ConsoleColor.White;
            Console.Clear();
        }

        static string currentline = string.Empty;

        public static string ReadLine()
        {
            while (true)
            {
                if (Console.KeyAvailable)
                {
                    var keyInfo = Console.ReadKey(true);
                    // Ignore if Alt or Ctrl is pressed.
                    if ((keyInfo.Modifiers & ConsoleModifiers.Alt) == ConsoleModifiers.Alt)
                        continue;
                    if ((keyInfo.Modifiers & ConsoleModifiers.Control) == ConsoleModifiers.Control)
                        continue;
                    // Ignore if KeyChar value is \u0000.
                    if (keyInfo.KeyChar == '\u0000') continue;
                    // Ignore tab key.
                    if (keyInfo.Key == ConsoleKey.Tab) continue;
                    // Handle backspace.
                    if (keyInfo.Key == ConsoleKey.Backspace)
                    {
                        // Are there any characters to erase?
                        if (currentline.Length > 0)
                        {
                            // Determine where we are in the console buffer.
                            int cursorCol = Console.CursorLeft - 1;
                            int oldLength = currentline.Length;
                            int extraRows = (oldLength + 2) / Console.BufferWidth;

                            currentline = currentline.Substring(0, oldLength - 1);
                            Console.CursorLeft = 0;
                            Console.CursorTop = Console.CursorTop - extraRows;
                            Console.Write("> ");
                            Console.Write(currentline);
                            Console.Write(" ");

                            if (cursorCol >= 0) Console.CursorLeft = cursorCol;
                            else
                            {
                                --Console.CursorTop;
                                Console.CursorLeft = Console.BufferWidth - 1;
                            }
                        }
                        continue;
                    }
                    // Handle Escape key.
                    if (keyInfo.Key == ConsoleKey.Escape)
                    {
                        // Are there any characters to erase? Erase them all.
                        if (currentline.Length > 0)
                        {
                            // Determine where we are in the console buffer.
                            int oldLength = currentline.Length;
                            int extraRows = (oldLength + 2) / Console.BufferWidth;

                            Console.CursorLeft = 0;
                            Console.CursorTop = Console.CursorTop - extraRows;

                            Console.Write("> ");
                            Console.Write(new string(' ', oldLength));

                            Console.CursorTop = Console.CursorTop - extraRows;
                            Console.CursorLeft = 2;
                        }

                        currentline = string.Empty;

                        continue;
                    }

                    if (keyInfo.Key == ConsoleKey.Enter)
                    {
                        string readline = currentline;
                        currentline = string.Empty;
                        Console.WriteLine("> ");
                        return readline;
                    }
                    else
                    {
                        // Handle key by adding it to input string.
                        Console.Write(keyInfo.KeyChar);
                        currentline += keyInfo.KeyChar;
                    }
                }
            }

        }

        public static void WriteLine(string OutString = "", string header = null, ConsoleColor color = ConsoleColor.Black)
        {
            if (!init)
                Init();

            lock (locker)
            {
                lock(currentline)
{
                if (currentline.Length > 0)
                {
                    // Determine where we are in the console buffer.
                    int oldLength = currentline.Length;
                    int oldRows = oldLength / 80;

                    Console.CursorLeft = 0;

                    Console.CursorTop = Console.CursorTop - oldRows;

                    Console.Write(new string(' ', oldLength + 2));

                    Console.CursorLeft = 0;

                    Console.CursorTop = Console.CursorTop - oldRows;

                }
                else
                {
                    Console.CursorLeft = 0;
                    Console.Write("  ");
                    Console.CursorLeft = 0;
                }


                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write("[" + System.DateTime.Now + "] ");
                if (header != null)
                {
                    Console.Write("[");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write(header);
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write("] ");
                }

                Console.Write(">> ");
                //Console.ForegroundColor = color;
                if (color == ConsoleColor.Black)
                {
                    switch (header)
                    {
                        case BOT:
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            break;
                        case ERROR:
                            Console.ForegroundColor = ConsoleColor.Red;
                            break;
                        case GAME:
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            break;
                        default:
                            Console.ForegroundColor = ConsoleColor.White;
                            break;
                    }
                }
                Console.WriteLine(OutString);
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write("> ");
                Console.Write(currentline);
}
            }
        }
    }
}

using System;
using System.Text;
using System.Threading;

namespace SnowingConsoleApplication
{
    class SnowingConsole
    {
        public static void Main()
        {
            ResizeConsole();

            ClearConsole();

            string[,] arr = new string[Console.WindowHeight - 1, Console.WindowWidth - 1];

            InitializeSnowArray(arr);

            Console.WriteLine("Generating the relief. Please wait...");
            GenerateRelief(arr);

            int delay = 300;

            while (true)
            {
                GenerateSnow(arr);

                Thread.Sleep(delay);

                PrintSnow(arr);

                ConsoleKeyInfo button;

                if (Console.KeyAvailable)
                {
                    button = Console.ReadKey();

                    KeysActing(button, arr, delay);
                }
            }
        }

        public static int GenerateRandomNumber(int minValue, int maxValue)
        {
            Random random = new Random();
            return random.Next(minValue, maxValue);
        }

        public static void ResizeConsole()
        {
            int consoleWidth = 100;
            int consoleHeight = 30;
            Console.SetWindowSize(consoleWidth, consoleHeight);
            Console.SetBufferSize(consoleWidth, consoleHeight);
        }
        public static void ClearConsole()
        {
            Console.Clear();
        }

        public static string[,] InitializeSnowArray(string[,] arr)
        {
            for (int row = 0; row < arr.GetLength(0); row++)
            {
                for (int col = 0; col < arr.GetLength(1); col++)
                {
                    arr[row, col] = " ";
                }
            }
            return arr;
        }

        public static void GenerateRelief(string[,] arr)
        {
            int row = 0;
            for (int col = 0; col < arr.GetLength(1); col++)
            {
                row = GenerateRandomNumber(arr.GetLength(0) - 3, arr.GetLength(0));
                arr[row, col] = "@";
                Thread.Sleep(50);
            }
        }

        public static void GenerateSnow(string[,] arr)
        {
            arr[0, GenerateRandomNumber(0, Console.WindowWidth - 1)] = "*";

            for (int row = arr.GetLength(0) - 2; row >= 0; row--)
            {
                for (int col = 0; col < arr.GetLength(1); col++)
                {
                    if (arr[row, col] == "*")
                    {
                        if (arr[row + 1, col] != "*" && arr[row + 1, col] != "@")
                        {
                            arr[row + 1, col] = "*";
                            arr[row, col] = "";
                        }
                    }
                }
            }
        }

        public static void PrintSnow(string[,] arr)
        {
            StringBuilder sb = new StringBuilder();

            for (int row = 0; row < arr.GetLength(0); row++)
            {
                for (int col = 0; col < arr.GetLength(1); col++)
                {
                    sb.Append(arr[row, col]);
                }
                sb.Append("\n");
            }
            Console.WriteLine(sb);
        }

        public static void KeysActing(ConsoleKeyInfo button, string[,] arr, int delay)
        {
            if (button.Key == ConsoleKey.UpArrow)
            {
                if (delay > 1)
                {
                    delay -= 50;
                }
            }
            else if (button.Key == ConsoleKey.DownArrow)
            {
                delay += 50;
            }
            else if (button.Key == ConsoleKey.LeftArrow)
            {
                for (int row = 0; row < arr.GetLength(0); row++)
                {
                    for (int col = 1; col < arr.GetLength(1); col++)
                    {
                        arr[row, col - 1] = arr[row, col];
                    }
                }
            }
            else if (button.Key == ConsoleKey.RightArrow)
            {
                for (int row = 0; row < arr.GetLength(0); row++)
                {
                    for (int col = arr.GetLength(1) - 2; col >= 0; col--)
                    {
                        arr[row, col + 1] = arr[row, col];
                    }
                }
            }
        }
    }
}
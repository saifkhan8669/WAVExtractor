using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WAVExtractor
{
    internal class Visual

    {

        public static void Yellow(string message)

        {

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(message);
            Console.ResetColor();

        }

        public static string Green(string message)

        {

            Console.ForegroundColor = ConsoleColor.Green;
            return message;

        }

        public static void Red(string message)

        {

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();

        }
    }
}

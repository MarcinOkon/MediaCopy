using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaCopy
{
    class Messenger
    {
        public static void Display(ConsoleColor color, string message, params object[] args)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message, args);
            Console.ResetColor();
        }
    }
}

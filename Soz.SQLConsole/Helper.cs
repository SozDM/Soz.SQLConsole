using System;
using System.Collections.Generic;
using System.Text;

namespace Soz.SQLConsole
{
    public static class Helper
    {

        public static string InputStringNotWhiteSpace(this string Str, string StringName)
        {
            bool ValidString = false;
            while (!ValidString)      
            {
                Console.Write($"Input {StringName}: ");
                Str = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(Str)) Console.WriteLine($"Wrong {StringName}. Try again");
                else ValidString = true;
            }
            return Str;
        }

        public static string InputIntByString(this string Str, string StringName)
        {
            bool ValidString = false;
            while (!ValidString)
            {
                Console.Write($"Input {StringName}: ");
                Str = Console.ReadLine();
                if ((string.IsNullOrWhiteSpace(Str)) || (!Int32.TryParse(Str, out int result)))
                {
                    Console.WriteLine($"Wrong {StringName}. Try again");
                }
                else ValidString = true;
            }
            return Str;
        }

        public static string InputIdByString(this string Id, string IdName, List<int> IdList)
        {
            bool ValidId = false;
            while (!ValidId)      // Setting ID of user
            {
                Console.Write($"Input {IdName}: ");
                Id = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(Id) || !Int32.TryParse(Id, out int result) ||
                    !IdList.Contains(Int32.Parse(Id)))
                {
                    Console.WriteLine($"Wrong {IdName}. Try again");
                }
                else ValidId = true;
            }
            return Id;
        }
    }
}

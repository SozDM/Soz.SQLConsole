using System;
using System.Collections.Generic;
using System.Text;

namespace Soz.SQLConsole
{
    public static class Extensions
    {

        public static string InputStringNotWhiteSpace(this string Str, string StringName)
        {
            bool ValidString = false;                   //checks if string is not null or whitespace
            while (!ValidString)                        //if everything OK - returns string itself
            {                                           //if string is not OK - waits for valid string
                Console.Write($"Input {StringName}: ");
                Str = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(Str)) Console.WriteLine($"Empty input. Please input {StringName}");
                else ValidString = true;
            }
            return Str;
        }

        public static string InputIntByString(this string Str, string StringName)
        {
            bool ValidString = false;                           //checks if there is valid Int32 from console
            while (!ValidString)                                //if it is not OK - waits for valid string
            {                                                   //if user inputs "exit" - function stops
                Str = Str.InputStringNotWhiteSpace(StringName);
                if (Int32.TryParse(Str, out int result) | (Str == "exit")) ValidString = true;
                else Console.WriteLine("Please input a number");
            }
            return Str;
        }

        public static string InputIdByString(this string Id, string IdName, List<int> IdList)
        {
            bool ValidId = false;                           //checks if there is valid Id from console
            while (!ValidId)                                //Id must be Int32 and must be in IdList
            {                                               //if Id is not OK - waits for valid Id
                Id = Id.InputIntByString(IdName);           //if user inputs "exit" - function stops
                if (IdList.Contains(Int32.Parse(Id)) | (Id == "exit")) ValidId = true;
                else Console.WriteLine($"Please input a valid {IdName}");
            }
            return Id;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Soz.SQLConsole
{
    public static class Extensions
    {
        //checks if string is not null or whitespace
        //if everything OK - returns string itself
        //if string is not OK - waits for valid string

        public static string InputStringNotWhiteSpace(this string Str, string StringName)
        {
            bool ValidString = false;                   
            while (!ValidString)                        
            {                                           
                Console.Write($"Input {StringName}: ");
                Str = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(Str)) Console.WriteLine($"Empty input. Please input {StringName}");
                else ValidString = true;
            }
            return Str;
        }


        //checks if there is valid Int32 from console
        //if it is not OK - waits for valid string
        //if user inputs "exit" - function stops

        public static string InputIntByString(this string Str, string StringName)
        {
            bool ValidString = false;                           
            while (!ValidString)                                
            {                                                   
                Str = Str.InputStringNotWhiteSpace(StringName);
                if (Int32.TryParse(Str, out int result) | (Str == "exit")) ValidString = true;
                else Console.WriteLine("Please input a number");
            }
            return Str;
        }


        //checks if there is valid Id from console
        //Id must be Int32 and must be in IdList
        //if Id is not OK - waits for valid Id
        //if user inputs "exit" - function stops

        public static string InputIdByString(this string Id, string IdName, List<int> IdList)
        {
            bool ValidId = false;                           
            while (!ValidId)                                
            {                                               
                Id = Id.InputIntByString(IdName);           
                if (IdList.Contains(Int32.Parse(Id)) | (Id == "exit")) ValidId = true;
                else Console.WriteLine($"Please input a valid {IdName}");
            }
            return Id;
        }
    }
}

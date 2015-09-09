using FizzBuzz;
using System;
using System.Collections.Generic;
using System.IO;

namespace FizzBuzzConsole
{
    class Program
    {
        /// <summary>
        /// Use the rules engine to process a range from 0 to 100 using the default rules and 
        /// 2 custom rules and associated strings:
        ///   - Custom Rule 1: if the number equals 4, display "FOUR"
        ///   - Custom Rule 2: if the number is > 50 and a multiple of 6, display "GREATER_THAN_FIFTY_AND_MULTIPLE_OF_6"
        /// </summary>
        static void Main(string[] args)
        {
            var fizzBuzz = new RulesEngine();
            int upperBound;

            //Define custom rules to pass to rules engine
            var rules = new Dictionary<NumberRule, string>()
            {
                {x => x == 4, "FOUR"},
                {x => x > 50 && x%6 == 0, "GREATER_THAN_FIFTY_AND_MULTIPLE_OF_6"}
            };

            if (args.Length == 0)
            {
                //Process the range, storing the results in a collection
                var result = fizzBuzz.ProcessRange(rules);

                //Iterate over the results collection, writing the results for each
                //number in the range to the console
                foreach (var item in result)
                {
                    var number = item.Key;
                    var output = item.Value;

                    Console.WriteLine("{0}: {1}", number, output);
                }
            }
            else if (args.Length == 1 && Int32.TryParse(args[0], out upperBound))
            {
                MemoryStream stream = new MemoryStream();
                StreamWriter writer = new StreamWriter(stream);
                StreamReader reader = new StreamReader(stream);

                fizzBuzz.ProcessRange(upperBound, WriteOutput, rules);
            }
            else
            {
                Console.WriteLine("Incorrect arguments supplied. Please pass either no arguments, or an integer < {0}", int.MaxValue);
            }

            //Allow the user to press any key to terminate the console app
            Console.WriteLine("\n\nPress any key to continue...");
            Console.ReadKey();
        }

        private static void WriteOutput(int number, string output)
        {
            Console.WriteLine("{0}: {1}", number, output);
        }
    }
}

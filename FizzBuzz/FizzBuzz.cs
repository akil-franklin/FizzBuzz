using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FizzBuzz
{
    /// <summary>
    /// Represents a number rule that can be used by the rules engine
    /// </summary>
    /// <param name="number">The number to be evaluated by the rule</param>
    /// <returns>The string to be returned if the number matches the rule</returns>
    public delegate bool NumberRule(int number);

    public delegate void Callback(int number, string result);

    /// <summary>
    /// Provides methods for evaluating a numeric range against a series of rules
    /// </summary>
    public class RulesEngine
    {
        public void ProcessRange(int upperBound, Callback callback)
        {
            for (var number = 0; number <= upperBound; number++)
            {
                callback(number, number.ToString());
            }
        }


        /// <summary>
        /// Return a dictionary of numbers (from 0 to the given upper boud) 
        /// and their matching string representations
        /// based on the default number rules and an optional custom rule.
        /// </summary>
        public Dictionary<int, string> ProcessRange(int upperBound = 100, bool applyDefaultRules = true)
        {
            return ProcessRange(null, upperBound, applyDefaultRules);
        }


        /// <summary>
        /// Return a dictionary of numbers (from 0 to the given upper boud) 
        /// and their matching string representations
        /// based on the default number rules and an optional rules list
        /// </summary>
        public Dictionary<int, string> ProcessRange(Dictionary<NumberRule, string> ruleList, 
            int upperBound = 100,
            bool applyDefaultRules = true)
        {

            if (upperBound < 0)
            {
                throw new ArgumentOutOfRangeException("upperBound", upperBound, 
                    "Only positive upper bounds are supported at this time.");
            }

            var output = new Dictionary<int, string>();

            //Loop through numbers, appending the proper output for each
            for (var number = 1; number <= upperBound; number++)
            {
                var result = ProcessNumber(number, ruleList);
                output.Add(number, result);
            }

            return output;
        }

        /// <summary>
        /// Return the appropriate string for the number based on 
        /// the default rules and an optional custom rule.
        /// </summary>
        public string ProcessNumber(int number, NumberRule rule, string textForMatches, bool applyDefaultRules = true)
        {
            var ruleList = new Dictionary<NumberRule, string>() { {rule, textForMatches} };
            return ProcessNumber(number, ruleList, applyDefaultRules);
        }

        /// <summary>
        /// Return the appropriate string for the number based on 
        /// the default rules and an optional list of custom rules.
        /// </summary>
        /// <param name="number">The number to be processed</param>
        /// <param name="ruleList">A list of rule delegates and associated text to be used if the number rule matches the number</param>
        /// <param name="applyDefaultRules">Specifies whether or not default rules should be applied</param>
        /// <returns></returns>
        public string ProcessNumber(int number, Dictionary<NumberRule, string> ruleList = null, bool applyDefaultRules = true)
        {
            var result = new StringBuilder();

            if (applyDefaultRules) result.Append(ProcessNumberWithDefaultRules(number));

            //Process custom rules (if specified)
            if (ruleList != null)
            {
                foreach (var item in ruleList)
                {
                    var rule = item.Key;
                    var textForMatches = item.Value;
                    if (rule != null)
                    {
                        result.Append(ProcessNumberWithRule(number, rule, textForMatches));
                    }
                }
            }

            //If none of the rules have matched so far, simply append the number as a string
            if (String.IsNullOrEmpty(result.ToString())) result.Append(number.ToString());
            
            return result.ToString().TrimEnd();
        }


        /// <summary>
        /// Process the number using the Default Rules
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        private string ProcessNumberWithDefaultRules(int number)
        {
            var result = new StringBuilder();

            result.Append(ProcessNumberWithRule(number,
                x => x > 0 && x % 3 == 0,
                "fizz"));

            result.Append(ProcessNumberWithRule(number,
                x => x % 2 == 0,
                "even"));

            result.Append(ProcessNumberWithRule(number,
                x => x % 2 != 0,
                "odd"));

            return result.ToString();
        }


        /// <summary>
        /// Process the number using the given rule
        /// </summary>
        /// <param name="number">The number to be evaluated by the rule</param>
        /// <param name="rule">A rule delegate</param>
        /// <param name="textForMatches">The string to be returned if the number matches the rule</param>
        private string ProcessNumberWithRule(int number, NumberRule rule, string textForMatches)
        {
            try
            {
                return rule(number) ? textForMatches + " " : String.Empty;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Unable to process the given number rule", ex);
            }
        }

    }

}

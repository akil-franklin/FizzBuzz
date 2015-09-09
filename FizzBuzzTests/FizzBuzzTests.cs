using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using FizzBuzz;

namespace FizzBuzzTests
{
    [TestFixture]
    public class FizzBuzzTests
    {

        [Test][Explicit]
        public void ProcessNumbers_Should_Display_Output()
        {
            var fizzBuzz = new RulesEngine();
            var output = fizzBuzz.ProcessRange();
            foreach (var item in output)
            {
                Console.WriteLine(item);
            }         
        }

        [Test]
        public void ProcessNumbers_Should_Throw_Exception_For_Negative_Numbers()
        {
            var fizzBuzz = new RulesEngine();
            Assert.Throws<ArgumentOutOfRangeException>(() => fizzBuzz.ProcessRange(-1));
        }

        [Test]
        public void ProcessNumbers_Should_Throw_Exception_For_Large_Numbers()
        {
            var fizzBuzz = new RulesEngine();
            Assert.Throws<ArgumentOutOfRangeException>(() => fizzBuzz.ProcessRange(999999));
        }

        [Test]
        public void ProcessNumber_Should_Return_Even_For_0()
        {
            var fizzBuzz = new RulesEngine();
            var result = fizzBuzz.ProcessNumber(0);
            Assert.AreEqual("even", result);
        }

        [Test]
        public void ProcessNumber_Should_Return_Odd_For_1()
        {
            var fizzBuzz = new RulesEngine();
            var result = fizzBuzz.ProcessNumber(1);
            Assert.AreEqual("odd", result);
        }


        [Test]
        public void ProcessNumber_Should_Return_Fizz_Odd_For_3()
        {
            var fizzBuzz = new RulesEngine();
            var result = fizzBuzz.ProcessNumber(3);
            Assert.AreEqual("fizz odd", result);
        }

        [Test]
        public void ProcessNumber_Should_Return_Even_For_4()
        {
            var fizzBuzz = new RulesEngine();
            var result = fizzBuzz.ProcessNumber(4);
            Assert.AreEqual("even", result);
        }

        [Test]
        public void ProcessNumber_Should_Evaluate_Custom_Rule()
        {
            var fizzBuzz = new RulesEngine();
            const string textForMatches = "FOUR!";

            var result = fizzBuzz.ProcessNumber(4, ((x) => x == 4), textForMatches);
            Assert.AreEqual("even " + textForMatches, result);
        }

        [Test]
        public void ProcessNumber_Should_Bypass_Default_Rules_If_Apply_Default_Rules_IsFalse()
        {
            var fizzBuzz = new RulesEngine();
            const string textForMatches = "FOUR!";

            var result = fizzBuzz.ProcessNumber(4, ((x) => x == 4), textForMatches, false);
            Assert.AreEqual(textForMatches, result);
        }

        [Test]
        public void ProcessNumber_Should_Evaluate_Multiple_Custom_Rules()
        {
            var fizzBuzz = new RulesEngine();
            const string textForMatches4 = "FOUR!";
            const string textForGreaterThan3 = "GREATER_THAN_THREE";

            var rules = new Dictionary<NumberRule, string>()
            {
                {(x) => x == 4, textForMatches4},
                {(x) => x > 3, textForGreaterThan3}
            };

            var result = fizzBuzz.ProcessNumber(4, rules);
            Assert.AreEqual("even " + textForMatches4 + " " + textForGreaterThan3, result);
        }

        [Test]
        public void ProcessNumber_Should_Evaluate_Multiple_Custom_Rules_And_Bypass_Default_Rules()
        {
            var fizzBuzz = new RulesEngine();
            const string textForMatches4 = "FOUR!";
            const string textForGreaterThan3 = "GREATER_THAN_THREE";

            var rules = new Dictionary<NumberRule, string>()
            {
                {(x) => x == 4, textForMatches4},
                {(x) => x > 3, textForGreaterThan3}
            };

            var result = fizzBuzz.ProcessNumber(4, rules, false);
            Assert.AreEqual(textForMatches4 + " " + textForGreaterThan3, result);
        }

        [Test][Explicit]
        public void ProcessNumber_Should_Handle_Large_Input()
        {
            var fizzBuzz = new RulesEngine();
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            StreamReader reader = new StreamReader(stream);

            fizzBuzz.ProcessRange(999999, WriteOutput);

           
        }

        private void WriteOutput(int number, string output)
        {
            Console.WriteLine(output);
        }

    }
}

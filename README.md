# FizzBuzz
Sample implementation of FizzBuzz

##Installation (Windows)
After cloning the repo locally, you can:
- Build the app by running the `build` command. The build command will:
  - Build the the FizzBuzz dlls and copy them to the `dist` folder
  - Run the unit tests for the FizzBuzz dlls
- Run a simple example against a range of 0..100 by running the `demo` command


##Sample Code
The following sample processes the default range of 0..100 with the default
rules, adding 2 additional custom rules.

    var fizzBuzz = new RulesEngine();

    //Define custom rules to pass to rules engine
    var rules = new Dictionary<NumberRule, string>()
    {
        {x => x == 4, "FOUR"},
        {x => x > 50 && x%6 == 0, "GREATER_THAN_FIFTY_AND_MULTIPLE_OF_6"}
    };

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

var testData1 = new string[]{
    "vJrwpWtwJgWrhcsFMMfFFhFp",
    "jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL",
    "PmmdzqPrVvPwwTWBwg",
    "wMqvLMZHhHMvwLHjbvcjnnSBnvTQFn",
    "ttgJtRGJQctTZtZT",
    "CrZsJsPPZsGzwwsLwLmpwMDw"  
};

var testData2 = new string[]{
    "vJrwpWtwJgWrhcsFMMfFFhFp",
    "jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL",
    "PmmdzqPrVvPwwTWBwg",
    "wMqvLMZHhHMvwLHjbvcjnnSBnvTQFn",
    "ttgJtRGJQctTZtZT",
    "CrZsJsPPZsGzwwsLwLmpwMDw"
};

var FindMatchAndReturnValue = (string[] input) => {
    var array = new int[53];
    for (int j = 0; j < input.Length; j++)
    {
        for (int i = 0; i < input[j].Length; i++)
        {
            var current = input[j][i];
            var currentInt = Char.IsUpper(current) ? current - '&' : current - '`';
            if (j == 0 || array[currentInt] == j)
            {
                array[currentInt] = j + 1;
            }
            if (array[currentInt] == input.Length)
            {
                return currentInt;
            }
        }
    }
    return 0;
};

var ProcessInput = (string[] input) => {
    var output = 0;
    foreach (var item in input)
    {
        var half = item.Length / 2;
        output += FindMatchAndReturnValue(new string[] { item.Substring(0, half), item.Substring(half, half) });
    }
    return output;
};

var ProcessInput2 = (string[] input) => {
    var output = 0;
    for (int i = 0; i < input.Length; i += 3)
    {
        output += FindMatchAndReturnValue(new string[]{input[i], input[i + 1], input[i + 2]});
    }
    return output;
};

var testAnswer = ProcessInput(testData1);

Console.WriteLine($"test answer: {testAnswer}");
var input = File.ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), "input.txt"));

var firstAnswer = ProcessInput(input);

Console.WriteLine($"First Answer: {firstAnswer}");

var secondTestAnswer = ProcessInput2(testData2);

Console.WriteLine($"Second Test Answer: {secondTestAnswer}");

var secondAnswer = ProcessInput2(input);

Console.WriteLine($"Second Answer: {secondAnswer}");
using System.Linq;

var testData1 = new string[]
{
    "2-4,6-8",
    "2-3,4-5",
    "5-7,7-9",
    "2-8,3-7",
    "6-6,4-6",
    "2-6,4-8"
};

var FindOverlaps = (string[] input) => {
    var output = 0;
    foreach (var item in input)
    {
        var pairs = item.Split(',');
        var pairsInt = pairs.Select(x=>x.Split('-').Select(y=> Convert.ToInt32(y)).ToArray()).OrderBy(z=>z[0]).ToArray();
        if (pairsInt[0][0] <= pairsInt[1][0] &&
            pairsInt[0][1] >= pairsInt[1][1] ||
            pairsInt[1][0] <= pairsInt[0][0] &&
            pairsInt[1][1] >= pairsInt[0][1])
            output++;
    }
    return output;
};

var FindAnyOverlap = (string[] input) => {
    var output = 0;
    foreach (var item in input)
    {
        var pairs = item.Split(',');
        var pairsInt = pairs.Select(x=>x.Split('-').Select(y=> Convert.ToInt32(y)).ToArray()).OrderBy(z=>z[0]).ToArray();
        if (pairsInt[0][0] == pairsInt[1][0] ||
            pairsInt[0][1] >= pairsInt[1][0])
            output++;
    }
    return output;
};

var testResult1 = FindOverlaps(testData1);
Console.WriteLine($"Test result: {testResult1}");

var input = File.ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), "input.txt"));
var firstResult = FindOverlaps(input);
Console.WriteLine($"First result: {firstResult}");

var testResult2 = FindAnyOverlap(testData1);
Console.WriteLine($"Test result 2: {testResult2}");

var secondResult = FindAnyOverlap(input);
Console.WriteLine($"Second result: {secondResult}");
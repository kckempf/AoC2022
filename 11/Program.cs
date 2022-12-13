var input = File.ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), "input.txt"));
var test = File.ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), "test.txt"));

long CalculateMonkeyBusiness(string[] input, long divisor, int rounds)
{
    var monkeys = new List<Monkey>();
    var currentIndex = 0;
    for (int i = 0; i < input.Length; i++)
    {
        var parsedRow = input[i].Trim().Split(' ');
        switch (parsedRow[0])
        {
            case "Monkey":
                monkeys.Add(new Monkey(){ Index = currentIndex, StartingItems = new List<long>()});
                currentIndex++;
                break;
            case "Starting":
                var monkey = monkeys[currentIndex - 1];
                for (int j = 2; j < parsedRow.Length; j++)
                {
                    Int64.TryParse(parsedRow[j].Trim(new Char[]{','}), out long item);
                    monkey.StartingItems.Add(item);
                }
                break;
            case "Operation:":
                monkey = monkeys[currentIndex - 1];
                monkey.Operation = new string[] { parsedRow[4], parsedRow[5] };
                break;
            case "Test:":
                monkey = monkeys[currentIndex - 1];
                monkey.Test = new int[3];
                Int32.TryParse(parsedRow[3], out int endValue);
                monkey.Test[0] = endValue;
                i++;
                parsedRow = input[i].Trim().Split(' ');
                Int32.TryParse(parsedRow[5], out endValue);
                monkey.Test[1] = endValue;
                i++;
                parsedRow = input[i].Trim().Split(' ');
                Int32.TryParse(parsedRow[5], out endValue);
                monkey.Test[2] = endValue;
                break;
            default:
                break;
        }
    }
    for (int i = 0; i < rounds; i++)
    {
        foreach (var monkey in monkeys)
        {
            var operation = monkey.Operation[0];
            var subject = monkey.Operation[1];
            foreach (var item in monkey.StartingItems.ToList())
            {
                monkey.Inspections += 1;
                long worryLevel = item;
                long number = 0;
                switch (operation)
                {
                    case "+":
                        worryLevel += Int64.TryParse(subject, out number) ? number : worryLevel;
                        break;
                    case "*":
                        worryLevel *= Int64.TryParse(subject, out number) ? number : worryLevel;
                        break;
                    default:
                        break;
                }
                worryLevel = worryLevel / divisor;
                var nextMonkey = worryLevel % Convert.ToInt64(monkey.Test[0]) == 0 ? monkey.Test[1] : monkey.Test[2];
                monkeys[nextMonkey].StartingItems.Add(worryLevel);
                monkey.StartingItems.Remove(item);
            }
        }
    }
    long first = 0;
    long second = 0;
    foreach (var monkey in monkeys)
    {
        if (monkey.Inspections > first)
        {
            second = first;
            first = monkey.Inspections;
        }
        else if (monkey.Inspections > second)
            second = monkey.Inspections;
    }
    Console.WriteLine($"first: {first} second: {second}");
    return first * second;
}

Console.WriteLine($"first test: {CalculateMonkeyBusiness(test, 3, 20)}");
Console.WriteLine($"first result: {CalculateMonkeyBusiness(input, 3, 20)}");
Console.WriteLine($"second test: {CalculateMonkeyBusiness(test, 1, 1000)}");
Console.WriteLine($"second result: {CalculateMonkeyBusiness(input, 1, 10000)}");
class Monkey
{
    public int Index { get; set; }
    public List<long> StartingItems { get; set; }
    public string[] Operation { get; set; }
    public int[] Test { get; set; }
    public long Inspections { get; set; }
}

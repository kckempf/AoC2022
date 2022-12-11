var input = File.ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), "input.txt"));
var test = File.ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), "test.txt"));

var SignalClock = (string[] input) => 
{
    int total = 1;
    int step = 0;
    var output = 0;
    var checks = new int[] { 20, 60, 100, 140, 180, 220 };
    foreach (var row in input)
    {
        var instructions = row.Split(' ');
        switch (instructions[0])
        {
            case "noop":
                step++;
                if (Array.IndexOf(checks, step) > -1)
                {
                    output += total * checks[Array.IndexOf(checks, step)];
                }
                break;
            case "addx":
                step++;
                if (Array.IndexOf(checks, step) > -1)
                {
                    output += total * checks[Array.IndexOf(checks, step)];
                }
                step++;
                Int32.TryParse(instructions[1], out int signal);
                if (Array.IndexOf(checks, step) > -1)
                {
                    output += total * checks[Array.IndexOf(checks, step)];
                }
                total += signal;
                break;
        }
    }
    return output;
};

var PrintCharacter = (string rowString, int step, int total) =>
{
    if 
    (
        step >= total - 1 &&
        step <= total + 1
    )
        rowString += "#";
    else
        rowString += ".";
    return rowString;
};

var PrintSignal = (string[] input) => 
{
    int total = 1;
    int step = 0;
    var rowString = string.Empty;
    foreach (var row in input)
    {
        var instructions = row.Split(' ');
        switch (instructions[0])
        {
            case "noop":
                rowString = PrintCharacter(rowString, step, total);
                step++;
                if (step == 40)
                {
                    Console.WriteLine(rowString);
                    step = 0;
                    rowString = string.Empty;
                }
                break;
            case "addx":
                rowString = PrintCharacter(rowString, step, total);
                step++;
                if (step == 40)
                {
                    Console.WriteLine(rowString);
                    step = 0;
                    rowString = string.Empty;
                }
                rowString = PrintCharacter(rowString, step, total);
                step++;
                if (step == 40)
                {
                    Console.WriteLine(rowString);
                    step = 0;
                    rowString = string.Empty;
                }
                Int32.TryParse(instructions[1], out int signal);
                total += signal;
                break;
        }
    }
    return 1;
};

Console.WriteLine($"first test: {SignalClock(test)}");
Console.WriteLine($"first result: {SignalClock(input)}");
PrintSignal(test);
Console.WriteLine();
PrintSignal(input);
var input = File.ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), "input.txt"));

var firstScore = CalculateScore(input);

Console.WriteLine($"First result: {firstScore}");

var secondScore = CalculateScore2(input);

Console.WriteLine($"Second result: {secondScore}");

int CalculateScore(string[] input)
{
    var output = 0;
    foreach (var row in input)
    {
        var myPlay = row[2] - 'W';
        var hisPlay = row[0] - '@';
        output += myPlay;
        var result = myPlay - hisPlay;
        if (result == 0)
        {
            output += 3;
        }
        else if (result == 1 || result == -2)
        {
            output += 6;
        }
    }
    return output;
}

int CalculateScore2(string[] input)
{
    var output = 0;
    foreach (var row in input)
    {
        var result = row[2] - 'W' - 1;
        var hisPlay = row[0] - '@';
        
        output += result * 3;
        
        var myPlay = (hisPlay + result + 2) % 3;
        if (myPlay == 0)
            output += 3;
        else
            output += myPlay;
        
        // 1 LOSE   0   3   1 + 0 + 2  
        // 1 DRAW   1   1   1 + 0 + 2 % 3
        // 1 WIN    2   2  
        // 2 LOSE   0   1  
        // 2 DRAW   1   2
        // 2 WIN    2   3
        // 3 LOSE   0   2
        // 3 DRAW   1   3
        // 3 WIN    2   1
    }
    return output;
}
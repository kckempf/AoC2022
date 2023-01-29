var test = new string[]
{
    "R 4",
    "U 4",
    "L 3",
    "D 1",
    "R 4",
    "D 1",
    "L 5",
    "R 2"
};

var test2 = new string[]
{
    "R 5",
    "U 8",
    "L 8",
    "D 3",
    "R 17",
    "D 10",
    "L 25",
    "U 20"
};

var FindTailTraversal = (string[] input, int ropeLength) =>
{
    var history = new HashSet<string>();
    var knots = new int[ropeLength][];
    for (int i = 0; i < ropeLength; i++)
    {
        knots[i] = new int[2]{0, 0};
    }
    history.Add($"0,0");
    var output = 1;
    var dirs = new Dictionary<string, int[]>()
    {
        { "U", new int[]{ 0, 1 } },
        { "D", new int[]{ 0, -1 } },
        { "L", new int[]{ -1, 0 } },
        { "R", new int[]{ 1, 0 } }
    };
    foreach  (var row in input)
    {
        var instruction = row.Split(' ');
        var direction = instruction[0];
        Int32.TryParse(instruction[1], out int distance);
        var queue = new Queue<int[]>();
        while (distance > 0)
        {
            knots[0][0] += dirs[direction][0];  // advance the head
            knots[0][1] += dirs[direction][1];
            var tempString = string.Empty;

            // Advanced the following knots
            for (int i = 1; i < knots.Length; i++)
            {
                // Knots in same column, advance when row difference > 1
                if (knots[i][0] == knots[i - 1][0])
                {
                    if (knots[i][1] - knots[i - 1][1] == 2)
                        knots[i][1] -= 1;
                    else if (knots[i][1] - knots[i - 1][1] == -2)
                        knots[i][1] += 1;
                }
                // Knots in same row, advance when column difference > 1
                else if (knots[i][1] == knots[i - 1][1])
                {
                    if (knots[i][0] - knots[i - 1][0] == 2)
                        knots[i][0] -= 1;
                    else if (knots[i][0] - knots[i - 1][0] == -2)
                        knots[i][0] += 1;
                }
                // if not in same row or column, advance when distance > 1 for both row and column
                // Distance formula: d = sqrt((x1 - x2)^2 + (y1 - y2)^2)
                // Distance is > 1 when (x1 - x2)^2 + (y1 - y2)^2 > 2 because sqrt(2) < 2 < sqrt(5)
                else if (((knots[i][0] - knots[i - 1][0])*(knots[i][0] - knots[i - 1][0]))
                    + ((knots[i][1] - knots[i - 1][1])*(knots[i][1] - knots[i - 1][1]))
                    > 2)
                {
                    if (knots[i][0] < knots[i - 1][0])
                        knots[i][0] += 1;
                    else
                        knots[i][0] -= 1;
                    if (knots[i][1] < knots[i - 1][1])
                        knots[i][1] += 1;
                    else
                        knots[i][1] -= 1;
                }
                tempString = $"{knots[i][0]},{knots[i][1]}";
            }
            if (!history.Contains(tempString))
            {
                history.Add(tempString);
                output++;
            }
            distance--;
        }
    }
    return output;
};

Console.WriteLine($"first test: {FindTailTraversal(test, 2)}");

var input = File.ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), "input.txt"));
Console.WriteLine($"first result: {FindTailTraversal(input, 2)}");

Console.WriteLine($"second test: {FindTailTraversal(test2, 10)}");
Console.WriteLine($"second result: {FindTailTraversal(input, 10)}");

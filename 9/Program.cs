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

var FindTailTraversal = (string[] input) =>
{
    var history = new HashSet<string>();
    var head = new int[2];
    var temp = new int[2];
    history.Add($"{temp[0]},{temp[1]}");
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
            var hold = new int[2];          
            hold[0] = head[0];              
            hold[1] = head[1];              // set hold to current head
            head[0] += dirs[direction][0];  // advance the head
            head[1] += dirs[direction][1];
            if 
            (
                head[0] - temp[0] > 1 ||    // see if temp needs to move
                temp[0] - head[0] > 1 ||
                head[1] - temp[1] > 1 ||
                temp[1] - head[1] > 1
            )
            {
                temp[0] = hold[0];          // set temp to previous head position
                temp[1] = hold[1];
                var tempString = $"{temp[0]},{temp[1]}";
                if (!history.Contains(tempString))
                {
                    history.Add(tempString);
                    output++;
                }
            }
            
            distance--;
        }
    }
    return output;
};

Console.WriteLine($"first test: {FindTailTraversal(test)}");

var input = File.ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), "input.txt"));
Console.WriteLine($"first result: {FindTailTraversal(input)}");


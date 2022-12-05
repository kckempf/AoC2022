var test = new string[]
{
    "    [D]    ",
    "[N] [C]    ",
    "[Z] [M] [P]",
    " 1   2   3 ",
    "",
    "move 1 from 2 to 1",
    "move 3 from 1 to 3",
    "move 2 from 2 to 1",
    "move 1 from 1 to 2"
};

var MoveBoxes = (string[] input) => 
{
    var stacks = new Dictionary<int, LinkedList<char>>();
    var row = 0;
    while (!Char.IsNumber(input[row][1]))
    {
        var current = input[row];
        for (int i = 0; (1 + (i * 4))  < current.Length; i++)
        {
            if (!Char.IsWhiteSpace(current[(1 + (i * 4))]))
            {
                if (!stacks.ContainsKey(i))
                {
                    stacks[i] = new LinkedList<char>();
                }
                
                stacks[i].AddLast(current[1 + (i * 4)]);
            }
        }
        row++;
    }
    row++;
    row++;
    for (int i = row; i < input.Length; i++)
    {
        var parsedRow = input[i].Split(' ');
        Int32.TryParse(parsedRow[1], out int steps);
        Int32.TryParse(parsedRow[3], out int from);
        Int32.TryParse(parsedRow[5], out int to);
        
        while (steps > 0)
        {
            stacks[to - 1].AddFirst(stacks[from - 1].First.Value);
            stacks[from - 1].RemoveFirst();
            steps--;
        }
    }
    var output = string.Empty;
    for (int i = 0; i < stacks.Count; i++)
    {
        output += stacks[i].First.Value;
    }
    return output;
};

var MoveBoxes2 = (string[] input) => 
{
    var stacks = new Dictionary<int, LinkedList<char>>();
    var row = 0;
    while (!Char.IsNumber(input[row][1]))
    {
        var current = input[row];
        for (int i = 0; (1 + (i * 4))  < current.Length; i++)
        {
            if (!Char.IsWhiteSpace(current[(1 + (i * 4))]))
            {
                if (!stacks.ContainsKey(i))
                {
                    stacks[i] = new LinkedList<char>();
                }
                
                stacks[i].AddLast(current[1 + (i * 4)]);
            }
        }
        row++;
    }
    row++;
    row++;
    for (int i = row; i < input.Length; i++)
    {
        var parsedRow = input[i].Split(' ');
        Int32.TryParse(parsedRow[1], out int steps);
        Int32.TryParse(parsedRow[3], out int from);
        Int32.TryParse(parsedRow[5], out int to);
        
        var temp = new Stack<char>();
        
        while (steps > 0)
        {
            temp.Push(stacks[from - 1].First.Value);
            stacks[from - 1].RemoveFirst();
            steps--;
        }
        while (temp.Count > 0)
        {
            stacks[to - 1].AddFirst(temp.Pop());
        }
    }
    var output = string.Empty;
    for (int i = 0; i < stacks.Count; i++)
    {
        output += stacks[i].First.Value;
    }
    return output;
};

Console.WriteLine($"first test: {MoveBoxes(test)}");

var input = File.ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), "input.txt"));

Console.WriteLine($"first answer: {MoveBoxes(input)}");

Console.WriteLine($"second test: {MoveBoxes2(test)}");

Console.WriteLine($"second answer: {MoveBoxes2(input)}");
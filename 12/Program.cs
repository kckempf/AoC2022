var test = new string[] 
{
    "Sabqponm",
    "abcryxxl",
    "accszExk",
    "acctuvwj",
    "abdefghi"
};

var Distance = (int distance, int x, int y, int endX, int endY) =>
{
    return distance + Math.Sqrt(((x - endX)*(x - endX)) + ((y - endY)*(y - endY)));
};

void FindShortestPath(string[] input)
{
    var x = -1;
    var y = -1;
    var k = 0;
    var dirs = new int[][]
    {
        new int[]{1,0},
        new int[]{-1,0},
        new int[]{0,1},
        new int[]{0,-1}
    };
    while (x < 0)
    {
        y = k;
        x = input[y].IndexOf('S');
        k++;
    }
    var endX = -1;
    var endY = -1;
    k = 0;
    while (endX < 0)
    {
        endY = k;
        endX = input[endY].IndexOf('E');
        k++;
    }
    Console.WriteLine($"endX:{endX}, endY:{endY}");
    var visited = new bool[input[0].Length][];
    for (int i = 0; i < visited.Length; i++)
        visited[i]  = new bool[input.Length];
    visited[x][y] = true;
    var queue = new Queue<Cell>();
    queue.Enqueue(new Cell(x, y, 0));
    while (queue.Count > 0)
    {
        var curr = queue.Dequeue();
        if (input[curr.Y][curr.X] == 'E')
        {
            Console.WriteLine($"distance :{curr.Distance}");
            return;
        }
        
        foreach (var dir in dirs)
        {
            var next = new Cell(curr.X + dir[0], curr.Y + dir[1], curr.Distance + 1);
            if
            (
                next.X >= 0 &&
                next.X < input[0].Length &&
                next.Y >=0 &&
                next.Y < input.Length &&
                !visited[next.X][next.Y]
            )
            {
                var nextChar = input[next.Y][next.X];
                var currChar = input[curr.Y][curr.X];
                var currHeight = currChar == 'S' ? 0 : currChar - 'a';
                var nextHeight = nextChar == 'E' ? 25 : nextChar - 'a'; 
                if (nextHeight - 1 <= currHeight)
                {
                    visited[next.X][next.Y] = true;
                    queue.Enqueue(next);
                }
            }
        }
    }
}

var input = File.ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), "input.txt"));
FindShortestPath(test);
FindShortestPath(input);

class Cell
{
    public Cell(int x, int y, int distance)
    {
        X = x;
        Y = y;
        Distance = distance;
    }
    
    public int X { get; set; }
    public int Y { get; set; }
    public int Distance { get; set; }
    //public HashSet<string> Visited { get; set; }
}
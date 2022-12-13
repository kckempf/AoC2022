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
    while (x < 0)
    {
        endY = k;
        endX = input[endY].IndexOf('E');
        k++;
    }
    
    var visited = new HashSet<string>();
    visited.Add($"{x},{y}");
    var queue = new PriorityQueue<Cell, double>();
    queue.Enqueue(new Cell(x, y, 0), Distance(0, x, y, endX, endY));
    
    while (queue.Count > 0)
    {
        var curr = queue.Dequeue();
        if (input[curr.Y][curr.X] == 'E')
        {
            Console.WriteLine($"distance :{curr.Distance}");
            return;
        }
        visited.Add($"{curr.X},{curr.Y}");
        foreach (var dir in dirs)
        {
            var next = new Cell(curr.X + dir[0], curr.Y + dir[1], curr.Distance + 1);
            if
            (
                next.X >= 0 &&
                next.X < input[0].Length &&
                next.Y >=0 &&
                next.Y < input.Length &&
                !visited.Contains($"{next.X},{next.Y}")
            )
            {
                var nextChar = input[next.Y][next.X];
                var currChar = input[curr.Y][curr.X];
                var currHeight = currChar == 'S' ? 0 : currChar - 'a';
                var nextHeight = nextChar == 'E' ? 25 : nextChar - 'a'; 
                if (nextHeight - 1 <= currHeight)
                {
                    queue.Enqueue(next, Distance(next.Distance, next.X, next.Y, endX, endY));
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
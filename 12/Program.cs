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

void FindShortestPath(string[] input, char start)
{
    var dirs = new int[][]
    {
        new int[]{1,0},
        new int[]{-1,0},
        new int[]{0,1},
        new int[]{0,-1}
    };
    var endX = -1;
    var endY = -1;
    var k = 0;
    while (endX < 0)
    {
        endY = k;
        endX = input[endY].IndexOf('E');
        k++;
    }
    var visited = new bool[input[0].Length][];
    for (int i = 0; i < visited.Length; i++)
        visited[i]  = new bool[input.Length];
    visited[endX][endY] = true;
    var queue = new Queue<Cell>();
    queue.Enqueue(new Cell(endX, endY, 0));
    while (queue.Count > 0)
    {
        var curr = queue.Dequeue();
        if (input[curr.Y][curr.X] == start)
        {
            Console.WriteLine($"distance: {curr.Distance}");
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
                var currHeight = 0;
                if (currChar == 'E')
                    currHeight = 25;
                else if (currChar == 'S')
                    currHeight = 0;
                else
                    currHeight = currChar - 'a';
                var nextHeight = nextChar == 'S' ? 0 : nextChar - 'a'; 
                if (nextHeight >= currHeight - 1)
                {
                    visited[next.X][next.Y] = true;
                    queue.Enqueue(next);
                }
            }
        }
    }
}

var input = File.ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), "input.txt"));
FindShortestPath(test, 'S');
FindShortestPath(test, 'a');
FindShortestPath(input, 'S');
FindShortestPath(input, 'a');

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
}
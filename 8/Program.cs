var test = new string[]
{
    "30373",
    "25512",
    "65332",
    "33549",
    "35390"
};

var ScenicRecursion = (int x, int y, string[] input) =>
{
    var output = 1;
    var height = input[x][y] - '0';
    var dirs = new int[][] 
    {
        new int[] { 1, 0 },
        new int[] { -1, 0 },
        new int[] { 0, 1 },
        new int[] { 0, -1 }
    };
    for (int i = 0; i < dirs.Length; i++)
    {
        var temp = 0;
        
        var currentX = x + dirs[i][0];
        var currentY = y + dirs[i][1];
        var nextHeight = input[currentX][currentY] - '0';
        while (currentX < input[0].Length &&
            currentX >= 0 &&
            currentY < input.Length &&
            currentY >= 0 &&
            height > nextHeight)
        {
            temp++;
            nextHeight = input[currentX][currentY] - '0';
            currentX += dirs[i][0];
            currentY += dirs[i][1];
        }
        output *= (temp > 0 ? temp : 1);
    }
    return output;
};

var CalculateMostScenic = (string[] input) => 
{
    var output = 0;
    for (int i = 1; i < input.Length - 1; i++)
    {
        for (int j = 1; j < input[i].Length - 1; j++)
        {
            var result = ScenicRecursion(i, j, input);
            output = Math.Max(output, result);
        }
    }
    return output;
};

var CountVisibleTrees = (string[] input) =>
{
    var compare = new int[input.Length][];
    var verticalTemp = new int[input[0].Length];
    for (int i = 0; i < input.Length; i++)
    {
        compare[i] = new int[input[i].Length];
        var temp = 0;
        
        for (int j = 0; j < input[0].Length; j++)
        {
            if (i == 0)
            {
                verticalTemp[j] = input[i][j] - '0';;
            }
            if (j == 0)
                temp = input[i][j] - '0';
            else
            {
                compare[i][j] = temp;
                temp = Math.Max(temp, input[i][j] - '0');
            }
        }
        for (int j = input[0].Length - 1; j > 0; j--)
        {
            if (j == input[0].Length - 1)
                temp = input[i][j] - '0';
            else
            {
                compare[i][j] = Math.Min(temp, compare[i][j]);
                compare[i][j] = Math.Min(verticalTemp[j], compare[i][j]);
                temp = Math.Max(temp, input[i][j] - '0');
                verticalTemp[j] = Math.Max(verticalTemp[j], input[i][j] - '0');
            }
        }
    }
    verticalTemp = new int[input[0].Length];
    for (int i = input.Length - 1; i >= 0; i--)
    {
        for (int j = 0; j < input[0].Length; j++)
        {
            var temp = input[i][j] - '0';
            if (i == input.Length - 1)
            {
                verticalTemp[j] = input[i][j] - '0';
            }
            else
            {
                compare[i][j] = Math.Min(verticalTemp[j], compare[i][j]);
                verticalTemp[j] = Math.Max(verticalTemp[j], input[i][j] - '0');
            }
        }
    }
    var output = 0;
    for (int i = 1; i < input.Length - 1; i++)
    {
        for (int j = 1; j < input[0].Length - 1; j++)
        {
            var temp = input[i][j] - '0';
            if (temp <= compare[i][j])
                output++;
        }
    }
    return output;
};

Console.WriteLine($"first test: {(test.Length * test[0].Length) - CountVisibleTrees(test)}");

var input = File.ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), "input.txt"));
Console.WriteLine($"first answer: {(input.Length * input[0].Length) - CountVisibleTrees(input)}");

Console.WriteLine($"second test: {CalculateMostScenic(test)}");
Console.WriteLine($"second answer: {CalculateMostScenic(input)}");

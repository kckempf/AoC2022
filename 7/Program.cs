var test = new string[]
{
    "$ cd /",
    "$ ls",
    "dir a",
    "14848514 b.txtcde",
    "8504156 c.dat",
    "dir d",
    "$ cd a",
    "$ ls",
    "dir e",
    "29116 f",
    "2557 g",
    "62596 h.lst",
    "$ cd e",
    "$ ls",
    "584 i",
    "$ cd ..",
    "$ cd ..",
    "$ cd d",
    "$ ls",
    "4060174 j",
    "8033020 d.log",
    "5626152 d.ext",
    "7214296 k"
};

var root = new FileNode() 
{
    Name = "/",
    Parent = null,
    Children = new Dictionary<string, FileNode>()
};

var inputRoot = new FileNode() 
{
    Name = "/",
    Parent = null,
    Children = new Dictionary<string, FileNode>()
};

var LoadFileTree = (string[] input, FileNode root) =>
{
    var currentNode = root;
    for (int i = 1; i < input.Length; i++)
    {
        var currArray = input[i].Split(' ');
        if (currArray[0] == "$")
        {
            if (currArray[1] == "cd")
            {
                if (currArray[2] == "..")
                {
                    currentNode = currentNode.Parent;
                }
                else
                {
                    currentNode = currentNode.Children[currArray[2]];
                }
            }
        }
        else
        {
            currentNode.Children[currArray[1]] = new FileNode()
            {
                Name = currArray[1],
                Children = new Dictionary<string, FileNode>(),
                Parent = currentNode
            };
            if (currArray[0] != "dir")
            {
                Int32.TryParse(currArray[0], out int currentSize);
                currentNode.Children[currArray[1]] = new FileNode() 
                {
                    Name = currArray[1],
                    Size = currentSize,
                    Parent = currentNode
                };
            }
        }
    }
};

int GetSizes(FileNode root)
{
    if (root.Children != null && root.Children.Count > 0)
    {
        foreach (var child in root.Children)
        {
            root.Size += GetSizes(child.Value);
        }
    }
    return root.Size;
};

int GetFirstAnswer(FileNode root, int sum)
{
    if (root.Children != null && root.Children.Count > 0)
    {
        foreach (var child in root.Children)
        {
            sum = GetFirstAnswer(child.Value, sum);
        }
        if (root.Size <= 100000)
        sum += root.Size;
    }
    
    return sum;
}

int GetSecondAnswer(FileNode root, int neededSpace, int minMax)
{
    if (root.Children != null && root.Children.Count > 0)
    {
        foreach (var child in root.Children)
        {
            minMax = GetSecondAnswer(child.Value, neededSpace, minMax);
        }
        if (root.Size > neededSpace && root.Size < minMax)
            minMax = root.Size;
    }
    
    return minMax;
}

LoadFileTree(test, root);
GetSizes(root);
Console.WriteLine($"firstTest: {GetFirstAnswer(root, 0)}");

var emptyTestSpace =    70000000 - root.Size;
var neededTestSpace =   30000000 - emptyTestSpace;
Console.WriteLine($"second test: {GetSecondAnswer(root, neededTestSpace, 30000000)}");

var input = File.ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), "input.txt"));

LoadFileTree(input, inputRoot);
GetSizes(inputRoot);
Console.WriteLine($"first Answer: {GetFirstAnswer(inputRoot, 0)}");

var emptyInputSpace =   70000000 - inputRoot.Size;
var neededInputSpace =  30000000 - emptyInputSpace;
Console.WriteLine($"second Answer: {GetSecondAnswer(inputRoot, neededInputSpace, 30000000)}");

class FileNode 
{
    public FileNode Parent { get; set; }
    public Dictionary<string, FileNode> Children { get; set; }
    public string Name { get; set; }
    public int Size { get; set; } = default;
    public int Answer { get; set; } = default;
}
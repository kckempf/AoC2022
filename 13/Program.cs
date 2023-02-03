var test = new string[]
{
    "[1,1,3,1,1]",
    "[1,1,5,1,1]",
    "",
    "[[1],[2,3,4]]",
    "[[1],4]",
    "",
    "[9]",
    "[[8,7,6]]",
    "",
    "[[4,4],4,4]",
    "[[4,4],4,4,4]",
    "",
    "[7,7,7,7]",
    "[7,7,7]",
    "",
    "[]",
    "[3]",
    "",
    "[[[]]]",
    "[[]]",
    "",
    "[1,[2,[3,[4,[5,6,7]]]],8,9]",
    "[1,[2,[3,[4,[5,6,0]]]],8,9]"
};

var CustomSplit = (string input) => 
{
    if (input.StartsWith('['))
        input = input.Substring(1, input.Length - 2);
    var output = new List<string>();
    var bracketCount = 0;
    var curr = string.Empty;
    for (int i = 0; i < input.Length; i++)
    {
        if (input[i] == '[')
            bracketCount++;
        if (input[i] == ']')
            bracketCount--;
        if (input[i] == ',' && bracketCount == 0)
        {
            output.Add(curr);
            curr = string.Empty;
        }
        else
            curr += input[i];
    }
    if (curr.Length > 0)
        output.Add(curr);
    return output.ToArray();
};

Validation IsInOrder(string left, string right)
{
    var leftArr = CustomSplit(left);
    var rightArr = CustomSplit(right);
    for (int i = 0; i < leftArr.Length; i++)
    {
        if (i == rightArr.Length)
            return Validation.Invalid;
        if (int.TryParse(leftArr[i], out int leftInt) && int.TryParse(rightArr[i], out int rightInt))
        {
            if (leftInt > rightInt)
                return Validation.Invalid;
            if (leftInt < rightInt)
                return Validation.Valid;
        }
        else
        {
            var temp = IsInOrder(leftArr[i], rightArr[i]);
            if (temp != Validation.Pending)
                return temp;
        }
    }
    if (leftArr.Length < rightArr.Length)
        return Validation.Valid;
    else
        return Validation.Pending;
}

var FindResults = (string[] input) =>
{
    var output = 0;
    for (int i = 0; i < input.Length; i+=3)
    {
        if (IsInOrder(input[i],input[i + 1]) != Validation.Invalid)
            output += (i / 3) + 1;
    }
    return output;
};

var FindResults2 = (string[] input) => 
{
    input = input.Where(x => !string.IsNullOrEmpty(x)).Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
    var inputList = input.ToList();
    inputList.Add("[[6]]");
    inputList.Add("[[2]]");
    input = inputList.ToArray();
    Array.Sort(input, (x, y) => IsInOrder(x, y) != Validation.Invalid ? -1 : 1);
    var output = 1;
    for (int i = 0; i < input.Length; i++)
    {
        if (input[i] == "[[6]]" || input[i] == "[[2]]")
            output *= (i + 1);
    }
    return output;
};

var input = File.ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), "input.txt"));
Console.WriteLine($"test: {FindResults(test)}");
Console.WriteLine($"first: {FindResults(input)}");
Console.WriteLine($"test 2: {FindResults2(test)}");
Console.WriteLine($"second: {FindResults2(input)}");
enum Validation 
{
    Valid,
    Invalid,
    Pending
}
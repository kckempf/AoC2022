var tests = new string[]
{
    "mjqjpqmgbljsphdztnvjfqwrcgsmlb",
    "bvwbjplbgvbhsrlpgdmjqwftvncz",
    "nppdvjthqldpwncqszvftbrmjlhg",
    "nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg",
    "zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw"
};

var FindStartOfPacket = (string input, int lengthOfMarker) => 
{
    var temp = new Dictionary<char, int>();
    for (int i = 0; i < input.Length; i++)
    {
        if (!temp.ContainsKey(input[i]))
        {
            temp[input[i]] = 1;
        }
        else
        {
            temp[input[i]] += 1;
        }
        if (temp.Count == lengthOfMarker)
        {
            return i + 1;
        }
        if (i > lengthOfMarker - 2)
        {
            if (temp[input[i - lengthOfMarker + 1]] > 1)
            {
                temp[input[i - lengthOfMarker + 1]] -= 1;
            }
            else
            {
                temp.Remove(input[i - lengthOfMarker + 1]);
            }
        }
    }
    return 0;
};

Console.WriteLine("First Tests:");
foreach (var item in tests)
{
    Console.WriteLine($"{FindStartOfPacket(item, 4)}");
}

var input = File.ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), "input.txt"));
Console.WriteLine($"First Answer: {FindStartOfPacket(input[0], 4)}");

Console.WriteLine("Second Tests:");
foreach (var item in tests)
{
    Console.WriteLine($"{FindStartOfPacket(item, 14)}");
}

Console.WriteLine($"Second Answer: {FindStartOfPacket(input[0], 14)}");
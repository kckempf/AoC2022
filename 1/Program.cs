var input = File.ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), "input.txt"));

var max = 0;
var second = 0;
var third = 0;
var temp = 0;
for (int i = 0; i < input.Length; i++)
{
    var current = input[i];
    if (String.IsNullOrEmpty(current))
    {
        if (temp > max)
        {
            third = second;
            second = max;
            max = temp;
        }
        else if (temp > second)
        {
            third = second;
            second = temp;
        }
        else if (temp > third)
        {
            third = temp;
        }
        temp = 0;
    }
    else
    {
        int.TryParse(current, out int number);
        temp += number;
    }
}
max = Math.Max(max, temp);
Console.WriteLine($"Answer 1: {max}");
Console.WriteLine($"Answer 2: {max + second + third}");
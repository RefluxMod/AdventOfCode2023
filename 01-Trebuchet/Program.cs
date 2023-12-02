var part1 = File.ReadAllLines("input.txt")
    .Select(x => x.Where(char.IsDigit))
    .Select(x => int.Parse(x.First() + "" + x.Last()))
    .Sum();

Console.WriteLine(part1);

List<string> nums = ["_", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine"];

var part2 = File.ReadAllLines("input.txt")
    .Select(ParseNumbers)
    .Select(x => x[0] * 10 + x[^1])
    .Sum();

Console.WriteLine(part2);

int[] ParseNumbers(string s) =>
    s.Select((x, i) => x < 58 ? x - 48 : nums.FindIndex(s[i..].StartsWith)).Where(x => x > 0).ToArray();
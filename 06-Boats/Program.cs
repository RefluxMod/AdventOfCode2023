
var input = File.ReadAllLines("input.txt").Select(x => x.Split(' ', StringSplitOptions.RemoveEmptyEntries)[1..]).ToArray();
var times = input[0].Select(long.Parse).ToArray();
var records = input[1].Select(long.Parse).ToArray();

long part1 = 1;
for (int i = 0; i < times.Length; i++)
    part1 *= Wins(times[i], records[i]);

Console.WriteLine(part1);


var time = long.Parse(string.Concat(input[0]));
var record = long.Parse(string.Concat(input[1]));
long part2 = Wins(time, record);

Console.WriteLine(part2);


long Wins(long time, long distance)
{
    long wins = 0;
    for(long i = 1; i < time - 1; i++)
    {
        if((time - i) * i > distance)
            wins++;
    }
    return wins;
}
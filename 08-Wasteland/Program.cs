var instructions = string.Concat(File.ReadAllLines("input.txt")[0..1]);
var map = File.ReadAllLines("input.txt")[2..].ToDictionary(x => x[0..3], x => (x[7..10], x[12..15]));

string position = "AAA";
int steps = 0;
do
{
    foreach (char c in instructions)
    {
        position = c == 'R' ? map[position].Item2 : map[position].Item1;
        steps++;
    }
} while (position != "ZZZ");

Console.WriteLine(steps);

var positionList = map.Where(x => x.Key.EndsWith('A')).Select(x => new Position { Current = x.Key} ).ToList();
do
{
    foreach (char c in instructions)
    {
        for(int i =  0; i < positionList.Count; i++)
        {
            if (positionList[i].Current.EndsWith('Z'))
                continue;

            positionList[i].Current = c == 'R' ? map[positionList[i].Current].Item2 : map[positionList[i].Current].Item1;
            positionList[i].Steps++;
        }
    }
} while (positionList.Any(x => !x.Current.EndsWith('Z')));

Console.WriteLine(LeastCommonMultiple(positionList.Select(x => (long)x.Steps).ToArray()));


long GreatestCommonDivisor(long a, long b)
{
    while (b != 0)
    {
        long temp = b;
        b = a % b;
        a = temp;
    }
    return a;
}

long LeastCommonMultiple(long[] numbers)
{
    long lcm = numbers[0];
    for (int i = 1; i < numbers.Length; i++)
    {
        long gcd = GreatestCommonDivisor(lcm, numbers[i]);
        lcm = Math.Abs(lcm * numbers[i]) / gcd;
    }
    return lcm;
}

class Position
{
    public string Current = "";
    public int Steps = 0;
}
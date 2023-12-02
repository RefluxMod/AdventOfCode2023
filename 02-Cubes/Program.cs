var part1 = File.ReadAllLines("input.txt")
    .Select(MaxCubes)
    .Where(x => x.Red < 13 && x.Green < 14 && x.Blue < 15)
    .Sum(x => x.Id);

Console.WriteLine(part1);

var part2 = File.ReadAllLines("input.txt")
    .Select(MaxCubes)
    .Select(x => x.Red * x.Green * x.Blue)
    .Sum();

Console.WriteLine(part2);

(int Id, int Red, int Green, int Blue) MaxCubes(string line, int index)
{
    var rounds = line.Split(':',';');
    int maxRed = 0, maxGreen = 0, maxBlue = 0;

    foreach (string r in rounds[1..])
    {
        var parts = r.Split(' ', ',');

        for (int i = 1; i < parts.Length; i += 3)
        {
            int num = int.Parse(parts[i]);

            if (parts[i + 1] == "red")
                maxRed = Math.Max(maxRed, num);
            else if (parts[i + 1] == "green")
                maxGreen = Math.Max(maxGreen, num);
            else if (parts[i + 1] == "blue")
                maxBlue = Math.Max(maxBlue, num);
        }
    }
    return (index + 1, maxRed, maxGreen, maxBlue);
}
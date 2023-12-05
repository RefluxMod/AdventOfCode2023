using System.Diagnostics;

var input = File.ReadAllText("input.txt").Split("\r\n\r");
var maps = input[1..].Select(x => x.Split("\r\n")[1..]).Select(ParseMap).ToArray();
var seeds = input[0].Split(' ')[1..].Select(uint.Parse);

Console.WriteLine($"Part1 {seeds.Min(FindLocation)}");

var watch = Stopwatch.StartNew();
var tasks = seeds.Chunk(2).Select(x => Task.Run(() => FindLowestLocation(x[0], x[1])));
var locations = await Task.WhenAll(tasks);
watch.Stop();

Console.WriteLine($"Part2 {locations.Min()} {watch.Elapsed}"); // Det tog 1 min 30s att köra release
Console.ReadLine();


uint FindLowestLocation(uint start, uint length)
{
    Console.WriteLine($"Start Task {Task.CurrentId}");
    uint location = uint.MaxValue;
    for (uint i = 0; i < length; i++)
        location = Math.Min(location, FindLocation(start++));
    Console.WriteLine($"Finished Task {Task.CurrentId} with lowest location number {location}");
    return location;
}

uint FindLocation(uint seed)
{
    var d = FindDestination(maps[0], seed);
    d = FindDestination(maps[1], d);
    d = FindDestination(maps[2], d);
    d = FindDestination(maps[3], d);
    d = FindDestination(maps[4], d);
    d = FindDestination(maps[5], d);
    d = FindDestination(maps[6], d);
    return d;
}

uint FindDestination(Map[] map, uint source)
{
    for(int i = 0; i < map.Length; i++)
    {
        if (source >= map[i].SourceFrom && source <= map[i].SourceTo)
            return map[i].Dest + source - map[i].SourceFrom;
    }
    return source;
}

Map[] ParseMap(string[] lines) => lines.Select(ParseMapLine).ToArray();

Map ParseMapLine(string line)
{
    var split = line.Split(' ').Select(uint.Parse).ToArray();
    return new(split[1], split[1] + split[2] - 1, split[0]);
}

record Map(uint SourceFrom, uint SourceTo, uint Dest);
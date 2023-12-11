int galaxyCount = 0;
var rows = File.ReadAllLines("input.txt");

var points = rows.Select((line, y) => line.Select((val, x) => (x, y, val))).SelectMany(x => x);

var emptyRows = Enumerable.Range(0, rows.Length).Where(i => !points.Any(p => p.y == i && p.val == '#'));
var emptyColumns = Enumerable.Range(0, rows[0].Length).Where(i => !points.Any(p => p.x == i && p.val == '#'));

var pairs1 = GetPairs(GetGalaxies(1));
Console.WriteLine(pairs1.Sum(p => Math.Abs(p.g1.X - p.g2.X) + Math.Abs(p.g1.Y - p.g2.Y)));

var pairs2 = GetPairs(GetGalaxies(999999));
Console.WriteLine(pairs2.Sum(p => Math.Abs(p.g1.X - p.g2.X) + Math.Abs(p.g1.Y - p.g2.Y)));


Galaxy[] GetGalaxies(int expansion)=>  points.Where(p => p.val == '#').Select(p => new Galaxy(
        p.x + emptyColumns.Count(c => c < p.x) * expansion,
        p.y + emptyRows.Count(r => r < p.y) * expansion,
        Id: ++galaxyCount)).ToArray();

IEnumerable<(Galaxy g1, Galaxy g2)> GetPairs(Galaxy[] galaxies) =>
    from g1 in galaxies
    from g2 in galaxies
    where g1.Id < g2.Id
    select (g1, g2);

record struct Galaxy(long X, long Y, int Id);
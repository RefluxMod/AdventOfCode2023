using System.Text.RegularExpressions;

var schematic = File.ReadAllLines("input.txt");

var numbers = from y in Enumerable.Range(0, schematic.Length)
              from m in Regex.Matches(schematic[y], "\\d+")
              from x in Enumerable.Range(m.Index, m.Value.Length)
              select new { x, y, match = m };

var symbols = from y in Enumerable.Range(0, schematic.Length)
              from m in Regex.Matches(schematic[y], "[^0-9.]")
              select new { x = m.Index, y, m.Value };

var part1 = from n in numbers
            from s in symbols.Where(s => Math.Abs(n.x - s.x) < 2 && Math.Abs(n.y - s.y) < 2)
            group 0 by n.match;

Console.WriteLine(part1.Sum(x => int.Parse(x.Key.Value)));

var part2 = from s in symbols.Where(s => s.Value == "*")
            from n in numbers.GroupBy(x => x.match).Where(x => x.Any(n => Math.Abs(n.x - s.x) < 2 && Math.Abs(n.y - s.y) < 2))
            group int.Parse(n.Key.Value) by s;

Console.WriteLine(part2.Where(x => x.Count() == 2).Sum(x => x.First() * x.Last()));
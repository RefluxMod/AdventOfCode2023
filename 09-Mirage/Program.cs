var sequences = File.ReadAllLines("input.txt").Select(x => x.Split(' ').Select(long.Parse));
Console.WriteLine(sequences.Sum(GetValue1));
Console.WriteLine(sequences.Sum(GetValue2));


long GetValue1(IEnumerable<long> sequence)
{
    var diffs = GetDiffRows(sequence);

    for (int i = 1; i < diffs.Count; i++)
        diffs[i].Add(diffs[i].Last() + diffs[i - 1].Last());

    return sequence.Last() + diffs.Last().Last();
}

long GetValue2(IEnumerable<long> sequence)
{
    var diffs = GetDiffRows(sequence);

    for (int i = 1; i < diffs.Count; i++)
        diffs[i].Insert(0, diffs[i].First() - diffs[i - 1].First());

    return sequence.First() - diffs.Last().First();
}

List<List<long>> GetDiffRows(IEnumerable<long> sequence)
{
    var current = sequence.ToList();
    var diffs = new List<List<long>>();

    do
    {
        var newRow = new List<long>();
        for (int i = 1; i < current.Count; i++)
            newRow.Add(current[i] - current[i - 1]);

        diffs.Insert(0, newRow);
        current = newRow;

    } while (!current.All(x => x == 0));

    return diffs;
}
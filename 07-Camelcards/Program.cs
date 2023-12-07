List<char> labels1 = ['2', '3', '4', '5', '6', '7', '8', '9', 'T', 'J', 'Q', 'K', 'A'];
List<char> labels2 = ['J', '2', '3', '4', '5', '6', '7', '8', '9', 'T', 'Q', 'K', 'A'];

var rankedCards = File.ReadAllLines("input.txt")
    .Select(x => x.Split(' ')[0])
    .Select(GetCards1)
    .OrderBy(x => x.Kind)
    .ThenBy(x => x.Sort).ToArray();

Console.WriteLine(GetTotal(rankedCards));


rankedCards = File.ReadAllLines("input.txt")
    .Select(x => x.Split(' ')[0])
    .Select(GetCards2)
    .OrderBy(x => x.Kind)
    .ThenBy(x => x.Sort).ToArray();

Console.WriteLine(GetTotal(rankedCards));


int GetTotal((string Card, int Kind, string Sort)[] rankedCards)
{
    var cards = File.ReadAllLines("input.txt").Select(x => x.Split(' ')[0]);
    var bids = File.ReadAllLines("input.txt").Select(x => x.Split(' ')[1]).Select(int.Parse);
    var dict = cards.Zip(bids).ToDictionary(g => g.First, g => g.Second);

    int total = 0;
    for (int i = 0; i < rankedCards.Length; i++)
        total += (i + 1) * dict[rankedCards[i].Card];

    return total;
}

(string Card, int Kind, string Sort) GetCards1(string card) =>
    (card, GetKind(card), card.Select(s => labels1.IndexOf(s)).Aggregate("", (x, y) => x + "" + y.ToString("X")));

(string Card, int Kind, string Sort) GetCards2(string card)
{
    var kind = GetKind(card);
    var key = GetKindChar(card, kind);
    var cardSort = card.Select(s => labels2.IndexOf(s)).Aggregate("", (x, y) => x + "" + y.ToString("X"));

    if (card == "JJJJJ")
        return (card, kind, cardSort);

    if (key != 'J' && card.Contains('J'))
    {
        var newCard = GetCards1(card.Replace('J', key));
        return (card, newCard.Kind, cardSort);
    }
    else if (key == 'J')
    {
        var max = card.MaxBy(labels2.IndexOf);
        var newCard = GetCards1(card.Replace('J', max));
        return (card, newCard.Kind, cardSort);
    }
    return (card, kind, cardSort);
}

int GetKind(string card)
{
    var g = card.GroupBy(x => x).OrderByDescending(x => x.Count()).ToArray();
    return g.Any(x => x.Count() == 5) ? 7
        : g.Any(x => x.Count() == 4) ? 6
        : g.Any(x => x.Count() == 3) && g.Any(x => x.Count() == 2) ? 5
        : g.Any(x => x.Count() == 3) ? 4
        : g.Count(x => x.Count() == 2) == 2 ? 3
        : g.Any(x => x.Count() == 2) ? 2 : 1;
}

char GetKindChar(string card, int kind)
{
    var g = card.GroupBy(x => x).OrderByDescending(x => x.Count()).ToArray();
    if (kind == 3)
    {
        var key1 = labels2.IndexOf(g[0].Key);
        var key2 = labels2.IndexOf(g[1].Key);
        return key1 > key2 ? g[0].Key : g[1].Key;
    }
    else if (kind == 1)
    {
        return card.MaxBy(labels2.IndexOf);
    }
    return g[0].Key;
}
var cards = File.ReadAllLines("input.txt").Select(Parse).ToList();
Console.WriteLine(cards.Sum(x => x.Score));

var copies = new List<Card>();
foreach(var x in cards)
    AddCopies(x);

Console.WriteLine(cards.Count + copies.Count);

Card Parse(string line, int index)
{
    var parts = line.Split(':')[1].Split(' ', '|').Where(x => x != "").ToArray();
    var winning = parts[..10];
    var numbers = parts[10..];
    int score = 0;
    int matching = 0;

    foreach(var w in winning)
    {
        if (numbers.Contains(w))
        {
            score = score < 1 ? 1 : score * 2;
            matching++;
        }
    }

    return new(index, score, matching);
}

void AddCopies(Card card)
{
    int id = card.Id;
    for (int i = 0; i < card.Matching; i++)
    {
        id++;
        if (id < cards.Count)
        {
            var copy = cards[id];
            copies.Add(copy);
            AddCopies(copy);
        }
    }
}

record Card(int Id, int Score, int Matching);
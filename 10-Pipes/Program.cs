var lines = File.ReadAllLines("input.txt");
var map = lines.Select(line => line.ToArray()).ToArray();
var start = FindStartPosition(map);
var stepMap1 = Walk(start.Y, start.X, start.Y - 1, start.X);
var stepMap2 = Walk(start.Y, start.X, start.Y + 1, start.X);
FindMeetingPosition(stepMap1, stepMap2);


(int Y, int X) FindStartPosition(char[][] map)
{
    for (int y = 0; y < map.Length; y++)
        for (int x = 0; x < map[y].Length; x++)
            if (map[y][x] == 'S')
                return (y, x);

    throw new Exception("Start position not found");
}

int[][] Walk(int prevY, int prevX, int currentY, int currentX)
{
    var stepMap = map.Select(x => x.Select(_ => 0).ToArray()).ToArray();
    int steps = 1;
    while (true)
    {
        (int newY, int newX) = NextPosition(prevY, prevX, currentY, currentX);

        if (map[newY][newX] == 'S')
            break;

        prevY = currentY;
        prevX = currentX;
        currentY = newY;
        currentX = newX;
        stepMap[newY][newX] = ++steps;
    }
    return stepMap;
}

void FindMeetingPosition(int[][] stepMap1, int[][] stepMap2)
{
    for (int i = 0; i < stepMap1.Length; i++)
    {
        var s1 = stepMap1[i].Select((val, index) => (val, index)).Where(x => x.val > 0);
        var s2 = stepMap2[i].Select((val, index) => (val, index)).Where(x => x.val > 0);
        var position = s1.Intersect(s2);
        if (position.Count() > 0)
            Console.WriteLine(position.First().val);
    }
}

(int Y, int X) NextPosition(int prevY, int prevX, int Y, int X)
{
    var w = Ways(Y, X);
    return (prevY, prevX) == (w.Y1, w.X1) ? (w.Y2, w.X2) : (w.Y1, w.X1);
}

(int Y1, int X1, int Y2, int X2) Ways(int Y, int X) => map[Y][X] switch
{
    '|' => (Y + 1, X, Y - 1, X),
    '-' => (Y, X + 1, Y, X - 1),
    'L' => (Y - 1, X, Y, X + 1),
    'J' => (Y - 1, X, Y, X - 1),
    '7' => (Y + 1, X, Y, X - 1),
    'F' => (Y + 1, X, Y, X + 1),
    _ => throw new Exception("No way"),
};
for (int i=0; i<4; i++)
{
    for (int j=0; j<14; j++)
    {
        Card card = new Card((Color)i, (Rank)j);
        Console.WriteLine($"The {card.Color} {card.Rank}");
    }
}
class Card
{
    public Color Color { get; }
    public Rank Rank { get; }

    public Card(Color color, Rank rank)
    {
        Color = color;
        Rank = rank;
    }
    public bool CardIsNumber => Rank != Rank.DollarSign || Rank != Rank.Percent || Rank != Rank.Caret || Rank != Rank.Ampersand;
    public bool CardIsSymbol => !CardIsNumber;
}
public enum Color { Red, Green, Blue, Yellow }
public enum Rank { One, Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten, DollarSign, Percent, Caret, Ampersand }
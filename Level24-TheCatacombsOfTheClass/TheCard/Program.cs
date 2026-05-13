Card[] deck = new Card[56];

for (int i = 0; i < deck.Length; i++)
    deck[i] = new Card((Color)(i / 14), (Rank)(i % 14));

foreach (Card card in deck)
    Console.WriteLine($"The {card.Color} {card.Rank}");

internal class Card
{
    public Color Color { get; }
    public Rank Rank { get; }
    public Card(Color color, Rank rank)
    {
        Color = color;
        Rank = rank;
    }
    public bool IsNumber() => (int)Rank <= 9;
    public bool IsSymbol() => !IsNumber();
}
internal enum Color { Red, Green, Blue, Yellow }
internal enum Rank { One, Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten, DollarSign, Percent, Caret, Ampersand }
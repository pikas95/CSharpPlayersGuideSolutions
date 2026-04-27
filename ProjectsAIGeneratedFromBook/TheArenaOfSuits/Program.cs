new CardGame().Run();

internal class CardGame
{
    private Deck _deck = new Deck();
    private Player[] _player;

    public CardGame() { }

    public void Run()
    {
        Console.Write("How many players? ");
        // I see the critical issue, it will be addressed after later chapters
        _player = new Player[Convert.ToInt32(Console.ReadLine())];

        int cardsHandSize = _deck.TotalCards / _player.Length;
        CreatePlayers(cardsHandSize);

        while (true) // game engine here
        {
            _deck.ShuffleDeck();

            DealCards(cardsHandSize);

            PlayGame(cardsHandSize);

            DisplayWinner();

            if (AskReplay())
            {
                _deck.Reset();
                foreach (Player player in _player)
                    player.Reset();
            }
            else break;
        }
    }

    private void PlayGame(int cardsHandSize)
    {
        for (int round = 0; round < cardsHandSize; round++)
        {
            int wonRoundIndex = PlayRound();

            if (wonRoundIndex != -1)
            {
                Console.WriteLine($"{_player[wonRoundIndex].Name} won the round!");
                _player[wonRoundIndex].WonRound();
            }
            else
                Console.WriteLine("It's a tie!");

            DisplayScores();

            Console.WriteLine();
            if (round < cardsHandSize - 1)
            {
                Console.WriteLine("Press enter to continue to next round.");
                Console.ReadLine();
            }
        }
    }

    private int PlayRound()
    {
        int[] pickedCardIndex = new int[_player.Length];
        Card[] pickedCard = new Card[_player.Length];

        for (int i = 0; i < _player.Length; i++)
        {
            pickedCardIndex[i] = PlayerPickCard(_player[i]);
            pickedCard[i] = _player[i].PlayCard(pickedCardIndex[i]);
        }

        for (int i = 0; i < _player.Length; i++)
            DisplayRoundCard(_player[i].Name, pickedCard[i].ToString());
        Console.WriteLine();

        return DetermineRoundWinner(pickedCard);
    }

    private void CreatePlayers(int cardsHandSize)
    {
        for (int i = 0; i < _player.Length; i++)
        {
            Console.Write($"Player {i + 1}, write your name: ");
            _player[i] = new Player(Console.ReadLine(), cardsHandSize);
        }
    }

    private void DealCards( int cardsHandSize)
    {
        for (int i = 0; i < cardsHandSize; i++)
            for (int j = 0; j < _player.Length; j++)
                _player[j].ReceiveCard(_deck.DealCard());
    }

    private int PlayerPickCard(Player player)
    {
        DisplayPlayerCards(player.GetHand(), player.Holding);
        return AskCard(player.Holding) - 1; // returns chosen card index in _cards array

        void DisplayPlayerCards(Card[] cards, int holding)
        {
            Console.Clear();
            Console.WriteLine($"{player.Name}, your cards:");

            for (int i = 0; i < holding; i++) 
            {
                if (i % 4 == 0)
                    Console.WriteLine();
                Console.Write($"{(i + 1), -2}: {cards[i], -20}");
            }

            Console.WriteLine();
        }

        int AskCard(int cardsInHand)
        {
            int input = 0;
            Console.WriteLine();

            while (input < 1 || input > cardsInHand)
            {
                Console.Write("Pick a card from the list above: ");
                input = Convert.ToInt32(Console.ReadLine()); // I see the critical issue, it will be addressed after later chapters
            }

            Console.Clear();
            return input;
        }
    }

    private void DisplayRoundCard(string playerName, string card) => Console.WriteLine($"{playerName} plays {card}");

    private int DetermineRoundWinner(Card[] cards) // method to decide who won the round
    {
        int playerIndex = 0;
        bool tie = false;

        for (int i = 1; i < cards.Length; i++)
        {
            if (cards[playerIndex].Value < cards[i].Value)
            {
                playerIndex = i;
                tie = false;
            }
            else if (cards[playerIndex].Value == cards[i].Value)
                tie = true;
        }

        return tie ? -1 : playerIndex;
    }

    private void DisplayScores()
    {
        Console.WriteLine();
        Console.WriteLine("Scores:");
        foreach (Player player in _player)
            Console.WriteLine($"{player.Name} - {player.RoundsWon}");
    }

    private void DisplayWinner()
    {
        int wonGameIndex = DetermineGameWinner();

        if (wonGameIndex != -1)
            Console.WriteLine($"Congratulations {_player[wonGameIndex].Name}! You won!");
        else
            Console.WriteLine("Nobody won, it's a tie!");
    }

    private int DetermineGameWinner() // method to decide who won the game
    {
        int playerIndex = 0;
        bool tie = false;

        for (int i = 1; i < _player.Length; i++)
        {
            if (_player[playerIndex].RoundsWon < _player[i].RoundsWon)
            {
                playerIndex = i;
                tie = false;
            }
            else if (_player[playerIndex].RoundsWon == _player[i].RoundsWon)
                tie = true;
        }

        return tie ? -1 : playerIndex;
    }

    private bool AskReplay()
    {
        Console.WriteLine();
        Console.Write("Do you want to play again (yes/no)? ");
        string input = Console.ReadLine();

        if (input != "yes")
        {
            Console.WriteLine("You didn't say yes, so I guess it's a no..");
            return false;
        }
        return true;
    }
}

internal class Player
{
    public string Name { get; }
    private Card[] _hand;
    public int RoundsWon { get; private set; } = 0;
    public int Holding { get; private set; } = 0;

    public Player(string name, int cardsHandSize)
    {
        Name = name != "" ? name : "Unknown";
        _hand = new Card[cardsHandSize];
    }

    public void ReceiveCard(Card card)
    {
        _hand[Holding] = card;
        Holding++;
    }

    public Card PlayCard(int index)
    {
        Card playCard = _hand[index];

        for (int i = index; i < Holding - 1; i++)
            _hand[i] = _hand[i + 1];
        Holding--;

        return playCard;
    }

    public Card[] GetHand() => _hand[0..Holding];

    public void WonRound() => RoundsWon++;

    public void Reset()
    {
        RoundsWon = 0;
        Holding = 0;
    }
}

internal class Deck
{
    private readonly Card[] _cards = new Card[52];
    public int TotalCards => _cards.Length; // fixed total cards
    private int _cardsInDeck = 0; // contains actual (without 0) number of cards in deck

    public Deck() 
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 2; j <= 14; j++)
            {
                _cards[_cardsInDeck] = new((Suit)i, (Rank)j);
                _cardsInDeck++; 
            }
        }
    }

    public void ShuffleDeck()
    {
        for (int i = _cards.Length - 1; i > 0; i--)
        {
            int random = new Random().Next(0, i + 1);
            Card temp = _cards[random];
            _cards[random] = _cards[i];
            _cards[i] = temp;
        }
    }

    public Card DealCard()
    {
        _cardsInDeck--; // after constructor _cardsInDeck = 52, _cards[51] is the last possible member
        return _cards[_cardsInDeck];
    }

    public void Reset() => _cardsInDeck = TotalCards; // resets Count for each match
}

internal class Card
{
    public Suit Suit { get; }
    public Rank Rank { get; }
    public int Value => (int)Rank;

    public Card(Suit suit, Rank rank)
    {
        Suit = suit;
        Rank = rank;
    }

    public override string ToString() => $"{Rank} of {Suit}";
}

internal enum Suit { Clubs, Diamonds, Hearts, Spades}
internal enum Rank { Two = 2, Three = 3, Four = 4, Five = 5, Six = 6, Seven = 7, Eight = 8,
                     Nine = 9, Ten = 10, Jack = 11, Queen = 12, King = 13, Ace = 14}
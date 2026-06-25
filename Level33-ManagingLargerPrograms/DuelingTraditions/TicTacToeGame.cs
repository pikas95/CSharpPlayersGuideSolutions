namespace TicTacToe
{
    internal class TicTacToeGame
    {
        private readonly Player _p1 = new Player(SquareState.X);
        private readonly Player _p2 = new Player(SquareState.O);
        private readonly Board _board = new Board();

        public void Run()
        {
            DisplayControls();

            while (true)
            {
                for (int i = 0; _board.WhoWon() == SquareState.None && i < 9; i++)
                {
                    if (i % 2 == 0)
                        PlayTurn(_p1);
                    else
                        PlayTurn(_p2);

                    Console.Clear();
                }

                DisplayBoard();

                SquareState winner = _board.WhoWon();
                RoundOutcome(winner == SquareState.X ? _p1 :
                             winner == SquareState.O ? _p2 : null!);
                DisplayScores();
                if (!AskPlayAgain())
                    break;
            }


        }

        private static void DisplayControls()
        {
            Console.WriteLine("Players can choose a square with these letters:");
            Console.WriteLine(" q | w | e \n" +
                              "---+---+---\n" +
                              " a | s | d \n" +
                              "---+---+---\n" +
                              " z | x | c ");

            Console.Write("Press enter to continue");
            Console.ReadLine();
            Console.Clear();
        }

        private void DisplayBoard()
        {
            for (int col = 0; col < _board.Squares.GetLength(0); col++)
            {
                for (int row = 0; row < _board.Squares.GetLength(1); row++)
                {
                    if (_board.Squares[col, row] != SquareState.None)
                        Console.Write($" {_board.Squares[col, row]} ");
                    else
                        Console.Write("   ");

                    if (row + 1 != _board.Squares.GetLength(1))
                        Console.Write("|");
                }
                if (col + 1 != _board.Squares.GetLength(0))
                    Console.WriteLine("\n---+---+---");
                else
                    Console.WriteLine();
            }
        }

        private void PlayTurn(Player player)
        {
            Console.Write("It's ");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write(player.Name);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("'s turn.");

            DisplayBoard();

            Console.Write("What square do you want to play in? ");
            char input = Convert.ToChar(Console.ReadLine());

            while (true)
            {
                if (!InputValidation(input))
                    Console.Write("No such option. Again:");
                else if (!TryPlay(input))
                    Console.Write("That square is already occupied. Again");
                else break;

                input = Convert.ToChar(Console.ReadLine());
            }

            bool InputValidation(char input) => "qweasdzxc".Contains(input);

            bool TryPlay(char input)
            {
                return input switch
                {
                    'q' => _board.TryPlaySquare(0, 0, player),
                    'w' => _board.TryPlaySquare(0, 1, player),
                    'e' => _board.TryPlaySquare(0, 2, player),
                    'a' => _board.TryPlaySquare(1, 0, player),
                    's' => _board.TryPlaySquare(1, 1, player),
                    'd' => _board.TryPlaySquare(1, 2, player),
                    'z' => _board.TryPlaySquare(2, 0, player),
                    'x' => _board.TryPlaySquare(2, 1, player),
                    'c' => _board.TryPlaySquare(2, 2, player),
                };
            }
        }

        private static void RoundOutcome(Player player)
        {
            Console.WriteLine();

            if (player != null)
            {
                Console.WriteLine($"{player.Name} won!");
                player.RoundsWon++;
            }
            else
                Console.WriteLine("It's a draw.");
        }

        private void DisplayScores() => Console.WriteLine($"\nScore: {_p1.Name} - {_p1.RoundsWon} | {_p2.Name} - {_p2.RoundsWon}");

        private bool AskPlayAgain()
        {
            Console.WriteLine();
            Console.Write("Play again? (yes/no) ");
            if (Console.ReadLine()?.Contains("yes") == true)
            {
                Console.Clear();
                _board.Reset();
                return true;
            }

            return false;
        }
    }
}
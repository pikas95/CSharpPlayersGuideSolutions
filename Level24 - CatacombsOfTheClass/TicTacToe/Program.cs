// this is a speedrun convertion to all classes from first code attempt
new TicTacToeGame().Run();
public class TicTacToeGame
{
    public void Run()
    {
        Player player1 = new Player(1);
        Player player2 = new Player(2);
        BoardRenderer renderer = new BoardRenderer();
        Board board = new Board();
        Player currentPlayer = player1;

        int whichPlayerTurn = board.squaresTaken % 2 == 0 ? 1 : 2;
        Console.WriteLine("Press Enter to Start");
        while (Console.ReadKey().Key == ConsoleKey.Enter)
        {
            Console.Clear();
            while (true)
            {
                Console.Write("It is ");
                if (whichPlayerTurn == 1)
                    Console.WriteLine("X's turn.");
                else
                    Console.WriteLine("O's turn.");
                renderer.Draw(board);
                Console.WriteLine("What square you want to play in?");
                currentPlayer.PickSquare(board, whichPlayerTurn);

                if (board.DidHeWin(whichPlayerTurn))
                {
                    Console.Write("This round won ");
                    if (whichPlayerTurn == 1)
                    {
                        Console.WriteLine("X.");
                        player1.Score++;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("O.");
                        player2.Score++;
                        break;
                    }
                }

                // Checking if after turn it's draw
                if (board.IsItDraw())
                {
                    Console.WriteLine("cat...");
                    break;
                }
                whichPlayerTurn = board.squaresTaken % 2 == 0 ? 1 : 2;
                currentPlayer = currentPlayer == player1 ? player2 : player1;

                Console.Clear();
            }
            Console.WriteLine($"Current Score: X - {player1.Score}, O - {player2.Score}");
            Console.WriteLine("Press Enter to Play a new round");
        }
    }
}
public class Player
{
    public int PlayerNumber { get; }
    public int Score { get; set; }
    public Player(int number)
    {  
        PlayerNumber = number; 
    }
    public void PickSquare(Board game, int player)
    {
        while (true)
        {
            string? input = Console.ReadLine();
            while (input != "1" && input != "2" && input != "3" && input != "4" && input != "5" && input != "6" && input != "7" && input != "8" && input != "9")
            {
                Console.WriteLine("Oops, you can't do that. Try again.");
                input = Console.ReadLine();
            }
            int chosenSquare = Convert.ToInt32(input);
            if (AssignSquare(chosenSquare, player, game))
                return;
            Console.WriteLine("This square is already taken. Choose another one.");
        }
    }

    public bool AssignSquare(int chosenSquare, int player, Board game)
    {
        if (chosenSquare == 1 && game.Squares[2, 0] == 0)
        {
            game.Squares[2, 0] = player;
            game.squaresTaken++;
            return true;
        }
        if (chosenSquare == 2 && game.Squares[2, 1] == 0)
        {
            game.Squares[2, 1] = player;
            game.squaresTaken++;
            return true;
        }
        if (chosenSquare == 3 && game.Squares[2, 2] == 0)
        {
            game.Squares[2, 2] = player;
            game.squaresTaken++;
            return true;
        }
        if (chosenSquare == 4 && game.Squares[1, 0] == 0)
        {
            game.Squares[1, 0] = player;
            game.squaresTaken++;
            return true;
        }
        if (chosenSquare == 5 && game.Squares[1, 1] == 0)
        {
            game.Squares[1, 1] = player;
            game.squaresTaken++;
            return true;
        }
        if (chosenSquare == 6 && game.Squares[1, 2] == 0)
        {
            game.Squares[1, 2] = player;
            game.squaresTaken++;
            return true;
        }
        if (chosenSquare == 7 && game.Squares[0, 0] == 0)
        {
            game.Squares[0, 0] = player;
            game.squaresTaken++;
            return true;
        }
        if (chosenSquare == 8 && game.Squares[0, 1] == 0)
        {
            game.Squares[0, 1] = player;
            game.squaresTaken++;
            return true;
        }
        if (chosenSquare == 9 && game.Squares[0, 2] == 0)
        {
            game.Squares[0, 2] = player;
            game.squaresTaken++;
            return true;
        }
        return false;
    }
}
public class BoardRenderer
{
    public void Draw(Board board)
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                Console.Write(" ");
                if (board.Squares[i, j] == 0)
                    Console.Write(" ");
                else if (board.Squares[i, j] == 1)
                    Console.Write("X");
                else
                    Console.Write("O");
                Console.Write(" ");
                if (j < 2)
                    Console.Write("|");
            }
            Console.WriteLine();
            if (i < 2)
                Console.WriteLine("---+---+---");
        }
    }
}
public class Board
{
    public int[,] Squares { get; } = new int[3, 3];
    public int squaresTaken = 0;
    public bool DidHeWin(int player)
    {
        for (int i = 0; i < 3; i++)
        {
            if (Squares[i, 0] == player && Squares[i, 1] == player && Squares[i, 2] == player)
                return true;
            if (Squares[0, i] == player && Squares[1, i] == player && Squares[2, i] == player)
                return true;
        }
        if (Squares[0, 0] == player && Squares[1, 1] == player && Squares[2, 2] == player)
            return true;
        if (Squares[2, 0] == player && Squares[1, 1] == player && Squares[0, 2] == player)
            return true;
        return false;
    }
    public bool IsItDraw()
    {
        if (squaresTaken == 9)
            return true;
        return false;
    }
}
/*
 * First Attempt
int scoreX = 0, scoreO = 0;
Console.WriteLine("Press Enter to Start");
while (Console.ReadKey().Key == ConsoleKey.Enter)
{
    Console.Clear();
    Board game = new Board();
    while (true)
    {
        int whichPlayerTurn = game.squaresTaken % 2 == 0 ? 1 : 2;
        // Main flow of each round
        Console.Write("It is ");
        if (whichPlayerTurn == 1)
            Console.WriteLine("X's turn.");
        else
            Console.WriteLine("O's turn.");
        game.DisplayBoard();
        Console.WriteLine("What square you want to play in?");
        PickSquare(game, whichPlayerTurn);

        // Checking if player won after his turn
        if (game.DidHeWin(whichPlayerTurn))
        {
            Console.Write("This round won ");
            if (whichPlayerTurn == 1)
            {
                Console.WriteLine("X.");
                scoreX++;
                break;
            }
            else
            {
                Console.WriteLine("O.");
                scoreO++;
                break;
            }
        }

        // Checking if after turn it's draw
        if (game.IsItDraw())
        {
            Console.WriteLine("cat...");
            break;
        }
        Console.Clear();
    }
    Console.WriteLine($"Current Score: X - {scoreX}, O - {scoreO}");
    Console.WriteLine("Press Enter to Play a new round");
}
void PickSquare(Board game, int player)
{
    string? input = Console.ReadLine();
    while (input != "1" && input != "2" && input != "3" && input != "4" && input != "5" && input != "6" && input != "7" && input != "8" && input != "9")
    {
        Console.WriteLine("Oops, you can't do that. Try again.");
        input = Console.ReadLine();
    }
    int chosenSquare = Convert.ToInt32(input);
    while (!game.AssignSquare(chosenSquare, player))
    {
        Console.WriteLine("This square is already taken. Choose another one.");
        chosenSquare = Convert.ToInt32(Console.ReadLine());
    }
}
class Board
{
    private int[,] Squares = new int[3, 3];
    public int squaresTaken = 0;
    public bool DidHeWin(int player)
    {
        for (int i=0; i<3; i++)
        {
            if (Squares[i, 0] == player && Squares[i, 1] == player && Squares[i, 2] == player)
                return true;
            if (Squares[0, i] == player && Squares[1, i] == player && Squares[2, i] == player)
                return true;
        }
        if (Squares[0, 0] == player && Squares[1, 1] == player && Squares[2, 2] == player)
            return true;
        if (Squares[2, 0] == player && Squares[1, 1] == player && Squares[0, 2] == player)
            return true;
        return false;
    }
    public bool IsItDraw()
    {
        if (squaresTaken == 9)
            return true;
        return false;
    }
    public void DisplayBoard()
    {
        for (int i=0; i<3; i++)
        {
            for (int j=0; j<3; j++)
            {
                Console.Write(" ");
                if (Squares[i, j] == 0)
                    Console.Write(" ");
                else if (Squares[i, j] == 1)
                    Console.Write("X");
                else 
                    Console.Write("O");
                Console.Write(" ");
                if (j < 2) 
                    Console.Write("|");
            }
            Console.WriteLine();
            if (i < 2)
            {
                for (int u=1; u<=11; u++)
                {
                    if (u % 4 == 0)
                        Console.Write("+");
                    else 
                        Console.Write("-");
                }
                Console.WriteLine();
            }
        }
    }
    public bool AssignSquare(int chosenSquare, int player)
    {
        if (chosenSquare == 1 && Squares[2, 0] == 0)
        {
            Squares[2, 0] = player;
            squaresTaken++;
            return true;
        }
        if (chosenSquare == 2 && Squares[2, 1] == 0)
        {
            Squares[2, 1] = player;
            squaresTaken++;
            return true;
        }
        if (chosenSquare == 3 && Squares[2, 2] == 0)
        {
            Squares[2, 2] = player;
            squaresTaken++;
            return true;
        }
        if (chosenSquare == 4 && Squares[1, 0] == 0)
        {
            Squares[1, 0] = player;
            squaresTaken++;
            return true;
        }
        if (chosenSquare == 5 && Squares[1, 1] == 0)
        {
            Squares[1, 1] = player;
            squaresTaken++;
            return true;
        }
        if (chosenSquare == 6 && Squares[1, 2] == 0)
        {
            Squares[1, 2] = player;
            squaresTaken++;
            return true;
        }
        if (chosenSquare == 7 && Squares[0, 0] == 0)
        {
            Squares[0, 0] = player;
            squaresTaken++;
            return true;
        }
        if (chosenSquare == 8 && Squares[0, 1] == 0)
        {
            Squares[0, 1] = player;
            squaresTaken++;
            return true;
        }
        if (chosenSquare == 9 && Squares[0, 2] == 0)
        {
            Squares[0, 2] = player;
            squaresTaken++;
            return true;
        }
        return false;
    }
}*/
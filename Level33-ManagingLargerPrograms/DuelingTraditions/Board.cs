namespace TicTacToe
{
    internal class Board
    {
        public SquareState[,] Squares { get; private set; } = new SquareState[3, 3];
        public bool TryPlaySquare(int col, int row, Player player)
        {
            if (Squares[col, row] != SquareState.None)
                return false;

            Squares[col, row] = player.Name;
            return true;
        }

        public SquareState WhoWon()
        {
            for (int i = 0; i < Squares.GetLength(0); i++)
            {
                if (Squares[i, 0] != SquareState.None &&
                    Squares[i, 0] == Squares[i, 1] &&
                    Squares[i, 0] == Squares[i, 2])
                    return Squares[i, 0];

                if (Squares[0, i] != SquareState.None &&
                    Squares[0, i] == Squares[1, i] &&
                    Squares[0, i] == Squares[2, i])
                    return Squares[0, i];
            }

            if (Squares[0, 0] != SquareState.None &&
                Squares[0, 0] == Squares[1, 1] &&
                Squares[0, 0] == Squares[2, 2])
                return Squares[0, 0];

            if (Squares[0, 2] != SquareState.None &&
                Squares[0, 2] == Squares[1, 1] &&
                Squares[0, 2] == Squares[2, 0])
                return Squares[0, 2];

            return SquareState.None;
        }

        public void Reset() => Squares = new SquareState[3, 3];
    }
}
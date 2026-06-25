namespace TicTacToe
{
    internal class Player
    {
        public SquareState Name { get; }
        public int RoundsWon { get; set; }
        public Player(SquareState name) => Name = name;
    }
}
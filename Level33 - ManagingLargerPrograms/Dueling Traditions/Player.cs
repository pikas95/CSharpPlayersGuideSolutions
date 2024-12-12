namespace FountainOfObjectsGame
{
    public class Player
    {
        public int Row { get; private set; }
        public int Column { get; private set; }
        public int Arrows { get; private set; } = 5;
        public override string ToString() { return $"You are in the room at (Row={Row}, Column={Column}) and have {Arrows} arrows."; }
        public void ChangeCoordinates(int x, int y) { Row = x; Column = y; }
        public void ArrowDecrement() => Arrows--;
    }
}
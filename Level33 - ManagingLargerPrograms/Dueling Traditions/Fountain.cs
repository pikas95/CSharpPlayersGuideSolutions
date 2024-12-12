namespace FountainOfObjectsGame
{
    public class Fountain : WorldEntity
    {
        public bool IsActivated { get; private set; }
        public Fountain() : base(RoomType.FountainOfObjects) { }
        public override bool Event(Player player, Map map)
        {
            if (map.RoomIs(RoomType, player.Row, player.Column) && IsActivated == false)
            {
                IsActivated = true;
                return true;
            }
            return false;
        }
    }
}
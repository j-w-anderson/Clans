namespace Engine
{
    public class SelectEventCard : UIMode
    {
        public Player CurrentPlayer { get; set; }

        public SelectEventCard(GameSession game, Player player) : base(game)
        {
            CurrentPlayer = player;
        }


    }
}
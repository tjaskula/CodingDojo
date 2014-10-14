namespace TennisKata
{
    public class GameWithState
    {
        private GameState _state;

        public GameWithState()
        {
            _state = new InitialEqState(0, 0);
        }

        public void PointTo(Player player)
        {
            _state.PointTo(player);
            _state = _state.GetNextState();
        }

        public string Score
        {
            get
            {
                return _state.Score();
            }
        }
    }
}
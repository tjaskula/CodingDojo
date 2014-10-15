namespace TennisKata
{
    public class Game
    {

        public readonly IScore Player1Score;
        public readonly IScore Player2Score;

        public Game()
        {
            this.Player1Score = new ZeroPoint();
            this.Player2Score = new ZeroPoint();
        }

        private Game(IScore newPlayer1Score, IScore newPlayer2Score)
        {
            this.Player1Score = newPlayer1Score;
            this.Player2Score = newPlayer2Score;
        }

        public Game Player1WinsPoint()
        {
            var newPlayer1Score = this.Player2Score.Accept(this.Player1Score);
            var newPlayer2Score = this.Player2Score.LoseBall();

            return new Game(newPlayer1Score, newPlayer2Score);
        }

        public Game Player2WinsPoint()
        {
            var newPlayer2Score = this.Player1Score.Accept(this.Player2Score);
            var newPlayer1Score = this.Player1Score.LoseBall();

            return new Game(newPlayer1Score, newPlayer2Score);
        }
    }
}
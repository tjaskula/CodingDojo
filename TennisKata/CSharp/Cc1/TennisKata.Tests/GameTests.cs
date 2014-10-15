namespace TennisKata.Tests
{
    using Xunit;

    public class GameTests
    {
        [Fact]
        public void ShouldWinPlayer1CC1()
        {
            var game = new Game()
                            .Player1WinsPoint()
                            .Player1WinsPoint()
                            .Player1WinsPoint()
                            .Player2WinsPoint()
                            .Player2WinsPoint()
                            .Player2WinsPoint()
                            .Player2WinsPoint()
                            .Player1WinsPoint()
                            .Player1WinsPoint()
                            .Player1WinsPoint();

            Assert.Equal(new GamePoint().ToString(), game.Player1Score.ToString());
            Assert.Equal(new FortyPoints().ToString(), game.Player2Score.ToString());
        }
    }
}
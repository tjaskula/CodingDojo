namespace TennisKata.Tests
{
    using Xunit;

    public class GameTests
    {
        [Fact]
        public void ShouldDisplayInitialGameState()
        {
            var game = new Game();
            Assert.Equal("LoveAll", game.Score);
        }

        [Fact]
        public void ShouldScorePlayerOne()
        {
            var game = new Game();
            game.PointTo(Player.One);
            Assert.Equal("FifteenLove", game.Score);
        }

        [Fact]
        public void ShouldBeEqualState()
        {
            var game = new Game();
            game.PointTo(Player.One);
            game.PointTo(Player.Two);
            Assert.Equal("FifteenAll", game.Score);
        }

        [Fact]
        public void ShouldScorePlayerTwo()
        {
            var game = new Game();
            game.PointTo(Player.Two);
            Assert.Equal("LoveFifteen", game.Score);
        }

        [Fact]
        public void ShouldScorePlayerTwoTwice()
        {
            var game = new Game();
            game.PointTo(Player.Two);
            game.PointTo(Player.Two);
            Assert.Equal("LoveThirty", game.Score);
        }

        [Fact]
        public void ShouldScorePlayerOne3Times()
        {
            var game = new Game();
            game.PointTo(Player.One);
            game.PointTo(Player.One);
            game.PointTo(Player.One);
            Assert.Equal("FortyLove", game.Score);
        }

        [Fact]
        public void ShouldScoreAdvantagePlayerOne()
        {
            var game = new Game();
            game.PointTo(Player.One);
            game.PointTo(Player.Two);
            game.PointTo(Player.Two);
            game.PointTo(Player.Two);
            game.PointTo(Player.One);
            game.PointTo(Player.One);
            game.PointTo(Player.One);
            Assert.Equal("AdvantagePlayerOne", game.Score);
        }

        [Fact]
        public void ShouldScoreDeuceAfterAdvantage()
        {
            var game = new Game();
            game.PointTo(Player.One);
            game.PointTo(Player.Two);
            game.PointTo(Player.Two);
            game.PointTo(Player.Two);
            game.PointTo(Player.One);
            game.PointTo(Player.One);
            game.PointTo(Player.One);
            game.PointTo(Player.Two);
            Assert.Equal("Deuce", game.Score);
        }

        [Fact]
        public void ShouldWinPlayer1()
        {
            var game = new Game();
            game.PointTo(Player.One);
            game.PointTo(Player.Two);
            game.PointTo(Player.Two);
            game.PointTo(Player.Two);
            game.PointTo(Player.One);
            game.PointTo(Player.One);
            game.PointTo(Player.One);
            game.PointTo(Player.Two);
            game.PointTo(Player.One);
            game.PointTo(Player.One);
            Assert.Equal("GamePlayerOne", game.Score);
        }
    }
}
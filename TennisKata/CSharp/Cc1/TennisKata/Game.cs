namespace TennisKata
{
    using System;

    public class Game
    {
        private int _scorePlayer1;
        private int _scorePlayer2;

        public void PointTo(Player player)
        {
            if (player == Player.One) _scorePlayer1 ++;
            else _scorePlayer2++;
        }

        public string Score
        {
            get
            {
                if (_scorePlayer1 == _scorePlayer2 &&
                    _scorePlayer1 >= 3)
                    return "Deuce";
                if (_scorePlayer1 > 3 &&
                    _scorePlayer1 == _scorePlayer2 + 1)
                    return "AdvantagePlayerOne";
                if (_scorePlayer2 > 3 &&
                    _scorePlayer2 == _scorePlayer1 + 1)
                    return "AdvantagePlayerTwo";
                if (_scorePlayer1 > 3 &&
                    _scorePlayer1 >= _scorePlayer2 + 2)
                    return "GamePlayerOne";
                if (_scorePlayer2 > 3)
                    return "GamePlayerTwo";
                var score1Word = ToWord(_scorePlayer1);
                var score2Word = ToWord(_scorePlayer2);
                if (score1Word == score2Word)
                    return score1Word + "All";
                return score1Word + score2Word;
            }
        }

        private string ToWord(int score)
        {
            switch (score)
            {
                case 0:
                    return "Love";
                case 1:
                    return "Fifteen";
                case 2:
                    return "Thirty";
                case 3:
                    return "Forty";
                default:
                    throw new ArgumentException(
                        "Unexpected score value.",
                        "score");
            }
        }
    }
}
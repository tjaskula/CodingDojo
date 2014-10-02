namespace TennisKata
{
    using System;

    public class Game
    {
        private int _scorePlayer1;
        private int _scorePlayer2;

        public void PointTo(Player player)
        {
            if (player == Player.One) this._scorePlayer1 ++;
            else this._scorePlayer2++;
        }

        public string Score
        {
            get
            {
                if (this._scorePlayer1 == this._scorePlayer2 &&
                    this._scorePlayer1 >= 3)
                    return "Deuce";
                if (this._scorePlayer1 > 3 &&
                    this._scorePlayer1 == this._scorePlayer2 + 1)
                    return "AdvantagePlayerOne";
                if (this._scorePlayer2 > 3 &&
                    this._scorePlayer2 == this._scorePlayer1 + 1)
                    return "AdvantagePlayerTwo";
                if (this._scorePlayer1 > 3 &&
                    this._scorePlayer1 >= this._scorePlayer2 + 2)
                    return "GamePlayerOne";
                if (this._scorePlayer2 > 3)
                    return "GamePlayerTwo";
                var score1Word = this.ToWord(this._scorePlayer1);
                var score2Word = this.ToWord(this._scorePlayer2);
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
using System;

namespace TennisKata
{
    public class FifteenPoints : IScore
    {
        public IScore Accept(IScore oponnentScore)
        {
            return oponnentScore.WinBall(this);
        }

        public IScore WinBall(IScore opponentScore)
        {
            return new ThirtyPoints();
        }

        public IScore WinBall(AdvantagePoint opponentScore)
        {
            throw new Exception("Impossible case");
        }

        public IScore WinBall(FortyPoints opponentScore)
        {
            return new ThirtyPoints();
        }

        public IScore LoseBall()
        {
            return this;
        }

        public override string ToString()
        {
            return "FifteenPoints";
        }
    }
}
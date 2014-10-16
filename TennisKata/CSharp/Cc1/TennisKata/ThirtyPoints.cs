using System;

namespace TennisKata
{
    public class ThirtyPoints : IScore
    {
        public IScore Accept(IScore oponnentScore)
        {
            return oponnentScore.WinBall(this);
        }

        public IScore WinBall(IScore opponentScore)
        {
            return new FortyPoints();
        }

        public IScore WinBall(AdvantagePoint opponentScore)
        {
            throw new Exception("Impossible case");
        }

        public IScore WinBall(FortyPoints opponentScore)
        {
            return new FortyPoints();
        }

        public IScore LoseBall()
        {
            return this;
        }

        public override string ToString()
        {
            return "ThirtyPoints";
        }
    }
}
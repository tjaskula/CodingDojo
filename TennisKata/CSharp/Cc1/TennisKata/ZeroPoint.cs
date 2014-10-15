using System;

namespace TennisKata
{
    public interface IScore
    {
        IScore Accept(IScore oponnentScore);
        IScore WinBall(IScore opponentScore);
        IScore WinBall(AdvantagePoint opponentScore);
        IScore WinBall(FortyPoints opponentScore);
        IScore LoseBall();
    }

    public class ZeroPoint : IScore
    {
        public IScore Accept(IScore oponnentScore)
        {
            return oponnentScore.WinBall(this);
        }

        public IScore WinBall(IScore opponentScore)
        {
            return new FifteenPoints();
        }

        public IScore WinBall(AdvantagePoint opponentScore)
        {
            throw new Exception("Impossible case");
        }

        public IScore WinBall(FortyPoints opponentScore)
        {
            return new FifteenPoints();
        }

        public IScore LoseBall()
        {
            return this;
        }

        public override string ToString()
        {
            return "ZeroPoint";
        }
    }

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

    public class FortyPoints : IScore
    {
        public IScore Accept(IScore oponnentScore)
        {
            return oponnentScore.WinBall(this);
        }

        public IScore WinBall(IScore opponentScore)
        {
            return new GamePoint();
        }

        public IScore WinBall(AdvantagePoint opponentScore)
        {
            return this;
        }

        public IScore WinBall(FortyPoints opponentScore)
        {
            return new AdvantagePoint();
        }

        public IScore LoseBall()
        {
            return this;
        }

        public override string ToString()
        {
            return "FortyPoints";
        }
    }

    public class AdvantagePoint : IScore
    {
        public IScore Accept(IScore oponnentScore)
        {
            return oponnentScore.WinBall(this);
        }

        public IScore WinBall(IScore opponentScore)
        {
            return new GamePoint();
        }

        public IScore WinBall(AdvantagePoint opponentScore)
        {
            return new FortyPoints();
        }

        public IScore WinBall(FortyPoints opponentScore)
        {
            return new GamePoint();
        }

        public IScore LoseBall()
        {
            return new FortyPoints();
        }

        public override string ToString()
        {
            return "AdvantagePoint";
        }
    }

    public class GamePoint : IScore
    {
        public IScore Accept(IScore oponnentScore)
        {
            return oponnentScore.WinBall(this);
        }

        public IScore WinBall(IScore opponentScore)
        {
            return this;
        }

        public IScore WinBall(AdvantagePoint opponentScore)
        {
            return opponentScore;
        }

        public IScore WinBall(FortyPoints opponentScore)
        {
            return opponentScore;
        }

        public IScore LoseBall()
        {
            return this;
        }

        public override string ToString()
        {
            return "GamePoint";
        }
    }
}
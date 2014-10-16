namespace TennisKata
{
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
}
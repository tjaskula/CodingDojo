namespace TennisKata
{
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
}
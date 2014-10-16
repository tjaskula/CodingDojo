namespace TennisKata
{
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
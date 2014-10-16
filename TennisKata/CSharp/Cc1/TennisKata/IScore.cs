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
}
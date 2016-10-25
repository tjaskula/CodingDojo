namespace TennisKata
{
    using System;

    internal abstract class GameState
    {
        internal abstract int ScorePlayer1 { get; }
        internal abstract int ScorePlayer2 { get; }

        internal abstract void PointTo(Player player);
        internal abstract GameState GetNextState();
        internal abstract string Score();
    }

    
    // implement different states here 
}
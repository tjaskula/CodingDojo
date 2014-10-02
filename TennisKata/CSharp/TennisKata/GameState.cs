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

    internal class InitialIneqState : GameState
    {
        private GameState _nextState;

        private int _scorePlayer1;
        private int _scorePlayer2;

        internal InitialIneqState(int scorePlayer1, int scorePlayer2)
        {
            _scorePlayer1 = scorePlayer1;
            _scorePlayer2 = scorePlayer2;
        }

        internal override int ScorePlayer1
        {
            get
            {
                return _scorePlayer1;
            }
        }

        internal override int ScorePlayer2
        {
            get
            {
                return _scorePlayer2;
            }
        }

        internal override void PointTo(Player player)
        {
            if (player == Player.One) _scorePlayer1++;
            else _scorePlayer2++;

            TransitionState();
        }

        internal override GameState GetNextState()
        {
            return _nextState;
        }

        internal override string Score()
        {
            return this.ToWord(_scorePlayer1) + this.ToWord(_scorePlayer2);
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

        private void TransitionState()
        {
            if (ScorePlayer1 <= 3 && ScorePlayer2 <= 3 && ScorePlayer1 != ScorePlayer2) 
                _nextState = this;
            else if (ScorePlayer1 < 3 && ScorePlayer2 < 3 && ScorePlayer1 == ScorePlayer2)
                _nextState = new InitialEqState(ScorePlayer1, ScorePlayer2);
            else
                _nextState = new DeuceState(ScorePlayer1, ScorePlayer2);
        }
    }

    internal class DeuceState : GameState
    {
        private GameState _nextState;

        private int _scorePlayer1;
        private int _scorePlayer2;

        internal DeuceState(int scorePlayer1, int scorePlayer2)
        {
            _scorePlayer1 = scorePlayer1;
            _scorePlayer2 = scorePlayer2;
        }

        internal override int ScorePlayer1
        {
            get
            {
                return _scorePlayer1;
            }
        }

        internal override int ScorePlayer2
        {
            get
            {
                return _scorePlayer2;
            }
        }

        internal override void PointTo(Player player)
        {
            if (player == Player.One) _scorePlayer1++;
            else _scorePlayer2++;

            TransitionState();
        }

        internal override GameState GetNextState()
        {
            return _nextState;
        }

        internal override string Score()
        {
            return "Deuce";
        }

        private void TransitionState()
        {
            if (ScorePlayer1 > 3 && ScorePlayer1 == ScorePlayer2 + 1) 
                _nextState = new AdvantagePlayerOneState(ScorePlayer1, ScorePlayer2);
            else if (ScorePlayer1 < 3 && ScorePlayer2 < 3 && ScorePlayer1 == ScorePlayer2) 
                _nextState = this;
            else
                _nextState = new AdvantagePlayerTwoState(ScorePlayer1, ScorePlayer2);
        }
    }

    internal class AdvantagePlayerOneState : GameState
    {
        private GameState _nextState;

        private int _scorePlayer1;
        private int _scorePlayer2;

        internal AdvantagePlayerOneState(int scorePlayer1, int scorePlayer2)
        {
            _scorePlayer1 = scorePlayer1;
            _scorePlayer2 = scorePlayer2;
        }

        internal override int ScorePlayer1
        {
            get
            {
                return _scorePlayer1;
            }
        }

        internal override int ScorePlayer2
        {
            get
            {
                return _scorePlayer2;
            }
        }

        internal override void PointTo(Player player)
        {
            if (player == Player.One) _scorePlayer1++;
            else _scorePlayer2++;

            TransitionState();
        }

        internal override GameState GetNextState()
        {
            return _nextState;
        }

        internal override string Score()
        {
            return "AdvantagePlayerOne";
        }

        private void TransitionState()
        {
            if (ScorePlayer1 > 3 && ScorePlayer1 == ScorePlayer2 + 2) 
                _nextState = new GamePlayerOne(ScorePlayer1, ScorePlayer2);
            else
                _nextState = new DeuceState(ScorePlayer1, ScorePlayer2);
        }
    }

    internal class AdvantagePlayerTwoState : GameState
    {
        private GameState _nextState;

        private int _scorePlayer1;
        private int _scorePlayer2;

        internal AdvantagePlayerTwoState(int scorePlayer1, int scorePlayer2)
        {
            _scorePlayer1 = scorePlayer1;
            _scorePlayer2 = scorePlayer2;
        }

        internal override int ScorePlayer1
        {
            get
            {
                return _scorePlayer1;
            }
        }

        internal override int ScorePlayer2
        {
            get
            {
                return _scorePlayer2;
            }
        }

        internal override void PointTo(Player player)
        {
            if (player == Player.One) _scorePlayer1++;
            else _scorePlayer2++;

            TransitionState();
        }

        internal override GameState GetNextState()
        {
            return _nextState;
        }

        internal override string Score()
        {
            return "AdvantagePlayerTwo";
        }

        private void TransitionState()
        {
            if (ScorePlayer2 > 3 && ScorePlayer2 == ScorePlayer1 + 2)
                _nextState = new GamePlayerTwo(ScorePlayer1, ScorePlayer2);
            else
                _nextState = new DeuceState(ScorePlayer1, ScorePlayer2);
        }
    }

    internal class GamePlayerOne : GameState
    {
        private readonly int _scorePlayer1;
        private readonly int _scorePlayer2;

        internal GamePlayerOne(int scorePlayer1, int scorePlayer2)
        {
            _scorePlayer1 = scorePlayer1;
            _scorePlayer2 = scorePlayer2;
        }

        internal override int ScorePlayer1
        {
            get
            {
                return _scorePlayer1;
            }
        }

        internal override int ScorePlayer2
        {
            get
            {
                return _scorePlayer2;
            }
        }

        internal override void PointTo(Player player)
        {
        }

        internal override GameState GetNextState()
        {
            return this;
        }

        internal override string Score()
        {
            return "GamePlayerOne";
        }
    }

    internal class GamePlayerTwo : GameState
    {
        private readonly int _scorePlayer1;
        private readonly int _scorePlayer2;

        internal GamePlayerTwo(int scorePlayer1, int scorePlayer2)
        {
            _scorePlayer1 = scorePlayer1;
            _scorePlayer2 = scorePlayer2;
        }

        internal override int ScorePlayer1
        {
            get
            {
                return _scorePlayer1;
            }
        }

        internal override int ScorePlayer2
        {
            get
            {
                return _scorePlayer2;
            }
        }

        internal override void PointTo(Player player)
        {
        }

        internal override GameState GetNextState()
        {
            return this;
        }

        internal override string Score()
        {
            return "GamePlayerTwo";
        }
    }

    internal class InitialEqState : GameState
    {
        private GameState _nextState;

        private int _scorePlayer1;
        private int _scorePlayer2;

        internal InitialEqState(int scorePlayer1, int scorePlayer2)
        {
            _scorePlayer1 = scorePlayer1;
            _scorePlayer2 = scorePlayer2;
        }

        internal override int ScorePlayer1
        {
            get
            {
                return _scorePlayer1;
            }
        }

        internal override int ScorePlayer2
        {
            get
            {
                return _scorePlayer2;
            }
        }

        internal override void PointTo(Player player)
        {
            if (player == Player.One) _scorePlayer1++;
            else _scorePlayer2++;

            TransitionState();
        }

        internal override GameState GetNextState()
        {
            return _nextState;
        }

        internal override string Score()
        {
            return this.ToWord(_scorePlayer1) + "All";
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

        private void TransitionState()
        {
            _nextState = new InitialIneqState(ScorePlayer1, ScorePlayer2);
        }
    }
}
using System;

namespace Minesweeper.MVP
{
    public interface IGame
    {
        bool Lost { get; }
        bool Won { get; }
        bool GameOver { get; }
        IBoard Board { get; }
        
        event Action<IGame> OnGameOver;
        event Action<IGame> OnLost;
        event Action<IGame> OnWon;

        void Reset();
    }
}
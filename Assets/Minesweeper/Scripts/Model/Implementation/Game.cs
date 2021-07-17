using System;

namespace Minesweeper.MVP.Implementation
{
    public class Game : IGame
    {
        private int _discoveredCells;
        
        public bool Lost { get; private set; }
        public bool Won { get; private set; }
        public bool GameOver => Lost || Won;

        public IBoard Board { get; }

        public event Action<IGame> OnLost;
        public event Action<IGame> OnWon;
        
        public event Action<IGame> OnGameOver
        {
            add
            {
                OnLost += value;
                OnWon += value;
            }
            
            remove
            {
                OnLost -= value;
                OnWon -= value;
            }
        }
        
        public Game(IBoard board)
        {
            Board = board;
            Board.OnDiscovered += OnDiscoveredChange;
        }

        public void Reset()
        {
            _discoveredCells = 0;
            Lost = false;
            Won = false;
            Board.Reset();
        }
        
        private void OnDiscoveredChange(IBoard changedBoard, IBoardCell cell)
        {
            if (GameOver || !cell.Discovered) return;
            
            if (cell.HasBomb)
            {
                Lost = true;
                OnLost?.Invoke(this);
                return;
            }

            _discoveredCells++;
            if (_discoveredCells >= Board.NonBombCellsAmount)
            {
                Won = true;
                OnWon?.Invoke(this);
            }
        }
    }
}
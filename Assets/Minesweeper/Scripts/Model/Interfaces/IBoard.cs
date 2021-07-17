using System;
using System.Collections.Generic;

namespace Minesweeper.MVP
{
    public interface IBoard
    {
        int SizeX { get; }
        int SizeY { get; }
        int NonBombCellsAmount { get; }
        IReadOnlyList<IBoardCell> BombCells { get; }
        event Action<IBoard, IBoardCell> OnDiscovered;
        
        IBoardCell this[int x, int y] { get; }
        bool ValidCell(int x, int y);
        void Reset();
    }
}
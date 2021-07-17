using System;
using System.Collections.Generic;

namespace Minesweeper.MVP
{
    public interface IBoardCell
    {
        int NeighboursBombs { get; }
        IReadOnlyList<IBoardCell> Neighbours { get; }
        bool Discovered { get; set; }
        bool Flagged { get; }
        bool HasBomb { get; }
        
        event Action<IBoardCell> OnChanged;
        
        void Discover();
        void SwapFlagged();
    }
}
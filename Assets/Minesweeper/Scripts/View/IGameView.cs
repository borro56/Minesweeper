using System;

namespace Minesweeper.MVP
{
    public interface IGameView
    {
        event Action<BoardCellView, CellInputButton> CellClicked;
        event Action ResetClicked;

        void SetData(IBoard board);
        void FinishGame(bool won);
        void Reset();
    }
}
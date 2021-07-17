using UnityEngine;

namespace Minesweeper.MVP
{
    public abstract class BaseBoardFactory : ScriptableObject
    {
        public abstract IBoard CreateBoard(int sizeX, int sizeY, int initialBombCount = 0, int bombSeed = -1);
    }
}
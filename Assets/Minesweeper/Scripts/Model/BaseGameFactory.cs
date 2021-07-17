using UnityEngine;

namespace Minesweeper.MVP
{
    public abstract class BaseGameFactory : ScriptableObject
    {
        public abstract IGame CreateGame(IBoard board);
    }
}
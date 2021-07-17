using UnityEngine;

namespace Minesweeper.MVP.Implementation
{
    [CreateAssetMenu(menuName = "Game/Game Factory")]
    public class GameFactory : BaseGameFactory
    {
        public override IGame CreateGame(IBoard board) => new Game(board);
    }
}
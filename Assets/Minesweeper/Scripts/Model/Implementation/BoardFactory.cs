using UnityEngine;

namespace Minesweeper.MVP.Implementation
{
    [CreateAssetMenu(menuName = "Game/Board Factory")]
    public class BoardFactory : BaseBoardFactory
    {
        public override IBoard CreateBoard(int sizeX, int sizeY, int initialBombCount = 0, int bombSeed = -1) =>
            new Board(sizeX, sizeY, initialBombCount, bombSeed);
    }
}
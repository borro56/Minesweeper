using UnityEngine;

namespace Minesweeper.MVP
{
    public class BoardModel : MonoBehaviour
    {
        private IBoard _board;

        [SerializeField] private int _sizeX;
        [SerializeField] private int _sizeY;
        [SerializeField] private int _bombsCount;
        [SerializeField] private int _bombsSeed = -1;
        [SerializeField] private BaseBoardFactory _boardFactory;

        public IBoard Board => _board ?? (_board = _boardFactory.CreateBoard(_sizeX, _sizeY, _bombsCount, _bombsSeed));
    }
}
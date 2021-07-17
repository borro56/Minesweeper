using UnityEngine;

namespace Minesweeper.MVP
{
    public class GameModel : MonoBehaviour, IGameModel
    {
        private IGame _game;

        [SerializeField] private BaseGameFactory _gameFactory;
        [SerializeField] private BoardModel _boardModel;

        public IGame Game => _game ?? (_game = _gameFactory.CreateGame(_boardModel.Board));
    }
}
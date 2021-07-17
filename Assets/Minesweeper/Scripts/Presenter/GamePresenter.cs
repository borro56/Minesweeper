using UnityEngine;

namespace Minesweeper.MVP
{
    public class GamePresenter : MonoBehaviour
    {
        private IGameView _view;
        private IGameModel _model;

        [SerializeField] private GameModel _gameModel;
        [SerializeField] private GameView _gameView;

        private IBoard Board => Model.Game.Board;
        private IGame Game => Model.Game;

        public IGameView View
        {
            get => _view;
            set => _view = value;
        }
        public IGameModel Model
        {
            get => _model;
            set => _model = value;
        }

        private void OnEnable()
        {
            Game.OnLost += OnLost;
            Game.OnWon += OnWon;
            View.ResetClicked += OnResetClicked;
            View.CellClicked += OnCellClicked;
        }

        private void Awake()
        {
            View = View ?? _gameView;
            Model = Model ?? _gameModel;
            View.SetData(Board);
        }

        private void OnCellClicked(BoardCellView cellView, CellInputButton button)
        {
            if (!Game.GameOver)
            {
                if (button == CellInputButton.Left)
                    cellView.Cell.Discover();

                if (button == CellInputButton.Right)
                    cellView.Cell.SwapFlagged();
            }
        }

        private void OnResetClicked()
        {
            Game.Reset();
        }

        private void OnWon(IGame _)
        {
            View.FinishGame(true);
            ShowBombs();
        }

        private void OnLost(IGame _)
        {
            View.FinishGame(false);
            ShowBombs();
        }

        private void ShowBombs()
        {
            for (var i = 0; i < Board.BombCells.Count; i++)
                Board.BombCells[i].Discovered = true;
        }

        private void OnDisable()
        {
            Game.OnLost -= OnLost;
            Game.OnWon -= OnWon;
            View.ResetClicked -= OnResetClicked;
            View.CellClicked -= OnCellClicked;
        }
    }
}
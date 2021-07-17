using System;
using UnityEngine;
using UnityEngine.UI;

namespace Minesweeper.MVP
{
    public class GameView : MonoBehaviour, IGameView
    {
        [Header("General Settings")] 
        [SerializeField] private BoardCellView _cellViewPrefab;
        [SerializeField] private float _cellsMargin;

        [Header("View References")]
        [SerializeField] private GameObject _winLabel;
        [SerializeField] private GameObject _lostLabel;
        [SerializeField] private GameObject _resetPanel;
        [SerializeField] private RectTransform _cellsContainer;
        [SerializeField] private Button _resetButton;

        public event Action<BoardCellView, CellInputButton> CellClicked;
        public event Action ResetClicked;

        private void OnEnable()
        {
            _resetButton.onClick.AddListener(Reset);
        }

        public void SetData(IBoard board)
        {
            _resetPanel.SetActive(false);

            //Calculate board and cell sizes
            var cellRectTransform = _cellViewPrefab.GetComponent<RectTransform>();
            var cellOffsetX = cellRectTransform.sizeDelta.x + _cellsMargin;
            var cellOffsetY = cellRectTransform.sizeDelta.y + _cellsMargin;
            var boardWidth = (board.SizeX - 1) * cellOffsetX;
            var boardHeight = (board.SizeY - 1) * cellOffsetY;
            
            //Adjust board size and position
            _cellsContainer.localPosition = new Vector3(-boardWidth / 2, -boardHeight / 2);
            _cellsContainer.sizeDelta = new Vector2(boardWidth, boardHeight);

            //Spawn board cell views
            for (var x = 0; x < board.SizeX; x++)
            {
                for (var y = 0; y < board.SizeY; y++)
                {
                    var cellPosition = new Vector3(x * cellOffsetX, y * cellOffsetY);
                    var cellView = Instantiate(_cellViewPrefab, _cellsContainer);
                    cellView.transform.localPosition = cellPosition;
                    cellView.Cell = board[x, y];
                    cellView.OnClicked += OnCellClicked;
                }
            }
        }

        private void OnCellClicked(BoardCellView cellView, CellInputButton button)
        {
            CellClicked?.Invoke(cellView, button);
        }

        public void FinishGame(bool won)
        {
            _winLabel.SetActive(won);
            _lostLabel.SetActive(!won);
            _resetPanel.SetActive(true);
        }

        public void Reset()
        {
            _resetPanel.SetActive(false);
            ResetClicked?.Invoke();
        }

        private void OnDisable()
        {
            _resetButton.onClick.RemoveListener(Reset);
        }
    }
}
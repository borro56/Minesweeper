using System;
using Minesweeper.MVP;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Minesweeper.MVP
{
    public enum CellInputButton { Left, Right, Middle }

    [RequireComponent(typeof(Button))]
    public class BoardCellView : MonoBehaviour, IPointerClickHandler
    {
        private IBoardCell _cell;

        [SerializeField] private GameObject _flagIcon;
        [SerializeField] private GameObject _bombIcon;
        [SerializeField] private Text _bombsCountLabel;
        [SerializeField] private Button _button;

        public event Action<BoardCellView, CellInputButton> OnClicked;

        public IBoardCell Cell
        {
            get => _cell;
            set
            {
                if (_cell == value)
                {
                    return;
                }

                if (_cell != null)
                {
                    _cell.OnChanged -= Refresh;
                }

                _cell = value;

                if (_cell != null)
                {
                    _cell.OnChanged += Refresh;
                    Refresh(_cell);
                }
            }
        }

        private void Refresh(IBoardCell refreshedCell)
        {
            _bombIcon.SetActive(refreshedCell.Discovered && refreshedCell.HasBomb);
            _flagIcon.SetActive(refreshedCell.Flagged);
            _bombsCountLabel.gameObject.SetActive(refreshedCell.Discovered && refreshedCell.NeighboursBombs > 0);
            _bombsCountLabel.text = refreshedCell.NeighboursBombs.ToString();
            _button.interactable = !refreshedCell.Discovered;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (_cell.Discovered) return;
            var inputButton = (CellInputButton) eventData.button;
            OnClicked?.Invoke(this, inputButton);
        }
    }
}
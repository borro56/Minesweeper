using System;
using System.Collections.Generic;

namespace Minesweeper.MVP.Implementation
{
    public class BoardCell : IBoardCell
    {
        private List<BoardCell> _neighbours;
        private bool _flagged;
        private bool _hasBomb;
        private bool _discovered;
        private int _neighboursBombs;

        public IBoard Board { get; }
        public int X { get; }
        public int Y { get; }
        
        public int NeighboursBombs => _neighboursBombs;
        public IReadOnlyList<IBoardCell> Neighbours => _neighbours ?? GetNeighbours();

        public event Action<IBoardCell> OnChanged;
        public event Action<IBoardCell> OnFlaggedChanged;
        public event Action<IBoardCell> OnDiscoveredChanged;

        public bool Discovered
        {
            get => _discovered;
            set
            {
                if (_discovered == value) return;
                
                _discovered = value;
                _flagged = false;
                OnDiscoveredChanged?.Invoke(this);
                OnChanged?.Invoke(this);
            }
        }
        
        public bool Flagged
        {
            get => _flagged;
            set
            {
                if (Discovered)
                    throw new Exception("[BoardCell] Cannot flag an already discovered cell");

                if(_flagged == value) return;
                
                _flagged = value;
                OnFlaggedChanged?.Invoke(this);
                OnChanged?.Invoke(this);
            }
        }

        public bool HasBomb
        {
            get => _hasBomb;
            set
            {
                if(_hasBomb == value) return;
                _hasBomb = value;

                for (var i = 0; i < Neighbours.Count; i++)
                    _neighbours[i]._neighboursBombs += _hasBomb ? 1 : -1;
            }
        }

        public BoardCell(IBoard board, int x, int y, bool hasBomb = false)
        {
            Board = board;
            X = x;
            Y = y;
            HasBomb = hasBomb;
        }

        bool TryGetNeighbour(int offsetX, int offsetY, out BoardCell cell)
        {
            cell = null;
            var neighbourX = X + offsetX;
            var neighbourY = Y + offsetY;

            if (!Board.ValidCell(neighbourX, neighbourY))
                return false;
                
            cell = Board[neighbourX, neighbourY] as BoardCell;

            if (cell == null)
                throw new Exception("[BoardCell] Expected BoardCell interface implementation in IBoard");
            
            return true;
        }

        public void Discover()
        {
            if(Discovered)
                throw new Exception("[BoardCell] Cannot discover an already discovered cell");

            Discovered = true;

            if (!HasBomb)
                PropagateDiscover();
        }

        public void SwapFlagged()
        {
            Flagged = !Flagged;
        }

        internal void Reset()
        {
            if (_discovered)
            {
                _discovered = false;   
                OnDiscoveredChanged?.Invoke(this);
            }

            if (_flagged)
            {
                _flagged = false;
                OnFlaggedChanged?.Invoke(this);   
            }
            
            _hasBomb = false;
            _neighboursBombs = 0;
            OnChanged?.Invoke(this);
        }

        private List<BoardCell> GetNeighbours()
        {
            _neighbours = new List<BoardCell>(8);
            for (var offsetX = -1; offsetX <= 1; offsetX++)
            {
                for (var offsetY = -1; offsetY <= 1; offsetY++)
                {
                    if (offsetX == 0 && offsetY == 0) continue;
                    if (TryGetNeighbour(offsetX, offsetY, out var cell))
                    {
                        _neighbours.Add(cell);
                    }
                }
            }
            
            return _neighbours;
        }

        private void PropagateDiscover()
        {
            var visited = new HashSet<IBoardCell> {this};
            var openSet = new List<IBoardCell> {this};
            
            for (var i = 0; i < openSet.Count; i++)
            {
                var open = openSet[i];

                for (var j = 0; j < open.Neighbours.Count; j++)
                {
                    var openNeighbour = open.Neighbours[j];
                    if (visited.Contains(openNeighbour) || openNeighbour.HasBomb) 
                        continue;

                    openNeighbour.Discovered = true;
                    visited.Add(openNeighbour);

                    if (openNeighbour.NeighboursBombs == 0)
                        openSet.Add(openNeighbour);
                }
            }
        }
    }
}
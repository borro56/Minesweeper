using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace Minesweeper.MVP.Implementation
{
    public class Board : IBoard
    {
        private const int PlacementTryCount = 3;

        private readonly List<IBoardCell> _bombCells = new List<IBoardCell>();
        private BoardCell[,] _cells;
        
        public int BombsAmount { get; private set; }
        
        public int SizeX => _cells.GetLength(0);
        public int SizeY => _cells.GetLength(1);
        public int CellsAmount => SizeX * SizeY;
        public int NonBombCellsAmount => CellsAmount - BombsAmount;
        public IReadOnlyList<IBoardCell> BombCells => _bombCells;

        public event Action<IBoard, IBoardCell> OnDiscovered;

        public IBoardCell this[int x, int y]
        {
            get
            {
                if (!ValidCell(x, y))
                    throw new Exception($"[Board] Invalid cell index ({x},{y})");

                return _cells[x, y];
            }
        }

        public Board(int sizeX, int sizeY, int initialBombCount = 0, int bombSeed = -1)
        {
            InitializeBoard(sizeX, sizeY);
            InitializeBombs(initialBombCount, bombSeed);
        }

        public bool ValidCell(int x, int y) => x >= 0 && y >= 0 && x < SizeX && y < SizeY;

        public void Reset()
        {
            Reset(BombsAmount);
        }
        
        public void Reset(int bombsAmount, int bombsSeed = -1)
        {
            for (var x = 0; x < SizeX; x++)
                for (var y = 0; y < SizeY; y++)
                    _cells[x, y].Reset();
            
            _bombCells.Clear();
            InitializeBombs(bombsAmount, bombsSeed);
        }
        
        public bool TryPlaceBomb(int bombX, int bombY)
        {
            var cell = _cells[bombX, bombY]; 
            
            if (cell.HasBomb)
                return false;

            BombsAmount++;
            cell.HasBomb = true;
            _bombCells.Add(cell);
            return true;
        }

        private void InitializeBoard(int sizeX, int sizeY)
        {
            _cells = new BoardCell[sizeX, sizeY];
            for (var x = 0; x < sizeX; x++)
            {
                for (var y = 0; y < sizeY; y++)
                {
                    var cell = new BoardCell(this, x, y);
                    cell.OnDiscoveredChanged += OnDiscoveredChange;
                    _cells[x, y] = cell;
                }    
            }
        }

        private void InitializeBombs(int bombsAmount, int bombSeed = -1)
        {
            BombsAmount = 0;
            
            if (bombSeed >= 0)
                Random.InitState(bombSeed);

            for (var i = 0; i < bombsAmount; i++)
            {
                for (var j = 0; j < PlacementTryCount; j++)
                {
                    var bombX = Random.Range(0, SizeX);
                    var bombY = Random.Range(0, SizeY);
                    if (TryPlaceBomb(bombX, bombY))
                        break;
                }
            }
        }

        private void OnDiscoveredChange(IBoardCell cell)
        {
            OnDiscovered?.Invoke(this, cell);
        }
    }
}
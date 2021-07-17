using Minesweeper.MVP.Implementation;
using NUnit.Framework;

namespace Minesweeper.Tests
{
    public class MinesweeperModelTests
    {
        [Test]
        public void WinTest()
        {
            var board = new Board(5, 5);
            var game = new Game(board);
            board.TryPlaceBomb(0, 0);
            board[0, 1].Discover();
            Assert.IsTrue(game.Won, "Game must be won in this test");
        }

        [Test]
        public void LoseTest()
        {
            var board = new Board(5, 5);
            var game = new Game(board);
            board.TryPlaceBomb(0, 0);
            board[0, 0].Discover();
            Assert.IsTrue(game.Lost, "Game must be lose in this test");
        }

        [Test]
        public void TryPlaceBombTest()
        {
            var board = new Board(5, 5);
            board.TryPlaceBomb(0, 0);
            var placeResult = board.TryPlaceBomb(0, 0);
            Assert.IsTrue(!placeResult, "A bomb cannot be added two times to the same position");
        }

        [Test]
        public void ResetBombTest()
        {
            var board = new Board(5, 5);
            board.TryPlaceBomb(0, 0);
            board.Reset();
            Assert.IsTrue(!board[0, 0].HasBomb, "All cells should not have any bomb after reset");
        }

        [Test]
        public void ResetDiscoverTest()
        {
            var board = new Board(5, 5);
            board[0, 0].Discover();
            board.Reset();
            Assert.IsTrue(!board[0, 0].Discovered, "All cells should not be discovered after reset");
        }
    }
}
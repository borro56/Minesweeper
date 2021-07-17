using Minesweeper.MVP;
using NUnit.Framework;
using UnityEngine.EventSystems;

namespace Minesweeper.Tests
{
    public class MinesweeperViewTests
    {
        [Test]
        public void EnumMappingTest()
        {
            Assert.IsTrue((int)CellInputButton.Left == (int)PointerEventData.InputButton.Left, "Enum mapping for left click have changed");
            Assert.IsTrue((int)CellInputButton.Right == (int)PointerEventData.InputButton.Right, "Enum mapping for right click have changed");
            Assert.IsTrue((int)CellInputButton.Middle == (int)PointerEventData.InputButton.Middle, "Enum mapping for middle click have changed");
        }
    }
}
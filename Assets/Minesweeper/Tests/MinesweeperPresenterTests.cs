using Minesweeper.MVP;
using NSubstitute;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;

namespace Minesweeper.Tests
{
    public class MinesweeperPresenterTests
    {
        [UnityTest]
        public IEnumerator When_GameWon_Expect_ViewFinishGame()
        {
            var view = Substitute.For<IGameView>();
            var game = Substitute.For<IGame>();
            var model = Substitute.For<IGameModel>();
            model.Game.Returns(game);

            var go = new GameObject();
            go.SetActive(false);

            var presenter = go.AddComponent<GamePresenter>();
            presenter.View = view;
            presenter.Model = model;
            go.SetActive(true);

            yield return null;

            game.OnWon += Raise.Event<Action<IGame>>(game);

            view.Received().FinishGame(true);

            UnityEngine.Object.DestroyImmediate(go);
        }
    }
}
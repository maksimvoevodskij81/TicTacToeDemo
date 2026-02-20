
using TicTacToe.Domain.Game.Contracts;
using TicTacToe.Web.Board;
using TicTacToe.Web.Game;

namespace TicTacToe.Tests
{
    [TestFixture]
    public sealed class TicTacToeGameTests
    {
        [Test]
        public void Start_EmptyBoard_XTurn_InProgress()
        {
            var board = new Board();
            var game = new InMemoryGame(board);

            var gameState = game.Start();

            Assert.That(gameState.CurrentPlayer, Is.EqualTo(PlayerSymbol.X));
            Assert.That(gameState.Status, Is.EqualTo(GameStatus.InProgress));
        }

        [Test ]
        public void MakeMove_NotYourTurn_ReturnNotYourTurns_AndDoesNotChangeBoard()
        {
            var board = new Board();
            var game = new InMemoryGame(board);

            var stateBefore = game.Start();
            Assert.That(stateBefore.CurrentPlayer, Is.EqualTo(PlayerSymbol.X));

            var boardBefore = board.GetBoard().ToArray();

            var result = game.MakeMove(PlayerSymbol.O, 0, 0);

            var boardAfter = board.GetBoard();
            var stateAfter = game.GetState();

            Assert.That(result.IsSuccess, Is.False);
            Assert.That(result.ErrorCode, Is.EqualTo("NotYourTurn"));

            Assert.That(stateAfter.CurrentPlayer, Is.EqualTo(PlayerSymbol.X));
            Assert.That(boardAfter, Is.EqualTo(boardBefore));
         
        }

        [Test]
        public void MakeMove_OutOfBounds_Row_ReturnOutOfBounds_AndDoesNotChangeBoard()
        {
            var board = new Board();
            var game = new InMemoryGame(board);

            var stateBefore = game.Start();

            var boardBefore = board.GetBoard().ToArray();

            var result = game.MakeMove(stateBefore.CurrentPlayer, -1, 0);

            var boardAfter = board.GetBoard().ToArray();
            var stateAfter = game.GetState();

            Assert.That(result.IsSuccess, Is.False);
            Assert.That(result.ErrorCode, Is.EqualTo("OutOfBounds"));
            Assert.That(boardAfter, Is.EqualTo(boardBefore));
            Assert.That(stateAfter.CurrentPlayer, Is.EqualTo(stateBefore.CurrentPlayer));

        }


        [Test]
        public void MakeMove_OutOfBounds_Column_ReturnOutOfBounds_AndDoesNotChangeBoard()
        {
            var board = new Board();
            var game = new InMemoryGame(board);

            var stateBefore = game.Start();

            var boardBefore = board.GetBoard().ToArray();

            var result = game.MakeMove(stateBefore.CurrentPlayer, 0, 3);

            var boardAfter = board.GetBoard().ToArray();
            var stateAfter = game.GetState();

            Assert.That(result.IsSuccess, Is.False);
            Assert.That(result.ErrorCode, Is.EqualTo("OutOfBounds"));
            Assert.That(boardAfter, Is.EqualTo(boardBefore));
            Assert.That(stateAfter.CurrentPlayer, Is.EqualTo(stateBefore.CurrentPlayer));

        }

        [Test]
        public void MakeMove_CellIsOccupied_ReturnCellOccupied_AndDoesNotChangeBoard()
        {
            var board = new Board();
            var game = new InMemoryGame(board);

            var stateBefore = game.Start();
            Assert.That(stateBefore.CurrentPlayer, Is.EqualTo(PlayerSymbol.X));


            var firsMoveResult = game.MakeMove(stateBefore.CurrentPlayer, 0, 0);
            Assert.That(firsMoveResult.IsSuccess, Is.True);

            var boardAfterFirstMove = board.GetBoard().ToArray();
            var stateAfterFirstMove  = game.GetState();
            Assert.That(stateAfterFirstMove .CurrentPlayer, Is.EqualTo(PlayerSymbol.O));
            Assert.That(boardAfterFirstMove[0], Is.EqualTo(PlayerSymbol.X));

            var secondMoveResult = game.MakeMove(stateAfterFirstMove .CurrentPlayer, 0, 0);

            var boardAfterSecondMove = board.GetBoard().ToArray();
            var stateAfterSecondMove = game.GetState();

            Assert.That(secondMoveResult.IsSuccess, Is.False);
            Assert.That(secondMoveResult.ErrorCode, Is.EqualTo("CellOccupied"));
            Assert.That(boardAfterSecondMove, Is.EqualTo(boardAfterFirstMove));
            Assert.That(stateAfterSecondMove.CurrentPlayer, Is.EqualTo(stateAfterFirstMove.CurrentPlayer));
            Assert.That(boardAfterSecondMove[0], Is.EqualTo(PlayerSymbol.X));
        }

        [Test]
        public void Win_Row_SetsWonAndWinner_AndRejectsMovesAfterFinish()
        {
            var board = new Board();
            var game = new InMemoryGame(board);

            var start = game.Start();
            Assert.That(start.CurrentPlayer, Is.EqualTo(PlayerSymbol.X));

            game.MakeMove(PlayerSymbol.X, 0, 0);
            game.MakeMove(PlayerSymbol.O, 1, 0);
            game.MakeMove(PlayerSymbol.X, 0, 1);
            game.MakeMove(PlayerSymbol.O, 1, 2);

            var winMove = game.MakeMove(PlayerSymbol.X, 0, 2);
            Assert.That(winMove.IsSuccess, Is.True);

            var stateAfter = game.GetState();
            Assert.That(stateAfter.Status, Is.EqualTo(GameStatus.Won));
            Assert.That(stateAfter.Winner, Is.EqualTo(PlayerSymbol.X));

            var afterWonResult = game.MakeMove(PlayerSymbol.O, 2, 2);
            Assert.That(afterWonResult.IsSuccess, Is.False);
            Assert.That(afterWonResult.ErrorCode, Is.EqualTo("GameFinished"));
        }

        [Test]
        public void Draw_FullBoard_NoWinner_RejectsMovesAfterFinish()
        {
            var board = new Board();
            var game = new InMemoryGame(board);

            var start = game.Start();
            Assert.That(start.CurrentPlayer, Is.EqualTo(PlayerSymbol.X));

            game.MakeMove(PlayerSymbol.X, 0, 0);
            game.MakeMove(PlayerSymbol.O, 0, 1);
            game.MakeMove(PlayerSymbol.X, 0, 2);
            game.MakeMove(PlayerSymbol.O, 1, 1);
            game.MakeMove(PlayerSymbol.X, 1, 0);
            game.MakeMove(PlayerSymbol.O, 1, 2);
            game.MakeMove(PlayerSymbol.X, 2, 1);
            game.MakeMove(PlayerSymbol.O, 2, 0);

            var drawMove = game.MakeMove(PlayerSymbol.X, 2, 2);
            Assert.That(drawMove.IsSuccess, Is.True);

            var stateAfter = game.GetState();
            Assert.That(stateAfter.Status, Is.EqualTo(GameStatus.Draw));
            Assert.That(stateAfter.Winner, Is.EqualTo(null));
            Assert.That(stateAfter.GameBoard, Does.Not.Contain(PlayerSymbol.Empty));
            
            var boardBeforeAfterDraw = board.GetBoard().ToArray();

            var afterDrawResult = game.MakeMove(PlayerSymbol.O, 2, 2);
            var boardAfterAfterDraw = board.GetBoard().ToArray();

            Assert.That(boardAfterAfterDraw, Is.EqualTo(boardBeforeAfterDraw));
            Assert.That(afterDrawResult.IsSuccess, Is.False);
            Assert.That(afterDrawResult.ErrorCode, Is.EqualTo("GameFinished"));
        }
    }
}

using ChessExample.ChessBoard.Core;
using ChessExample.ChessBoard.Enums;
using ChessExample.ChessBoard.Exceptions;
using ChessExample.ChessPiece.Enums;
using ChessExample.Utilities.Extensions;
using NUnit.Framework;

namespace ChessExample.ChessBoard.Test.Core
{
    [TestFixture]
    public class ChessBoardTurnValidatorTests
    {
        #region WhitePiece

        [Test]
        public void WhiteRookFailTestStartOfGame()
        {
            ChessBoard board = new();

            ChessBoardMove move = new(new ChessPiece.Core.ChessPiece(ChessPieceType.Rook, ChessPieceColor.White));
            move.CurrentSquare = new ChessBoardSquare(0, 0);
            move.NewSquare = new ChessBoardSquare(0, 2);

            ChessBoardTurn turn = new()
            {
                new List<ChessBoardMove>() { move }
            };

            Assert.Throws<ChessSameColorException>(() => ChessBoardTurnValidator.RookValidation(board, turn));
        }

        [Test]
        public void WhiteKnightSuccessStartOfGame()
        {
            ChessBoard board = new();
            ChessBoardMove move = new(new ChessPiece.Core.ChessPiece(ChessPieceType.Knight, ChessPieceColor.White));
            move.CurrentSquare = new ChessBoardSquare(1, 0);
            move.NewSquare = new ChessBoardSquare(2, 2);

            ChessBoardTurn turn = new()
            {
                new List<ChessBoardMove>() { move }
            };

            Assert.That(ChessBoardTurnValidator.KnightValidation(board, turn), Is.EqualTo(true));
        }

        [Test]
        public void WhiteBishopFailTestStartOfGame()
        {
            ChessBoard board = new();
            ChessBoardMove move = new(new ChessPiece.Core.ChessPiece(ChessPieceType.Bishop, ChessPieceColor.White));
            move.CurrentSquare = new ChessBoardSquare(2, 0);
            move.NewSquare = new ChessBoardSquare(1, 1);

            ChessBoardTurn turn = new()
            {
                new List<ChessBoardMove>() { move }
            };

            Assert.Throws<ChessSameColorException>(() => ChessBoardTurnValidator.BishopValidation(board, turn));
        }

        [TestCase(0, 1, true)]
        [TestCase(1, 1, true)]
        [TestCase(2, 1, true)]
        [TestCase(3, 1, true)]
        [TestCase(4, 1, true)]
        [TestCase(5, 1, true)]
        [TestCase(6, 1, true)]
        [TestCase(7, 1, true)]
        public void WhitePawnMoveTwoStartOfGame(int col, int row, bool expected)
        {
            ChessBoard board = new();

            ChessBoardMove move = new(new ChessPiece.Core.ChessPiece(ChessPieceType.Pawn, ChessPieceColor.White));
            move.CurrentSquare = new ChessBoardSquare(col, row);
            move.NewSquare = new ChessBoardSquare(col, row + 2);

            ChessBoardTurn turn = new()
            {
                new List<ChessBoardMove>() { move }
            };

            Assert.That(turn.GenerateSanNotation(), Is.EqualTo($"{col.GetEnumFromDescription<ChessBoardColumn>().GetValueFromEnum().ToLower()}{row + 3}"));
            Assert.That(ChessBoardTurnValidator.PawnValidation(board, turn), Is.EqualTo(expected));
        }

        [TestCase(0, 1, true)]
        [TestCase(1, 1, true)]
        [TestCase(2, 1, true)]
        [TestCase(3, 1, true)]
        [TestCase(4, 1, true)]
        [TestCase(5, 1, true)]
        [TestCase(6, 1, true)]
        [TestCase(7, 1, true)]
        public void WhitePawnMoveOneStartOfGame(int col, int row, bool expected)
        {
            ChessBoard board = new();

            ChessBoardMove move = new(new ChessPiece.Core.ChessPiece(ChessPieceType.Pawn, ChessPieceColor.White));
            move.CurrentSquare = new ChessBoardSquare(col, row);
            move.NewSquare = new ChessBoardSquare(col, row + 1);

            ChessBoardTurn turn = new()
            {
                new List<ChessBoardMove>() { move }
            };

            Assert.That(turn.GenerateSanNotation(), Is.EqualTo($"{col.GetEnumFromDescription<ChessBoardColumn>().GetValueFromEnum().ToLower()}{row + 2}"));
            Assert.That(ChessBoardTurnValidator.PawnValidation(board, turn), Is.EqualTo(expected));
        }

        #endregion

        #region BlackPiece

        [TestCase(0, 6, true)]
        [TestCase(1, 6, true)]
        [TestCase(2, 6, true)]
        [TestCase(3, 6, true)]
        [TestCase(4, 6, true)]
        [TestCase(5, 6, true)]
        [TestCase(6, 6, true)]
        [TestCase(7, 6, true)]
        public void BlackPawnMoveTwoStartOfGame(int col, int row, bool expected)
        {
            ChessBoard board = new();
            ChessBoardMove move = new(new ChessPiece.Core.ChessPiece(ChessPieceType.Pawn, ChessPieceColor.Black));
            move.CurrentSquare = new ChessBoardSquare(col, row);
            move.NewSquare = new ChessBoardSquare(col, row - 2);

            ChessBoardTurn turn = new()
            {
                new List<ChessBoardMove>() { move }
            };

            Assert.That(turn.GenerateSanNotation(), Is.EqualTo($"{col.GetEnumFromDescription<ChessBoardColumn>().GetValueFromEnum().ToLower()}{row - 1}"));
            Assert.That(ChessBoardTurnValidator.PawnValidation(board, turn), Is.EqualTo(expected));
        }

        [TestCase(0, 6, true)]
        [TestCase(1, 6, true)]
        [TestCase(2, 6, true)]
        [TestCase(3, 6, true)]
        [TestCase(4, 6, true)]
        [TestCase(5, 6, true)]
        [TestCase(6, 6, true)]
        [TestCase(7, 6, true)]
        public void BlackPawnMoveOneStartOfGame(int col, int row, bool expected)
        {
            ChessBoard board = new();

            ChessBoardMove move = new(new ChessPiece.Core.ChessPiece(ChessPieceType.Pawn, ChessPieceColor.Black));
            move.CurrentSquare = new ChessBoardSquare(col, row);
            move.NewSquare = new ChessBoardSquare(col, row - 1);

            ChessBoardTurn turn = new()
            {
                new List<ChessBoardMove>() { move }
            };

            Assert.That(turn.GenerateSanNotation(), Is.EqualTo($"{col.GetEnumFromDescription<ChessBoardColumn>().GetValueFromEnum().ToLower()}{row}"));
            Assert.That(ChessBoardTurnValidator.PawnValidation(board, turn), Is.EqualTo(expected));
        }

        #endregion
    }
}
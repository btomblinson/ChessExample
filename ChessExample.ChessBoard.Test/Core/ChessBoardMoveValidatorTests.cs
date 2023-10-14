using ChessExample.ChessBoard.Core;
using ChessExample.ChessBoard.Exceptions;
using NUnit.Framework;

namespace ChessExample.ChessBoard.Test.Core
{
    [TestFixture]
	public class ChessBoardMoveValidatorTests
	{
		#region WhitePiece

		[Test]
		public void WhiteRookFailTestStartOfGame()
		{
			ChessBoard board = new ChessBoard();

			ChessBoardMove move = new ChessBoardMove(board.Board[0, 0].Item2, board.Board[0, 0].Item1, new ChessBoardSpace(0, 2));

			Assert.Throws<ChessSameColorException>(() => ChessBoardMoveValidator.RookValidation(move, board));
		}

		[Test]
		public void WhiteKnightSuccessStartOfGame()
		{
			ChessBoard board = new ChessBoard();

			ChessBoardMove move = new ChessBoardMove(board.Board[1, 0].Item2, board.Board[1, 0].Item1, new ChessBoardSpace(2, 2));
			Assert.That(ChessBoardMoveValidator.KnightValidation(move, board), Is.EqualTo(true));
		}

		[Test]
		public void WhiteBishopFailTestStartOfGame()
		{
			ChessBoard board = new ChessBoard();

			ChessBoardMove move = new ChessBoardMove(board.Board[2, 0].Item2, board.Board[2, 0].Item1, new ChessBoardSpace(1, 1));

			Assert.Throws<ChessSameColorException>(() => ChessBoardMoveValidator.BishopValidation(move, board));
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
			ChessBoard board = new ChessBoard();

			ChessBoardMove move = new ChessBoardMove(board.Board[col, row].Item2, board.Board[col, row].Item1, new ChessBoardSpace(col, row + 2));

			Assert.That(ChessBoardMoveValidator.PawnValidation(move, board), Is.EqualTo(expected));
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
			ChessBoard board = new ChessBoard();

			ChessBoardMove move = new ChessBoardMove(board.Board[col, row].Item2, board.Board[col, row].Item1, new ChessBoardSpace(col, row + 1));

			Assert.That(ChessBoardMoveValidator.PawnValidation(move, board), Is.EqualTo(expected));
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
			ChessBoard board = new ChessBoard();

			ChessBoardMove move = new ChessBoardMove(board.Board[col, row].Item2, board.Board[col, row].Item1, new ChessBoardSpace(col, row - 2));

			Assert.That(ChessBoardMoveValidator.PawnValidation(move, board), Is.EqualTo(expected));
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
			ChessBoard board = new ChessBoard();

			ChessBoardMove move = new ChessBoardMove(board.Board[col, row].Item2, board.Board[col, row].Item1, new ChessBoardSpace(col, row - 1));

			Assert.That(ChessBoardMoveValidator.PawnValidation(move, board), Is.EqualTo(expected));
		}

		#endregion
	}
}
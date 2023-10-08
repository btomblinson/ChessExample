using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessExample.ChessBoard.Core;
using ChessExample.ChessPiece.Enums;
using NUnit.Framework;

namespace ChessExample.ChessBoard.Test.Core
{
	[TestFixture]
	public class ChessBoardTests
	{
		[OneTimeSetUp]
		public void ChessBoardInitializeTest()
		{
			ChessBoard board = new ChessBoard();

			//white pieces
			Assert.That(board.Board[0, 0].Item2, Is.EqualTo(new ChessPiece.Core.ChessPiece(ChessPieceType.Rook, ChessPieceColor.White)));
			Assert.That(board.Board[1, 0].Item2, Is.EqualTo(new ChessPiece.Core.ChessPiece(ChessPieceType.Knight, ChessPieceColor.White)));
			Assert.That(board.Board[2, 0].Item2, Is.EqualTo(new ChessPiece.Core.ChessPiece(ChessPieceType.Bishop, ChessPieceColor.White)));
			Assert.That(board.Board[3, 0].Item2, Is.EqualTo(new ChessPiece.Core.ChessPiece(ChessPieceType.Queen, ChessPieceColor.White)));
			Assert.That(board.Board[4, 0].Item2, Is.EqualTo(new ChessPiece.Core.ChessPiece(ChessPieceType.King, ChessPieceColor.White)));
			Assert.That(board.Board[5, 0].Item2, Is.EqualTo(new ChessPiece.Core.ChessPiece(ChessPieceType.Bishop, ChessPieceColor.White)));
			Assert.That(board.Board[6, 0].Item2, Is.EqualTo(new ChessPiece.Core.ChessPiece(ChessPieceType.Knight, ChessPieceColor.White)));
			Assert.That(board.Board[7, 0].Item2, Is.EqualTo(new ChessPiece.Core.ChessPiece(ChessPieceType.Rook, ChessPieceColor.White)));

			Assert.That(board.Board[0, 1].Item2, Is.EqualTo(new ChessPiece.Core.ChessPiece(ChessPieceType.Pawn, ChessPieceColor.White)));
			Assert.That(board.Board[1, 1].Item2, Is.EqualTo(new ChessPiece.Core.ChessPiece(ChessPieceType.Pawn, ChessPieceColor.White)));
			Assert.That(board.Board[2, 1].Item2, Is.EqualTo(new ChessPiece.Core.ChessPiece(ChessPieceType.Pawn, ChessPieceColor.White)));
			Assert.That(board.Board[3, 1].Item2, Is.EqualTo(new ChessPiece.Core.ChessPiece(ChessPieceType.Pawn, ChessPieceColor.White)));
			Assert.That(board.Board[4, 1].Item2, Is.EqualTo(new ChessPiece.Core.ChessPiece(ChessPieceType.Pawn, ChessPieceColor.White)));
			Assert.That(board.Board[5, 1].Item2, Is.EqualTo(new ChessPiece.Core.ChessPiece(ChessPieceType.Pawn, ChessPieceColor.White)));
			Assert.That(board.Board[6, 1].Item2, Is.EqualTo(new ChessPiece.Core.ChessPiece(ChessPieceType.Pawn, ChessPieceColor.White)));
			Assert.That(board.Board[7, 1].Item2, Is.EqualTo(new ChessPiece.Core.ChessPiece(ChessPieceType.Pawn, ChessPieceColor.White)));

			//empty board
			Assert.That(board.Board[0, 2].Item2, Is.EqualTo(null));
			Assert.That(board.Board[1, 2].Item2, Is.EqualTo(null));
			Assert.That(board.Board[2, 2].Item2, Is.EqualTo(null));
			Assert.That(board.Board[3, 2].Item2, Is.EqualTo(null));
			Assert.That(board.Board[4, 2].Item2, Is.EqualTo(null));
			Assert.That(board.Board[5, 2].Item2, Is.EqualTo(null));
			Assert.That(board.Board[6, 2].Item2, Is.EqualTo(null));
			Assert.That(board.Board[7, 2].Item2, Is.EqualTo(null));

			Assert.That(board.Board[0, 3].Item2, Is.EqualTo(null));
			Assert.That(board.Board[1, 3].Item2, Is.EqualTo(null));
			Assert.That(board.Board[2, 3].Item2, Is.EqualTo(null));
			Assert.That(board.Board[3, 3].Item2, Is.EqualTo(null));
			Assert.That(board.Board[4, 3].Item2, Is.EqualTo(null));
			Assert.That(board.Board[5, 3].Item2, Is.EqualTo(null));
			Assert.That(board.Board[6, 3].Item2, Is.EqualTo(null));
			Assert.That(board.Board[7, 3].Item2, Is.EqualTo(null));

			Assert.That(board.Board[0, 4].Item2, Is.EqualTo(null));
			Assert.That(board.Board[1, 4].Item2, Is.EqualTo(null));
			Assert.That(board.Board[2, 4].Item2, Is.EqualTo(null));
			Assert.That(board.Board[3, 4].Item2, Is.EqualTo(null));
			Assert.That(board.Board[4, 4].Item2, Is.EqualTo(null));
			Assert.That(board.Board[5, 4].Item2, Is.EqualTo(null));
			Assert.That(board.Board[6, 4].Item2, Is.EqualTo(null));
			Assert.That(board.Board[7, 4].Item2, Is.EqualTo(null));

			Assert.That(board.Board[0, 5].Item2, Is.EqualTo(null));
			Assert.That(board.Board[1, 5].Item2, Is.EqualTo(null));
			Assert.That(board.Board[2, 5].Item2, Is.EqualTo(null));
			Assert.That(board.Board[3, 5].Item2, Is.EqualTo(null));
			Assert.That(board.Board[4, 5].Item2, Is.EqualTo(null));
			Assert.That(board.Board[5, 5].Item2, Is.EqualTo(null));
			Assert.That(board.Board[6, 5].Item2, Is.EqualTo(null));
			Assert.That(board.Board[7, 5].Item2, Is.EqualTo(null));

			//black pieces

			Assert.That(board.Board[0, 6].Item2, Is.EqualTo(new ChessPiece.Core.ChessPiece(ChessPieceType.Pawn, ChessPieceColor.Black)));
			Assert.That(board.Board[1, 6].Item2, Is.EqualTo(new ChessPiece.Core.ChessPiece(ChessPieceType.Pawn, ChessPieceColor.Black)));
			Assert.That(board.Board[2, 6].Item2, Is.EqualTo(new ChessPiece.Core.ChessPiece(ChessPieceType.Pawn, ChessPieceColor.Black)));
			Assert.That(board.Board[3, 6].Item2, Is.EqualTo(new ChessPiece.Core.ChessPiece(ChessPieceType.Pawn, ChessPieceColor.Black)));
			Assert.That(board.Board[4, 6].Item2, Is.EqualTo(new ChessPiece.Core.ChessPiece(ChessPieceType.Pawn, ChessPieceColor.Black)));
			Assert.That(board.Board[5, 6].Item2, Is.EqualTo(new ChessPiece.Core.ChessPiece(ChessPieceType.Pawn, ChessPieceColor.Black)));
			Assert.That(board.Board[6, 6].Item2, Is.EqualTo(new ChessPiece.Core.ChessPiece(ChessPieceType.Pawn, ChessPieceColor.Black)));
			Assert.That(board.Board[7, 6].Item2, Is.EqualTo(new ChessPiece.Core.ChessPiece(ChessPieceType.Pawn, ChessPieceColor.Black)));

			Assert.That(board.Board[0, 7].Item2, Is.EqualTo(new ChessPiece.Core.ChessPiece(ChessPieceType.Rook, ChessPieceColor.Black)));
			Assert.That(board.Board[1, 7].Item2, Is.EqualTo(new ChessPiece.Core.ChessPiece(ChessPieceType.Knight, ChessPieceColor.Black)));
			Assert.That(board.Board[2, 7].Item2, Is.EqualTo(new ChessPiece.Core.ChessPiece(ChessPieceType.Bishop, ChessPieceColor.Black)));
			Assert.That(board.Board[3, 7].Item2, Is.EqualTo(new ChessPiece.Core.ChessPiece(ChessPieceType.Queen, ChessPieceColor.Black)));
			Assert.That(board.Board[4, 7].Item2, Is.EqualTo(new ChessPiece.Core.ChessPiece(ChessPieceType.King, ChessPieceColor.Black)));
			Assert.That(board.Board[5, 7].Item2, Is.EqualTo(new ChessPiece.Core.ChessPiece(ChessPieceType.Bishop, ChessPieceColor.Black)));
			Assert.That(board.Board[6, 7].Item2, Is.EqualTo(new ChessPiece.Core.ChessPiece(ChessPieceType.Knight, ChessPieceColor.Black)));
			Assert.That(board.Board[7, 7].Item2, Is.EqualTo(new ChessPiece.Core.ChessPiece(ChessPieceType.Rook, ChessPieceColor.Black)));
		}

		[Test]
		public void ChessBoardStartGameWhitePawnCaptureBlackPawnTest()
		{
			ChessBoard board = new ChessBoard();

			//move first white piece
			ChessBoardMove whiteMove = new ChessBoardMove(board.Board[1, 1].Item2, board.Board[1, 1].Item1, new ChessBoardSpace(1, 3));
			Assert.That(board.IsValidMove(whiteMove), Is.EqualTo(true));
			board.ExecuteMove(whiteMove);

			//move first black piece
			ChessBoardMove blackMove = new ChessBoardMove(board.Board[0, 6].Item2, board.Board[0, 6].Item1, new ChessBoardSpace(0, 4));
			Assert.That(board.IsValidMove(blackMove), Is.EqualTo(true));
			board.ExecuteMove(blackMove);

			//take black pawn with white pawn
			ChessBoardMove whiteCaptureMove = new ChessBoardMove(board.Board[1, 3].Item2, board.Board[1, 3].Item1, new ChessBoardSpace(0, 4));
			Assert.That(board.IsValidMove(whiteCaptureMove), Is.EqualTo(true));
			board.ExecuteMove(whiteCaptureMove);

			Assert.That(board.BlackCaptured.Count, Is.EqualTo(1));
		}

		[Test]
		public void ChessBoardStartGameBlackPawnCaptureWhitePawnTest()
		{
			ChessBoard board = new ChessBoard();

			//move first white piece
			ChessBoardMove whiteMove = new ChessBoardMove(board.Board[0, 1].Item2, board.Board[0, 1].Item1, new ChessBoardSpace(0, 3));
			Assert.That(board.IsValidMove(whiteMove), Is.EqualTo(true));
			board.ExecuteMove(whiteMove);

			//move first black piece
			ChessBoardMove blackMove = new ChessBoardMove(board.Board[1, 6].Item2, board.Board[1, 6].Item1, new ChessBoardSpace(1, 4));
			Assert.That(board.IsValidMove(blackMove), Is.EqualTo(true));
			board.ExecuteMove(blackMove);

			//move second white piece
			whiteMove = new ChessBoardMove(board.Board[2, 1].Item2, board.Board[2, 1].Item1, new ChessBoardSpace(2, 3));
			Assert.That(board.IsValidMove(whiteMove), Is.EqualTo(true));
			board.ExecuteMove(whiteMove);

			//take white pawn with black pawn
			ChessBoardMove blackCaptureMove = new ChessBoardMove(board.Board[1, 4].Item2, board.Board[1, 4].Item1, new ChessBoardSpace(2, 3));
			Assert.That(board.IsValidMove(blackCaptureMove), Is.EqualTo(true));
			board.ExecuteMove(blackCaptureMove);

			Assert.That(board.WhiteCaptured.Count, Is.EqualTo(1));
		}
	}
}
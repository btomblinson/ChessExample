using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessExample.ChessBoard.Core;
using ChessExample.ChessPiece.Enums;
using NUnit.Framework;

namespace ChessExample.ChessBoard.Test
{
	[TestFixture]
	public class ChessBoardTests
	{
		[Test]
		public void BoardInitializeTest()
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
	}
}
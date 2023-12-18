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
			Assert.That(board.Board[0, 0].ChessPiece, Is.EqualTo(new ChessPiece.Core.ChessPiece(ChessPieceType.Rook, ChessPieceColor.White)));
			Assert.That(board.Board[1, 0].ChessPiece, Is.EqualTo(new ChessPiece.Core.ChessPiece(ChessPieceType.Knight, ChessPieceColor.White)));
			Assert.That(board.Board[2, 0].ChessPiece, Is.EqualTo(new ChessPiece.Core.ChessPiece(ChessPieceType.Bishop, ChessPieceColor.White)));
			Assert.That(board.Board[3, 0].ChessPiece, Is.EqualTo(new ChessPiece.Core.ChessPiece(ChessPieceType.Queen, ChessPieceColor.White)));
			Assert.That(board.Board[4, 0].ChessPiece, Is.EqualTo(new ChessPiece.Core.ChessPiece(ChessPieceType.King, ChessPieceColor.White)));
			Assert.That(board.Board[5, 0].ChessPiece, Is.EqualTo(new ChessPiece.Core.ChessPiece(ChessPieceType.Bishop, ChessPieceColor.White)));
			Assert.That(board.Board[6, 0].ChessPiece, Is.EqualTo(new ChessPiece.Core.ChessPiece(ChessPieceType.Knight, ChessPieceColor.White)));
			Assert.That(board.Board[7, 0].ChessPiece, Is.EqualTo(new ChessPiece.Core.ChessPiece(ChessPieceType.Rook, ChessPieceColor.White)));

			Assert.That(board.Board[0, 1].ChessPiece, Is.EqualTo(new ChessPiece.Core.ChessPiece(ChessPieceType.Pawn, ChessPieceColor.White)));
			Assert.That(board.Board[1, 1].ChessPiece, Is.EqualTo(new ChessPiece.Core.ChessPiece(ChessPieceType.Pawn, ChessPieceColor.White)));
			Assert.That(board.Board[2, 1].ChessPiece, Is.EqualTo(new ChessPiece.Core.ChessPiece(ChessPieceType.Pawn, ChessPieceColor.White)));
			Assert.That(board.Board[3, 1].ChessPiece, Is.EqualTo(new ChessPiece.Core.ChessPiece(ChessPieceType.Pawn, ChessPieceColor.White)));
			Assert.That(board.Board[4, 1].ChessPiece, Is.EqualTo(new ChessPiece.Core.ChessPiece(ChessPieceType.Pawn, ChessPieceColor.White)));
			Assert.That(board.Board[5, 1].ChessPiece, Is.EqualTo(new ChessPiece.Core.ChessPiece(ChessPieceType.Pawn, ChessPieceColor.White)));
			Assert.That(board.Board[6, 1].ChessPiece, Is.EqualTo(new ChessPiece.Core.ChessPiece(ChessPieceType.Pawn, ChessPieceColor.White)));
			Assert.That(board.Board[7, 1].ChessPiece, Is.EqualTo(new ChessPiece.Core.ChessPiece(ChessPieceType.Pawn, ChessPieceColor.White)));

			//empty board
			Assert.That(board.Board[0, 2].ChessPiece, Is.EqualTo(null));
			Assert.That(board.Board[1, 2].ChessPiece, Is.EqualTo(null));
			Assert.That(board.Board[2, 2].ChessPiece, Is.EqualTo(null));
			Assert.That(board.Board[3, 2].ChessPiece, Is.EqualTo(null));
			Assert.That(board.Board[4, 2].ChessPiece, Is.EqualTo(null));
			Assert.That(board.Board[5, 2].ChessPiece, Is.EqualTo(null));
			Assert.That(board.Board[6, 2].ChessPiece, Is.EqualTo(null));
			Assert.That(board.Board[7, 2].ChessPiece, Is.EqualTo(null));

			Assert.That(board.Board[0, 3].ChessPiece, Is.EqualTo(null));
			Assert.That(board.Board[1, 3].ChessPiece, Is.EqualTo(null));
			Assert.That(board.Board[2, 3].ChessPiece, Is.EqualTo(null));
			Assert.That(board.Board[3, 3].ChessPiece, Is.EqualTo(null));
			Assert.That(board.Board[4, 3].ChessPiece, Is.EqualTo(null));
			Assert.That(board.Board[5, 3].ChessPiece, Is.EqualTo(null));
			Assert.That(board.Board[6, 3].ChessPiece, Is.EqualTo(null));
			Assert.That(board.Board[7, 3].ChessPiece, Is.EqualTo(null));

			Assert.That(board.Board[0, 4].ChessPiece, Is.EqualTo(null));
			Assert.That(board.Board[1, 4].ChessPiece, Is.EqualTo(null));
			Assert.That(board.Board[2, 4].ChessPiece, Is.EqualTo(null));
			Assert.That(board.Board[3, 4].ChessPiece, Is.EqualTo(null));
			Assert.That(board.Board[4, 4].ChessPiece, Is.EqualTo(null));
			Assert.That(board.Board[5, 4].ChessPiece, Is.EqualTo(null));
			Assert.That(board.Board[6, 4].ChessPiece, Is.EqualTo(null));
			Assert.That(board.Board[7, 4].ChessPiece, Is.EqualTo(null));

			Assert.That(board.Board[0, 5].ChessPiece, Is.EqualTo(null));
			Assert.That(board.Board[1, 5].ChessPiece, Is.EqualTo(null));
			Assert.That(board.Board[2, 5].ChessPiece, Is.EqualTo(null));
			Assert.That(board.Board[3, 5].ChessPiece, Is.EqualTo(null));
			Assert.That(board.Board[4, 5].ChessPiece, Is.EqualTo(null));
			Assert.That(board.Board[5, 5].ChessPiece, Is.EqualTo(null));
			Assert.That(board.Board[6, 5].ChessPiece, Is.EqualTo(null));
			Assert.That(board.Board[7, 5].ChessPiece, Is.EqualTo(null));

			//black pieces

			Assert.That(board.Board[0, 6].ChessPiece, Is.EqualTo(new ChessPiece.Core.ChessPiece(ChessPieceType.Pawn, ChessPieceColor.Black)));
			Assert.That(board.Board[1, 6].ChessPiece, Is.EqualTo(new ChessPiece.Core.ChessPiece(ChessPieceType.Pawn, ChessPieceColor.Black)));
			Assert.That(board.Board[2, 6].ChessPiece, Is.EqualTo(new ChessPiece.Core.ChessPiece(ChessPieceType.Pawn, ChessPieceColor.Black)));
			Assert.That(board.Board[3, 6].ChessPiece, Is.EqualTo(new ChessPiece.Core.ChessPiece(ChessPieceType.Pawn, ChessPieceColor.Black)));
			Assert.That(board.Board[4, 6].ChessPiece, Is.EqualTo(new ChessPiece.Core.ChessPiece(ChessPieceType.Pawn, ChessPieceColor.Black)));
			Assert.That(board.Board[5, 6].ChessPiece, Is.EqualTo(new ChessPiece.Core.ChessPiece(ChessPieceType.Pawn, ChessPieceColor.Black)));
			Assert.That(board.Board[6, 6].ChessPiece, Is.EqualTo(new ChessPiece.Core.ChessPiece(ChessPieceType.Pawn, ChessPieceColor.Black)));
			Assert.That(board.Board[7, 6].ChessPiece, Is.EqualTo(new ChessPiece.Core.ChessPiece(ChessPieceType.Pawn, ChessPieceColor.Black)));

			Assert.That(board.Board[0, 7].ChessPiece, Is.EqualTo(new ChessPiece.Core.ChessPiece(ChessPieceType.Rook, ChessPieceColor.Black)));
			Assert.That(board.Board[1, 7].ChessPiece, Is.EqualTo(new ChessPiece.Core.ChessPiece(ChessPieceType.Knight, ChessPieceColor.Black)));
			Assert.That(board.Board[2, 7].ChessPiece, Is.EqualTo(new ChessPiece.Core.ChessPiece(ChessPieceType.Bishop, ChessPieceColor.Black)));
			Assert.That(board.Board[3, 7].ChessPiece, Is.EqualTo(new ChessPiece.Core.ChessPiece(ChessPieceType.Queen, ChessPieceColor.Black)));
			Assert.That(board.Board[4, 7].ChessPiece, Is.EqualTo(new ChessPiece.Core.ChessPiece(ChessPieceType.King, ChessPieceColor.Black)));
			Assert.That(board.Board[5, 7].ChessPiece, Is.EqualTo(new ChessPiece.Core.ChessPiece(ChessPieceType.Bishop, ChessPieceColor.Black)));
			Assert.That(board.Board[6, 7].ChessPiece, Is.EqualTo(new ChessPiece.Core.ChessPiece(ChessPieceType.Knight, ChessPieceColor.Black)));
			Assert.That(board.Board[7, 7].ChessPiece, Is.EqualTo(new ChessPiece.Core.ChessPiece(ChessPieceType.Rook, ChessPieceColor.Black)));
		}
	}
}
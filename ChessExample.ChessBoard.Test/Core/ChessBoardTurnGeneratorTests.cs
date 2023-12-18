using ChessExample.ChessBoard.Core;
using ChessExample.ChessBoard.Exceptions;
using ChessExample.ChessPiece.Enums;
using NUnit.Framework;

namespace ChessExample.ChessBoard.Test.Core
{
	[TestFixture]
	public class ChessBoardTurnGeneratorTests
	{
		[Test]
		public void WhiteKnightGenerateMovesStartOfGame()
		{
			ChessBoard board = new();
			Tuple<ChessBoardSpace, ChessPiece.Core.ChessPiece?> piece = new(new ChessBoardSpace(1, 0), new ChessPiece.Core.ChessPiece(ChessPieceType.Knight, ChessPieceColor.White));

			Assert.That(ChessBoardTurnGenerator.KnightGenerator(board, piece).Count, Is.EqualTo(2), "Invalid generated moves.");

			piece = new(new ChessBoardSpace(6, 0), new ChessPiece.Core.ChessPiece(ChessPieceType.Knight, ChessPieceColor.White));

			Assert.That(ChessBoardTurnGenerator.KnightGenerator(board, piece).Count, Is.EqualTo(2), "Invalid generated moves.");
		}

		[Test]
		public void WhiteRookGenerateMovesStartOfGame()
		{
			ChessBoard board = new();
			Tuple<ChessBoardSpace, ChessPiece.Core.ChessPiece?> piece = new(new ChessBoardSpace(0, 0), new ChessPiece.Core.ChessPiece(ChessPieceType.Rook, ChessPieceColor.White));

            Assert.That(ChessBoardTurnGenerator.RookGenerator(board, piece).Count, Is.EqualTo(0), "Invalid generated moves.");

            piece = new(new ChessBoardSpace(7, 0), new ChessPiece.Core.ChessPiece(ChessPieceType.Rook, ChessPieceColor.White));

            Assert.That(ChessBoardTurnGenerator.RookGenerator(board, piece).Count, Is.EqualTo(0), "Invalid generated moves.");
        }
	}
}
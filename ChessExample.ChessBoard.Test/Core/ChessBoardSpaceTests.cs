using ChessExample.ChessBoard.Core;
using ChessExample.ChessBoard.Enums;
using NUnit.Framework;

namespace ChessExample.ChessBoard.Test.Core
{
	[TestFixture]
	public class ChessBoardSpaceTests
	{
		//TODO: Finish these tests
		[TestCase(0, 0, ChessBoardSpaceColor.Black)]
		[TestCase(1, 0, ChessBoardSpaceColor.White)]
		[TestCase(2, 0, ChessBoardSpaceColor.Black)]
		public void BoardSpaceTest_Initialize(int col, int row, ChessBoardSpaceColor color)
		{
			ChessBoardSpace space = new ChessBoardSpace(col, row);
			Assert.That(space.Color, Is.EqualTo(color));
		}
	}
}
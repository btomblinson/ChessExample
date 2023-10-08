using System.ComponentModel;
using ChessExample.Utilities.Extensions;

namespace ChessExample.ChessBoard.Enums
{
	public enum ChessBoardColumn
	{
		[ChessBoardColumn("0", "A")] A,

		[ChessBoardColumn("1", "B")] B,

		[ChessBoardColumn("2", "C")] C,

		[ChessBoardColumn("3", "D")] D,

		[ChessBoardColumn("4", "E")] E,

		[ChessBoardColumn("5", "F")] F,

		[ChessBoardColumn("6", "G")] G,

		[ChessBoardColumn("7", "H")] H
	}
}
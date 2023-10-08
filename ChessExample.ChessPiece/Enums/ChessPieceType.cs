using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ChessExample.ChessPiece.Enums
{
	public enum ChessPieceType
	{
		[Display(Name = "R")] Rook,

		[Display(Name = "N")] Knight,

		[Display(Name = "B")] Bishop,

		[Display(Name = "K")] King,

		[Display(Name = "Q")] Queen,

		[Display(Name = "P")] Pawn
	}
}
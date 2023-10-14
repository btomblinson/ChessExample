using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessExample.ChessBoard.Core;
using ChessExample.ChessPiece.Enums;

namespace ChessExample.Player
{
    public abstract class ChessBasePlayer
	{
		#region Properties

		public bool IsHuman { get; set; }

		public ChessPieceColor Color { get; set; }

		#endregion

		#region Abstract Methods

		public abstract ChessBoardTurn DetermineTurn(ChessBoard.ChessBoard board);

		#endregion
	}
}
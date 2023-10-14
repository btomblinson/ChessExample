using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessExample.ChessBoard.Core
{
	public class ChessBoardMove
	{
		public ChessBoardSpace CurrentSpace;

		public ChessPiece.Core.ChessPiece CurrentPiece;

		public ChessBoardSpace NewSpace;

		public bool IsCapture { get; set; }

		public bool IsCheck { get; set; }

		public bool IsCheckmate { get; set; }

		public bool IsCastle { get; set; }

		public ChessBoardMove()
		{
		}

		public ChessBoardMove(ChessPiece.Core.ChessPiece? piece, ChessBoardSpace currentSpace, ChessBoardSpace newSpace)
		{
			if (piece != null)
			{
				CurrentPiece = piece;
				CurrentSpace = currentSpace;
				NewSpace = newSpace;
				return;
			}

			throw new Exception("Piece is not at space. ");
		}
	}
}
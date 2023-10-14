using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessExample.ChessBoard.Core
{
	/// <summary>
	/// Represents a list of pieces and their move(s), this object has multiple uses, it can represent a
	/// parsed SAN that is then executed, or used to generate all possible moves 
	/// </summary>
	public class ChessBoardTurn : List<Tuple<ChessPiece.Core.ChessPiece, List<ChessBoardMove>>>
	{
		public bool IsCapture { get; set; }

		public bool IsCheck { get; set; }

		public bool IsCheckmate { get; set; }

		public bool IsCastle { get; set; }

		public ChessBoardTurn()
		{
		}

		public ChessPiece.Core.ChessPiece GetFirstPiece()
		{
			if (this.Count == 0)
			{
				throw new ArgumentException("Turn is empty");
			}

			return this[0].Item1;
		}

		public Tuple<ChessPiece.Core.ChessPiece, List<ChessBoardMove>> GetFirstPieceTuple()
		{
			if (this.Count == 0)
			{
				throw new ArgumentException("Turn is empty");
			}

			return this[0];
		}

		public List<ChessBoardMove> GetFirstPieceMoves()
		{
			return GetFirstPieceTuple().Item2;
		}

		public ChessBoardMove GetFirstPieceFirstMove()
		{
			return GetFirstPieceTuple().Item2[0];
		}
	}
}
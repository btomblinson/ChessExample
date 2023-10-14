using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessExample.ChessBoard.Core;

namespace ChessExample.ChessBoard.Exceptions
{
    public class ChessSameColorException : ChessException
	{
		public ChessBoardMove Move { get; }

		public ChessSameColorException(ChessBoard board, ChessBoardMove move)
			: this(board, $"Given move hits same color piece.", move)
		{
		}

		public ChessSameColorException(ChessBoard board, string message, ChessBoardMove move) : base(board, message) => Move = move;
	}
}
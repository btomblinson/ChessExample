using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessExample.ChessBoard.Core;

namespace ChessExample.ChessBoard.Exceptions
{
    public class ChessNoMoveException : ChessException
	{
		public ChessBoardMove Move { get; }

		public ChessNoMoveException(ChessBoard board, ChessBoardMove move)
			: this(board, $"Given move does not move any piece. ", move)
		{
		}

		public ChessNoMoveException(ChessBoard board, string message, ChessBoardMove move) : base(board, message) => Move = move;
	}
}
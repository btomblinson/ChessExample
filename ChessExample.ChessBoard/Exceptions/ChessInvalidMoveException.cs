using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessExample.ChessBoard.Core;

namespace ChessExample.ChessBoard.Exceptions
{
    public class ChessInvalidMoveException : ChessException
	{
		public ChessBoardMove Move { get; }

		public ChessInvalidMoveException(ChessBoard board, ChessBoardMove move)
			: this(board, $"Given move is invalid. ", move)
		{
		}

		public ChessInvalidMoveException(ChessBoard board, string message, ChessBoardMove move) : base(board, message) => Move = move;
	}
}
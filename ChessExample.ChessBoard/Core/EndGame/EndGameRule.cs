using ChessExample.ChessBoard.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessExample.ChessBoard.Core.EndGame
{
	public abstract class EndGameRule
	{
		protected ChessBoard Board;

		public abstract ChessBoardMoveResultType Type { get; }

		public EndGameRule(ChessBoard board)
		{
			this.Board = board;
		}

		public abstract bool IsEndGame();
	}
}
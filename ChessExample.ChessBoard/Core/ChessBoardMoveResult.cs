using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessExample.ChessBoard.Enums;

namespace ChessExample.ChessBoard.Core
{
	public class ChessBoardMoveResult
	{
		public bool IsCheck { get; set; }

		public bool IsCheckmate { get; set; }

		public ChessBoardMoveResultType Result { get; set; }
	}
}
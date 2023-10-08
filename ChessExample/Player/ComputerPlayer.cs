using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessExample.ChessBoard.Core;

namespace ChessExample.Player
{
	public class ComputerPlayer : BasePlayer
	{
		public ComputerPlayer()
		{
			IsHuman = false;
		}

		public override List<ChessBoardMove> DetermineMoves(ChessBoard.ChessBoard board)
		{
			throw new NotImplementedException();
		}
	}
}
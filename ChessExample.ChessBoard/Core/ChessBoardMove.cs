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

		public ChessBoardSpace NewSpace;

		public ChessBoardMove()
		{

		}

		public ChessBoardMove(ChessBoardSpace currentSpace, ChessBoardSpace newSpace)
		{
			CurrentSpace = currentSpace;
			NewSpace = newSpace;
		}
	}
}
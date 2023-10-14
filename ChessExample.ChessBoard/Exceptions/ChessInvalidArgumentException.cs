using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessExample.ChessBoard.Exceptions
{
    public class ChessInvalidArgumentException : ChessException
	{
		public ChessInvalidArgumentException(ChessBoard board, string message)
			: base(board, message)
		{
		}
	}
}
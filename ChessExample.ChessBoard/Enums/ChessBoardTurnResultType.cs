using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessExample.ChessBoard.Enums
{
	public enum ChessBoardTurnResultType
	{
		Continue,

		Checkmate,

		Resigned,

		Timeout,

		Stalemate,

		DrawDeclared,

		InsufficientMaterial,

		FiftyMoveRule,

		Repetition,
	}
}
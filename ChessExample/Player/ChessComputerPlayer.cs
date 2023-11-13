using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessExample.ChessBoard.Core;
using ChessExample.ChessPiece.Enums;

namespace ChessExample.Player
{
	public class ChessComputerPlayer : ChessBasePlayer
	{
		public ChessComputerPlayer(ChessPieceColor color)
		{
			Color = color;
			IsHuman = false;
		}

		public override ChessBoardTurn DetermineTurn(ChessBoard.ChessBoard board)
		{
			ChessBoardTurn turn = new ChessBoardTurn();

			//loop through board and determine all valid moves
			for (int y = 0; y < 8; y++)
			{
				for (int x = 0; x < 8; x++)
				{
					//space is empty skip
					if (board.Board[x, y].Item2 == null)
					{
						continue;
					}

					if (board.Board[x, y].Item2?.Color != Color)
					{
						continue;
					}

					switch (board.Board[x, y].Item2?.Type)
					{
						case ChessPieceType.Rook:
							break;
						case ChessPieceType.Knight:
							turn.AddRange(ChessBoardTurnGenerator.KnightGenerator(board, board.Board[x, y]));
							break;
						case ChessPieceType.Bishop:
							break;
						case ChessPieceType.King:
							break;
						case ChessPieceType.Queen:
							break;
						case ChessPieceType.Pawn:
							break;
						default:
							throw new ArgumentOutOfRangeException();
					}
				}
			}

			return turn;
		}
	}
}
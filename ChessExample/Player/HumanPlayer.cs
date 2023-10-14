using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ChessExample.ChessBoard.Builders;
using ChessExample.ChessBoard.Core;
using ChessExample.ChessPiece.Enums;

namespace ChessExample.Player
{
    public class HumanPlayer : BasePlayer
	{
		public ChessPieceColor Color { get; set; }

		public HumanPlayer(ChessPieceColor color)
		{
			Color = color;
			IsHuman = true;
		}

		public override List<ChessBoardMove> DetermineMoves(ChessBoard.ChessBoard board)
		{
			Console.WriteLine($"What move do you want to do? ");
			string response = string.Empty;
			while (string.IsNullOrWhiteSpace(response))
			{
				response = Console.ReadLine() ?? string.Empty;
				board.CurrentMoveColor = Color;
				SanBuilder.TryParse(board, response, out List<ChessBoardMove> moves);
				return moves;
			}

			throw new ArgumentException("Unable to parse move. ");
		}
	}
}
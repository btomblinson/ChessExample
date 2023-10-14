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
    public class ChessHumanPlayer : ChessBasePlayer
	{
		public ChessPieceColor Color { get; set; }

		public ChessHumanPlayer(ChessPieceColor color)
		{
			Color = color;
			IsHuman = true;
		}

		public override ChessBoardTurn DetermineTurn(ChessBoard.ChessBoard board)
		{
			Console.WriteLine($"What move do you want to do? ");
			string response = string.Empty;
			while (string.IsNullOrWhiteSpace(response))
			{
				response = Console.ReadLine() ?? string.Empty;
				board.CurrentMoveColor = Color;
				SanBuilder.TryParse(board, response, out ChessBoardTurn turn);
				return turn;
			}

			throw new ArgumentException("Unable to parse turn. ");
		}
	}
}
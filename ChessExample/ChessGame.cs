using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessExample.ChessPiece.Enums;
using ChessExample.Player;

namespace ChessExample
{
	public class ChessGame
	{
		public ChessBoard.ChessBoard Board { get; private set; }

		public ChessBasePlayer WhitePlayer { get; private set; }

		public ChessBasePlayer BlackPlayer { get; private set; }

		public ChessGame(int players, ChessBoard.ChessBoard board)
		{
			Board = board;

			switch (players)
			{
				case 0:
					WhitePlayer = new ChessComputerPlayer(ChessPieceColor.White);
					BlackPlayer = new ChessComputerPlayer(ChessPieceColor.Black);
					break;
				case 1:
					WhitePlayer = new ChessHumanPlayer(ChessPieceColor.White);
					BlackPlayer = new ChessComputerPlayer(ChessPieceColor.Black);
					break;
				case 2:
					WhitePlayer = new ChessHumanPlayer(ChessPieceColor.White);
					BlackPlayer = new ChessHumanPlayer(ChessPieceColor.Black);
					break;
			}
		}
	}
}
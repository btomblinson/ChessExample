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

		public BasePlayer WhitePlayer { get; private set; }

		public BasePlayer BlackPlayer { get; private set; }

		public ChessGame(int players, ChessBoard.ChessBoard board)
		{
			Board = board;

			switch (players)
			{
				case 0:
					WhitePlayer = new ComputerPlayer();
					BlackPlayer = new ComputerPlayer();
					break;
				case 1:
					WhitePlayer = new HumanPlayer(ChessPieceColor.White);
					BlackPlayer = new ComputerPlayer();
					break;
				case 2:
					WhitePlayer = new HumanPlayer(ChessPieceColor.White);
					BlackPlayer = new HumanPlayer(ChessPieceColor.Black);
					break;
			}
		}
	}
}
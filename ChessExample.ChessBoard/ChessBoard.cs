using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessExample.ChessBoard.Core;
using ChessExample.ChessBoard.Core.EndGame;
using ChessExample.ChessBoard.Enums;
using ChessExample.ChessBoard.Validations;
using ChessExample.ChessPiece.Enums;
using ChessExample.Utilities.Extensions;

namespace ChessExample.ChessBoard
{
	public class ChessBoard
	{
		public Tuple<ChessBoardSpace, ChessPiece.Core.ChessPiece?>[,] Board;

		public List<ChessPiece.Core.ChessPiece> WhiteCaptured;

		public List<ChessPiece.Core.ChessPiece> BlackCaptured;

		public ChessPieceColor CurrentMoveColor { get; set; }

		public ChessBoard()
		{
			WhiteCaptured = new List<ChessPiece.Core.ChessPiece>();
			BlackCaptured = new List<ChessPiece.Core.ChessPiece>();

			Board = new Tuple<ChessBoardSpace, ChessPiece.Core.ChessPiece?>[8, 8];

			InitializeBoard();
		}

		private void InitializeBoard()
		{
			for (int y = 0; y < 8; y++)
			{
				for (int x = 0; x < 8; x++)
				{
					if (y is 0 or 1 or 6 or 7)
					{
						ChessPieceType type = ChessPieceType.Pawn;

						ChessPieceColor color = ChessPieceColor.White;

						//add white pieces
						if (y is 0 or 1)
						{
							color = ChessPieceColor.White;

							//if back row do special pieces, otherwise it lets it stay pawns for row 2
							if (y is 0)
							{
								type = x switch
								{
									0 or 7 => ChessPieceType.Rook,
									1 or 6 => ChessPieceType.Knight,
									2 or 5 => ChessPieceType.Bishop,
									3 => ChessPieceType.Queen,
									4 => ChessPieceType.King,
									_ => type
								};
							}
						}

						//add black pieces
						if (y is 6 or 7)
						{
							color = ChessPieceColor.Black;

							//if back row do special pieces, otherwise it lets it stay pawns for row 2
							if (y is 7)
							{
								type = x switch
								{
									0 or 7 => ChessPieceType.Rook,
									1 or 6 => ChessPieceType.Knight,
									2 or 5 => ChessPieceType.Bishop,
									3 => ChessPieceType.Queen,
									4 => ChessPieceType.King,
									_ => type
								};
							}
						}

						Board[x, y] = new Tuple<ChessBoardSpace, ChessPiece.Core.ChessPiece?>(new ChessBoardSpace(x, y), new ChessPiece.Core.ChessPiece(type, color));
					}
					else
					{
						Board[x, y] = new Tuple<ChessBoardSpace, ChessPiece.Core.ChessPiece?>(new ChessBoardSpace(x, y), null);
					}
				}
			}
		}

		public bool IsValidMove(ChessBoardMove move)
		{
			switch (move.CurrentPiece.Type)
			{
				case ChessPieceType.Rook:
					return ChessBoardMoveValidator.RookValidation(move, this);
				case ChessPieceType.Knight:
					return ChessBoardMoveValidator.KnightValidation(move, this);
				case ChessPieceType.Bishop:
					return ChessBoardMoveValidator.BishopValidation(move, this);
				case ChessPieceType.King:
					return ChessBoardMoveValidator.KingValidation(move, this);
				case ChessPieceType.Queen:
					return ChessBoardMoveValidator.QueenValidation(move, this);
				case ChessPieceType.Pawn:
					return ChessBoardMoveValidator.PawnValidation(move, this);
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		public ChessBoardMoveResult ExecuteMove(ChessBoardMove move)
		{
			ChessBoardMoveResult result = new ChessBoardMoveResult();

			//move has been validated, update the board

			//remove piece from original place
			Board[move.CurrentSpace.Column.GetDescriptionFromEnum(), move.CurrentSpace.Row.GetDescriptionFromEnum()] = new Tuple<ChessBoardSpace, ChessPiece.Core.ChessPiece?>(move.CurrentSpace, null);

			//move piece to new place

			//if piece is already there kill it
			if (Board[move.NewSpace.Column.GetDescriptionFromEnum(), move.NewSpace.Row.GetDescriptionFromEnum()].Item2 != null)
			{
				if (Board[move.NewSpace.Column.GetDescriptionFromEnum(), move.NewSpace.Row.GetDescriptionFromEnum()].Item2?.Color == ChessPieceColor.Black)
				{
					BlackCaptured.Add(Board[move.NewSpace.Column.GetDescriptionFromEnum(), move.NewSpace.Row.GetDescriptionFromEnum()].Item2);
				}
				else
				{
					WhiteCaptured.Add(Board[move.NewSpace.Column.GetDescriptionFromEnum(), move.NewSpace.Row.GetDescriptionFromEnum()].Item2);
				}
			}

			Board[move.NewSpace.Column.GetDescriptionFromEnum(), move.NewSpace.Row.GetDescriptionFromEnum()] = new Tuple<ChessBoardSpace, ChessPiece.Core.ChessPiece?>(move.NewSpace, move.CurrentPiece);

			//check endgame scenarios

			//checkmate
			if (move.IsCheckmate)
			{
				result.IsCheck = true;
				result.IsCheckmate = true;
				result.Result = ChessBoardMoveResultType.Checkmate;
				return result;
			}

			//draw endgame scenarios

			//check InsufficientMaterialRule
			InsufficientMaterialRule rule = new InsufficientMaterialRule(this);
			if (rule.IsEndGame())
			{
				result.Result = ChessBoardMoveResultType.InsufficientMaterial;
				return result;
			}

			result.Result = ChessBoardMoveResultType.Continue;
			return result;
		}

		/// <summary>
		/// Generates ASCII string representing current board
		/// </summary>
		public string ToAscii(bool displayFull = false)
		{
			StringBuilder builder = new("   ┌────────────────────────────────┐\n");
			for (int y = 8 - 1; y >= 0; y--)
			{
				builder.Append(" " + (y + 1) + " │");
				for (int x = 0; x < 8; x++)
				{
					builder.Append(' ');

					if (Board[x, y].Item2 is not null)
					{
						builder.Append(Board[x, y].Item2?.ToFen());
					}
					else
					{
						builder.Append(". ");
					}

					builder.Append(' ');
				}

				builder.Append("│\n");
			}

			builder.Append("   └────────────────────────────────┘\n");
			builder.Append("     a   b   c   d   e   f   g   h  \n");

			if (displayFull)
			{
				builder.Append('\n');

				if (WhiteCaptured.Count > 0)
					builder.Append("  White Captured: " + string.Join(", ", WhiteCaptured.Select(p => p.ToFen())) + '\n');
				if (BlackCaptured.Count > 0)
					builder.Append("  Black Captured: " + string.Join(", ", BlackCaptured.Select(p => p.ToFen())) + '\n');
			}

			return builder.ToString();
		}
	}
}
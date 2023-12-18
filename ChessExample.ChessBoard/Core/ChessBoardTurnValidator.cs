using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessExample.ChessBoard.Exceptions;
using ChessExample.ChessPiece.Enums;
using ChessExample.Utilities.Extensions;

namespace ChessExample.ChessBoard.Core
{
	public static class ChessBoardTurnValidator
	{
		public static bool PawnValidation(ChessBoard board, ChessBoardTurn turn)
		{
			ChessPiece.Core.ChessPiece piece = turn.GetFirstPiece();
			ChessBoardMove move = turn.GetFirstMove();

			int v = move.NewSquare.Row.GetDescriptionFromEnum() - move.CurrentSquare.Row.GetDescriptionFromEnum(); // Vertical difference
			int h = move.NewSquare.Column.GetDescriptionFromEnum() - move.CurrentSquare.Column.GetDescriptionFromEnum(); // Horizontal difference

			int stepV = Math.Abs(v);
			int stepH = Math.Abs(h);

			// If moving forwards
			if ((piece.Color != ChessPieceColor.White || v <= 0) && (piece.Color != ChessPieceColor.Black || v >= 0))
			{
				return false;
			}

			switch (stepH)
			{
				// 1 step forward
				case 0 when stepV == 1
				            && board.Board[move.NewSquare.Column.GetDescriptionFromEnum(), move.NewSquare.Row.GetDescriptionFromEnum()].ChessPiece is null:

					return true;
				// 2 steps forward if in the beginning
				case 0 when stepV == 2
				            && (move.CurrentSquare.Row.GetDescriptionFromEnum() == 1
				                && board.Board[move.NewSquare.Column.GetDescriptionFromEnum(), 2].ChessPiece is null
				                && board.Board[move.NewSquare.Column.GetDescriptionFromEnum(), 3].ChessPiece is null
				                || move.CurrentSquare.Row.GetDescriptionFromEnum() == 6
				                && board.Board[move.NewSquare.Column.GetDescriptionFromEnum(), 5].ChessPiece is null
				                && board.Board[move.NewSquare.Column.GetDescriptionFromEnum(), 4].ChessPiece is null):
					return true;
				// Second condition horizontal taking piece
				default:
				{
					if (stepV == 1 && stepH == 1
					               && board.Board[move.NewSquare.Column.GetDescriptionFromEnum(), move.NewSquare.Row.GetDescriptionFromEnum()].ChessPiece is not null
					               && piece.Color != board.Board[move.NewSquare.Column.GetDescriptionFromEnum(), move.NewSquare.Row.GetDescriptionFromEnum()].ChessPiece?.Color)
					{
						return true;
					}

					break;
				}
			}

			return false;
		}

		public static bool QueenValidation(ChessBoard board, ChessBoardTurn turn)
		{
			// For queen just using validation of bishop OR rook
			return BishopValidation(board, turn) || RookValidation(board, turn);
		}

		public static bool RookValidation(ChessBoard board, ChessBoardTurn turn)
		{
			ChessPiece.Core.ChessPiece piece = turn.GetFirstPiece();
			ChessBoardMove move = turn.GetFirstMove();

			int v = move.NewSquare.Row.GetDescriptionFromEnum() - move.CurrentSquare.Row.GetDescriptionFromEnum(); // Vertical difference
			int h = move.NewSquare.Column.GetDescriptionFromEnum() - move.CurrentSquare.Column.GetDescriptionFromEnum(); // Horizontal difference

			if (v == 0 && h == 0)
			{
				throw new ChessNoMoveException(board, move);
			}

			if (Math.Abs(v) > 0 && Math.Abs(h) > 0)
			{
				throw new ChessInvalidMoveException(board, move);
			}

			// if moving horizontally or vertically
			if (v == 0 || h == 0)
			{
				// These vars are always 1 or -1, one of them will stay 0
				var stepH = h != 0 ? Math.Abs(h) / h : 0;
				var stepV = v != 0 ? Math.Abs(v) / v : 0;

				// A bit too difficult for loop to explain
				for (int i = move.CurrentSquare.Row.GetDescriptionFromEnum() + stepV, j = move.CurrentSquare.Column.GetDescriptionFromEnum() + stepH;
				     Math.Abs(i - move.NewSquare.Row.GetDescriptionFromEnum() - (j - move.NewSquare.Column.GetDescriptionFromEnum())) >= 0;
				     i += stepV, j += stepH)
				{
					if (board.Board[j, i].ChessPiece is not null || i == move.NewSquare.Row.GetDescriptionFromEnum() && j == move.NewSquare.Column.GetDescriptionFromEnum())
					{
						if (piece.Color == board.Board[j, i].ChessPiece?.Color)
						{
							if (turn.IsCastle)
							{
								continue;
							}

							throw new ChessSameColorException(board, move);
						}

						return i == move.NewSquare.Row.GetDescriptionFromEnum() && j == move.NewSquare.Column.GetDescriptionFromEnum();
					}
				}

				// This return will never be reached (in theory)
				return false;
			}

			return false;
		}

		public static bool KnightValidation(ChessBoard board, ChessBoardTurn turn)
		{
			ChessPiece.Core.ChessPiece piece = turn.GetFirstPiece();
			ChessBoardMove move = turn.GetFirstMove();

			// New position must be with stepH = 1 and steV = 2 or vice versa
			if (Math.Abs(move.NewSquare.Column.GetDescriptionFromEnum() - move.CurrentSquare.Column.GetDescriptionFromEnum()) == 2 && Math.Abs(move.NewSquare.Row.GetDescriptionFromEnum() - move.CurrentSquare.Row.GetDescriptionFromEnum()) == 1
			    || Math.Abs(move.NewSquare.Column.GetDescriptionFromEnum() - move.CurrentSquare.Column.GetDescriptionFromEnum()) == 1 && Math.Abs(move.NewSquare.Row.GetDescriptionFromEnum() - move.CurrentSquare.Row.GetDescriptionFromEnum()) == 2)
			{
				if (board.Board[move.NewSquare.Column.GetDescriptionFromEnum(), move.NewSquare.Row.GetDescriptionFromEnum()].ChessPiece == null)
				{
					return true;
				}

				return board.Board[move.NewSquare.Column.GetDescriptionFromEnum(), move.NewSquare.Row.GetDescriptionFromEnum()].ChessPiece?.Color != piece.Color;
			}
			else
			{
				return false;
			}
		}

		public static bool BishopValidation(ChessBoard board, ChessBoardTurn turn)
		{
			ChessPiece.Core.ChessPiece piece = turn.GetFirstPiece();
			ChessBoardMove move = turn.GetFirstMove();

			int v = move.NewSquare.Row.GetDescriptionFromEnum() - move.CurrentSquare.Row.GetDescriptionFromEnum(); // Vertical difference
			int h = move.NewSquare.Column.GetDescriptionFromEnum() - move.CurrentSquare.Column.GetDescriptionFromEnum(); // Horizontal difference

			if (v == 0 && h == 0)
			{
				throw new ChessNoMoveException(board, move);
			}

			// If moving diagonal
			if (Math.Abs(v) == Math.Abs(h))
			{
				// These vars are always 1 or -1
				var stepV = Math.Abs(v) / v;
				var stepH = Math.Abs(h) / h;

				// A bit too difficult for loop to explain
				for (int i = move.CurrentSquare.Row.GetDescriptionFromEnum() + stepV, j = move.CurrentSquare.Column.GetDescriptionFromEnum() + stepH; Math.Abs(i - move.NewSquare.Row.GetDescriptionFromEnum()) >= 0; i += stepV, j += stepH)
				{
					if (board.Board[j, i].ChessPiece is not null || i == move.NewSquare.Row.GetDescriptionFromEnum() && j == move.NewSquare.Column.GetDescriptionFromEnum())
					{
						if (piece.Color == board.Board[j, i].ChessPiece?.Color)
						{
							throw new ChessSameColorException(board, move);
						}

						return i == move.NewSquare.Row.GetDescriptionFromEnum() && j == move.NewSquare.Column.GetDescriptionFromEnum();
					}
				}

				// This return will never be reached (in theory)
				return false;
			}
			else
			{
				return false;
			}
		}

		public static bool KingValidation(ChessBoard board, ChessBoardTurn turn)
		{
			ChessPiece.Core.ChessPiece piece = turn.GetFirstPiece();
			ChessBoardMove move = turn.GetFirstMove();

			if (Math.Abs(move.NewSquare.Column.GetDescriptionFromEnum() - move.CurrentSquare.Column.GetDescriptionFromEnum()) < 2 && Math.Abs(move.NewSquare.Row.GetDescriptionFromEnum() - move.CurrentSquare.Row.GetDescriptionFromEnum()) < 2)
			{
				if (board.Board[move.NewSquare.Column.GetDescriptionFromEnum(), move.NewSquare.Row.GetDescriptionFromEnum()].ChessPiece == null)
				{
					return true;
				}

				// Piece has different color than king
				return board.Board[move.NewSquare.Column.GetDescriptionFromEnum(), move.NewSquare.Row.GetDescriptionFromEnum()].ChessPiece?.Color != piece.Color;
			}

			// Check if king is on begin pos
			if (move.CurrentSquare.Column.GetDescriptionFromEnum() == 4 && move.CurrentSquare.Row.GetDescriptionFromEnum() % 7 == 0
			                                                           && move.CurrentSquare.Row.GetDescriptionFromEnum() == move.NewSquare.Row.GetDescriptionFromEnum())
			{
				// if drop on rooks position to castle
				// OR drop on kings new position after castle
				if (move.NewSquare.Column.GetDescriptionFromEnum() % 7 == 0 && move.NewSquare.Row.GetDescriptionFromEnum() % 7 == 0
				    || Math.Abs(move.NewSquare.Column.GetDescriptionFromEnum() - move.CurrentSquare.Column.GetDescriptionFromEnum()) == 2)
				{
					switch (piece.Color)
					{
						case ChessPieceColor.White:

							// Queen Castle
							if (move.NewSquare.Column.GetDescriptionFromEnum() == 0 || move.NewSquare.Column.GetDescriptionFromEnum() == 2)
							{
								if (!HasRightToCastle(board, piece))
								{
									return false;
								}

								if (board.Board[1, 0].ChessPiece == null && board.Board[2, 0].ChessPiece == null && board.Board[3, 0].ChessPiece == null)
								{
									return true;
								}
							}

							// King Castle
							if (move.NewSquare.Column.GetDescriptionFromEnum() == 7 || move.NewSquare.Column.GetDescriptionFromEnum() == 6)
							{
								if (!HasRightToCastle(board, piece))
								{
									return false;
								}

								if (board.Board[0, 5].ChessPiece == null && board.Board[0, 6].ChessPiece == null)
								{
									return true;
								}
							}

							break;
						case ChessPieceColor.Black:

							//Queen Castle
							if (move.NewSquare.Column.GetDescriptionFromEnum() == 0 || move.NewSquare.Column.GetDescriptionFromEnum() == 2)
							{
								if (!HasRightToCastle(board, piece))
								{
									return false;
								}

								if (board.Board[1, 7].ChessPiece == null && board.Board[2, 7].ChessPiece == null && board.Board[3, 7].ChessPiece == null)
								{
									return true;
								}
							}

							//King Castle
							if (move.NewSquare.Column.GetDescriptionFromEnum() == 7 || move.NewSquare.Column.GetDescriptionFromEnum() == 6)
							{
								if (!HasRightToCastle(board, piece))
								{
									return false;
								}

								if (board.Board[7, 5].ChessPiece == null && board.Board[7, 6].ChessPiece == null)
								{
									return true;
								}
							}

							break;
					}
				}
			}

			return false;
		}

		private static bool HasRightToCastle(ChessBoard board, ChessPiece.Core.ChessPiece king)
		{
			ChessBoardSquare rookBoardSquare = new ChessBoardSquare(7, (short)(king.Color == ChessPieceColor.White ? 0 : 7));

			return board.Board[rookBoardSquare.Column.GetDescriptionFromEnum(), rookBoardSquare.Row.GetDescriptionFromEnum()].ChessPiece != null
			       && board.Board[rookBoardSquare.Column.GetDescriptionFromEnum(), rookBoardSquare.Row.GetDescriptionFromEnum()].ChessPiece.Type == ChessPieceType.Rook
			       && board.Board[rookBoardSquare.Column.GetDescriptionFromEnum(), rookBoardSquare.Row.GetDescriptionFromEnum()].ChessPiece.Color == king.Color
			       && !king.HasPieceBeenMoved & !board.Board[rookBoardSquare.Column.GetDescriptionFromEnum(), rookBoardSquare.Row.GetDescriptionFromEnum()].ChessPiece.HasPieceBeenMoved;
		}
	}
}
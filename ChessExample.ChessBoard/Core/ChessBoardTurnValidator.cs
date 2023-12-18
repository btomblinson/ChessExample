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
			ChessBoardMove move = turn.GetFirstPieceFirstMove();

			int v = move.NewSpace.Row.GetDescriptionFromEnum() - move.CurrentSpace.Row.GetDescriptionFromEnum(); // Vertical difference
			int h = move.NewSpace.Column.GetDescriptionFromEnum() - move.CurrentSpace.Column.GetDescriptionFromEnum(); // Horizontal difference

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
				            && board.Board[move.NewSpace.Column.GetDescriptionFromEnum(), move.NewSpace.Row.GetDescriptionFromEnum()].Item2 is null:

					return true;
				// 2 steps forward if in the beginning
				case 0 when stepV == 2
				            && (move.CurrentSpace.Row.GetDescriptionFromEnum() == 1
				                && board.Board[move.NewSpace.Column.GetDescriptionFromEnum(), 2].Item2 is null
				                && board.Board[move.NewSpace.Column.GetDescriptionFromEnum(), 3].Item2 is null
				                || move.CurrentSpace.Row.GetDescriptionFromEnum() == 6
				                && board.Board[move.NewSpace.Column.GetDescriptionFromEnum(), 5].Item2 is null
				                && board.Board[move.NewSpace.Column.GetDescriptionFromEnum(), 4].Item2 is null):
					return true;
				// Second condition horizontal taking piece
				default:
				{
					if (stepV == 1 && stepH == 1
					               && board.Board[move.NewSpace.Column.GetDescriptionFromEnum(), move.NewSpace.Row.GetDescriptionFromEnum()].Item2 is not null
					               && piece.Color != board.Board[move.NewSpace.Column.GetDescriptionFromEnum(), move.NewSpace.Row.GetDescriptionFromEnum()]?.Item2?.Color)
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
			ChessBoardMove move = turn.GetFirstPieceFirstMove();

			int v = move.NewSpace.Row.GetDescriptionFromEnum() - move.CurrentSpace.Row.GetDescriptionFromEnum(); // Vertical difference
			int h = move.NewSpace.Column.GetDescriptionFromEnum() - move.CurrentSpace.Column.GetDescriptionFromEnum(); // Horizontal difference

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
				for (int i = move.CurrentSpace.Row.GetDescriptionFromEnum() + stepV, j = move.CurrentSpace.Column.GetDescriptionFromEnum() + stepH;
				     Math.Abs(i - move.NewSpace.Row.GetDescriptionFromEnum() - (j - move.NewSpace.Column.GetDescriptionFromEnum())) >= 0;
				     i += stepV, j += stepH)
				{
					if (board.Board[j, i].Item2 is not null || i == move.NewSpace.Row.GetDescriptionFromEnum() && j == move.NewSpace.Column.GetDescriptionFromEnum())
					{
						if (piece.Color == board.Board[j, i].Item2?.Color)
						{
							if (turn.IsCastle)
							{
								continue;
							}

							throw new ChessSameColorException(board, move);
						}

						return i == move.NewSpace.Row.GetDescriptionFromEnum() && j == move.NewSpace.Column.GetDescriptionFromEnum();
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
			ChessBoardMove move = turn.GetFirstPieceFirstMove();

			// New position must be with stepH = 1 and steV = 2 or vice versa
			if (Math.Abs(move.NewSpace.Column.GetDescriptionFromEnum() - move.CurrentSpace.Column.GetDescriptionFromEnum()) == 2 && Math.Abs(move.NewSpace.Row.GetDescriptionFromEnum() - move.CurrentSpace.Row.GetDescriptionFromEnum()) == 1
			    || Math.Abs(move.NewSpace.Column.GetDescriptionFromEnum() - move.CurrentSpace.Column.GetDescriptionFromEnum()) == 1 && Math.Abs(move.NewSpace.Row.GetDescriptionFromEnum() - move.CurrentSpace.Row.GetDescriptionFromEnum()) == 2)
			{
				if (board.Board[move.NewSpace.Column.GetDescriptionFromEnum(), move.NewSpace.Row.GetDescriptionFromEnum()].Item2 == null)
				{
					return true;
				}

				return board.Board[move.NewSpace.Column.GetDescriptionFromEnum(), move.NewSpace.Row.GetDescriptionFromEnum()].Item2?.Color != piece.Color;
			}
			else
			{
				return false;
			}
		}

		public static bool BishopValidation(ChessBoard board, ChessBoardTurn turn)
		{
			ChessPiece.Core.ChessPiece piece = turn.GetFirstPiece();
			ChessBoardMove move = turn.GetFirstPieceFirstMove();

			int v = move.NewSpace.Row.GetDescriptionFromEnum() - move.CurrentSpace.Row.GetDescriptionFromEnum(); // Vertical difference
			int h = move.NewSpace.Column.GetDescriptionFromEnum() - move.CurrentSpace.Column.GetDescriptionFromEnum(); // Horizontal difference

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
				for (int i = move.CurrentSpace.Row.GetDescriptionFromEnum() + stepV, j = move.CurrentSpace.Column.GetDescriptionFromEnum() + stepH; Math.Abs(i - move.NewSpace.Row.GetDescriptionFromEnum()) >= 0; i += stepV, j += stepH)
				{
					if (board.Board[j, i].Item2 is not null || i == move.NewSpace.Row.GetDescriptionFromEnum() && j == move.NewSpace.Column.GetDescriptionFromEnum())
					{
						if (piece.Color == board.Board[j, i].Item2?.Color)
						{
							throw new ChessSameColorException(board, move);
						}

						return i == move.NewSpace.Row.GetDescriptionFromEnum() && j == move.NewSpace.Column.GetDescriptionFromEnum();
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
			ChessBoardMove move = turn.GetFirstPieceFirstMove();

			if (Math.Abs(move.NewSpace.Column.GetDescriptionFromEnum() - move.CurrentSpace.Column.GetDescriptionFromEnum()) < 2 && Math.Abs(move.NewSpace.Row.GetDescriptionFromEnum() - move.CurrentSpace.Row.GetDescriptionFromEnum()) < 2)
			{
				if (board.Board[move.NewSpace.Column.GetDescriptionFromEnum(), move.NewSpace.Row.GetDescriptionFromEnum()].Item2 == null)
				{
					return true;
				}

				// Piece has different color than king
				return board.Board[move.NewSpace.Column.GetDescriptionFromEnum(), move.NewSpace.Row.GetDescriptionFromEnum()].Item2?.Color != piece.Color;
			}

			// Check if king is on begin pos
			if (move.CurrentSpace.Column.GetDescriptionFromEnum() == 4 && move.CurrentSpace.Row.GetDescriptionFromEnum() % 7 == 0
			                                                           && move.CurrentSpace.Row.GetDescriptionFromEnum() == move.NewSpace.Row.GetDescriptionFromEnum())
			{
				// if drop on rooks position to castle
				// OR drop on kings new position after castle
				if (move.NewSpace.Column.GetDescriptionFromEnum() % 7 == 0 && move.NewSpace.Row.GetDescriptionFromEnum() % 7 == 0
				    || Math.Abs(move.NewSpace.Column.GetDescriptionFromEnum() - move.CurrentSpace.Column.GetDescriptionFromEnum()) == 2)
				{
					switch (piece.Color)
					{
						case ChessPieceColor.White:

							// Queen Castle
							if (move.NewSpace.Column.GetDescriptionFromEnum() == 0 || move.NewSpace.Column.GetDescriptionFromEnum() == 2)
							{
								if (!HasRightToCastle(board, piece))
								{
									return false;
								}

								if (board.Board[1, 0].Item2 == null && board.Board[2, 0].Item2 == null && board.Board[3, 0].Item2 == null)
								{
									return true;
								}
							}

							// King Castle
							if (move.NewSpace.Column.GetDescriptionFromEnum() == 7 || move.NewSpace.Column.GetDescriptionFromEnum() == 6)
							{
								if (!HasRightToCastle(board, piece))
								{
									return false;
								}

								if (board.Board[0, 5].Item2 == null && board.Board[0, 6].Item2 == null)
								{
									return true;
								}
							}

							break;
						case ChessPieceColor.Black:

							//Queen Castle
							if (move.NewSpace.Column.GetDescriptionFromEnum() == 0 || move.NewSpace.Column.GetDescriptionFromEnum() == 2)
							{
								if (!HasRightToCastle(board, piece))
								{
									return false;
								}

								if (board.Board[1, 7].Item2 == null && board.Board[2, 7].Item2 == null && board.Board[3, 7].Item2 == null)
								{
									return true;
								}
							}

							//King Castle
							if (move.NewSpace.Column.GetDescriptionFromEnum() == 7 || move.NewSpace.Column.GetDescriptionFromEnum() == 6)
							{
								if (!HasRightToCastle(board, piece))
								{
									return false;
								}

								if (board.Board[7, 5].Item2 == null && board.Board[7, 6].Item2 == null)
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
			ChessBoardSpace rookBoardSpace = new ChessBoardSpace(7, (short)(king.Color == ChessPieceColor.White ? 0 : 7));

			return board.Board[rookBoardSpace.Column.GetDescriptionFromEnum(), rookBoardSpace.Row.GetDescriptionFromEnum()].Item2 != null
			       && board.Board[rookBoardSpace.Column.GetDescriptionFromEnum(), rookBoardSpace.Row.GetDescriptionFromEnum()].Item2.Type == ChessPieceType.Rook
			       && board.Board[rookBoardSpace.Column.GetDescriptionFromEnum(), rookBoardSpace.Row.GetDescriptionFromEnum()].Item2.Color == king.Color
			       && !king.HasPieceBeenMoved & !board.Board[rookBoardSpace.Column.GetDescriptionFromEnum(), rookBoardSpace.Row.GetDescriptionFromEnum()].Item2.HasPieceBeenMoved;
		}
	}
}
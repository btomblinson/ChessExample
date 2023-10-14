using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessExample.ChessBoard.Core;
using ChessExample.ChessBoard.Exceptions;
using ChessExample.ChessPiece.Enums;
using ChessExample.Utilities.Extensions;

namespace ChessExample.ChessBoard.Validations
{
	public static class ChessBoardMoveValidator
	{
		public static bool PawnValidation(ChessBoardMove move, ChessBoard board)
		{
			int v = move.NewSpace.Row.GetDescriptionFromEnum() - move.CurrentSpace.Row.GetDescriptionFromEnum(); // Vertical difference
			int h = move.NewSpace.Column.GetDescriptionFromEnum() - move.CurrentSpace.Column.GetDescriptionFromEnum(); // Horizontal difference

			int stepV = Math.Abs(v);
			int stepH = Math.Abs(h);

			// If moving forwards
			if ((move.CurrentPiece.Color != ChessPieceColor.White || v <= 0) && (move.CurrentPiece.Color != ChessPieceColor.Black || v >= 0))
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
				            && ((move.CurrentSpace.Row.GetDescriptionFromEnum() == 1
				                 && board.Board[move.NewSpace.Column.GetDescriptionFromEnum(), 2].Item2 is null
				                 && board.Board[move.NewSpace.Column.GetDescriptionFromEnum(), 3].Item2 is null)
				                || (move.CurrentSpace.Row.GetDescriptionFromEnum() == 6
				                    && board.Board[move.NewSpace.Column.GetDescriptionFromEnum(), 5].Item2 is null
				                    && board.Board[move.NewSpace.Column.GetDescriptionFromEnum(), 4].Item2 is null)):
					return true;
				// Second condition horizontal taking piece
				default:
				{
					if (stepV == 1 && stepH == 1
					               && board.Board[move.NewSpace.Column.GetDescriptionFromEnum(), move.NewSpace.Row.GetDescriptionFromEnum()].Item2 is not null
					               && move.CurrentPiece.Color != board.Board[move.NewSpace.Column.GetDescriptionFromEnum(), move.NewSpace.Row.GetDescriptionFromEnum()]?.Item2?.Color)
					{
						return true;
					}

					break;
				}
			}

			return false;
		}

		public static bool QueenValidation(ChessBoardMove move, ChessBoard board)
		{
			// For queen just using validation of bishop OR rook
			return BishopValidation(move, board) || RookValidation(move, board);
		}

		public static bool RookValidation(ChessBoardMove move, ChessBoard board)
		{
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
					if (board.Board[j, i].Item2 is not null || (i == move.NewSpace.Row.GetDescriptionFromEnum() && j == move.NewSpace.Column.GetDescriptionFromEnum()))
					{
						if (move.CurrentPiece.Color == board.Board[j, i].Item2?.Color)
						{
							if (move.IsCastle)
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

		public static bool KnightValidation(ChessBoardMove move, ChessBoard board)
		{
			// New position must be with stepH = 1 and steV = 2 or vice versa
			if ((Math.Abs(move.NewSpace.Column.GetDescriptionFromEnum() - move.CurrentSpace.Column.GetDescriptionFromEnum()) == 2 && Math.Abs(move.NewSpace.Row.GetDescriptionFromEnum() - move.CurrentSpace.Row.GetDescriptionFromEnum()) == 1)
			    || (Math.Abs(move.NewSpace.Column.GetDescriptionFromEnum() - move.CurrentSpace.Column.GetDescriptionFromEnum()) == 1 && Math.Abs(move.NewSpace.Row.GetDescriptionFromEnum() - move.CurrentSpace.Row.GetDescriptionFromEnum()) == 2))
			{
				if (board.Board[move.NewSpace.Column.GetDescriptionFromEnum(), move.NewSpace.Row.GetDescriptionFromEnum()].Item2 == null)
				{
					return true;
				}

				return board.Board[move.NewSpace.Column.GetDescriptionFromEnum(), move.NewSpace.Row.GetDescriptionFromEnum()].Item2?.Color != move.CurrentPiece.Color;
			}
			else
			{
				return false;
			}
		}

		public static bool BishopValidation(ChessBoardMove move, ChessBoard board)
		{
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
					if (board.Board[j, i].Item2 is not null || (i == move.NewSpace.Row.GetDescriptionFromEnum() && j == move.NewSpace.Column.GetDescriptionFromEnum()))
					{
						if (move.CurrentPiece.Color == board.Board[j, i].Item2?.Color)
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

		public static bool KingValidation(ChessBoardMove move, ChessBoard board)
		{
			if (Math.Abs(move.NewSpace.Column.GetDescriptionFromEnum() - move.CurrentSpace.Column.GetDescriptionFromEnum()) < 2 && Math.Abs(move.NewSpace.Row.GetDescriptionFromEnum() - move.CurrentSpace.Row.GetDescriptionFromEnum()) < 2)
			{
				if (board.Board[move.NewSpace.Column.GetDescriptionFromEnum(), move.NewSpace.Row.GetDescriptionFromEnum()].Item2 == null)
				{
					return true;
				}

				// Piece has different color than king
				return board.Board[move.NewSpace.Column.GetDescriptionFromEnum(), move.NewSpace.Row.GetDescriptionFromEnum()].Item2?.Color != move.CurrentPiece.Color;
			}

			// Check if king is on begin pos
			if (move.CurrentSpace.Column.GetDescriptionFromEnum() == 4 && move.CurrentSpace.Row.GetDescriptionFromEnum() % 7 == 0
			                                                           && move.CurrentSpace.Row.GetDescriptionFromEnum() == move.NewSpace.Row.GetDescriptionFromEnum())
			{
				// if drop on rooks position to castle
				// OR drop on kings new position after castle
				if ((move.NewSpace.Column.GetDescriptionFromEnum() % 7 == 0 && move.NewSpace.Row.GetDescriptionFromEnum() % 7 == 0)
				    || (Math.Abs(move.NewSpace.Column.GetDescriptionFromEnum() - move.CurrentSpace.Column.GetDescriptionFromEnum()) == 2))
				{
					switch (move.CurrentPiece.Color)
					{
						case ChessPieceColor.White:

							//// Queen Castle
							//if (move.NewSpace.Column.GetDescriptionFromEnum() == 0 || move.NewSpace.Column.GetDescriptionFromEnum() == 2)
							//{
							//	if (!HasRightToCastle(move.CurrentPiece, board))
							//	{
							//		return false;
							//	}

							//	if (board.Board[0, 1] is null && board.pieces[0, 2] is null && board.pieces[0, 3] is null)
							//	{
							//		return true;
							//	}
							//}
							//// King Castle
							//else if (move.NewSpace.Column.GetDescriptionFromEnum() == 7 || move.NewSpace.Column.GetDescriptionFromEnum() == 6)
							//{
							//	move.Parameter = new MoveCastle(CastleType.King);

							//	if (!HasRightToCastle(PieceColor.White, CastleType.King, board))
							//	{
							//		return false;
							//	}

							//	if (board.pieces[0, 5] is null && board.pieces[0, 6] is null)
							//	{
							//		return true;
							//	}
							//}

							break;
						case ChessPieceColor.Black:

							// Queen Castle
							//if (move.NewSpace.Column.GetDescriptionFromEnum() == 0 || move.NewSpace.Column.GetDescriptionFromEnum() == 2)
							//{
							//	move.Parameter = new MoveCastle(CastleType.Queen);

							//	if (!HasRightToCastle(PieceColor.Black, CastleType.Queen, board))
							//	{
							//		return false;
							//	}

							//	if (board.pieces[7, 1] is null && board.pieces[7, 2] is null && board.pieces[7, 3] is null)
							//	{
							//		return true;
							//	}
							//}
							// King Castle
							//else 
							if (move.NewSpace.Column.GetDescriptionFromEnum() == 7 || move.NewSpace.Column.GetDescriptionFromEnum() == 6)
							{
								if (!HasRightToCastle(board, move.CurrentPiece))
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
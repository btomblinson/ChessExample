using ChessExample.ChessBoard.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ChessExample.ChessBoard.Core;
using ChessExample.Utilities;
using ChessExample.ChessBoard.Enums;
using ChessExample.ChessPiece.Enums;
using ChessExample.Utilities.Extensions;

namespace ChessExample.ChessBoard.Builders
{
	public static class SanBuilder
	{
		public static bool TryParse(ChessBoard board, string san, out List<ChessBoardMove> moves)
		{
			bool isCapture = false;
			bool isCastle = false;
			bool isCheck = false;
			bool isCheckMate = false;

			moves = new List<ChessBoardMove>();
			var matches = Regexes.RegexSanOneMove.Matches(san);

			ChessBoardSpace originalSpace = null;
			ChessBoardSpace newSpace = null;
			ChessPiece.Core.ChessPiece piece = null;

			if (matches.Count == 0)
			{
				throw new ChessInvalidArgumentException(board, "SAN move string should match pattern: " + Regexes.SanMovesPattern);
			}

			foreach (var group in matches[0].Groups.Values)
			{
				if (!group.Success)
				{
					continue;
				}

				switch (group.Name)
				{
					case "1":
						if (group.Value is "O-O" or "O-O-O")
						{
							moves.AddRange(ParseCastling(board, group.Value));
							isCastle = true;
						}

						break;
					case "2":
						piece = new ChessPiece.Core.ChessPiece(group.Value[0].ToString().GetEnumFromDisplayName<ChessPieceType>(), board.CurrentMoveColor);
						break;
					case "3":
						originalSpace = new ChessBoardSpace();
						originalSpace.SetColumn(Convert.ToInt32(group.Value[0].ToString().GetDescriptionFromValue<ChessBoardColumn>()));
						break;
					case "4":
						originalSpace = new ChessBoardSpace();
						originalSpace.SetRow(Convert.ToInt32(group.Value[0].ToString()));
						break;
					case "5":
						if (group.Value is "x" or "X")
						{
							isCapture = true;
						}

						break;
					case "6":
						int col = group.Value[0].ToString().GetDescriptionFromValue<ChessBoardColumn>();
						int row = Convert.ToInt32(group.Value[1].ToString()) - 1;
						newSpace = new ChessBoardSpace(col, row);
						break;

					case "9":
						if (group.Value is "+")
						{
							isCheck = true;
						}
						else if (group.Value is "#")
						{
							isCheck = true;
							isCheckMate = true;
						}
						else if (group.Value is "$")
						{
							isCheckMate = true;
						}

						break;
				}

				if (isCastle)
				{
					break;
				}
			}

			if (isCastle)
			{
				return true;
			}

			// If piece is not specified => Pawn
			if (piece == null)
			{
				piece = new ChessPiece.Core.ChessPiece(ChessPieceType.Pawn, board.CurrentMoveColor);
			}

			ParseOriginalPosition(board, piece, newSpace, ref originalSpace);

			ChessBoardMove move = new ChessBoardMove(piece, originalSpace, newSpace);
			move.IsCapture = isCapture;
			move.IsCheck = isCheck;
			move.IsCheckmate = isCheckMate;

			moves.Add(move);
			return true;
		}

		private static void ParseOriginalPosition(ChessBoard board, ChessPiece.Core.ChessPiece piece, ChessBoardSpace newSpace, ref ChessBoardSpace? originalSpace)
		{
			List<ChessBoardMove> ambiguousMoves = GetMovesOfPieceOnPosition(piece, newSpace, board).ToList();

			if (originalSpace == null)
			{
				if (ambiguousMoves.Count == 0)
				{
					throw new ArgumentException("No valid moves");
				}

				originalSpace = new ChessBoardSpace(ambiguousMoves[0].CurrentSpace.Column.GetDescriptionFromEnum(), ambiguousMoves[0].CurrentSpace.Row.GetDescriptionFromEnum());
			}

			if (originalSpace.HasColumn)
			{
				var originalCol = originalSpace.Column;
				ambiguousMoves = ambiguousMoves.Where(m => m.CurrentSpace.Column == originalCol).ToList();

				if (ambiguousMoves.Count != 1)
				{
					throw new ArgumentException("No valid moves");
				}

				originalSpace.Row = ambiguousMoves.ElementAt(0).CurrentSpace.Row;
			}
			else if (originalSpace.HasRow)
			{
				var originalRow = originalSpace.Row;
				ambiguousMoves = ambiguousMoves.Where(m => m.CurrentSpace.Row == originalRow).ToList();

				if (ambiguousMoves.Count != 1)
				{
					throw new ArgumentException("No valid moves");
				}

				originalSpace.Column = ambiguousMoves.ElementAt(0).CurrentSpace.Column;
			}
		}

		private static IEnumerable<ChessBoardMove> GetMovesOfPieceOnPosition(ChessPiece.Core.ChessPiece piece, ChessBoardSpace newPosition, ChessBoard board)
		{
			for (short x = 0; x < 8; x++)
			{
				for (short y = 0; y < 8; y++)
				{
					if (board.Board[x, y].Item2 != null
					    && board.Board[x, y].Item2.Color == piece.Color
					    && board.Board[x, y].Item2.Type == piece.Type)
					{
						// if original pos == new pos
						if (newPosition.Column.GetDescriptionFromEnum() == x && newPosition.Row.GetDescriptionFromEnum() == y)
						{
							continue;
						}

						ChessBoardMove move = new ChessBoardMove(piece, new ChessBoardSpace(x, y), newPosition);

						//TODO: Add this back
						//if (board.IsValidMove(move) && !board.IsKingCheckedValidation(move, piece.Color, board))
						//{
						//	yield return move;
						//}

						bool valid;
						try
						{
							valid = board.IsValidMove(move);
						}
						catch (ChessException e)
						{
							continue;
						}

						if (valid)
						{
							yield return move;
						}
					}
				}
			}
		}

		private static List<ChessBoardMove> ParseCastling(ChessBoard board, string value)
		{
			ChessBoardMove kingMove = new ChessBoardMove();
			ChessBoardMove rookMove = new ChessBoardMove();
			if (board.CurrentMoveColor == ChessPieceColor.White)
			{
				kingMove.CurrentPiece = new ChessPiece.Core.ChessPiece(ChessPieceType.King, ChessPieceColor.White);
				rookMove.CurrentPiece = new ChessPiece.Core.ChessPiece(ChessPieceType.Rook, ChessPieceColor.White);

				string space = "e1";
				int col = space[0].GetDescriptionFromValue<ChessBoardColumn>();
				int row = Convert.ToInt32(space[1].ToString()) - 1;

				kingMove.CurrentSpace = new ChessBoardSpace(col, row);

				if (value == "O-O")
				{
					space = "h1";
					col = space[0].GetDescriptionFromValue<ChessBoardColumn>();
					row = Convert.ToInt32(space[1].ToString()) - 1;
					kingMove.NewSpace = new ChessBoardSpace(col, row);
					rookMove.CurrentSpace = kingMove.NewSpace;
					rookMove.NewSpace = new ChessBoardSpace(col + 1, row);
				}
				else if (value == "O-O-O")
				{
					space = "a1";
					col = space[0].GetDescriptionFromValue<ChessBoardColumn>();
					row = Convert.ToInt32(space[1].ToString()) - 1;
					kingMove.NewSpace = new ChessBoardSpace(col, row);
				}

				return new List<ChessBoardMove> { kingMove, rookMove };
			}

			if (board.CurrentMoveColor == ChessPieceColor.Black)
			{
				kingMove.CurrentPiece = new ChessPiece.Core.ChessPiece(ChessPieceType.King, ChessPieceColor.Black);
				rookMove.CurrentPiece = new ChessPiece.Core.ChessPiece(ChessPieceType.Rook, ChessPieceColor.Black);

				string space = "e8";
				int col = space[0].GetDescriptionFromValue<ChessBoardColumn>();
				int row = Convert.ToInt32(space[1].ToString()) - 1;

				kingMove.CurrentSpace = new ChessBoardSpace(col, row);

				if (value == "O-O")
				{
					space = "h8";
					col = space[0].GetDescriptionFromValue<ChessBoardColumn>() - 1;
					row = Convert.ToInt32(space[1].ToString()) - 1;
					kingMove.NewSpace = new ChessBoardSpace(col, row);

					col = space[0].GetDescriptionFromValue<ChessBoardColumn>();
					row = Convert.ToInt32(space[1].ToString()) - 1;

					rookMove.CurrentSpace = new ChessBoardSpace(col, row);
					rookMove.NewSpace = new ChessBoardSpace(col - 2, row);
				}
				else if (value == "O-O-O")
				{
					space = "a8";
					col = space[0].GetDescriptionFromValue<ChessBoardColumn>();
					row = Convert.ToInt32(space[1].ToString()) - 1;
					kingMove.NewSpace = new ChessBoardSpace(col, row);
				}

				return new List<ChessBoardMove> { kingMove, rookMove };
			}

			throw new ChessInvalidMoveException(board, "Invalid castle.", kingMove);
		}
	}
}
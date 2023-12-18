using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessExample.ChessBoard.Enums;
using ChessExample.ChessPiece.Enums;
using ChessExample.Utilities.Extensions;

namespace ChessExample.ChessBoard.Core.EndGame
{
    public class InsufficientMaterialRule : EndGameRule
	{
		public override ChessBoardTurnResultType Type => ChessBoardTurnResultType.InsufficientMaterial;

		public InsufficientMaterialRule(ChessBoard board) : base(board)
		{
		}

		public override bool IsEndGame()
		{
			List<ChessPiece.Core.ChessPiece> pieces = new();
			for (int y = 0; y < 8; y++)
			{
				for (int x = 0; x < 8; x++)
				{
					if (Board.Board[x, y].ChessPiece is not null)
					{
						pieces.Add(Board.Board[x, y].ChessPiece);
					}
				}
			}

			return IsFirstLevelDraw(pieces)
			       || IsSecondLevelDraw(pieces)
			       || IsThirdLevelDraw(pieces);
		}

		private bool IsFirstLevelDraw(List<ChessPiece.Core.ChessPiece> pieces)
		{
			return pieces.All(p => p.Type == ChessPieceType.King);
		}

		private bool IsSecondLevelDraw(List<ChessPiece.Core.ChessPiece> pieces)
		{
			var hasStrongPieces = pieces.Count(p => p.Type == ChessPieceType.Pawn
			                                        || p.Type == ChessPieceType.Queen
			                                        || p.Type == ChessPieceType.Rook) > 0;

			// The only piece remaining will be Bishop or Knight, what results in draw
			return !hasStrongPieces && pieces.Count(p => p.Type != ChessPieceType.King) == 1;
			;
		}

		private bool IsThirdLevelDraw(List<ChessPiece.Core.ChessPiece> pieces)
		{
			var isDraw = false;

			if (pieces.Count == 4)
			{
				if (pieces.All(p => p.Type == ChessPieceType.King || p.Type == ChessPieceType.Bishop))
				{
					var firstPiece = pieces.First(p => p.Type == ChessPieceType.Bishop);
					var lastPiece = pieces.Last(p => p.Type == ChessPieceType.Bishop);

					isDraw = firstPiece.Color != lastPiece.Color && BishopsAreOnSameColor();
				}
				else if (pieces.All(p => p.Type == ChessPieceType.King || p.Type == ChessPieceType.Knight))
				{
					var firstPiece = pieces.First(p => p.Type == ChessPieceType.Knight);
					var lastPiece = pieces.Last(p => p.Type == ChessPieceType.Knight);

					isDraw = firstPiece.Color == lastPiece.Color;
				}
			}

			return isDraw;
		}

		private bool BishopsAreOnSameColor()
		{
			var bishopsCoords = new List<ChessBoardSpace>();

			for (short y = 0; y < 8 && bishopsCoords.Count < 2; y++)
			{
				for (short x = 0; x < 8 && bishopsCoords.Count < 2; x++)
				{
					if (Board.Board[x, y].ChessPiece.Type == ChessPieceType.Bishop)
					{
						bishopsCoords.Add(new ChessBoardSpace(x, y));
					}
				}
			}

			return (bishopsCoords[0].Column.GetDescriptionFromEnum() + bishopsCoords[1].Column.GetDescriptionFromEnum() + bishopsCoords[0].Row.GetDescriptionFromEnum() + bishopsCoords[1].Row.GetDescriptionFromEnum()) % 2 == 0;
		}
	}
}
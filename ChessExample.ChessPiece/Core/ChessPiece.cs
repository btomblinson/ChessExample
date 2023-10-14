using ChessExample.ChessPiece.Enums;
using ChessExample.Utilities.Extensions;

namespace ChessExample.ChessPiece.Core
{
	public class ChessPiece : IEquatable<ChessPiece>
	{
		public ChessPieceType Type { get; set; }

		public ChessPieceColor Color { get; set; }

		public bool HasPieceBeenMoved { get; set; }

		public ChessPiece(ChessPieceType type, ChessPieceColor color)
		{
			HasPieceBeenMoved = false;
			Type = type;
			Color = color;
		}

		public bool Equals(ChessPiece? other)
		{
			if (ReferenceEquals(null, other))
			{
				return false;
			}

			if (ReferenceEquals(this, other))
			{
				return true;
			}

			return Type == other.Type && Color == other.Color;
		}

		public override bool Equals(object? obj)
		{
			if (ReferenceEquals(null, obj))
			{
				return false;
			}

			if (ReferenceEquals(this, obj))
			{
				return true;
			}

			if (obj.GetType() != this.GetType())
			{
				return false;
			}

			return Equals((ChessPiece)obj);
		}

		public override int GetHashCode()
		{
			return HashCode.Combine((int)Type, (int)Color);
		}

		/// <summary>
		/// Piece as FEN char<br/>
		/// Uppercase => White piece<br/>
		/// Lowercase => Black piece<br/>
		/// See: new Piece(char fenChar)<br/>
		/// </summary>
		public string ToFen()
		{
			if (Color == ChessPieceColor.White)
			{
				return $"W{Type.GetDisplayName()}";
			}
			else
			{
				return $"b{Type.GetDisplayName()}";
			}
		}
	}
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessExample.ChessPiece.Enums;
using ChessExample.Utilities.Extensions;

namespace ChessExample.ChessBoard.Core
{
    /// <summary>
    /// Represents a list of pieces and their move(s), this object has multiple uses, it can represent a
    /// parsed SAN that is then executed, or used to generate all possible moves 
    /// </summary>
    public class ChessBoardTurn : List<List<ChessBoardMove>>
    {
        public bool IsCheck { get; set; }

        public bool IsCheckmate { get; set; }

        public bool IsCastle { get; set; }

        public ChessBoardTurn()
        {
        }

        public ChessPiece.Core.ChessPiece GetFirstPiece()
        {
            if (this.Count == 0)
            {
                throw new ArgumentException("Turn is empty");
            }

            return this[0][0].ChessPiece;
        }

        public ChessBoardMove GetFirstMove()
        {
            if (this.Count == 0)
            {
                throw new ArgumentException("Turn is empty");
            }

            return this[0][0];
        }

        public string GenerateSanNotation()
        {
            if (Count == 1)
            {
                switch (this[0][0].ChessPiece.Type)
                {
                    case ChessPieceType.Rook:
                        break;
                    case ChessPieceType.Knight:
                        break;
                    case ChessPieceType.Bishop:
                        break;
                    case ChessPieceType.King:
                        break;
                    case ChessPieceType.Queen:
                        break;
                    case ChessPieceType.Pawn:
                        if (this[0].Any(x => x.IsCapture))
                        {
                            return $"{this[0][0].CurrentSquare.Column.GetValueFromEnum().ToLower()}x{this[0][0].NewSquare}";
                        }

                        return this[0][0].NewSquare.ToString();
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            throw new NotImplementedException();
        }
    }
}
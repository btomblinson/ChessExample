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
    public class ChessBoardTurn : List<Tuple<ChessPiece.Core.ChessPiece, List<ChessBoardMove>>>
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

            return this[0].Item1;
        }

        public Tuple<ChessPiece.Core.ChessPiece, List<ChessBoardMove>> GetFirstPieceTuple()
        {
            if (this.Count == 0)
            {
                throw new ArgumentException("Turn is empty");
            }

            return this[0];
        }

        public List<ChessBoardMove> GetFirstPieceMoves()
        {
            return GetFirstPieceTuple().Item2;
        }

        public ChessBoardMove GetFirstPieceFirstMove()
        {
            return GetFirstPieceTuple().Item2[0];
        }

        public string GenerateSanNotation()
        {
            if (Count == 1)
            {
                switch (this[0].Item1.Type)
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
                        if (this[0].Item2.Any(x => x.IsCapture))
                        {
                            return $"{this[0].Item2[0].CurrentSpace.Column.GetValueFromEnum().ToLower()}x{this[0].Item2[0].NewSpace}";
                        }

                        return this[0].Item2[0].NewSpace.ToString();
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            throw new NotImplementedException();
        }
    }
}
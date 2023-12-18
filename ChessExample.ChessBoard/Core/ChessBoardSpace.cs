using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessExample.ChessBoard.Core
{
    public struct ChessBoardSpace
    {
        public ChessBoardSquare Square { get; private set; }

        public ChessPiece.Core.ChessPiece? ChessPiece { get; private set; }

        public ChessBoardSpace(ChessBoardSquare square, ChessPiece.Core.ChessPiece? piece = null)
        {
            Square = square;
            ChessPiece = piece;
        }
    }
}
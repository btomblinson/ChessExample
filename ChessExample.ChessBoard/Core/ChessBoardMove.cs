using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessExample.ChessBoard.Core
{
    public class ChessBoardMove
    {
        public ChessBoardSquare? CurrentSquare;

        public ChessBoardSquare? NewSquare;

        public ChessPiece.Core.ChessPiece ChessPiece;

        public bool IsCapture { get; set; }

        public ChessBoardMove()
        {
            
        }
        
        public ChessBoardMove(ChessPiece.Core.ChessPiece chessPiece, ChessBoardSquare? currentSquare = null, ChessBoardSquare? newSquare = null)
        {
            CurrentSquare = currentSquare;
            NewSquare = newSquare;
            ChessPiece = chessPiece;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessExample.ChessBoard.Enums;
using ChessExample.Utilities.Extensions;

namespace ChessExample.ChessBoard.Core
{
    public class ChessBoardSpace
    {
        public bool HasColumn { get; private set; }

        public bool HasRow { get; private set; }

        /// <summary>
        /// This represents X axis
        /// </summary>
        public ChessBoardColumn Column { get; set; }

        /// <summary>
        /// This represents Y axis
        /// </summary>
        public ChessBoardRow Row { get; set; }

        public ChessBoardSpaceColor Color { get; private set; }

        public ChessPiece.Core.ChessPiece? ChessPiece { get; private set; }

        public ChessBoardSpace()
        {
            HasColumn = false;
            HasRow = false;
        }

        public ChessBoardSpace(int col, int row, ChessPiece.Core.ChessPiece? piece = null)
        {
            Column = col.GetEnumFromDescription<ChessBoardColumn>();
            Row = row.GetEnumFromDescription<ChessBoardRow>();

            HasColumn = true;
            HasRow = true;

            Color = GetColor();

            if (piece != null)
            {
                SetChessPiece(piece);
            }
        }

        public void SetChessPiece(ChessPiece.Core.ChessPiece piece)
        {
            ChessPiece = piece;
        }

        public void ClearChessPiece()
        {
            ChessPiece = null;
        }

        public ChessBoardSpaceColor GetColor()
        {
            int row = Row.GetDescriptionFromEnum() + 1;
            int col = Column.GetDescriptionFromEnum() + 1;

            if (row.IsOdd() && col.IsOdd())
            {
                return ChessBoardSpaceColor.Black;
            }

            if (row.IsEven() && col.IsEven())
            {
                return ChessBoardSpaceColor.Black;
            }

            return ChessBoardSpaceColor.White;
        }

        public void SetColumn(int col)
        {
            Column = col.GetEnumFromDescription<ChessBoardColumn>();
            HasColumn = true;

            if (HasColumn && HasRow)
            {
                Color = GetColor();
            }
        }

        public void SetRow(int row)
        {
            Row = row.GetEnumFromDescription<ChessBoardRow>();
            HasRow = true;

            if (HasColumn && HasRow)
            {
                Color = GetColor();
            }
        }

        public override string ToString()
        {
            return $"{Column.GetValueFromEnum().ToLower()}{Row.GetDescriptionFromEnum() + 1}";
        }
    }
}
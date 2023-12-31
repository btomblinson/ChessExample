﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessExample.ChessBoard.Enums;
using ChessExample.Utilities.Extensions;

namespace ChessExample.ChessBoard.Core
{
	public class ChessBoardSquare
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

		public ChessBoardSquareColor Color { get; private set; }

		public ChessBoardSquare()
		{
			HasColumn = false;
			HasRow = false;
		}

		public ChessBoardSquare(int col, int row)
		{
			Column = col.GetEnumFromDescription<ChessBoardColumn>();
			Row = row.GetEnumFromDescription<ChessBoardRow>();

			HasColumn = true;
			HasRow = true;

			Color = GetColor();
		}

		public ChessBoardSquareColor GetColor()
		{
			int row = Row.GetDescriptionFromEnum() + 1;
			int col = Column.GetDescriptionFromEnum() + 1;

			if (row.IsOdd() && col.IsOdd())
			{
				return ChessBoardSquareColor.Black;
			}

			if (row.IsEven() && col.IsEven())
			{
				return ChessBoardSquareColor.Black;
			}

			return ChessBoardSquareColor.White;
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
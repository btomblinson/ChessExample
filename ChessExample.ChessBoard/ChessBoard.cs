using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessExample.ChessBoard.Core;
using ChessExample.ChessBoard.Core.EndGame;
using ChessExample.ChessBoard.Enums;
using ChessExample.ChessPiece.Enums;
using ChessExample.Utilities.Extensions;

namespace ChessExample.ChessBoard
{
    public class ChessBoard
    {
        public readonly int NumColumns = 8;

        public readonly int NumRows = 8;

        public ChessBoardSpace[,] Board;

        public List<ChessPiece.Core.ChessPiece> WhiteCaptured;

        public List<ChessPiece.Core.ChessPiece> BlackCaptured;

        public List<string> SanNotationMoves;

        public ChessPieceColor CurrentMoveColor { get; set; }

        public ChessBoard()
        {
            WhiteCaptured = new List<ChessPiece.Core.ChessPiece>();
            BlackCaptured = new List<ChessPiece.Core.ChessPiece>();
            SanNotationMoves = new List<string>();

            Board = new ChessBoardSpace[NumColumns, NumRows];

            InitializeBoard();
        }

        private void InitializeBoard()
        {
            for (int x = 0; x < NumColumns; x++)
            {
                for (int y = 0; y < NumRows; y++)
                {
                    if (y is 0 or 1 or 6 or 7)
                    {
                        ChessPieceType type = ChessPieceType.Pawn;

                        ChessPieceColor color = ChessPieceColor.White;

                        //add white pieces
                        if (y is 0 or 1)
                        {
                            color = ChessPieceColor.White;

                            //if back row do special pieces, otherwise it lets it stay pawns for row 2
                            if (y is 0)
                            {
                                type = x switch
                                {
                                    0 or 7 => ChessPieceType.Rook,
                                    1 or 6 => ChessPieceType.Knight,
                                    2 or 5 => ChessPieceType.Bishop,
                                    3 => ChessPieceType.Queen,
                                    4 => ChessPieceType.King,
                                    _ => type
                                };
                            }
                        }

                        //add black pieces
                        if (y is 6 or 7)
                        {
                            color = ChessPieceColor.Black;

                            //if back row do special pieces, otherwise it lets it stay pawns for row 2
                            if (y is 7)
                            {
                                type = x switch
                                {
                                    0 or 7 => ChessPieceType.Rook,
                                    1 or 6 => ChessPieceType.Knight,
                                    2 or 5 => ChessPieceType.Bishop,
                                    3 => ChessPieceType.Queen,
                                    4 => ChessPieceType.King,
                                    _ => type
                                };
                            }
                        }

                        Board[x, y] = new ChessBoardSpace(new ChessBoardSquare(x, y), new ChessPiece.Core.ChessPiece(type, color));
                    }
                    else
                    {
                        Board[x, y] = new ChessBoardSpace(new ChessBoardSquare(x, y), null);
                    }
                }
            }
        }

        public bool IsValidTurn(ChessBoardTurn turn)
        {
            switch (turn.GetFirstPiece().Type)
            {
                case ChessPieceType.Rook:
                    return ChessBoardTurnValidator.RookValidation(this, turn);
                case ChessPieceType.Knight:
                    return ChessBoardTurnValidator.KnightValidation(this, turn);
                case ChessPieceType.Bishop:
                    return ChessBoardTurnValidator.BishopValidation(this, turn);
                case ChessPieceType.King:
                    return ChessBoardTurnValidator.KingValidation(this, turn);
                case ChessPieceType.Queen:
                    return ChessBoardTurnValidator.QueenValidation(this, turn);
                case ChessPieceType.Pawn:
                    return ChessBoardTurnValidator.PawnValidation(this, turn);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public ChessBoardTurnResult ExecuteTurn(ChessBoardTurn turn)
        {
            ChessBoardTurnResult result = new ChessBoardTurnResult();

            //turn has been validated, update the board

            foreach (List<ChessBoardMove> moves in turn)
            {
                foreach (ChessBoardMove move in moves)
                {
                    //remove piece from original place
                    Board[move.CurrentSquare.Column.GetDescriptionFromEnum(), move.CurrentSquare.Row.GetDescriptionFromEnum()] = new ChessBoardSpace(move.CurrentSquare, null);

                    //move piece to new place

                    //if piece is already there kill it
                    if (Board[move.NewSquare.Column.GetDescriptionFromEnum(), move.NewSquare.Row.GetDescriptionFromEnum()].ChessPiece != null)
                    {
                        if (Board[move.NewSquare.Column.GetDescriptionFromEnum(), move.NewSquare.Row.GetDescriptionFromEnum()].ChessPiece?.Color == ChessPieceColor.Black)
                        {
                            BlackCaptured.Add(Board[move.NewSquare.Column.GetDescriptionFromEnum(), move.NewSquare.Row.GetDescriptionFromEnum()].ChessPiece);
                        }
                        else
                        {
                            WhiteCaptured.Add(Board[move.NewSquare.Column.GetDescriptionFromEnum(), move.NewSquare.Row.GetDescriptionFromEnum()].ChessPiece);
                        }
                    }

                    Board[move.NewSquare.Column.GetDescriptionFromEnum(), move.NewSquare.Row.GetDescriptionFromEnum()] = new ChessBoardSpace(move.NewSquare, move.ChessPiece);

                    move.ChessPiece.HasPieceBeenMoved = true;
                }
            }

            //check endgame scenarios

            //checkmate
            if (turn.IsCheckmate)
            {
                result.IsCheck = true;
                result.IsCheckmate = true;
                result.Result = ChessBoardTurnResultType.Checkmate;
                return result;
            }

            //draw endgame scenarios

            //check InsufficientMaterialRule
            InsufficientMaterialRule rule = new InsufficientMaterialRule(this);
            if (rule.IsEndGame())
            {
                result.Result = ChessBoardTurnResultType.InsufficientMaterial;
                return result;
            }

            SanNotationMoves.Add(turn.GenerateSanNotation());
            result.Result = ChessBoardTurnResultType.Continue;

            return result;
        }

        /// <summary>
        /// Generates ASCII string representing current board
        /// </summary>
        public string ToAscii(bool displayFull = false)
        {
            StringBuilder builder = new("   ┌────────────────────────────────┐\n");
            for (int y = 8 - 1; y >= 0; y--)
            {
                builder.Append(" " + (y + 1) + " │");
                for (int x = 0; x < 8; x++)
                {
                    builder.Append(' ');

                    if (Board[x, y].ChessPiece is not null)
                    {
                        builder.Append(Board[x, y].ChessPiece?.ToFen());
                    }
                    else
                    {
                        builder.Append(". ");
                    }

                    builder.Append(' ');
                }

                builder.Append("│\n");
            }

            builder.Append("   └────────────────────────────────┘\n");
            builder.Append("     a   b   c   d   e   f   g   h  \n");

            if (displayFull)
            {
                builder.Append('\n');

                if (WhiteCaptured.Count > 0)
                    builder.Append("  White Captured: " + string.Join(", ", WhiteCaptured.Select(p => p.ToFen())) + '\n');
                if (BlackCaptured.Count > 0)
                    builder.Append("  Black Captured: " + string.Join(", ", BlackCaptured.Select(p => p.ToFen())) + '\n');
            }

            return builder.ToString();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessExample.ChessBoard.Exceptions;
using ChessExample.ChessPiece.Enums;
using ChessExample.Utilities.Extensions;

namespace ChessExample.ChessBoard.Core
{
    public static class ChessBoardTurnGenerator
    {
        public static ChessBoardTurn PawnGenerator(ChessBoard board, ChessBoardSpace space)
        {
            ChessBoardTurn turn = new ChessBoardTurn();
            ChessBoardSquare currentSquare = space.Square;
            ChessBoardMove move;

            if (space.ChessPiece.Color == ChessPieceColor.White)
            {
                //move 1 space
                move = new ChessBoardMove(space.ChessPiece, currentSquare, new ChessBoardSquare(currentSquare.Column.GetDescriptionFromEnum(), currentSquare.Row.GetDescriptionFromEnum() + 1));

                ChessBoardTurn temp = new()
                {
                    new List<ChessBoardMove> { move }
                };

                if (board.IsValidTurn(temp))
                {
                    turn.AddRange(temp);
                }

                //move 2 spaces
                if (!space.ChessPiece.HasPieceBeenMoved)
                {
                    move = new ChessBoardMove(space.ChessPiece, currentSquare, new ChessBoardSquare(currentSquare.Column.GetDescriptionFromEnum(), currentSquare.Row.GetDescriptionFromEnum() + 2));
                    temp = new()
                    {
                        new List<ChessBoardMove> { move }
                    };

                    if (board.IsValidTurn(temp))
                    {
                        turn.AddRange(temp);
                    }
                }

                //check right diagonal capture
                if (currentSquare.Column.GetDescriptionFromEnum() + 1 < 8 && currentSquare.Row.GetDescriptionFromEnum() + 1 < 8)
                {
                    move = new ChessBoardMove(space.ChessPiece, currentSquare, new ChessBoardSquare(currentSquare.Column.GetDescriptionFromEnum() + 1, currentSquare.Row.GetDescriptionFromEnum() + 1))
                    {
                        IsCapture = true
                    };

                    temp = new()
                    {
                        new List<ChessBoardMove> { move }
                    };

                    if (board.IsValidTurn(temp))
                    {
                        turn.AddRange(temp);
                    }
                }

                //check left diagonal capture
                if (currentSquare.Column.GetDescriptionFromEnum() - 1 > 0 && currentSquare.Row.GetDescriptionFromEnum() + 1 < 8)
                {
                    move = new ChessBoardMove
                    {
                        ChessPiece = space.ChessPiece,
                        CurrentSquare = currentSquare,
                        NewSquare = new ChessBoardSquare(currentSquare.Column.GetDescriptionFromEnum() - 1, currentSquare.Row.GetDescriptionFromEnum() + 1),
                        IsCapture = true
                    };

                    temp = new()
                    {
                        new List<ChessBoardMove> { move }
                    };

                    if (board.IsValidTurn(temp))
                    {
                        turn.AddRange(temp);
                    }
                }
            }

            else
            {
                //move 1 space
                move = new ChessBoardMove()
                {
                    ChessPiece = space.ChessPiece,
                    CurrentSquare = currentSquare,
                    NewSquare = new ChessBoardSquare(currentSquare.Column.GetDescriptionFromEnum(), currentSquare.Row.GetDescriptionFromEnum() - 1)
                };

                ChessBoardTurn temp = new()
                {
                    new List<ChessBoardMove> { move }
                };

                if (board.IsValidTurn(temp))
                {
                    turn.AddRange(temp);
                }

                //move 2 spaces
                if (!space.ChessPiece.HasPieceBeenMoved)
                {
                    move = new ChessBoardMove()
                    {
                        ChessPiece = space.ChessPiece,
                        CurrentSquare = currentSquare,
                        NewSquare = new ChessBoardSquare(currentSquare.Column.GetDescriptionFromEnum(), currentSquare.Row.GetDescriptionFromEnum() - 2)
                    };

                    temp = new()
                    {
                        new List<ChessBoardMove> { move }
                    };

                    if (board.IsValidTurn(temp))
                    {
                        turn.AddRange(temp);
                    }
                }

                //check right diagonal capture
                if (currentSquare.Column.GetDescriptionFromEnum() + 1 < 8 && currentSquare.Row.GetDescriptionFromEnum() - 1 > 0)
                {
                    move = new ChessBoardMove
                    {
                        ChessPiece = space.ChessPiece,
                        CurrentSquare = currentSquare,
                        NewSquare = new ChessBoardSquare(currentSquare.Column.GetDescriptionFromEnum() + 1, currentSquare.Row.GetDescriptionFromEnum() - 1),
                        IsCapture = true
                    };

                    temp = new()
                    {
                        new List<ChessBoardMove> { move }
                    };

                    if (board.IsValidTurn(temp))
                    {
                        turn.AddRange(temp);
                    }
                }

                //check left diagonal capture
                if (currentSquare.Column.GetDescriptionFromEnum() - 1 > 0 && currentSquare.Row.GetDescriptionFromEnum() - 1 > 0)
                {
                    move = new ChessBoardMove
                    {
                        ChessPiece = space.ChessPiece,
                        CurrentSquare = currentSquare,
                        NewSquare = new ChessBoardSquare(currentSquare.Column.GetDescriptionFromEnum() - 1, currentSquare.Row.GetDescriptionFromEnum() - 1),
                        IsCapture = true
                    };

                    temp = new()
                    {
                        new List<ChessBoardMove> { move }
                    };

                    if (board.IsValidTurn(temp))
                    {
                        turn.AddRange(temp);
                    }
                }
            }

            return turn;
        }

        public static ChessBoardTurn QueenGenerator(ChessBoard board, ChessBoardSpace space)
        {
            // For queen just using validation of bishop AND rook

            ChessBoardTurn turn = new ChessBoardTurn();
            turn.AddRange(BishopGenerator(board, space));
            turn.AddRange(RookGenerator(board, space));
            return turn;
        }

        public static ChessBoardTurn RookGenerator(ChessBoard board, ChessBoardSpace space)
        {
            ChessBoardTurn turn = new ChessBoardTurn();
            ChessBoardSquare currentSquare = space.Square;

            // Check if each possible
            // move is valid or not

            //move horizontal both ways
            for (int i = 1; i <= 8; i++)
            {
                int x = currentSquare.Column.GetDescriptionFromEnum() + i;
                int y = currentSquare.Row.GetDescriptionFromEnum();

                if (x >= 0 && y >= 0 && x < board.NumColumns && y < board.NumRows && board.Board[x, y].ChessPiece == null)
                {
                    ChessBoardMove move = new()
                    {
                        ChessPiece = space.ChessPiece,
                        CurrentSquare = currentSquare,
                        NewSquare = new ChessBoardSquare(x, y)
                    };

                    if (board.Board[x, y].ChessPiece != null && board.Board[x, y].ChessPiece?.Color != space.ChessPiece.Color)
                    {
                        move.IsCapture = true;
                    }

                    ChessBoardTurn temp = new()
                    {
                        new List<ChessBoardMove> { move }
                    };

                    try
                    {
                        if (board.IsValidTurn(temp))
                        {
                            turn.AddRange(temp);
                        }
                    }
                    catch (ChessException)
                    {
                    }
                }

                x = currentSquare.Column.GetDescriptionFromEnum() - i;

                if (x >= 0 && y >= 0 && x < board.NumColumns && y < board.NumRows && board.Board[x, y].ChessPiece == null)
                {
                    ChessBoardMove move = new()
                    {
                        ChessPiece = space.ChessPiece,
                        CurrentSquare = currentSquare,
                        NewSquare = new ChessBoardSquare(x, y)
                    };

                    if (board.Board[x, y].ChessPiece != null && board.Board[x, y].ChessPiece?.Color != space.ChessPiece.Color)
                    {
                        move.IsCapture = true;
                    }

                    ChessBoardTurn temp = new()
                    {
                        new List<ChessBoardMove> { move }
                    };

                    try
                    {
                        if (board.IsValidTurn(temp))
                        {
                            turn.AddRange(temp);
                        }
                    }
                    catch (ChessException)
                    {
                    }
                }
            }

            //move vertical both ways
            for (int i = 1; i <= 8; i++)
            {
                int x = currentSquare.Column.GetDescriptionFromEnum();
                int y = currentSquare.Row.GetDescriptionFromEnum() + i;

                if (x >= 0 && y >= 0 && x < board.NumColumns && y < board.NumRows && board.Board[x, y].ChessPiece == null)
                {
                    ChessBoardMove move = new()
                    {
                        ChessPiece = space.ChessPiece,
                        CurrentSquare = currentSquare,
                        NewSquare = new ChessBoardSquare(x, y)
                    };

                    if (board.Board[x, y].ChessPiece != null && board.Board[x, y].ChessPiece?.Color != space.ChessPiece.Color)
                    {
                        move.IsCapture = true;
                    }

                    ChessBoardTurn temp = new()
                    {
                        new List<ChessBoardMove> { move }
                    };

                    try
                    {
                        if (board.IsValidTurn(temp))
                        {
                            turn.AddRange(temp);
                        }
                    }
                    catch (ChessException)
                    {
                    }
                }

                y = currentSquare.Row.GetDescriptionFromEnum() - i;

                if (x >= 0 && y >= 0 && x < board.NumColumns && y < board.NumRows && board.Board[x, y].ChessPiece == null)
                {
                    ChessBoardMove move = new()
                    {
                        ChessPiece = space.ChessPiece,
                        CurrentSquare = currentSquare,
                        NewSquare = new ChessBoardSquare(x, y)
                    };

                    if (board.Board[x, y].ChessPiece != null && board.Board[x, y].ChessPiece?.Color != space.ChessPiece.Color)
                    {
                        move.IsCapture = true;
                    }

                    ChessBoardTurn temp = new()
                    {
                        new List<ChessBoardMove> { move }
                    };

                    try
                    {
                        if (board.IsValidTurn(temp))
                        {
                            turn.AddRange(temp);
                        }
                    }
                    catch (ChessException)
                    {
                    }
                }
            }

            return turn;
        }

        public static ChessBoardTurn KnightGenerator(ChessBoard board, ChessBoardSpace space)
        {
            ChessBoardTurn turn = new ChessBoardTurn();
            ChessBoardSquare currentSquare = space.Square;

            //knight isn't bad, there are 8 options

            // All possible moves
            // of a knight
            int[] h =
            {
                2, 1, -1, -2,
                -2, -1, 1, 2
            };

            int[] v =
            {
                1, 2, 2, 1,
                -1, -2, -2, -1
            };

            // Check if each possible
            // move is valid or not
            for (int i = 0; i < 8; i++)
            {
                // Position of knight
                // after move
                int x = currentSquare.Column.GetDescriptionFromEnum() + h[i];
                int y = currentSquare.Row.GetDescriptionFromEnum() + v[i];

                if (x >= 0 && y >= 0 && x < board.NumColumns && y < board.NumRows && board.Board[x, y].ChessPiece == null)
                {
                    ChessBoardMove move = new()
                    {
                        ChessPiece = space.ChessPiece,
                        CurrentSquare = currentSquare,
                        NewSquare = new ChessBoardSquare(x, y)
                    };

                    ChessBoardTurn temp = new()
                    {
                        new List<ChessBoardMove> { move }
                    };

                    if (board.IsValidTurn(temp))
                    {
                        turn.AddRange(temp);
                    }
                }
            }

            return turn;
        }

        public static ChessBoardTurn BishopGenerator(ChessBoard board, ChessBoardSpace space)
        {
            ChessBoardTurn turn = new ChessBoardTurn();
            ChessBoardSquare currentSquare = space.Square;

            // Check if each possible
            // move is valid or not

            for (int i = 1; i <= 8; i++)
            {
                int x = currentSquare.Column.GetDescriptionFromEnum() + i;
                int y = currentSquare.Row.GetDescriptionFromEnum() + i;

                if (x >= 0 && y >= 0 && x < board.NumColumns && y < board.NumRows && board.Board[x, y].ChessPiece == null)
                {
                    ChessBoardMove move = new()
                    {
                        ChessPiece = space.ChessPiece,
                        CurrentSquare = currentSquare,
                        NewSquare = new ChessBoardSquare(x, y)
                    };

                    if (board.Board[x, y].ChessPiece != null && board.Board[x, y].ChessPiece?.Color != space.ChessPiece.Color)
                    {
                        move.IsCapture = true;
                    }

                    ChessBoardTurn temp = new()
                    {
                        new List<ChessBoardMove> { move }
                    };

                    try
                    {
                        if (board.IsValidTurn(temp))
                        {
                            turn.AddRange(temp);
                        }
                    }
                    catch (ChessException)
                    {
                    }
                }

                x = currentSquare.Column.GetDescriptionFromEnum() - i;
                y = currentSquare.Row.GetDescriptionFromEnum() - i;

                if (x >= 0 && y >= 0 && x < board.NumColumns && y < board.NumRows && board.Board[x, y].ChessPiece == null)
                {
                    ChessBoardMove move = new()
                    {
                        ChessPiece = space.ChessPiece,
                        CurrentSquare = currentSquare,
                        NewSquare = new ChessBoardSquare(x, y)
                    };

                    if (board.Board[x, y].ChessPiece != null && board.Board[x, y].ChessPiece?.Color != space.ChessPiece.Color)
                    {
                        move.IsCapture = true;
                    }

                    ChessBoardTurn temp = new()
                    {
                        new List<ChessBoardMove> { move }
                    };

                    try
                    {
                        if (board.IsValidTurn(temp))
                        {
                            turn.AddRange(temp);
                        }
                    }
                    catch (ChessException)
                    {
                    }
                }
            }

            return turn;
        }

        public static bool KingValidation(ChessBoard board, ChessBoardTurn turn)
        {
            ChessPiece.Core.ChessPiece piece = turn.GetFirstPiece();
            ChessBoardMove move = turn.GetFirstMove();

            if (Math.Abs(move.NewSquare.Column.GetDescriptionFromEnum() - move.CurrentSquare.Column.GetDescriptionFromEnum()) < 2 && Math.Abs(move.NewSquare.Row.GetDescriptionFromEnum() - move.CurrentSquare.Row.GetDescriptionFromEnum()) < 2)
            {
                if (board.Board[move.NewSquare.Column.GetDescriptionFromEnum(), move.NewSquare.Row.GetDescriptionFromEnum()].ChessPiece == null)
                {
                    return true;
                }

                // Piece has different color than king
                return board.Board[move.NewSquare.Column.GetDescriptionFromEnum(), move.NewSquare.Row.GetDescriptionFromEnum()].ChessPiece?.Color != piece.Color;
            }

            // Check if king is on begin pos
            if (move.CurrentSquare.Column.GetDescriptionFromEnum() == 4 && move.CurrentSquare.Row.GetDescriptionFromEnum() % 7 == 0
                                                                        && move.CurrentSquare.Row.GetDescriptionFromEnum() == move.NewSquare.Row.GetDescriptionFromEnum())
            {
                // if drop on rooks position to castle
                // OR drop on kings new position after castle
                if (move.NewSquare.Column.GetDescriptionFromEnum() % 7 == 0 && move.NewSquare.Row.GetDescriptionFromEnum() % 7 == 0
                    || Math.Abs(move.NewSquare.Column.GetDescriptionFromEnum() - move.CurrentSquare.Column.GetDescriptionFromEnum()) == 2)
                {
                    switch (piece.Color)
                    {
                        case ChessPieceColor.White:

                            // Queen Castle
                            if (move.NewSquare.Column.GetDescriptionFromEnum() == 0 || move.NewSquare.Column.GetDescriptionFromEnum() == 2)
                            {
                                if (!HasRightToCastle(board, piece))
                                {
                                    return false;
                                }

                                if (board.Board[1, 0].ChessPiece == null && board.Board[2, 0].ChessPiece == null && board.Board[3, 0].ChessPiece == null)
                                {
                                    return true;
                                }
                            }

                            // King Castle
                            if (move.NewSquare.Column.GetDescriptionFromEnum() == 7 || move.NewSquare.Column.GetDescriptionFromEnum() == 6)
                            {
                                if (!HasRightToCastle(board, piece))
                                {
                                    return false;
                                }

                                if (board.Board[0, 5].ChessPiece == null && board.Board[0, 6].ChessPiece == null)
                                {
                                    return true;
                                }
                            }

                            break;
                        case ChessPieceColor.Black:

                            //Queen Castle
                            if (move.NewSquare.Column.GetDescriptionFromEnum() == 0 || move.NewSquare.Column.GetDescriptionFromEnum() == 2)
                            {
                                if (!HasRightToCastle(board, piece))
                                {
                                    return false;
                                }

                                if (board.Board[1, 7].ChessPiece == null && board.Board[2, 7].ChessPiece == null && board.Board[3, 7].ChessPiece == null)
                                {
                                    return true;
                                }
                            }

                            //King Castle
                            if (move.NewSquare.Column.GetDescriptionFromEnum() == 7 || move.NewSquare.Column.GetDescriptionFromEnum() == 6)
                            {
                                if (!HasRightToCastle(board, piece))
                                {
                                    return false;
                                }

                                if (board.Board[7, 5].ChessPiece == null && board.Board[7, 6].ChessPiece == null)
                                {
                                    return true;
                                }
                            }

                            break;
                    }
                }
            }

            return false;
        }

        private static bool HasRightToCastle(ChessBoard board, ChessPiece.Core.ChessPiece king)
        {
            ChessBoardSquare rookBoardSquare = new ChessBoardSquare(7, (short)(king.Color == ChessPieceColor.White ? 0 : 7));

            return board.Board[rookBoardSquare.Column.GetDescriptionFromEnum(), rookBoardSquare.Row.GetDescriptionFromEnum()].ChessPiece != null
                   && board.Board[rookBoardSquare.Column.GetDescriptionFromEnum(), rookBoardSquare.Row.GetDescriptionFromEnum()].ChessPiece.Type == ChessPieceType.Rook
                   && board.Board[rookBoardSquare.Column.GetDescriptionFromEnum(), rookBoardSquare.Row.GetDescriptionFromEnum()].ChessPiece.Color == king.Color
                   && !king.HasPieceBeenMoved & !board.Board[rookBoardSquare.Column.GetDescriptionFromEnum(), rookBoardSquare.Row.GetDescriptionFromEnum()].ChessPiece.HasPieceBeenMoved;
        }
    }
}
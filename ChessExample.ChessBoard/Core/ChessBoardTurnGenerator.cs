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
            ChessBoardSpace currentSpace = space;
            ChessBoardMove move;

            if (space.ChessPiece.Color == ChessPieceColor.White)
            {
                //move 1 space
                move = new ChessBoardMove
                {
                    CurrentSpace = currentSpace,
                    NewSpace = new ChessBoardSpace(currentSpace.Column.GetDescriptionFromEnum(), currentSpace.Row.GetDescriptionFromEnum() + 1)
                };

                ChessBoardTurn temp = new()
                {
                    new Tuple<ChessPiece.Core.ChessPiece, List<ChessBoardMove>>(space.ChessPiece, new List<ChessBoardMove>() { move })
                };

                if (board.IsValidTurn(temp))
                {
                    turn.AddRange(temp);
                }

                //move 2 spaces
                if (!space.ChessPiece.HasPieceBeenMoved)
                {
                    move = new ChessBoardMove
                    {
                        CurrentSpace = currentSpace,
                        NewSpace = new ChessBoardSpace(currentSpace.Column.GetDescriptionFromEnum(), currentSpace.Row.GetDescriptionFromEnum() + 2)
                    };

                    temp = new()
                    {
                        new Tuple<ChessPiece.Core.ChessPiece, List<ChessBoardMove>>(space.ChessPiece, new List<ChessBoardMove>() { move })
                    };

                    if (board.IsValidTurn(temp))
                    {
                        turn.AddRange(temp);
                    }
                }

                //check right diagonal capture
                if (currentSpace.Column.GetDescriptionFromEnum() + 1 < 8 && currentSpace.Row.GetDescriptionFromEnum() + 1 < 8)
                {
                    move = new ChessBoardMove
                    {
                        CurrentSpace = currentSpace,
                        NewSpace = new ChessBoardSpace(currentSpace.Column.GetDescriptionFromEnum() + 1, currentSpace.Row.GetDescriptionFromEnum() + 1),
                        IsCapture = true
                    };

                    temp = new()
                    {
                        new Tuple<ChessPiece.Core.ChessPiece, List<ChessBoardMove>>(space.ChessPiece, new List<ChessBoardMove>() { move })
                    };

                    if (board.IsValidTurn(temp))
                    {
                        turn.AddRange(temp);
                    }
                }

                //check left diagonal capture
                if (currentSpace.Column.GetDescriptionFromEnum() - 1 > 0 && currentSpace.Row.GetDescriptionFromEnum() + 1 < 8)
                {
                    move = new ChessBoardMove
                    {
                        CurrentSpace = currentSpace,
                        NewSpace = new ChessBoardSpace(currentSpace.Column.GetDescriptionFromEnum() - 1, currentSpace.Row.GetDescriptionFromEnum() + 1),
                        IsCapture = true
                    };

                    temp = new()
                    {
                        new Tuple<ChessPiece.Core.ChessPiece, List<ChessBoardMove>>(space.ChessPiece, new List<ChessBoardMove>() { move })
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
                move = new ChessBoardMove
                {
                    CurrentSpace = currentSpace,
                    NewSpace = new ChessBoardSpace(currentSpace.Column.GetDescriptionFromEnum(), currentSpace.Row.GetDescriptionFromEnum() - 1)
                };

                ChessBoardTurn temp = new()
                {
                    new Tuple<ChessPiece.Core.ChessPiece, List<ChessBoardMove>>(space.ChessPiece, new List<ChessBoardMove>() { move })
                };

                if (board.IsValidTurn(temp))
                {
                    turn.AddRange(temp);
                }

                //move 2 spaces
                if (!space.ChessPiece.HasPieceBeenMoved)
                {
                    move = new ChessBoardMove
                    {
                        CurrentSpace = currentSpace,
                        NewSpace = new ChessBoardSpace(currentSpace.Column.GetDescriptionFromEnum(), currentSpace.Row.GetDescriptionFromEnum() - 2)
                    };

                    temp = new()
                    {
                        new Tuple<ChessPiece.Core.ChessPiece, List<ChessBoardMove>>(space.ChessPiece, new List<ChessBoardMove>() { move })
                    };

                    if (board.IsValidTurn(temp))
                    {
                        turn.AddRange(temp);
                    }
                }

                //check right diagonal capture
                if (currentSpace.Column.GetDescriptionFromEnum() + 1 < 8 && currentSpace.Row.GetDescriptionFromEnum() - 1 > 0)
                {
                    move = new ChessBoardMove
                    {
                        CurrentSpace = currentSpace,
                        NewSpace = new ChessBoardSpace(currentSpace.Column.GetDescriptionFromEnum() + 1, currentSpace.Row.GetDescriptionFromEnum() - 1),
                        IsCapture = true
                    };

                    temp = new()
                    {
                        new Tuple<ChessPiece.Core.ChessPiece, List<ChessBoardMove>>(space.ChessPiece, new List<ChessBoardMove>() { move })
                    };

                    if (board.IsValidTurn(temp))
                    {
                        turn.AddRange(temp);
                    }
                }

                //check left diagonal capture
                if (currentSpace.Column.GetDescriptionFromEnum() - 1 > 0 && currentSpace.Row.GetDescriptionFromEnum() - 1 > 0)
                {
                    move = new ChessBoardMove
                    {
                        CurrentSpace = currentSpace,
                        NewSpace = new ChessBoardSpace(currentSpace.Column.GetDescriptionFromEnum() - 1, currentSpace.Row.GetDescriptionFromEnum() - 1),
                        IsCapture = true
                    };

                    temp = new()
                    {
                        new Tuple<ChessPiece.Core.ChessPiece, List<ChessBoardMove>>(space.ChessPiece, new List<ChessBoardMove>() { move })
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
            ChessBoardSpace currentSpace = space;

            // Check if each possible
            // move is valid or not

            //move horizontal both ways
            for (int i = 1; i <= 8; i++)
            {
                int x = currentSpace.Column.GetDescriptionFromEnum() + i;
                int y = currentSpace.Row.GetDescriptionFromEnum();

                if (x >= 0 && y >= 0 && x < board.NumColumns && y < board.NumRows && board.Board[x, y].ChessPiece == null)
                {
                    ChessBoardMove move = new() { CurrentSpace = currentSpace, NewSpace = new ChessBoardSpace(x, y) };

                    if (board.Board[x, y].ChessPiece != null && board.Board[x, y].ChessPiece?.Color != space.ChessPiece.Color)
                    {
                        move.IsCapture = true;
                    }

                    ChessBoardTurn temp = new()
                    {
                        new Tuple<ChessPiece.Core.ChessPiece, List<ChessBoardMove>>(space.ChessPiece, new List<ChessBoardMove>() { move })
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

                x = currentSpace.Column.GetDescriptionFromEnum() - i;

                if (x >= 0 && y >= 0 && x < board.NumColumns && y < board.NumRows && board.Board[x, y].ChessPiece == null)
                {
                    ChessBoardMove move = new() { CurrentSpace = currentSpace, NewSpace = new ChessBoardSpace(x, y) };

                    if (board.Board[x, y].ChessPiece != null && board.Board[x, y].ChessPiece?.Color != space.ChessPiece.Color)
                    {
                        move.IsCapture = true;
                    }

                    ChessBoardTurn temp = new()
                    {
                        new Tuple<ChessPiece.Core.ChessPiece, List<ChessBoardMove>>(space.ChessPiece, new List<ChessBoardMove>() { move })
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
                int x = currentSpace.Column.GetDescriptionFromEnum();
                int y = currentSpace.Row.GetDescriptionFromEnum() + i;

                if (x >= 0 && y >= 0 && x < board.NumColumns && y < board.NumRows && board.Board[x, y].ChessPiece == null)
                {
                    ChessBoardMove move = new() { CurrentSpace = currentSpace, NewSpace = new ChessBoardSpace(x, y) };

                    if (board.Board[x, y].ChessPiece != null && board.Board[x, y].ChessPiece?.Color != space.ChessPiece.Color)
                    {
                        move.IsCapture = true;
                    }

                    ChessBoardTurn temp = new()
                    {
                        new Tuple<ChessPiece.Core.ChessPiece, List<ChessBoardMove>>(space.ChessPiece, new List<ChessBoardMove>() { move })
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

                y = currentSpace.Row.GetDescriptionFromEnum() - i;

                if (x >= 0 && y >= 0 && x < board.NumColumns && y < board.NumRows && board.Board[x, y].ChessPiece == null)
                {
                    ChessBoardMove move = new() { CurrentSpace = currentSpace, NewSpace = new ChessBoardSpace(x, y) };

                    if (board.Board[x, y].ChessPiece != null && board.Board[x, y].ChessPiece?.Color != space.ChessPiece.Color)
                    {
                        move.IsCapture = true;
                    }

                    ChessBoardTurn temp = new()
                    {
                        new Tuple<ChessPiece.Core.ChessPiece, List<ChessBoardMove>>(space.ChessPiece, new List<ChessBoardMove>() { move })
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
            ChessBoardSpace currentSpace = space;

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
                int x = currentSpace.Column.GetDescriptionFromEnum() + h[i];
                int y = currentSpace.Row.GetDescriptionFromEnum() + v[i];

                if (x >= 0 && y >= 0 && x < board.NumColumns && y < board.NumRows && board.Board[x, y].ChessPiece == null)
                {
                    ChessBoardMove move = new() { CurrentSpace = currentSpace, NewSpace = new ChessBoardSpace(x, y) };

                    ChessBoardTurn temp = new()
                    {
                        new Tuple<ChessPiece.Core.ChessPiece, List<ChessBoardMove>>(space.ChessPiece, new List<ChessBoardMove>() { move })
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
            ChessBoardSpace currentSpace = space;

            // Check if each possible
            // move is valid or not

            for (int i = 1; i <= 8; i++)
            {
                int x = currentSpace.Column.GetDescriptionFromEnum() + i;
                int y = currentSpace.Row.GetDescriptionFromEnum() + i;

                if (x >= 0 && y >= 0 && x < board.NumColumns && y < board.NumRows && board.Board[x, y].ChessPiece == null)
                {
                    ChessBoardMove move = new() { CurrentSpace = currentSpace, NewSpace = new ChessBoardSpace(x, y) };

                    if (board.Board[x, y].ChessPiece != null && board.Board[x, y].ChessPiece?.Color != space.ChessPiece.Color)
                    {
                        move.IsCapture = true;
                    }

                    ChessBoardTurn temp = new()
                    {
                        new Tuple<ChessPiece.Core.ChessPiece, List<ChessBoardMove>>(space.ChessPiece, new List<ChessBoardMove>() { move })
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

                x = currentSpace.Column.GetDescriptionFromEnum() - i;
                y = currentSpace.Row.GetDescriptionFromEnum() - i;

                if (x >= 0 && y >= 0 && x < board.NumColumns && y < board.NumRows && board.Board[x, y].ChessPiece == null)
                {
                    ChessBoardMove move = new() { CurrentSpace = currentSpace, NewSpace = new ChessBoardSpace(x, y) };

                    if (board.Board[x, y].ChessPiece != null && board.Board[x, y].ChessPiece?.Color != space.ChessPiece.Color)
                    {
                        move.IsCapture = true;
                    }

                    ChessBoardTurn temp = new()
                    {
                        new Tuple<ChessPiece.Core.ChessPiece, List<ChessBoardMove>>(space.ChessPiece, new List<ChessBoardMove>() { move })
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
            ChessBoardMove move = turn.GetFirstPieceFirstMove();

            if (Math.Abs(move.NewSpace.Column.GetDescriptionFromEnum() - move.CurrentSpace.Column.GetDescriptionFromEnum()) < 2 && Math.Abs(move.NewSpace.Row.GetDescriptionFromEnum() - move.CurrentSpace.Row.GetDescriptionFromEnum()) < 2)
            {
                if (board.Board[move.NewSpace.Column.GetDescriptionFromEnum(), move.NewSpace.Row.GetDescriptionFromEnum()].ChessPiece == null)
                {
                    return true;
                }

                // Piece has different color than king
                return board.Board[move.NewSpace.Column.GetDescriptionFromEnum(), move.NewSpace.Row.GetDescriptionFromEnum()].ChessPiece?.Color != piece.Color;
            }

            // Check if king is on begin pos
            if (move.CurrentSpace.Column.GetDescriptionFromEnum() == 4 && move.CurrentSpace.Row.GetDescriptionFromEnum() % 7 == 0
                                                                       && move.CurrentSpace.Row.GetDescriptionFromEnum() == move.NewSpace.Row.GetDescriptionFromEnum())
            {
                // if drop on rooks position to castle
                // OR drop on kings new position after castle
                if (move.NewSpace.Column.GetDescriptionFromEnum() % 7 == 0 && move.NewSpace.Row.GetDescriptionFromEnum() % 7 == 0
                    || Math.Abs(move.NewSpace.Column.GetDescriptionFromEnum() - move.CurrentSpace.Column.GetDescriptionFromEnum()) == 2)
                {
                    switch (piece.Color)
                    {
                        case ChessPieceColor.White:

                            // Queen Castle
                            if (move.NewSpace.Column.GetDescriptionFromEnum() == 0 || move.NewSpace.Column.GetDescriptionFromEnum() == 2)
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
                            if (move.NewSpace.Column.GetDescriptionFromEnum() == 7 || move.NewSpace.Column.GetDescriptionFromEnum() == 6)
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
                            if (move.NewSpace.Column.GetDescriptionFromEnum() == 0 || move.NewSpace.Column.GetDescriptionFromEnum() == 2)
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
                            if (move.NewSpace.Column.GetDescriptionFromEnum() == 7 || move.NewSpace.Column.GetDescriptionFromEnum() == 6)
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
            ChessBoardSpace rookBoardSpace = new ChessBoardSpace(7, (short)(king.Color == ChessPieceColor.White ? 0 : 7));

            return board.Board[rookBoardSpace.Column.GetDescriptionFromEnum(), rookBoardSpace.Row.GetDescriptionFromEnum()].ChessPiece != null
                   && board.Board[rookBoardSpace.Column.GetDescriptionFromEnum(), rookBoardSpace.Row.GetDescriptionFromEnum()].ChessPiece.Type == ChessPieceType.Rook
                   && board.Board[rookBoardSpace.Column.GetDescriptionFromEnum(), rookBoardSpace.Row.GetDescriptionFromEnum()].ChessPiece.Color == king.Color
                   && !king.HasPieceBeenMoved & !board.Board[rookBoardSpace.Column.GetDescriptionFromEnum(), rookBoardSpace.Row.GetDescriptionFromEnum()].ChessPiece.HasPieceBeenMoved;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessExample.ChessBoard.Core;
using ChessExample.ChessPiece.Enums;

namespace ChessExample.Player
{
    public class ChessComputerPlayer : ChessBasePlayer
    {
        public ChessComputerPlayer(ChessPieceColor color)
        {
            Color = color;
            IsHuman = false;
        }

        public override ChessBoardTurn DetermineTurn(ChessBoard.ChessBoard board)
        {
            ChessBoardTurn turn = new ChessBoardTurn();

            //loop through board and determine all valid moves
            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    //space is empty skip
                    if (board.Board[x, y].ChessPiece == null)
                    {
                        continue;
                    }

                    if (board.Board[x, y].ChessPiece?.Color != Color)
                    {
                        continue;
                    }

                    switch (board.Board[x, y].ChessPiece?.Type)
                    {
                        case ChessPieceType.Rook:
                            turn.AddRange(ChessBoardTurnGenerator.RookGenerator(board, board.Board[x, y]));
                            break;
                        case ChessPieceType.Knight:
                            turn.AddRange(ChessBoardTurnGenerator.KnightGenerator(board, board.Board[x, y]));
                            break;
                        case ChessPieceType.Bishop:
                            turn.AddRange(ChessBoardTurnGenerator.BishopGenerator(board, board.Board[x, y]));
                            break;
                        case ChessPieceType.King:
                            break;
                        case ChessPieceType.Queen:
                            turn.AddRange(ChessBoardTurnGenerator.QueenGenerator(board, board.Board[x, y]));
                            break;
                        case ChessPieceType.Pawn:
                            turn.AddRange(ChessBoardTurnGenerator.PawnGenerator(board, board.Board[x, y]));
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
            }

            //we can capture material, do it
            if (turn.Any(x => x.Item2.Any(y => y.IsCapture)))
            {
                Tuple<ChessPiece.Core.ChessPiece, List<ChessBoardMove>> move = turn.First(x => x.Item2.Any(y => y.IsCapture));
                turn.Clear();
                turn.Add(move);
                return turn;
            }

            if (turn.Count > 0)
            {
                Random rnd = new Random();
                Tuple<ChessPiece.Core.ChessPiece, List<ChessBoardMove>> randomMove = turn[rnd.Next(0, turn.Count)];

                turn.Clear();
                turn.Add(randomMove);
            }

            return turn;
        }
    }
}
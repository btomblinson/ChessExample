using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessExample.ChessBoard.Builders;
using ChessExample.ChessBoard.Core;
using ChessExample.ChessPiece.Enums;
using ChessExample.Utilities.Extensions;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace ChessExample.ChessBoard.Test.Builders
{
    [TestFixture]
    public class SanBuilderTests
    {
        #region Queenside Castle Tests

        [Test]
        public void SanTestBlackQueenSideCastleTest()
        {
            var movesArray = new[]
            {
                "Nf3", "Nc6",
                "d4", "d5",
                "Bf4", "Bg4",
                "Ne5", "Nxe5",
                "dxe5", "Qd7",
                "Nc3", "O-O-O"
            };

            MovesHelper(movesArray);
        }

        [Test]
        public void SanTestWhiteQueenSideCastleTest()
        {
            var movesArray = new[]
            {
                "c4", "c5",
                "d4", "cxd4",
                "Qxd4", "Nf6",
                "Bf4", "Nh5",
                "Nd2", "Nxf4",
                "Qxf4", "Nc6",
                "O-O-O"
            };

            MovesHelper(movesArray);
        }

        #endregion

        #region Check Tests

        [Test]
        public void SanTestWhiteQueenCheckTest()
        {
            var movesArray = new[]
            {
                "b3", "a6",
                "c4", "e6",
                "e3", "f6",
                "h3", "c5",
                "b4", "cxb4",
                "Qh5+"
            };

            MovesHelper(movesArray);
        }

        #endregion

        #region Checkmate Tests

        /// <summary>
        /// This is taken from a recent Magnus Carlsen game
        /// https://www.chess.com/games/view/16372061
        /// </summary>
        [Test]
        public void SanTestWhiteCheckmate()
        {
            var movesArray = new[]
            {
                "a4", "Nf6",
                "d4", "d5",
                "Nf3", "Bf5",
                "Nh4", "Be4",
                "f3", "Bg6",
                "Nc3", "c5",
                "e4", "cxd4",
                "Nxg6", "hxg6",
                "Qxd4", "Nc6",
                "Qf2", "d4",
                "Nd1", "e5",
                "Bc4", "Rc8",
                "Qe2", "Bb4+",
                "Kf1", "Na5",
                "Bd3", "O-O",
                "Nf2", "Qb6",
                "h4", "Nh5",
                "Rh3", "Qf6",
                "g4", "Nf4",
                "Bxf4", "Qxf4",
                "h5", "g5",
                "Rd1", "a6",
                "Kg2", "Rc7",
                "Rhh1", "Rfc8",
                "Nh3", "Qf6",
                "Ra1", "Nc6",
                "Rhc1", "Bd6",
                "Qd2", "Bb4",
                "c3", "Be7",
                "Nf2", "dxc3",
                "bxc3", "Nd8",
                "Bb1", "Ne6",
                "Nh3", "Bc5",
                "Ba2", "Rd8",
                "Qe2", "Nf4+",
                "Nxf4", "gxf4",
                "Kh3", "g6",
                "Rd1", "Rcd7",
                "Rxd7", "Rxd7",
                "Rd1", "Bf2",
                "Bxf7+", "Kf8",
                "Qxf2", "Rxd1",
                "Bxg6", "Qd6",
                "g5", "Qd3",
                "Qc5+", "Qd6",
                "Qc8+", "Kg7",
                "Qxb7+", "Kf8",
                "Qf7#"
            };

            ChessBoardTurnResult result = MovesHelper(movesArray);

            Assert.That(result.IsCheck, Is.EqualTo(true), "Not in check state.");
            Assert.That(result.IsCheckmate, Is.EqualTo(true), "Not in checkmate state.");
        }

        /// <summary>
        /// This is taken from a recent Magnus Carlsen game
        /// https://www.chess.com/game/live/89585430569
        /// </summary>
        [Test]
        public void SanTestBlackCheckmate()
        {
            var movesArray = new[]
            {
                "e4", "c5",
                "Nf3", "e6",
                "d4", "cxd4",
                "Nxd4", "Nf6",
                "Nc3", "Nc6",
                "Nxc6", "bxc6",
                "e5", "Nd5",
                "Ne4", "Bb7",
                "Nd6+", "Bxd6",
                "exd6", "c5",
                "c4", "Nb4",
                "a3", "Nc6",
                "Be3", "Qf6",
                "Qd2", "Rb8",
                "Bxc5", "Ba8",
                "Bb4", "Qe5+",
                "Be2", "Nxb4",
                "axb4", "Bxg2",
                "Ra5", "Qe4",
                "Rg1", "Bf3",
                "Rxg7", "Bxe2",
                "Qxe2", "Qb1+",
                "Qd1", "Qxb2",
                "Rag5", "Qxb4+",
                "Kf1", "Qxc4+",
                "Kg1", "Rf8",
                "Rxf7", "Qe4",
                "Rgg7", "Rxf7",
                "Rg8+", "Rf8",
                "Qh5+", "Qg6+",
                "Rxg6", "hxg6",
                "Qxg6+", "Kd8",
                "Qg5+", "Kc8",
                "Qc5+", "Kb7",
                "Qb5+", "Kc8",
                "Qc4+", "Kb7",
                "Qc7+", "Ka6",
                "Qxd7", "Rfd8",
                "Qxe6", "Kb6",
                "Qe7", "a5",
                "Qc7+", "Kb5",
                "Kf1", "a4",
                "Ke2", "a3",
                "Kd3", "Ra8",
                "Kd4", "a2",
                "Qb7+", "Ka4",
                "Qb2", "Rxd6+",
                "Kc5", "Rb6",
                "Qxa2#",
            };

            ChessBoardTurnResult result = MovesHelper(movesArray);

            Assert.That(result.IsCheck, Is.EqualTo(true), "Not in check state.");
            Assert.That(result.IsCheckmate, Is.EqualTo(true), "Not in checkmate state.");
        }

        #endregion

        private static ChessBoardTurnResult MovesHelper(string[] movesArray)
        {
            ChessBoard board = new ChessBoard();
            ChessBoardTurnResult result = new ChessBoardTurnResult();

            for (int i = 0; i < movesArray.Length; i++)
            {
                board.CurrentMoveColor = i % 2 == 0 ? ChessPieceColor.White : ChessPieceColor.Black;

                Assert.That(SanBuilder.TryParse(board, movesArray[i], out ChessBoardTurn turn), Is.EqualTo(true), "TryParse failed.");
                Assert.Multiple(() =>
                {
                    Assert.That(turn, Is.Not.Empty, "Turn is empty.");

                    Assert.That(board.IsValidTurn(turn), Is.EqualTo(true), "Not a valid turn.");

                    Assert.That(turn.GenerateSanNotation(), Is.EqualTo(movesArray[i]), "San generator failed");
                });

                int currentCapturedPieceCount = 0;
                if (turn.GetFirstPieceFirstMove().IsCapture)
                {
                    currentCapturedPieceCount = board.CurrentMoveColor == ChessPieceColor.White ? board.BlackCaptured.Count : board.WhiteCaptured.Count;
                }

                result = board.ExecuteTurn(turn);

                if (turn.GetFirstPieceFirstMove().IsCapture)
                {
                    Assert.That(board.CurrentMoveColor == ChessPieceColor.White ? board.BlackCaptured.Count : board.WhiteCaptured.Count, Is.EqualTo(currentCapturedPieceCount + 1), "Captured count is different.");
                }

                ChessBoardMove move = turn.GetFirstPieceFirstMove();

                //make sure piece moved correctly
                Assert.That(board.Board[move.CurrentSpace.Column.GetDescriptionFromEnum(), move.CurrentSpace.Row.GetDescriptionFromEnum()].Item2, Is.EqualTo(null), "Piece did not move.");
                Assert.That(board.Board[move.NewSpace.Column.GetDescriptionFromEnum(), move.NewSpace.Row.GetDescriptionFromEnum()].Item2, Is.EqualTo(turn.GetFirstPiece()), "Piece was not moved to correct location.");
            }

            return result;
        }
    }
}
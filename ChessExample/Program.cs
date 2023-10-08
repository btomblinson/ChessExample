using ChessExample;
using ChessExample.ChessBoard;
using ChessExample.ChessBoard.Core;
using ChessExample.ChessBoard.Enums;

Console.WriteLine("How many players? ");
string numPlayers = Console.ReadLine() ?? "0";

if (!int.TryParse(numPlayers, out int players))
{
	throw new Exception("Invalid players");
}

if (players is < 0 or > 2)
{
	throw new Exception("Invalid players");
}

ChessGame game = new ChessGame(players, new ChessBoard());

//start game
ChessBoardMoveResult result = new ChessBoardMoveResult() { IsCheck = false, IsCheckmate = false, Result = ChessBoardMoveResultType.Continue };

int counter = 0;
while (result.Result == ChessBoardMoveResultType.Continue)
{
	Console.WriteLine(game.Board.ToAscii());
	List<ChessBoardMove> moves = counter % 2 == 0 ? game.WhitePlayer.DetermineMoves(game.Board) : game.BlackPlayer.DetermineMoves(game.Board);
	foreach (ChessBoardMove move in moves)
	{
		result = game.Board.ExecuteMove(move);
	}

	counter++;
}
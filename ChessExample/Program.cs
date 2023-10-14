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
ChessBoardTurnResult result = new ChessBoardTurnResult() { IsCheck = false, IsCheckmate = false, Result = ChessBoardTurnResultType.Continue };

int counter = 0;
while (result.Result == ChessBoardTurnResultType.Continue)
{
	Console.WriteLine(game.Board.ToAscii());
	ChessBoardTurn turn = counter % 2 == 0 ? game.WhitePlayer.DetermineTurn(game.Board) : game.BlackPlayer.DetermineTurn(game.Board);
	
		if (game.Board.IsValidTurn(turn))
		{
			result = game.Board.ExecuteTurn(turn);
		}
		else
		{
			throw new Exception("Couldn't read player input. ");
		}
	

	counter++;
}
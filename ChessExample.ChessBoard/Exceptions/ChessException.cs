namespace ChessExample.ChessBoard.Exceptions
{
	public class ChessException : Exception
	{
		public ChessExample.ChessBoard.ChessBoard? Board { get; }

		public ChessException(ChessExample.ChessBoard.ChessBoard? board, string message) : base(message) => Board = board;
	}
}
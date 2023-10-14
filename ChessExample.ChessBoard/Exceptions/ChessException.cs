namespace ChessExample.ChessBoard.Exceptions
{
    public class ChessException : Exception
	{
		public ChessBoard? Board { get; }

		public ChessException(ChessBoard? board, string message) : base(message) => Board = board;
	}
}
using System.Text.RegularExpressions;

namespace ChessExample.Utilities;

public static class Regexes
{
	public const string SanOneMovePattern = @"(^([PNBRQK])?([a-h])?([1-8])?(x|X|-)?([a-h][1-8])(=[NBRQ]| ?e\.p\.)?|^O-O(-O)?)(\+|\#|\$)?$";

	public const string SanMovesPattern = @"(?:[PNBRQK]?[a-h]?[1-8]?[xX-]?[a-h][1-8](?:=[NBRQ]| ?e\.p\.)?|O-O(?:-O)?)[+#$]?";

	public const string HeadersPattern = @"\[([^ ]+) ""([^""]*)""\]";

	public const string AlternativesPattern = @"\([^)]*\)";

	public const string CommentsPattern = @"\{[^}]*\}";

	public const string FenPattern = @"^(((?:[rnbqkpRNBQKP1-8]+\/){7})[rnbqkpRNBQKP1-8]+) ([bw]) (-|[KQkq]{1,4}) (-|[a-h][36]) (\d+ \d+)$";

	public const string FenContainsOneWhiteKingPattern = "^[^ K]*K[^ K]* ";

	public const string FenContainsOneBlackKingPattern = "^[^ k]*k[^ k]* ";

	public const string PiecePattern = "^[wb][bknpqr]$";

	public const string FenPiecePattern = "^[bknpqrBKNPQR]$";

	public const string PositionPattern = "^[a-h][1-8]$";

	public const string MovePattern = @"^{(([wb][bknpqr]) - )?([a-h][1-8]) - ([a-h][1-8])( - ([wb][bknpqr]))?( - (o-o|o-o-o|e\.p\.|=|=q|=r|=b|=n))?( - ([+#$]))?}$";

	public static readonly Regex RegexSanOneMove = new(SanOneMovePattern, RegexOptions.Compiled, TimeSpan.FromMilliseconds(250));

	public static readonly Regex RegexSanMoves = new(SanMovesPattern, RegexOptions.Compiled, TimeSpan.FromMilliseconds(250));

	public static readonly Regex RegexHeaders = new(HeadersPattern, RegexOptions.Compiled, TimeSpan.FromMilliseconds(250));

	public static readonly Regex RegexAlternatives = new(AlternativesPattern, RegexOptions.Compiled | RegexOptions.Singleline, TimeSpan.FromMilliseconds(250));

	public static readonly Regex RegexComments = new(CommentsPattern, RegexOptions.Compiled | RegexOptions.Singleline, TimeSpan.FromMilliseconds(250));

	public static readonly Regex RegexFen = new(FenPattern, RegexOptions.Compiled, TimeSpan.FromMilliseconds(250));

	public static readonly Regex RegexFenContainsOneWhiteKing = new(FenContainsOneWhiteKingPattern, RegexOptions.Compiled, TimeSpan.FromMilliseconds(250));

	public static readonly Regex RegexFenContainsOneBlackKing = new(FenContainsOneBlackKingPattern, RegexOptions.Compiled, TimeSpan.FromMilliseconds(250));

	public static readonly Regex RegexPiece = new(PiecePattern, RegexOptions.Compiled, TimeSpan.FromMilliseconds(250));

	public static readonly Regex RegexFenPiece = new(FenPiecePattern, RegexOptions.Compiled, TimeSpan.FromMilliseconds(250));

	public static readonly Regex RegexPosition = new(PositionPattern, RegexOptions.Compiled, TimeSpan.FromMilliseconds(250));

	public static readonly Regex RegexMove = new(MovePattern, RegexOptions.Compiled, TimeSpan.FromMilliseconds(250));
}
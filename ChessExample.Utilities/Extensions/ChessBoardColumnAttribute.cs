using System.ComponentModel;

namespace ChessExample.Utilities.Extensions
{
	[AttributeUsage(AttributeTargets.All)]
	public class ChessBoardColumnAttribute : DescriptionAttribute
	{
		public new string Description { get; set; }

		public string Value { get; set; }

		public ChessBoardColumnAttribute(string description, string value)
		{
			this.Description = description;
			this.Value = value;
		}
	}
}
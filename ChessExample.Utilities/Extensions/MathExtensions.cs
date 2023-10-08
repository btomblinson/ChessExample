using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessExample.Utilities.Extensions
{
	public static class MathExtensions
	{
		public static bool IsOdd(this int value)
		{
			return value % 2 != 0;
		}

		public static bool IsEven(this int value)
		{
			return value % 2 != 0;
		}
	}
}
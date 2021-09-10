using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagGame
{
	class Program
	{
		static void Main(string[] args)
		{
			var game = new Board(new List<byte> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 });
			while (true)
			{
				game.PrintOut();
				var a = Console.ReadKey().KeyChar;
				game.Move(a);
				Console.Clear();
			}
		}


	}

	class Board
	{
		List<byte> table;
		byte i;
		public Board(List<byte> t)
		{
			table = t;
			i = table.Find(j => j == 0);
		}
		public void PrintOut()
		{
			for (int i = 0; i < 4; i++)
			{
				Console.WriteLine(String.Format("|{0,5}|{1,5}|{2,5}|{3,5}|", table[i * 4], table[i * 4 + 1], table[i * 4 + 2], table[i * 4 + 3]));
				//Console.WriteLine();
			}
		}

		public bool Move(char direction)
		{
			switch (Char.ToLower(direction).ToString())
			{
				case "w":
					{
						if (i >= 4)
						{
							Swap<byte>(table, i, i - 4);
							i -= 4;
							return true;
						}
						return false;
					}
				case "s":
					{
						if (i <= 11)
						{
							Swap<byte>(table, i, i + 4);
							i += 4;
							return true;
						}
						return false;
					}
				case "a":
					{
						if (i >= 1 && i%4!=0)
						{
							Swap<byte>(table, i, i-1);
							i -= 1;
							return true;
						}
						return false;
					}
				case "d":
					{
						if (i <= 14 && (i-3)%4!=0)
						{
							Swap<byte>(table, i, i + 1);
							i += 1;
							return true;
						}
						return false;
					}
				default:
					return false;
			}
		}

		public string Hex()
		{
			return string.Join("", table.Select(c => ((int)c).ToString("X")));

		}

		public static void Swap<T>(IList<T> list, int indexA, int indexB)
		{
			T tmp = list[indexA];
			list[indexA] = list[indexB];
			list[indexB] = tmp;
		}
	}

	class Solution
	{

	}
}

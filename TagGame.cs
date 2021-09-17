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
			//var game = new Board(new List<byte> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 });
			var game = new Board("123456789ABCDEF0");
			//Console.WriteLine(game.Solvability());
			Console.ReadKey();
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
		private int master;
		private List<byte> table;
		private byte i;

		public List<byte> Table
		{
			get
			{
				return table;
			}
		}

		public byte I
		{
			get
			{
				return i;
			}
		}

		public int Master
		{
			get
			{
				return master;
			}
		}
		public Board(List<byte> t, int mast = -1)
		{
			table = t;
			i = (byte)table.FindIndex(j => j == 0);
		}

		public Board(string t, int mast = -1)
		{
			table = new List<byte>();
			foreach (char j in t)
			{
				table.Add((byte)hex2int(j));
			}
			i = (byte)table.FindIndex(j => j == 0);

		}

		public bool Solvability()
		{
			int inv = 0;
			for (int i = 0; i < 16; i++)
			{
				if (table[i]!=0)
					for (int j = 0; j < i; j++)
						if (table[j] > table[i])
							inv++;
			}
			for (int i = 0; i < 16; i++)
				if (table[i] == 0)
					inv += 1 + i / 4;

			return (inv%2==0);
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
							Up();
							return true;
						}
						return false;
					}
				case "s":
					{
						if (i <= 11)
						{
							Down();
							return true;
						}
						return false;
					}
				case "a":
					{
						if (i >= 1 && i%4!=0)
						{
							Left();
							return true;
						}
						return false;
					}
				case "d":
					{
						if (i <= 14 && (i-3)%4!=0)
						{
							Right();
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

		int hex2int(char ch)
		{
			if (ch >= '0' && ch <= '9')
				return ch - '0';
			if (ch >= 'A' && ch <= 'F')
				return ch - 'A' + 10;
			if (ch >= 'a' && ch <= 'f')
				return ch - 'a' + 10;
			return -1;
		}

		private void Up()
		{
			Swap<byte>(table, i, i - 4);
			i -= 4;
		}

		private void Down()
		{
			Swap<byte>(table, i, i + 4);
			i += 4;
		}

		private void Left()
		{
			Swap<byte>(table, i, i - 1);
			i -= 1;
		}

		private void Right()
		{
			Swap<byte>(table, i, i + 1);
			i += 1;
		}

	}

	class Solver
	{
		static string endres = "123456789ABCDEF0";
		List<Board> used;

		protected Solver()
		{
			used = new List<Board>();
		}
	}

	class BFSSolver : Solver
	{
		Queue<Board> boards;

		static BFSSolver Solve(Board start)
		{
			return new BFSSolver(start) ;
		}

		private BFSSolver(Board b) :base()
		{
			boards = new Queue<Board>();
			boards.Enqueue(b);
		}


	}
}

using System;

namespace interfaces
{
	class Program
	{
		static ILogger Logger { get; set; }
		static void Main(string[] args)
		{
			Logger = new Logger();

			Calc calc = new Calc(Logger);

			Console.WriteLine("программа завершит работу, если обоим аргументам присвоить нулевые значения!");
			do
			{
				Console.WriteLine("поехали...");
				calc.TakeArguments();
				calc.DisplaySum();
			} while ((calc.a ?? -1) != 0 || (calc.b ?? -1) != 0);
		}
	}

	public interface ILogger
	{
		public void Event(string message);
		public void Error(string message);
	}

	public class Logger : ILogger
	{
		private System.ConsoleColor fc = Console.ForegroundColor;
		private System.ConsoleColor bc = Console.BackgroundColor;

		public void Event(string message)
		{
			Console.BackgroundColor = ConsoleColor.DarkBlue;
			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.WriteLine(message);
			Console.BackgroundColor = bc;
			Console.ForegroundColor = fc;
		}

		public void Error(string message)
		{
			Console.BackgroundColor = ConsoleColor.DarkRed;
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine(message);
			Console.BackgroundColor = bc;
			Console.ForegroundColor = fc;
		}
	}

	public interface IGetDecimal
	{
		public decimal? Get();
	}

	public class GetDecimal : IGetDecimal
	{
		public decimal? Get()
		{
			decimal result;
			if (!decimal.TryParse(Console.ReadLine(), out result)) //вместо Try/Catch/Finally
			{
				return null;
			}
			return result;
		}
	}

	public interface ICalc
	{
		public void TakeArguments();
		public decimal? Sum();
		public void DisplaySum();
	}

	public class Calc : ICalc
	{
		ILogger Logger { get; }

		public decimal? a;
		public decimal? b;

		public Calc(ILogger logger)
		{
			this.Logger = logger;
		}

		public void TakeArguments()
		{
			GetDecimal getDecimal = new GetDecimal();
			Console.Write("введите первое слагаемое: ");
			a = getDecimal.Get();
			if (a == null) Logger.Error("понавводят тут всякого...");
			Console.Write("введите второе слагаемое: ");
			b = getDecimal.Get();
			if (b == null) Logger.Error("понавводят тут всякого...");
		}

		public void DisplaySum()
		{
			decimal? sum = Sum();
			if (sum == null) { Logger.Error("и как по-вашему это считать?"); }
			else { Logger.Event(sum.ToString()); }
		}

		public decimal? Sum()
		{
			return a + b;
		}
	}
}

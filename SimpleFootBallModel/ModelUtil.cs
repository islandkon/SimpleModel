using System;

namespace SimpleFootBallModel
{
	public class ModelUtil
	{
		public static double probabilityToOdds(double probability, double margin)
		{
			return 1.0 / (probability + margin);
		}

		public static string oddsFormat(double odds)
		{
			return String.Format("{0:0.00}",odds);
		}

		public static void printResult(double[] homeWin, double[] awayWin, double[] draw, double[] hilo, double margin ,double hiloTarget) {
			Console.WriteLine("Results: ");
			Console.WriteLine();

			Console.WriteLine("| Home | Away | Draw | (With Margin)");
			Console.WriteLine("| " + oddsFormat(probabilityToOdds(homeWin[AbstractModel.FULL_TIME], margin)) + " | " + oddsFormat(probabilityToOdds(awayWin[AbstractModel.FULL_TIME], margin)) + " | " + oddsFormat(probabilityToOdds(draw[AbstractModel.FULL_TIME],margin)) + " |");
			Console.WriteLine();

			Console.WriteLine("| Home | Away | Draw | (WithOut Margin)");
			Console.WriteLine("| " + oddsFormat(probabilityToOdds(homeWin[AbstractModel.FULL_TIME], 0.0)) + " | " + oddsFormat(probabilityToOdds(awayWin[AbstractModel.FULL_TIME], 0.0)) + " | " + oddsFormat(probabilityToOdds(draw[AbstractModel.FULL_TIME], 0.0)) + " |");
			Console.WriteLine();

			Console.WriteLine("| Home | Away | Draw | First Half HAD (With Margin)");
			Console.WriteLine("| " + oddsFormat(probabilityToOdds(homeWin[AbstractModel.HALF_TIME], margin)) + " | " + oddsFormat(probabilityToOdds(awayWin[AbstractModel.HALF_TIME], margin)) + " | " + oddsFormat(probabilityToOdds(draw[AbstractModel.HALF_TIME], margin)) + " |");
			Console.WriteLine();

			Console.WriteLine("| Home | Away | Draw | First Half HAD (WithOut Margin)");
			Console.WriteLine("| " + oddsFormat(probabilityToOdds(homeWin[AbstractModel.HALF_TIME], 0.0)) + " | " + oddsFormat(probabilityToOdds(awayWin[AbstractModel.HALF_TIME], 0.0)) + " | " + oddsFormat(probabilityToOdds(draw[AbstractModel.HALF_TIME], 0.0))+ " |");
			Console.WriteLine();

			Console.WriteLine("| Line | High | Low  | HiLo (With Margin)");
			Console.WriteLine("| " + hiloTarget + "  | " +oddsFormat(probabilityToOdds(hilo[AbstractModel.OVER_TARGET], margin)) + " | " + oddsFormat(probabilityToOdds(hilo[AbstractModel.UNDER_TARGET], margin)) + " |");
			Console.WriteLine();

			Console.WriteLine("| Line | High | Low  | HiLo (WithOut Margin)");
			Console.WriteLine("| " + hiloTarget + "  | " +oddsFormat(probabilityToOdds(hilo[AbstractModel.OVER_TARGET], 0.0)) + " | " + oddsFormat(probabilityToOdds(hilo[AbstractModel.UNDER_TARGET], 0.0)) + " |");
			Console.WriteLine();
		}
	}
}


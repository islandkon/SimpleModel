using System;

namespace SimpleFootBallModel
{
	/// <summary>
	/// Util class for calculating Zero Inflated Poission probability
	/// </summary>
	class MathUtil
	{
		/// <summary>
		/// Use Zero inflated Poission distribution to cater for zero inflated issue in football match 
		/// </summary>
		/// <param name="expected">target event</param>
		/// <param name="mean">mean for poission </param>
		/// <param name="lamdba">where (1-lambda) is the probability for extra zero</param>
		/// <returns>probability for ZIP expected value</returns>
		public static double ZeroInflatedPoisson(int expected, double mean ,double lamdba)
		{
			if (expected == 0)
			{
				return (1-lamdba) + lamdba * Poission(expected, mean);
			}
			else
			{
				return lamdba * Poission(expected, mean);
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="expected">target event</param>
		/// <param name="mean">mean for poission</param>
		/// <returns>probability for expected value</returns>
		public static double Poission(int expected, double mean)
		{
			return Math.Exp(-1.0*mean)*Math.Pow(mean,expected)/factorial(expected);
		}

		private static int factorial(int x)
		{
			if (x == 0)
				return 1;
			else
				return x * factorial(x - 1);
		}
	}
}


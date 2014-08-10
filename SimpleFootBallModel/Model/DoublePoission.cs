using System;

namespace SimpleFootBallModel
{
	public class DoublePoission : AbstractModel
	{
		private double halfTimeWeight;

		public DoublePoission (double home, double away, double weight) : base(home, away) 
		{
			this.halfTimeWeight = weight;
		}

		protected override double[,] generateScoreProbability(double expectancy)
		{
			double[,] array = new double[2,MATRIX_LENGTH];

			for (int i = 0; i < HALF_AND_FULLTIME;i++)
				for (int j = 0; j < MATRIX_LENGTH; j++)
				{
					if (i == HALF_TIME)
						array[i, j] = MathUtil.Poission(j, expectancy*halfTimeWeight);
					else
						array[i, j] = MathUtil.Poission(j, expectancy);
				}

			return array;
		}
	}
}


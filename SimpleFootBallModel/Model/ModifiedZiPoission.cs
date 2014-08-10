using System;

namespace SimpleFootBallModel
{
	public class ModifiedZiPoission : AbstractModel
	{
		private double lambda;
		private double halfTimeWeight;

		public ModifiedZiPoission (double home, double away, double lambda, double weight) : base(home, away)
		{
			this.lambda = lambda;
			this.halfTimeWeight = weight;
		}

		protected override double[,] generateScoreProbability(double expectancy)
		{
			double[,] array = new double[2,MATRIX_LENGTH];

			for (int i = 0; i < HALF_AND_FULLTIME;i++)
				for (int j = 0; j < MATRIX_LENGTH; j++)
				{
					if (i == HALF_TIME)
						array[i, j] = MathUtil.ZeroInflatedPoisson(j, expectancy*halfTimeWeight, lambda);
					else
						array[i, j] = MathUtil.ZeroInflatedPoisson(j, expectancy, lambda);
				}

			return array;
		}
	}
}


using System;

namespace SimpleFootBallModel
{
	/// <summary>
	/// Main class for odds generation
	/// </summary>
	class OddsGenerator
	{
		private IFootBallModel model;

		/// <summary>
		/// 4 percent margin
		/// </summary>
		private double margin = 0.04;
		private const double hiloTarget = 2.5;

		public OddsGenerator(IFootBallModel model)
		{
			this.model = model;
		}

		/// <summary>
		/// Generate score probabiility for each teamm then generate the joint score probability matrix
		/// </summary>
		public void generateOdds()
		{
			model.runModel();

			double[] homeWin = model.getHADProbability (AbstractModel.Result.HomeWin);
			double[] awayWin = model.getHADProbability (AbstractModel.Result.AwayWin);
			double[] draw = model.getHADProbability (AbstractModel.Result.Draw);

			double[] hilo = model.getHiLoProbabilty(hiloTarget);

			ModelUtil.printResult(homeWin, awayWin, draw, hilo, margin, hiloTarget);
		}
	}
}


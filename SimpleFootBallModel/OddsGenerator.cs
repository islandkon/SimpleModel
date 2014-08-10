using System;

namespace SimpleFootBallModel
{
	/// <summary>
	/// Main class for odds generation
	/// </summary>
	class OddsGenerator
	{
		private IFootBallModel model;
		private double margin;
		private double hiloTarget;

		public OddsGenerator(IFootBallModel model, double margin, double target)
		{
			this.model = model;
			this.margin = margin;
			this.hiloTarget = target;
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

			ModelUtil.printResult(model.GetType().ToString(), homeWin, awayWin, draw, hilo, margin, hiloTarget);
		}
	}
}


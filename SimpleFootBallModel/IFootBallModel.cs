using System;

namespace SimpleFootBallModel
{
	interface IFootBallModel
	{
		void runModel();
		double[] getHiLoProbabilty (double target);
		double[] getHADProbability (AbstractModel.Result result);
	}
}


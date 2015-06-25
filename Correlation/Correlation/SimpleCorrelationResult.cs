using System;

namespace Correlation
{
	public class SimpleCorrelationResult
	{
		public double meanA = 0.0;
		public double meanB = 0.0;
		public double sumAxB = 0.0;
		public double stdDevA = 0.0;
		public double stdDevB = 0.0;
		public double nbItem = 0;
		public SimpleCorrelationResult (
			double mA, double mB, double sumAB,
			double stddA, double stddB, double nbIte )
		{
			meanA = mA;
			meanB = mB;
			sumAxB = sumAB;
			stdDevA = stddA;
			stdDevB = stddB;
			nbItem = nbIte;
		}

	}
}


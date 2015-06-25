using System;
using System.Collections.Generic;

namespace Correlation
{
	public class CoreCorrelation
	{
		public CoreCorrelation (){
		}

		public Double Compute(List<int> listA, List<int> listB) {
			if ((listA == null) || (listB == null)){
				System.Console.WriteLine (".......computation skipped due to the lake ok input.......");
				return -5.0;
			}
			if (listA.Count != listB.Count){
				System.Console.WriteLine (".......computation skipped 'cause input must have same size as for now.......");
				return -5.0;
			}

			SimpleCorrelationResult simpleCorrelationResult = SimpleCorrelation(listA,listB);
			Double result = correlationFormula (simpleCorrelationResult);
			return result ;
		}

		public SimpleCorrelationResult SimpleCorrelation(List<int> listA, List<int> listB){
			Double meanA = meanComputation (listA);
			Double meanB = meanComputation (listB);
			SimpleCorrelationResult simpleCorrelationResult = 
				new SimpleCorrelationResult (
					meanA,meanB,
                    sumTupleMultipled(listA,listB),
					StandardDeviation(listA,meanA),
					StandardDeviation(listB,meanB),
					listA.Count);

		//	System.Console.WriteLine ("-> sum (x,y) = " + simpleCorrelationResult.sumAxB);
		//	System.Console.WriteLine ("-> mean (x) = " + simpleCorrelationResult.meanA);
		//	System.Console.WriteLine ("-> mean (y) = " + simpleCorrelationResult.meanB);
		//	System.Console.WriteLine ("-> stDev (x) = " + simpleCorrelationResult.stdDevA);
		//	System.Console.WriteLine ("-> stDev (y) = " + simpleCorrelationResult.stdDevB);

			return simpleCorrelationResult;
		}
		public double correlationFormula ( SimpleCorrelationResult simpleCorrelationResult ){
			double result = 0.0;
			result = (simpleCorrelationResult.sumAxB / simpleCorrelationResult.nbItem) - (simpleCorrelationResult.meanA *simpleCorrelationResult.meanB);
			result = result / (simpleCorrelationResult.stdDevA * simpleCorrelationResult.stdDevB);
			result = result *(simpleCorrelationResult.nbItem/(simpleCorrelationResult.nbItem -1));
			return result;
		}

		public double meanComputation(List<int> listX){
			Double result = 0.0;
			int sizeOfLists = listX.Count;
			for (int i=0; i< sizeOfLists ; i++){
				result = result + listX[i];
			}

			return result/sizeOfLists;
		}

		public double sumListElement(List<int> listX){
			double result = 0.0;
			foreach (int x in listX) {
				result = result + x;
			}
			return result;
		}

		public double sumTupleMultipled(List<int> listA, List<int> listB){
			//sum AxB
			double result = 0.0;
			double sizeOfLists = listA.Count;

			for (int i=0; i< sizeOfLists ; i++){
				result = result + (listA[i] * listB[i]);
			}			
			return result;
		}
	public double StandardDeviation(List<int> listX, double mean){
			double result = 0.0;
			int sizeOfLists = listX.Count;
			for (int i=0; i< sizeOfLists ; i++){
				result = result +Math.Pow((listX[i] - mean),2) ;
			}			
			return Math.Sqrt(result / (sizeOfLists-1)) ;
		}

	public double Slope(List<int> listA, List<int> listB){
			Double result = 0.0; 
			Double nbElement = listA.Count;
			Double sumA = sumListElement (listA);
			Double sumB = sumListElement (listB);
			Double sumAxB = sumTupleMultipled (listA, listB);
			Double sumAxA = sumTupleMultipled (listA, listA);
			result = (nbElement * sumAxB) - (sumA * sumB);
			result = result/((nbElement*sumAxA)-Math.Pow(sumA,2));
			System.Console.WriteLine ("-> slope = " + result);
			return result;
		}

		public double Intercept2(List<int> listA, List<int> listB){
			Double result = 0.0; 
			Double nbElement = listA.Count;
			Double sumA = sumListElement (listA);
			Double sumB = sumListElement (listB);
			Double sumAxB = sumTupleMultipled (listA, listB);
			Double sumAxA = sumTupleMultipled (listA, listA);
			result = (sumB * sumAxA) - (sumAxB * sumA);
			result = result/((nbElement*sumAxA)-Math.Pow(sumA,2));
			System.Console.WriteLine ("-> slope 2 = " + result);
			return result;
		}
		public double Intercept( List<int> listA, List<int> listB, double slope){
			double result = 0.0;
			Double nbElement = listA.Count;
			Double sumA = sumListElement (listA);
			Double sumB = sumListElement (listB);
			result = (sumB - (slope) * sumA) / nbElement;
			return result;
		}

		//S(t)=S(0)exp(mean−((stdev*stdev)/2))t+Bt
		public double BrownianStep ( double s0, double mean, double stdev, int t, double brownianT){
			return s0*(Math.Exp(mean-((stdev*stdev)/2))) + brownianT;
		}

	}
}


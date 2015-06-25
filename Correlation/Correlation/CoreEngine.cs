using System;
using System.Collections.Generic;

namespace Correlation
{
	public class CoreEngine
	{
		CoreCorrelation coreCorrelation = new CoreCorrelation();
		public CoreEngine ()
		{
		}

		public List<double> computeMultiCorrelation (List<int> base0, List<Item> items){
			List<double> result = new List<double> ();
			foreach (Item item in items) {
				result.Add(coreCorrelation.Compute (base0, item.getListResult()));
			}
			return result;
		}

		public GlobalResult computeAllMultiCorrelation ( List<Item> items , int nbIter){
			GlobalResult globalResult = new GlobalResult ();
			int count = 1;
			globalResult.globalMeanSumOfb = 0.0;
			globalResult.globalMeanCorrelFacingAllTogether = 0.0;
			foreach (Item it in items) {
				ItemResult itemResult = new ItemResult ();
				itemResult.id = it.id;
				List<int> base0 = it.getListResult ();
				itemResult.correlFacingAllOther = computeMultiCorrelation (base0,items);
				itemResult.sumOfb = it.sumOfb ();
				globalResult.globalMeanSumOfb = globalResult.globalMeanSumOfb + itemResult.sumOfb;
				globalResult.globalMeanCorrelFacingAllTogether = globalResult.globalMeanCorrelFacingAllTogether +
					itemResult.computeMeanCorrelFacingAllOther();
				globalResult.itemResults.Add (itemResult);
				System.Console.WriteLine ("computeAllMultiCorrelation " + count + " / " + nbIter);
				count ++;
				if (count > nbIter) {
					break;
				}
			}
			globalResult.nbItem = nbIter;
			globalResult.globalMeanSumOfb = globalResult.globalMeanSumOfb / globalResult.nbItem;
			globalResult.globalMeanCorrelFacingAllTogether = globalResult.globalMeanCorrelFacingAllTogether / globalResult.nbItem;
			return globalResult;
		}

		public OccursResult computeOccursForAll(List<Item> items, int start, int end){
			OccursResult occursResult = new OccursResult ();
			int count = 1;
			foreach (Item it in items) {
				if ((count <= end) && (count >=start)) {
					occursResult.addOccurs (it.b1, it.id);
					occursResult.addOccurs (it.b2, it.id);
					occursResult.addOccurs (it.b3, it.id);
					occursResult.addOccurs (it.b4, it.id);
					occursResult.addOccurs (it.b5, it.id);
					//occursResult.addOccurs (it.bc);
				} else {
					if (count > end) {
						break;
					}
				}
				count++;
			}
			return occursResult;
		}
	}
}


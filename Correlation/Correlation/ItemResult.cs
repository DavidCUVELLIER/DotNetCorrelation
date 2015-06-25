using System;
using System.Collections.Generic;

namespace Correlation
{
	public class ItemResult
	{
		public int id;
		public List<double> correlFacingAllOther;
		public double meanCorrelFacingAllOther;
		public int sumOfb;
		public ItemResult ()
		{
		}
			
		public String correlFacingAllOtherToString(int depth){
			String result = "";
			int count = 0;
			foreach (double val in correlFacingAllOther) {
				if (depth > count) {
					if (count == 0) {
						result = val.ToString();
					} else {
						result = result + ";" + val;
					}
					++count;
				} else {
					break;
				}
			}
			return result;
		}

		public double computeMeanCorrelFacingAllOther(){
			double result = 0.0;
			foreach (double val in correlFacingAllOther) {
				result =  result + val;
			}
			return result/correlFacingAllOther.Count;
		}
	}
}


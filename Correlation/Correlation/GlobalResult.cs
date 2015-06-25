using System;
using System.Collections.Generic;

namespace Correlation
{
	public class GlobalResult
	{
		public List<ItemResult> itemResults = new List<ItemResult>();
		public Dictionary<int,ListSpread> listSpreadForEachItem = new Dictionary<int,ListSpread>();
		public Dictionary<int,int> listStatSpread = new Dictionary<int,int>();
		public double globalMeanSumOfb ;
		public double globalMeanCorrelFacingAllTogether;
		public int nbItem;

		public GlobalResult ()
		{
		}

		public void computeListSpreadForEachItem (InputSumUp inputSumUp, OccursResult occursResult)
		{
			foreach (KeyValuePair<int, DetailOccurs> kvp in occursResult.occurs) {
				DetailOccurs detailOccurs = new DetailOccurs ();
				detailOccurs.detailFrequency = occursResult.occurs [kvp.Key].detailFrequency;
				ListSpread listSpread = new ListSpread ();
				listSpread.spreads = detailOccurs.getListSpread (inputSumUp.nbRunByYear);
				listSpreadForEachItem.Add (kvp.Key, listSpread);
			}
		}

		public void computelistStatSpread(){
			foreach(KeyValuePair<int, ListSpread> kvp in listSpreadForEachItem){
				foreach(int val in kvp.Value.spreads){
					if (listStatSpread.ContainsKey (val)) {
						int count = listStatSpread [val];
						listStatSpread [val] = ++count;
					}else{
						listStatSpread.Add(val, 1);
					}
				}
			}
		}
	}
}


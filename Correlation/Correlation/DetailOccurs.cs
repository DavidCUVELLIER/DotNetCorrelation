using System;
using System.Collections.Generic;

namespace Correlation
{
	public class DetailOccurs
	{
		public int nbOccurs = 0;
		public List<int> detailFrequency = new List<int>();

		public DetailOccurs ()
		{
		}

		public String DetailFrequencyToString(){
			String detailFreqInALine = "";
			bool isFirst = true;
			try{
			foreach ( int val in detailFrequency){
				if (isFirst == true){
					detailFreqInALine = detailFreqInALine + val;
					isFirst = false;
				}else{
					detailFreqInALine = detailFreqInALine + ";" + val;
				}
			}
			}catch( Exception e){
				System.Console.WriteLine ("DetailFrequencyToString failed " + e.StackTrace.ToString());

			}
			return detailFreqInALine;
		}

		public List<int> getListSpread(Dictionary<int, int> nbRunByRun){
			List<int> result = new List<int> ();
			bool isFirst = true;
			int lastVal = 0;
			foreach (int val in detailFrequency) {
				if (isFirst == false) {

					int yearForThisRun = Convert.ToInt16(val.ToString ().Substring (0, 4));
					if (yearForThisRun == 2008) {
						yearForThisRun = 2009;
					}
					int yearforPreviousRun = Convert.ToInt16(lastVal.ToString ().Substring (0, 4));
					if (yearforPreviousRun == 2008) {
						yearforPreviousRun = 2009;
					}					
					int runNumberforThisRun = Convert.ToInt16 (val.ToString ().Substring (4, 3));
					int runNumberforPreviousRun = Convert.ToInt16 (lastVal.ToString ().Substring (4, 3));

					int difYear = yearforPreviousRun -yearForThisRun ;
					int difVal = 0;
					if (difYear == 0) {
						difVal = runNumberforPreviousRun - runNumberforThisRun;
					}else  {
						System.Console.WriteLine (runNumberforThisRun + ";" + yearForThisRun);
						difVal = (runNumberforPreviousRun) +
						(nbRunByRun [yearForThisRun] - runNumberforThisRun + 1);
					}
					result.Add (difVal);
				} else {

					isFirst = false;
				}
				lastVal = val;
			}
			return result;
		}
	}
}


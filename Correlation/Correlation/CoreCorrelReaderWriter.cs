using System;
using System.IO;
using System.Collections.Generic;

namespace Correlation
{
	public class CoreCorrelReaderWriter
	{
		public string pathMain;
		public string pathCorrel;
		public string pathOccurs;
		public string pathListSpreadStat;
		public string pathListSpread;
		public string pathExampleBrownian;

		public CoreCorrelReaderWriter ()
		{
		}

		public void WriteOccursResult(OccursResult occursResult){
			using (StreamWriter sw = new StreamWriter(pathOccurs)) {
				sw.WriteLine ("b;nbOccurs;occursFrequency");
				sw.WriteLine (occursResult.ToString ());
			}
		}

		public void WriteGlobalResult(GlobalResult globalResult, int depth){

			using (StreamWriter sw = new StreamWriter(pathCorrel)) {
				sw.WriteLine ("id;mean of the sum;mean of the MeanCorrelFacingAllOther;sumOfb;MeanCorrelFacingAllOther;correlFacingAllOtherToString");
				foreach (ItemResult val in globalResult.itemResults) {
					sw.WriteLine(val.id 
						+ ";" + globalResult.globalMeanSumOfb
						+ ";" + globalResult.globalMeanCorrelFacingAllTogether
						+ ";" + val.sumOfb
						+ ";" + val.computeMeanCorrelFacingAllOther() 
						+ ";" + val.correlFacingAllOtherToString(depth));
				}
			}
		}

		public void WriteDetailOccursListSpread(GlobalResult globalResult){
			using (StreamWriter sw = new StreamWriter (pathListSpread)) {
				sw.WriteLine ("item;listDelta");
				for (int i = 1; i < globalResult.listSpreadForEachItem.Keys.Count; i++) {
					String line = i + ";";
					ListSpread listSpread = new ListSpread ();
					globalResult.listSpreadForEachItem.TryGetValue (i, out listSpread);
					foreach (int val in listSpread.spreads) {
						line = line + val + ";";
					}
					sw.WriteLine (line);
				
				}
			}
		}

		public void WriteDetailOccursListSpreadStat(GlobalResult globalResult){
			using (StreamWriter sw = new StreamWriter (pathListSpreadStat)) {
				sw.WriteLine ("item;stat");
				foreach (KeyValuePair<int,int> kvp in  globalResult.listStatSpread) {
					sw.WriteLine (kvp.Key +";" +kvp.Value);
				}
			}
		}

		public void WriteBrownian(List<double> input){
			using (StreamWriter sw = new StreamWriter (pathExampleBrownian)) {
				sw.WriteLine ("value");

				foreach (double value in input) {
					sw.WriteLine (value);
				}
			}
		}

		public Boolean forceDeleteFile(){
			if (File.Exists (pathCorrel)) {
				File.Delete (pathCorrel);
			}
			if (File.Exists (pathOccurs)) {
				File.Delete (pathOccurs);
			}
			return true;
		}


		public InputSumUp ReadAllLines(){
			InputSumUp inputSumUp = new InputSumUp ();
			Boolean isHeader = true;

			using (StreamReader sr = new StreamReader(pathMain)) 
			{
				while (sr.Peek () >= 0) {
					String line = sr.ReadLine ();
					if (isHeader == false) {
						String[] lineElements = line.Split (';');
						int Year = Convert.ToInt16 (lineElements [0].Substring (0, 4));
						if (Year == 2008) {
							Year = 2009;
						}
						int runForThisYear = Convert.ToInt16 (lineElements [0].Substring (4, 3));
						if (inputSumUp.nbRunByYear.ContainsKey (Year) == true) {
							if ( runForThisYear > inputSumUp.nbRunByYear [Year]) {
								inputSumUp.nbRunByYear [Year] = runForThisYear;
							}
						}else{
							inputSumUp.nbRunByYear.Add (Year, runForThisYear);
						}

						int b01 = Convert.ToInt16 (lineElements [4]);
						int b02 = Convert.ToInt16 (lineElements [5]);
						int b03 = Convert.ToInt16 (lineElements [6]);
						int b04 = Convert.ToInt16 (lineElements [7]);
						int b05 = Convert.ToInt16 (lineElements [8]);
						int b0c = Convert.ToInt16 (lineElements [9]);
						int id0 = Convert.ToInt32 (lineElements [0]);
						Item item = new Item (b01, b02, b03, b04, b05, b0c, id0);
						inputSumUp.items.Add (item);
					} else {
						isHeader = false;
					}
				}
			}
			return inputSumUp;
		}
	}    
}


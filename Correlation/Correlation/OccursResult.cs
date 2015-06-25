using System;
using System.Collections.Generic;
namespace Correlation
{
	public class OccursResult
	{
		public Dictionary<int,DetailOccurs> occurs = new Dictionary<int,DetailOccurs>();
		public OccursResult ()
		{
		}

		public void resetOccurs(){
			occurs.Clear ();
		}

		public void addOccurs(int value, int id){
			if ( occurs.ContainsKey(value) == false){
				DetailOccurs detailOccurs = new DetailOccurs ();
				detailOccurs.nbOccurs = 1;
				detailOccurs.detailFrequency.Add (id);
				occurs.Add(value,detailOccurs);
			}else{
				DetailOccurs detailOccurs = occurs[value];
				detailOccurs.detailFrequency.Add (id);
				detailOccurs.nbOccurs++;
				occurs[value]=detailOccurs;
			}
		}

		override public String ToString(){
			String result = "";

			foreach (KeyValuePair<int,DetailOccurs> kvp in occurs) {
				result = result + kvp.Key + ";" + kvp.Value.nbOccurs + ";" + kvp.Value.DetailFrequencyToString()+ "\n";
			}

			return result;
		}

	}
}


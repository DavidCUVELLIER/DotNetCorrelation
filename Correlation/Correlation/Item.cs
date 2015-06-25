using System;
using System.Collections.Generic;

namespace Correlation
{
	public class Item
	{
		public int b1;
		public int b2;
		public int b3;
		public int b4;
		public int b5;
		public int bc;
		public int id;

		public Item ()
		{
		}

		public Item (int b01,
			 int b02,
			 int b03,
			 int b04,
			 int b05,
			int b0c,
			int id0){
			b1 = b01;
			b2 = b02;
			b3 = b03;
			b4 = b04;
			b5 = b05;
			bc = b0c;
			id = id0;
		}

		public List<int> getListResult(){
			List<int> result = new List<int> ();
			result.Add (b1);
			result.Add (b2);
			result.Add (b3);
			result.Add (b4);
			result.Add (b5);
	//		result.Add (bc);
			return result;
		}

		public int sumOfb() {
//			return b1 + b2 + b3 + b4 + b5 + bc;
			return b1 + b2 + b3 + b4 + b5;
		}
	}
}


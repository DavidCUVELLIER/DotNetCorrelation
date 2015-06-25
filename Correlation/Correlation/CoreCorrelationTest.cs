using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Correlation
{
	[TestFixture ()]
	public class CoreCorrelationTest
	{
		CoreCorrelation coreCorrelatrion = new CoreCorrelation ();
		CoreCorrelReaderWriter coreCorrelReader = new CoreCorrelReaderWriter();
		CoreCorrelReaderWriter coreCorrelWriter = new CoreCorrelReaderWriter();
		CoreEngine coreEngine = new CoreEngine();
		String testPath = "/Users/cuvellierdavid/IT/dir/DotNetCorrelation";

		[Test ()]
		public void TestComputePerfectCorrelation (){
			List<int> listFiguresA = new List<int> ();
			listFiguresA.Add (1);
			listFiguresA.Add (2);
			listFiguresA.Add (3);
			listFiguresA.Add (4);
			List<int> listFiguresB = new List<int> ();
			listFiguresB.Add (1);
			listFiguresB.Add (2);
			listFiguresB.Add (3);
			listFiguresB.Add (4);
			Double result = coreCorrelatrion.Compute (listFiguresA, listFiguresB);
			Assert.AreEqual (1, result);
		}

		[Test ()]
		public void TestComputeAlmostPerfectCorrelation (){
			List<int> listFiguresA = new List<int> ();
			listFiguresA.Add (1);
			listFiguresA.Add (2);
			listFiguresA.Add (3);
			listFiguresA.Add (4);
			List<int> listFiguresB = new List<int> ();
			listFiguresB.Add (1);
			listFiguresB.Add (2);
			listFiguresB.Add (3);
			listFiguresB.Add (6);
			Double result = coreCorrelatrion.Compute (listFiguresA, listFiguresB);
			Assert.AreEqual (0.956, Math.Round(result,3));
		}

		[Test ()]
		public void TestSimpleCorrelationNotCorrelated(){
			List<int> listFiguresA = new List<int> ();
			listFiguresA.Add (65);
			listFiguresA.Add (67);
			listFiguresA.Add (71);
			listFiguresA.Add (71);
			listFiguresA.Add (66);
			listFiguresA.Add (75);
			listFiguresA.Add (67);
			listFiguresA.Add (70);
			listFiguresA.Add (71);
			listFiguresA.Add (69);
			listFiguresA.Add (69);

			List<int> listFiguresB = new List<int> ();
			listFiguresB.Add (175);
			listFiguresB.Add (133);
			listFiguresB.Add (185);
			listFiguresB.Add (163);
			listFiguresB.Add (126);
			listFiguresB.Add (198);
			listFiguresB.Add (153);
			listFiguresB.Add (163);
			listFiguresB.Add (159);
			listFiguresB.Add (151);
			listFiguresB.Add (159);
			SimpleCorrelationResult result = coreCorrelatrion.SimpleCorrelation (listFiguresA, listFiguresB);
			Assert.AreEqual (11, result.nbItem);
			Assert.AreEqual (2.85720779146991, Math.Round(result.stdDevA,14));
			Assert.AreEqual (20.8, Math.Round(result.stdDevB,1));
			Assert.AreEqual (69.2, Math.Round(result.meanA,1));
			Assert.AreEqual (160.5, Math.Round(result.meanB,1));

			Double correl = coreCorrelatrion.correlationFormula (result);
			Assert.AreEqual (0.663, Math.Round(correl,3));
		}

		[Test ()]
		public void TestRegressionSimple(){
			List<int> listFiguresA = new List<int> ();
			listFiguresA.Add (55);
			listFiguresA.Add (60);
			listFiguresA.Add (65);
			listFiguresA.Add (70);
			listFiguresA.Add (80);

			List<int> listFiguresB = new List<int> ();
			listFiguresB.Add (52);
			listFiguresB.Add (54);
			listFiguresB.Add (56);
			listFiguresB.Add (58);
			listFiguresB.Add (62);
			double slope = coreCorrelatrion.Slope (listFiguresA, listFiguresB);
			Assert.AreEqual (0.4, slope);
			double result = coreCorrelatrion.Intercept(listFiguresA, listFiguresB, slope);
			Assert.AreEqual (30, result);
			double result2 = coreCorrelatrion.Intercept2(listFiguresA, listFiguresB);
			Assert.AreEqual (30, result2);

		}

		[Test ()]
		public void TestChain(){
			coreCorrelReader.pathMain =  testPath + @"/Correlation/Correlation/resources/nl.csv";
			InputSumUp inputSumUp = coreCorrelReader.ReadAllLines ();
			Assert.IsNotNull (inputSumUp.items);
			Assert.IsTrue (inputSumUp.nbRunByYear [2010] == 156);
			GlobalResult globalResult = coreEngine.computeAllMultiCorrelation (inputSumUp.items,20);
			Assert.IsNotNull (globalResult.itemResults);
			coreCorrelWriter.pathCorrel = testPath + @"/Correlation/Correlation/output/multiCorrelation.csv";
			coreCorrelWriter.WriteGlobalResult (globalResult,20);

			OccursResult occursResult = coreEngine.computeOccursForAll (inputSumUp.items, 1, 1044);
			Assert.IsNotNull (occursResult.occurs.Count);
			coreCorrelWriter.pathOccurs = testPath + @"/Correlation/Correlation/output/multiOccurs.csv";
			coreCorrelWriter.WriteOccursResult (occursResult);

			globalResult.computeListSpreadForEachItem (inputSumUp, occursResult);
			coreCorrelWriter.pathListSpread = testPath + @"/Correlation/Correlation/output/detailOccurs.csv";

			globalResult.computelistStatSpread ();
			coreCorrelWriter.pathListSpreadStat = testPath + @"/Correlation/Correlation/output/ListSpreadStat.csv";
			coreCorrelWriter.WriteDetailOccursListSpreadStat (globalResult);
		}

		[Test ()]
		public void TestDetailOccursIsOk(){
			DetailOccurs detailOccurs = new DetailOccurs ();
			List<int> detail = new List<int> ();
			detail.Add (2013001);
			detail.Add (2012157);
			detail.Add (2012156);
			detailOccurs.detailFrequency = detail;
			Dictionary<int, int> nbRunByRun = new Dictionary<int, int> ();
			nbRunByRun.Add (2013, 150);
			nbRunByRun.Add (2012, 159);
			List<int> result = detailOccurs.getListSpread(nbRunByRun);
			Assert.IsTrue (result[0] == 4);		
			Assert.IsTrue (result[1] == 1);		
		}

		[Test ()]
		public void TestBrownian(){
			double s0 = 0.5;
			double mean = 1;
			double stdev = 0.4;
			int t = 1;
			double brownianT = 0.1;
			double brownianStep = coreCorrelatrion.BrownianStep (s0, mean, stdev, t, brownianT);
			Assert.IsTrue (Math.Round(brownianStep,2) == 1.35);		

			List<double> brownianCurve = new List<double> ();
			double previous = 0.5;
			for (int i=1; i<=100; i++){
				double rand = (new Random ()).Next(40);
				double next = coreCorrelatrion.BrownianStep (previous, mean, stdev, t,rand) ;
				brownianCurve.Add (next);
				previous = next;
			}

			coreCorrelWriter.pathExampleBrownian =  testPath + @"/Correlation/Correlation/output/BrownianExample.csv";
			coreCorrelWriter.WriteBrownian (brownianCurve);
		}
	}
}


/*
pmml4net - easy lib to read and consume tree model in PMML file
Copyright (C) 2013  Damien Carol <damien.carol@gmail.com>

This library is free software; you can redistribute it and/or
modify it under the terms of the GNU Library General Public
License as published by the Free Software Foundation; either
version 2 of the License, or (at your option) any later version.

This library is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
Library General Public License for more details.

You should have received a copy of the GNU Library General Public
License along with this library; if not, write to the
Free Software Foundation, Inc., 51 Franklin St, Fifth Floor,
Boston, MA  02110-1301, USA.
 */

using System;
using System.Collections.Generic;
using System.Xml;
using NUnit.Framework;

namespace pmml4net.tests
{
	/// <summary>
	/// Description of RuleSetModelTest.
	/// </summary>
	public class RuleSetModelTest
	{
		[Test()]
		[Ignore()]
		public void ScoreExample1Test()
		{
			string pFilePath = "test-ruleset1.xml";
			string modelname = "NestedDrug";
			string paramList = "BP='HIGH', K=0.0621, Age = 36, Na = 0.5023";
			string res = "may play";
			decimal confidence = 0.47M;
			
			Pmml pmml = Pmml.loadModels(pFilePath);
			
			Assert.NotNull(pmml);
			
			ModelElement model = pmml.getByName(modelname);
			Assert.NotNull(model);
			
			Assert.IsInstanceOf<RuleSetModel>(model);
			
			RuleSetModel rs = (RuleSetModel)model;
			
			// Check exemple 1 as 3 RuleSelectionMethod for first node
			Assert.IsNotNull(rs.RuleSet);
			Assert.IsNotNull(rs.RuleSet.RuleSelectionMethods);
			Assert.AreEqual(3, rs.RuleSet.RuleSelectionMethods.Count);
			
			// Check 3 Rule
			Assert.AreEqual(3, rs.RuleSet.Rules.Count);
			
			// Modification for aggregateNode
			//tree.MissingValueStrategy = MissingValueStrategy.AggregateNodes;
			
			Dictionary<string, object> lDict = parseParams(paramList);
			
			ScoreResult result = rs.Score(lDict);
			Assert.NotNull(result);
			
			
			/*foreach(Node item in result.Nodes)
			{
				Console.WriteLine("Node {0} = score {1}", item.Id, item.Score);
				
				foreach(ScoreDistribution it2 in item.ScoreDistributions)
					Console.WriteLine("\tScore Dist. {0} ({1}) = {2}", it2.Value, it2.RecordCount, it2.Confidence);
			}*/
			
			Assert.AreEqual(res, result.Value);
			Assert.AreEqual(confidence, result.Confidence);
		}
		
		[Test()]
		public void ScoreCarfTest()
		{
			ScoreCarfTest("CHEQUE='0002110', ZIB='075030003908', ZIN = '309037200000', Montant = 0.01", "1", 1.0M);
		}
		
		public void ScoreCarfTest(string paramList, string res, decimal confidence)
		{
			string pFilePath = "models\\RuleSetCarrefour.xml";
			string modelname = "CARF-20140124";
			
			Pmml pmml = Pmml.loadModels(pFilePath);
			
			Assert.NotNull(pmml);
			
			ModelElement model = pmml.getByName(modelname);
			Assert.NotNull(model);
			
			Assert.IsInstanceOf<RuleSetModel>(model);
			
			RuleSetModel rs = (RuleSetModel)model;
			
			// Check CARF as 1 RuleSelectionMethod for first node
			Assert.IsNotNull(rs.RuleSet);
			Assert.IsNotNull(rs.RuleSet.RuleSelectionMethods);
			Assert.AreEqual(1, rs.RuleSet.RuleSelectionMethods.Count);
			Assert.AreEqual("firstHit", rs.RuleSet.RuleSelectionMethods[0].Criterion);
			
			// Check 11 Rule
			Assert.AreEqual(11, rs.RuleSet.Rules.Count);
			
			// Modification for aggregateNode
			//tree.MissingValueStrategy = MissingValueStrategy.AggregateNodes;
			
			Dictionary<string, object> lDict = parseParams(paramList);
			
			ScoreResult result = rs.Score(lDict);
			Assert.NotNull(result);
			
			
			/*foreach(Node item in result.Nodes)
			{
				Console.WriteLine("Node {0} = score {1}", item.Id, item.Score);
				
				foreach(ScoreDistribution it2 in item.ScoreDistributions)
					Console.WriteLine("\tScore Dist. {0} ({1}) = {2}", it2.Value, it2.RecordCount, it2.Confidence);
			}*/
			
			Assert.AreEqual(res, result.Value);
			Assert.AreEqual(confidence, result.Confidence);
		}
		
		private Dictionary<string, object> parseParams(string parameters)
		{
			Dictionary<string, object> lDict = new Dictionary<string, object>();
			
			foreach (string item in parameters.Split( new char[] {','}, StringSplitOptions.RemoveEmptyEntries))
			{
				lDict.Add(item.Split('=')[0].Trim(), item.Split('=')[1].Trim().Replace("\"", ""));
			}
			
			return lDict;
		}
	}
}

/*
 * Created by SharpDevelop.
 * User: Damien
 * Date: 24/04/2013
 * Time: 15:14
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Xml;
using NUnit.Framework;

namespace pmml4net.tests
{
	/// <summary>
	/// Description of TreeModelTest.
	/// </summary>
	public class TreeModelTest
	{
		/// <summary>
		/// Load some generated PMML from other vendors.
		/// </summary>
		/// <param name="pFilePath"></param>
		[TestCase("AuditTree.xml")]
		[TestCase("BigML1.xml")] // 51794c13e4b024977881b628
		[TestCase("IrisTree.xml")]
		[TestCase("SPSS.xml")]
		[TestCase("test-golfing1.xml")]
		[TestCase("test-golfing2.xml")]
		[TestCase("test-simpleset.xml")]
		public void TreeModelsTest(string pFilePath)
		{
			Pmml pmml = Pmml.loadModels(pFilePath);
			
			Assert.NotNull(pmml);
			
			Assert.AreEqual(pmml.TreeModels.Count, 1);
		}
		
		[TestCase("test-golfing1.xml", "golfing",
		          "temperature=75, humidity=55, windy=\"false\", outlook=\"overcast\"",
		          "may play")]
		[TestCase("test-golfing2.xml", "golfing",
		          "temperature=45, humidity=60, outlook=\"sunny\"",
		          "no play")]
		[TestCase("test-golfing2.xml", "golfing",
		          "outlook=\"sunny\"",
		          "will play")]
		[TestCase("test-golfing2.xml", "golfing",
		          "",
		          "will play")]
		[TestCase("BigML1.xml", "51794c13e4b024977881b628",
		          "000000=78, 000012=0.05, 000013=0.05",
		          "Non recurrent")] // confidence = 83%
		[TestCase("BigML1.xml", "51794c13e4b024977881b628",
		          "000000=36, 00000c=3.12, 000013=0",
		          "Non recurrent")] // confidence = 83%
		[TestCase("BigML1.xml", "51794c13e4b024977881b628",
		          "000000=4, 00000c=2.1441125, 000013=0.03677125, 00000f=0.0191225",
		          "Non recurrent")] // confidence = 51%
		[TestCase("BigML1.xml", "51794c13e4b024977881b628",
		          "00001d=1, 00000c=1, 000013=0.01, 000019=0.51323, 000018=900, 000000=25",
		          "Recurrent")] // confidence = 83.18%
		public void ScoreTest(string pFilePath, string modelname, string paramList, string res)
		{
			Pmml pmml = Pmml.loadModels(pFilePath);
			
			Assert.NotNull(pmml);
			
			TreeModel tree = pmml.getByName(modelname);
			Assert.NotNull(tree);
			
			Dictionary<string, object> lDict = parseParams(paramList);
			
			ScoreResult result = tree.Score(lDict);
			Assert.NotNull(result);
			
			
			foreach(Node item in result.Nodes)
			{
				Console.WriteLine("Node {0} = score {1}", item.Id, item.Score);
				
				foreach(ScoreDistribution it2 in item.ScoreDistributions)
					Console.WriteLine("\tScore Dist. {0} ({1}) = {2}", it2.Value, it2.RecordCount, it2.Confidence);
			}
			
			Assert.AreEqual(res, result.Value);
		}
		
		[TestCase("test-golfing1.xml")]
		public void loadTest(string pFilePath)
		{
			Pmml pmml = Pmml.loadModels(pFilePath);
			
			Assert.NotNull(pmml);
			
			TreeModel tree = pmml.getByName("golfing");
			Assert.NotNull(tree);
			
			// Test first Node
			Node nodeFirst = tree.Node;
			Assert.NotNull(nodeFirst);
			Assert.NotNull(nodeFirst.Predicate);
			Assert.IsTrue(nodeFirst.Predicate is TruePredicate);
			Assert.AreEqual(2, nodeFirst.Nodes.Count);
			
			//
			Assert.AreEqual(2, tree.Node.Nodes[0].Nodes.Count);
			Assert.AreEqual(2, tree.Node.Nodes[1].Nodes.Count);
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
		
		[TestCase("test-simpleset.xml", 
		          "in=5", 
		          "1")]
		public void SimpleSetPredicateTest(string pFilePath, string paramList, string res)
		{
			Pmml pmml = Pmml.loadModels(pFilePath);
			
			Assert.NotNull(pmml);
			
			TreeModel tree = pmml.getByName("SimpleSetTest");
			Assert.NotNull(tree);
			
			Dictionary<string, object> lDict = parseParams(paramList);
			
			ScoreResult result = tree.Score(lDict);
			Assert.NotNull(result);
			
			
			Assert.AreEqual(2, result.Nodes.Count);
			
			Assert.AreEqual(res, result.Value);
			
		}
		
		/// <summary>
		/// Test operator isMissing for a simple predicate
		/// </summary>
		[TestCase()]
		public void SimplePredicateIsMissingTest()
		{
			string pmmlStr = @"<?xml version=""1.0"" ?>
<PMML version=""4.1"" xmlns=""http://www.dmg.org/PMML-4_1"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"">
	<Header copyright=""www.dmg.org"" description=""A very small binary tree model to test SimpleSetPredicate.""/>
	<DataDictionary numberOfFields=""2"" >
		<DataField name=""in"" optype=""continuous"" dataType=""double""/>
		<DataField name=""out"" optype=""continuous"" dataType=""double""/>
	</DataDictionary>
	<TreeModel modelName=""SimpleIsMissingTest"" functionName=""classification"">
		<MiningSchema>
			<MiningField name=""in""/>
			<MiningField name=""out"" usageType=""predicted""/>
		</MiningSchema>
		<Node score=""0"">
			<True/>
			<Node score=""1"">
				<SimplePredicate field=""in"" operator=""isMissing"" />
			</Node>
		</Node>
	</TreeModel>
</PMML>
";
			XmlDocument xml = new XmlDocument();
			xml.LoadXml(pmmlStr);
			Pmml pmml = Pmml.loadModels(xml);
			
			ScoreResult res = pmml.getByName("SimpleIsMissingTest").Score(new Dictionary<string, object>());
			
			Assert.AreEqual("1", res.Value);
		}
		
		/// <summary>
		/// Test operator isNotMissing for a simple predicate
		/// </summary>
		[TestCase()]
		public void SimplePredicateIsNotMissingTest()
		{
			string pmmlStr = @"<?xml version=""1.0"" ?>
<PMML version=""4.1"" xmlns=""http://www.dmg.org/PMML-4_1"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"">
	<Header copyright=""www.dmg.org"" description=""A very small binary tree model to test SimpleSetPredicate.""/>
	<DataDictionary numberOfFields=""2"" >
		<DataField name=""in"" optype=""continuous"" dataType=""double""/>
		<DataField name=""out"" optype=""continuous"" dataType=""double""/>
	</DataDictionary>
	<TreeModel modelName=""SimpleIsNotMissingTest"" functionName=""classification"">
		<MiningSchema>
			<MiningField name=""in""/>
			<MiningField name=""out"" usageType=""predicted""/>
		</MiningSchema>
		<Node score=""0"">
			<True/>
			<Node score=""1"">
				<SimplePredicate field=""in"" operator=""isNotMissing"" />
			</Node>
		</Node>
	</TreeModel>
</PMML>
";
			XmlDocument xml = new XmlDocument();
			xml.LoadXml(pmmlStr);
			Pmml pmml = Pmml.loadModels(xml);
			
			ScoreResult res = pmml.getByName("SimpleIsNotMissingTest").Score(parseParams("  in=\"foo\"  "));
			
			Assert.AreEqual("1", res.Value);
		}
	}
}

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
using NUnit.Framework;

namespace pmml4net.tests
{
	/// <summary>
	/// Description of TreeModelTest.
	/// </summary>
	public class TreeModelTest
	{
		[TestCase("test-golfing1.xml")]
		public void TreeModelsTest(string pFilePath)
		{
			Pmml pmml = Pmml.loadModels(pFilePath);
			
			Assert.NotNull(pmml);
			
			Assert.AreEqual(pmml.TreeModels.Count, 1);
		}
		
		[TestCase("test-golfing1.xml", 
		          "temperature=75, humidity=55, windy=\"false\", outlook=\"overcast\"", 
		          "whatIdo", "may play")]
		public void ScoreTest(string pFilePath, string paramList, string name, string res)
		{
			Pmml pmml = Pmml.loadModels(pFilePath);
			
			Assert.NotNull(pmml);
			
			TreeModel tree = pmml.getByName("golfing");
			Assert.NotNull(tree);
			
			Dictionary<string, object> lDict = parseParams(paramList);
			
			ScoreResult result = tree.Score(lDict);
			Assert.NotNull(result);
			
			Assert.AreEqual(name, result.Name);
			Assert.AreEqual(res, result.Value);
		}
		
		[TestCase("test-golfing1.xml")]
		public void loadTest(string pFilePath)
		{
			Pmml pmml = Pmml.loadModels(pFilePath);
			
			Assert.NotNull(pmml);
			
			TreeModel tree = pmml.getByName("golfing");
			Assert.NotNull(tree);
			
			Assert.AreEqual(2, tree.Node.Nodes.Count);
			
			Assert.AreEqual(2, tree.Node.Nodes[0].Nodes.Count);
			Assert.AreEqual(2, tree.Node.Nodes[1].Nodes.Count);
		}
		
		private Dictionary<string, object> parseParams(string parameters)
		{
			Dictionary<string, object> lDict = new Dictionary<string, object>();
			
			foreach (string item in parameters.Split(','))
			{
				lDict.Add(item.Split('=')[0].Trim(), item.Split('=')[1].Trim());
			}
			
			return lDict;
		}
	}
}

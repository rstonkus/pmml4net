/*
 * Created by SharpDevelop.
 * User: Damien
 * Date: 24/04/2013
 * Time: 11:49
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

using NUnit.Framework;

using pmml4net;

namespace pmml4net.tests
{
	/// <summary>
	/// Description of PmmlTest.
	/// </summary>
	[TestFixture()]
	public class PmmlTest
	{
		[TestCase("test-golfing1.xml")]
		public void LoadModelsTest(string pFilePath)
		{
			Assert.NotNull(Pmml.loadModels(pFilePath));
		}
		
		[TestCase("test-golfing1.xml", 1)]
		public void TreeModelsTest(string pFilePath, int nb_treemodel)
		{
			Pmml pmml = Pmml.loadModels(pFilePath);
			
			Assert.NotNull(pmml);
			
			Assert.NotNull(pmml.getByName("golfing"));
			
			Assert.AreEqual(nb_treemodel, pmml.TreeModels.Count);
		}
	}
}

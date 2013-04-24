/*
 * Created by SharpDevelop.
 * User: Damien
 * Date: 24/04/2013
 * Time: 15:14
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
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
	}
}

/*
 * Created by SharpDevelop.
 * User: Damien
 * Date: 24/04/2013
 * Time: 11:49
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;
using System.Xml;
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
			// Try from string
			Assert.NotNull(Pmml.loadModels(pFilePath));
			
			// Same but with file info
			FileInfo info = new FileInfo(pFilePath);
			Assert.NotNull(Pmml.loadModels(info));
			
			// Test the Xml constructor
			XmlDocument xml = new XmlDocument();
			xml.Load(pFilePath);
			Assert.NotNull(Pmml.loadModels(xml));
		}
		
		[TestCase("test-golfing1.xml", "golfing", 1)]
		[TestCase("test-golfing2.xml", "golfing", 1)]
		public void TreeModelsTest(string filePath, string modelName, int nbTreemodels)
		{
			Pmml pmml = Pmml.loadModels(filePath);
			
			Assert.NotNull(pmml);
			
			Assert.NotNull(pmml.getByName(modelName));
			
			Assert.AreEqual(nbTreemodels, pmml.TreeModels.Count);
		}
	}
}

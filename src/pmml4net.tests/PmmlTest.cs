/*
 * Created by SharpDevelop.
 * User: Damien
 * Date: 24/04/2013
 * Time: 11:49
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
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
		/// <summary>
		/// test to load Mining Model
		/// </summary>
		/// <param name="pFilePath"></param>
		[TestCase("Segmentation.xml")]
		public void LoadMiningModelTest(string pFilePath)
		{
			Pmml pmml = Pmml.loadModels(pFilePath);
			Assert.NotNull(pmml);
			
			Assert.AreEqual(1, pmml.Models.Count);
			
			ModelElement model = pmml.Models[0];
			Assert.IsInstanceOf(typeof(MiningModel), pmml.Models[0]);
			
			MiningModel miningModel = (MiningModel)model;
			Assert.AreEqual(3, miningModel.Segmentation.Segments.Count);
		}
		
		/// <summary>
		/// Test with random data
		/// </summary>
		/// <param name="pFilePath"></param>
		[TestCase("Segmentation.xml")]
		public void RandomTest(string pFilePath)
		{
			Pmml pmml = Pmml.loadModels(pFilePath);
			Assert.NotNull(pmml);
			
			ScoreResult res = pmml.Models[0].Score(new Dictionary<string, object>());
			Assert.IsNotNull(res);
		}
		
		/// <summary>
		/// Load some generated PMML from other vendors.
		/// </summary>
		/// <param name="pFilePath"></param>
		[TestCase("AuditTree.xml")]
		[TestCase("BigML1.xml")] // 51794c13e4b024977881b628
		[TestCase("IrisTree.xml")]
		[TestCase("Segmentation.xml")]
		[TestCase("SIPINA1.xml")]
		[TestCase("SPSS.xml")]
		[TestCase("SPSS-2.xml")]
		[TestCase("test-golfing1.xml")]
		[TestCase("test-golfing2.xml")]
		[TestCase("test-simpleset.xml")]
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
			
			Assert.AreEqual(nbTreemodels, pmml.Models.Count);
		}
		
		/// <summary>
		/// Load some generated PMML from other vendors and save it.
		/// </summary>
		/// <param name="pFilePath"></param>
		[TestCase("AuditTree.xml")]
		[TestCase("BigML1.xml")] // 51794c13e4b024977881b628
		[TestCase("IrisTree.xml")]
		[TestCase("Segmentation.xml")]
		[TestCase("SIPINA1.xml")]
		[TestCase("SPSS.xml")]
		[TestCase("SPSS-2.xml")]
		[TestCase("test-golfing1.xml")]
		[TestCase("test-golfing2.xml")]
		[TestCase("test-simpleset.xml")]
		public void saveTest(string pFilePath)
		{
			// Load
			Pmml pmml = Pmml.loadModels(pFilePath);
			Assert.NotNull(pmml);
			string temp_file = Path.GetTempFileName();
			// Save
			pmml.save(temp_file);
			
			Pmml pmml2 = Pmml.loadModels(temp_file);
			
			// Test data dictionnary
			Assert.AreEqual(pmml.DataDictionary.DataFields.Count, pmml2.DataDictionary.DataFields.Count);
			
			// Test models
			Assert.AreEqual(pmml.Models.Count, pmml2.Models.Count);
			for (int j = 0; j < pmml.Models.Count; j++)
			{
				ModelElement model = pmml.Models[j];
				ModelElement model2 = pmml2.Models[j];
				
				Assert.IsInstanceOf(model.GetType(), model2);
				
				if (model is TreeModel)
				{
					
				}
			}
		}
	}
}

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
	/// Tests for class <see cref="Segmentation">Segmentation</see>.
	/// </summary>
	[TestFixture()]
	public class SegmentationTest
	{
		/// <summary>
		/// test to load Mining Model
		/// </summary>
		/// <param name="pFilePath"></param>
		[TestCase("average", MultipleModelMethod.Average)]
		[TestCase("  average", MultipleModelMethod.Average)]
		[TestCase("average  ", MultipleModelMethod.Average)]
		[TestCase("AVERAGE", MultipleModelMethod.Average)]
		[TestCase("AvErAgE", MultipleModelMethod.Average)]
		[TestCase("MajorityVote", MultipleModelMethod.MajorityVote)]
		[TestCase("Max", MultipleModelMethod.Max)]
		[TestCase("Median", MultipleModelMethod.Median)]
		[TestCase("ModelChain", MultipleModelMethod.ModelChain)]
		[TestCase("SelectAll", MultipleModelMethod.SelectAll)]
		[TestCase("SelectFirst", MultipleModelMethod.SelectFirst)]
		[TestCase("Sum", MultipleModelMethod.Sum)]
		[TestCase("WeightedAverage", MultipleModelMethod.WeightedAverage)]
		[TestCase("WeightedMajorityVote", MultipleModelMethod.WeightedMajorityVote)]
		public void MultipleModelMethodfromStringTest(string val, MultipleModelMethod res)
		{
			Assert.AreEqual(res, Segmentation.MultipleModelMethodfromString(val));
		}
	}
}
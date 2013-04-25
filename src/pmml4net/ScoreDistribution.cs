/*
 * Created by SharpDevelop.
 * User: Damien
 * Date: 25/04/2013
 * Time: 14:51
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Xml;

namespace pmml4net
{
	/// <summary>
	/// An element of Node to represent segments of the score that a Node predicts in a classification model.
	/// If the Node holds an enumeration, each entry of the enumeration is stored in one ScoreDistribution element.
	/// </summary>
	public class ScoreDistribution
	{
		private string fvalue;
		private string frecordCount;
		private string fconfidence;
		private string fprobability;
		
		/// <summary>
		/// Load Node from XmlNode
		/// </summary>
		/// <param name="node"></param>
		/// <returns></returns>
		public static ScoreDistribution loadFromXmlNode(XmlNode node)
		{
			ScoreDistribution root = new ScoreDistribution();
			
			root.fvalue = node.Attributes["value"].Value;
			
			root.frecordCount = node.Attributes["recordCount"].Value;
			
			if (node.Attributes["probability"] != null)
				root.fprobability = node.Attributes["probability"].Value;
			
			return root;
		}
	}
}

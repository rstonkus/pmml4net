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
		/// This attribute of ScoreDistribution is the label in a classification model.
		/// </summary>
		public string Value { get { return fvalue; } set { fvalue = value; } }
		
		/// <summary>
		/// This attribute of ScoreDistribution is the size (in number of records) associated with the value attribute.
		/// </summary>
		public string RecordCount { get { return frecordCount; } set { frecordCount = value; } }
		
		/// <summary>
		/// This optional attribute of ScoreDistribution assigns a confidence to a given prediction class for this tree node.
		/// Confidences are similar to probabilities but more relaxed. The confidences may not necessarily sum to 1 across the 
		/// different classes, like probabilities would. Confidences should normally lie in the range 0.0 to 1.0 though.
		/// In tree models, using the laplace correction results in a confidence rather than a probability 
		/// (the confidences sum to less than 1.0).
		/// </summary>
		public string Confidence { get { return fconfidence; } set { fconfidence = value; } }
		
		/// <summary>
		/// This optional attribute assigns a predicted probability for the given value within the node. If not specified, 
		/// the predicted probability is calculated from the record counts. If defined for any class label, it must be defined 
		/// for all and the predicted probabilities must sum to 1
		/// </summary>
		public string Probability { get { return fprobability; } set { fprobability = value; } }
		
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
			
			if (node.Attributes["confidence"] != null)
				root.fconfidence = node.Attributes["confidence"].Value;
			
			return root;
		}
	}
}

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

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
using System.Collections.Generic;
using System.Xml;
using System.Globalization;

namespace pmml4net
{
	/// <summary>
	/// Description of TreeModel.
	/// </summary>
	public class TreeModel
	{
		private String modelName;
		private MissingValueStrategy missingValueStrategy;
		private NoTrueChildStrategy noTrueChildStrategy;
		
		private MiningSchema miningSchema;
		private Node node;
		
		/// <summary>
		/// Identifies the model with a unique name in the context of the PMML file.
		/// </summary>
		public String ModelName { get { return modelName; } set { modelName = value; } }
		
		/// <summary>
		/// Defines a strategy for dealing with missing values.
		/// </summary>
		public MissingValueStrategy MissingValueStrategy { get { return missingValueStrategy; } set { missingValueStrategy = value; } }
		
		/// <summary>
		/// Defines what to do in situations where scoring cannot reach a leaf node.
		/// </summary>
		public NoTrueChildStrategy NoTrueChildStrategy { get { return noTrueChildStrategy; } set { noTrueChildStrategy = value; } }
		
		/// <summary>
		/// Mining schema for this model.
		/// </summary>
		public MiningSchema MiningSchema { get { return miningSchema; } set { miningSchema = value; } }
		
		/// <summary>
		/// Root node of this model.
		/// </summary>
		public Node Node { get { return node; } set { node = value; } }
		
		/// <summary>
		/// Scoring with Tree Model
		/// </summary>
		/// <param name="dict">Values</param>
		/// <returns></returns>
		public ScoreResult Score(Dictionary<string, object> dict)
		{
			ScoreResult resStart = new ScoreResult("", null);
			Node root = this.node;
			resStart.Nodes.Add(root);
			resStart.Value = root.Score;
			
			return Node.Evaluate(root, missingValueStrategy, noTrueChildStrategy, dict, resStart);
		}
		
		/// <summary>
		/// Load tree model from xmlnode
		/// </summary>
		/// <param name="node"></param>
		/// <returns></returns>
		public static TreeModel loadFromXmlNode(XmlNode node)
		{
			TreeModel tree = new TreeModel();
			
			tree.ModelName = node.Attributes["modelName"].Value;
			
			if (node.Attributes["missingValueStrategy"] != null)
				tree.MissingValueStrategy = MissingValueStrategyfromString(node.Attributes["missingValueStrategy"].Value);
			
			// By default noTrueChildStrategy = returnNullPrediction
			tree.noTrueChildStrategy = NoTrueChildStrategy.ReturnNullPrediction;
			if (node.Attributes["noTrueChildStrategy"] != null)
				tree.noTrueChildStrategy = NoTrueChildStrategyfromString(node.Attributes["noTrueChildStrategy"].Value);
			
			
			foreach(XmlNode item in node.ChildNodes)
			{
				if ("Node".Equals(item.Name))
				{
					tree.Node = Node.loadFromXmlNode(item);
				}
			}
			
			return tree;
		}
		
		private static MissingValueStrategy MissingValueStrategyfromString(string val)
		{
			switch (val)
			{
			case "lastPrediction": 
				return MissingValueStrategy.LastPrediction;
			
			case "weightedConfidence": 
				return MissingValueStrategy.WeightedConfidence;
			
			case "none":
				return MissingValueStrategy.None;
				
			default:
				throw new NotImplementedException();
			}
		}
		
		private static NoTrueChildStrategy NoTrueChildStrategyfromString(string val)
		{
			switch (val)
			{
			case "returnNullPrediction": 
				return NoTrueChildStrategy.ReturnNullPrediction;
			
			case "returnLastPrediction":
				return NoTrueChildStrategy.ReturnLastPrediction;
				
			default:
				throw new NotImplementedException();
			}
		}
	}
}

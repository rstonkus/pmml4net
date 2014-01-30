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
	/// Description of RuleSetModel.
	/// </summary>
	public class RuleSetModel : ModelElement
	{
		private MissingValueStrategy missingValueStrategy;
		private NoTrueChildStrategy noTrueChildStrategy;
		
		private RuleSet ruleSet;
		
		/// <summary>
		/// Defines a strategy for dealing with missing values.
		/// </summary>
		public MissingValueStrategy MissingValueStrategy { get { return missingValueStrategy; } set { missingValueStrategy = value; } }
		
		/// <summary>
		/// Defines what to do in situations where scoring cannot reach a leaf node.
		/// </summary>
		public NoTrueChildStrategy NoTrueChildStrategy { get { return noTrueChildStrategy; } set { noTrueChildStrategy = value; } }
		
		/// <summary>
		/// RuleSet node of this model.
		/// </summary>
		public RuleSet RuleSet { get { return ruleSet; } set { ruleSet = value; } }
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="functionName"></param>
		public RuleSetModel(MiningFunction functionName)
		{
			this.FunctionName = functionName;
		}
		
		/// <summary>
		/// 
		/// </summary>
		public RuleSetModel()
		{
			this.FunctionName = MiningFunction.Classification;
		}
		
		/// <summary>
		/// Scoring with Tree Model
		/// </summary>
		/// <param name="dict">Values</param>
		/// <returns></returns>
		public override ScoreResult Score(Dictionary<string, object> dict)
		{
			ScoreResult resStart = new ScoreResult("", null);
			RuleSet ruleset = this.ruleSet;
			/*resStart.Nodes.Add(root);
			resStart.Value = root.Score;
			
			return Node.Evaluate(root, missingValueStrategy, noTrueChildStrategy, dict, resStart);*/
			//throw new NotImplementedException();
			
			// Check if there are only one SelectionMethod
			return RuleSet.Evaluate(ruleset, "", dict, resStart);
		}
		
		/// <summary>
		/// Load tree model from xmlnode
		/// </summary>
		/// <param name="node"></param>
		/// <returns></returns>
		public static RuleSetModel loadFromXmlNode(XmlNode node)
		{
			string functionName = null;
			if (node.Attributes["functionName"] != null)
				functionName = node.Attributes["functionName"].Value;
			
			RuleSetModel model = new RuleSetModel(MiningFunctionFromString(functionName));
			
			model.ModelName = node.Attributes["modelName"].Value;
			
			/*if (node.Attributes["missingValueStrategy"] != null)
				tree.MissingValueStrategy = MissingValueStrategyfromString(node.Attributes["missingValueStrategy"].Value);
			
			// By default noTrueChildStrategy = returnNullPrediction
			tree.noTrueChildStrategy = NoTrueChildStrategy.ReturnNullPrediction;
			if (node.Attributes["noTrueChildStrategy"] != null)
				tree.noTrueChildStrategy = NoTrueChildStrategyfromString(node.Attributes["noTrueChildStrategy"].Value);*/
			
			
			foreach(XmlNode item in node.ChildNodes)
			{
				if ("extension".Equals(item.Name.ToLowerInvariant()))
				{
					// TODO : implement extension
					//root.Nodes.Add(Node.loadFromXmlNode(item));
				}
				else if ("MiningSchema".Equals(item.Name))
				{
					model.MiningSchema = MiningSchema.loadFromXmlNode(item);
				}
				else if ("RuleSet".Equals(item.Name))
				{
					model.RuleSet = RuleSet.loadFromXmlNode(item);
				}
				else
					throw new NotImplementedException();
			}
			
			return model;
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
		
		private static MiningFunction MiningFunctionFromString(string val)
		{
			switch (val.ToLowerInvariant().Trim())
			{
			case "associationrules": 
				return MiningFunction.AssociationRules;
			
			case "classification":
				return MiningFunction.Classification;
				
			default:
				throw new NotImplementedException();
			}
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="writer"></param>
		public override void save(XmlWriter writer)
		{
			writer.WriteStartElement("RuleSetModel");
			
			if (!string.IsNullOrEmpty(this.ModelName))
				writer.WriteAttributeString("modelName", this.ModelName);
			
			writer.WriteAttributeString("functionName", MiningFunctionToString(this.FunctionName));
			
			if (!string.IsNullOrEmpty(this.AlgorithmName))
				writer.WriteAttributeString("algorithmName", this.AlgorithmName);
			
			// Save Mining schema
			this.MiningSchema.save(writer);
			
			// FIXME : Add all elements in xml
			
			// Save Node
			this.RuleSet.save(writer);
			
			writer.WriteEndElement();
		}
	}
}

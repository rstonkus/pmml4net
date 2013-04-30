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
	/// Description of Node.
	/// </summary>
	public class Node
	{
		private string id;
		private string score;
		private decimal recordCount;
		private List<Node> nodes;
		private AbstractPredicate predicate;
		private List<ScoreDistribution> scoreDistributions;
		
		private Node()
		{
			nodes = new List<Node>();
		}
		
		/// <summary>
		/// The value of id serves as a unique identifier for any given Node within the tree model.
		/// </summary>
		public string Id { get { return id; } set { id = value; } }
		
		/// <summary>
		/// score of this node
		/// </summary>
		public string Score { get { return score; } set { score = value; } }
		
		/// <summary>
		/// The value of recordCount in a Node serves as a base size for recordCount values in ScoreDistribution elements.
		/// These numbers do not necessarily determine the number of records which have been used to build/train the model.
		/// Nevertheless, they allow to determine the relative size of given values in a ScoreDistribution as well as the 
		/// relative size of a Node when compared to the parent Node.
		/// </summary>
		public decimal RecordCount { get { return recordCount; } set { recordCount = value; } }
		
		/// <summary>
		/// siblings of this node
		/// </summary>
		public List<Node> Nodes { get { return nodes; } set { nodes = value; } }
		
		/// <summary>
		/// Predicates of this node
		/// </summary>
		public AbstractPredicate Predicate { get { return predicate; } set { predicate = value; } }
		
		/// <summary>
		/// siblings of this node
		/// </summary>
		public List<ScoreDistribution> ScoreDistributions { get { return scoreDistributions; } set { scoreDistributions = value; } }
		
		/// <summary>
		/// Load Node from XmlNode
		/// </summary>
		/// <param name="node"></param>
		/// <returns></returns>
		public static Node loadFromXmlNode(XmlNode node)
		{
			Node root = new Node();
			
			if (node.Attributes["id"] != null)
				root.id = node.Attributes["id"].Value;
			
			if (node.Attributes["score"] != null)
				root.score = node.Attributes["score"].Value;
			
			if (node.Attributes["recordCount"] != null)
				root.recordCount = Convert.ToDecimal(node.Attributes["recordCount"].Value, CultureInfo.InvariantCulture);
			
			root.scoreDistributions = new List<ScoreDistribution>();
			foreach(XmlNode item in node.ChildNodes)
			{
				if ("extension".Equals(item.Name.ToLowerInvariant()))
				{
					// TODO : implement extension
					//root.Nodes.Add(Node.loadFromXmlNode(item));
				}
				else if ("node".Equals(item.Name.ToLowerInvariant()))
				{
					root.Nodes.Add(Node.loadFromXmlNode(item));
				}
				else if ("simplepredicate".Equals(item.Name.ToLowerInvariant()))
				{
					root.Predicate = SimplePredicate.loadFromXmlNode(item);
				}
				else if ("true".Equals(item.Name.ToLowerInvariant()))
				{
					root.Predicate = new TruePredicate();
				}
				else if ("false".Equals(item.Name.ToLowerInvariant()))
				{
					root.Predicate = new FalsePredicate();
				}
				else if ("compoundpredicate".Equals(item.Name.ToLowerInvariant()))
				{
					root.Predicate = CompoundPredicate.loadFromXmlNode(item);
				}
				else if ("simplesetpredicate".Equals(item.Name.ToLowerInvariant()))
				{
					root.Predicate = SimpleSetPredicate.loadFromXmlNode(item);
				}
				else if ("scoredistribution".Equals(item.Name.ToLowerInvariant()))
				{
					root.ScoreDistributions.Add(ScoreDistribution.loadFromXmlNode(item));
				}
				else
					throw new NotImplementedException();
			}
			
			return root;
		}
		
		/// <summary>
		/// Scoring with Tree Model
		/// </summary>
		/// <param name="root">Parent node</param>
		/// <param name="missingvalueStr">Missing value strategy to evaluate this node.</param>
		/// <param name="noTrueChildStr">Strategy to evaluate this node if no child are true</param>
		/// <param name="dict">Values</param>
		/// <param name="res" >Result to return</param>
		/// <returns></returns>
		public static ScoreResult Evaluate(Node root, MissingValueStrategy missingvalueStr, NoTrueChildStrategy noTrueChildStr, 
		                            Dictionary<string, object> dict, ScoreResult res)
		{
			// Test childs
			foreach(Node child in root.Nodes)
			{
				PredicateResult childPredicate = child.Predicate.Evaluate(dict);
				if (childPredicate == PredicateResult.True)
				{
					res.Nodes.Add(child);
					res.Value = child.Score;
					
					return Evaluate(child, missingvalueStr, noTrueChildStr, dict, res);
				}
				else if (childPredicate == PredicateResult.Unknown)
				{
					// Unknow value lead to act with missingvalueStr
					switch(missingvalueStr)
					{
						case MissingValueStrategy.LastPrediction:
							return res;
						case MissingValueStrategy.WeightedConfidence:
							Dictionary<string, decimal> conf = CalculateConfidence(root, dict);
							string max_conf = null;
							foreach(string key in conf.Keys)
							{
								if (max_conf == null)
									max_conf = key;
								
								if (conf[key] > conf[max_conf])
									max_conf = key;
							}
							res.Value = max_conf;
							res.Confidence = conf[max_conf];
							return res;
						default:
							throw new NotImplementedException();
					}
				}
			}
			
			// All child nodes are false
			if (root.Nodes.Count > 0)
				if (noTrueChildStr == NoTrueChildStrategy.ReturnNullPrediction)
				{
					res.Value = null;
				}
			return res;
		}
		
		private static Dictionary<String, decimal> CalculateConfidence(Node node, Dictionary<string, object> dict)
		{
			Dictionary<String, decimal> ret = new Dictionary<string, decimal>();
			
			// Test childs
			foreach(Node child in node.Nodes)
			{
				PredicateResult childPredicate = child.Predicate.Evaluate(dict);
				if (childPredicate != PredicateResult.False)
				{
					foreach(ScoreDistribution sd in child.ScoreDistributions)
					{
						if (!ret.ContainsKey(sd.Value))
							ret.Add(sd.Value, 0);
						
						decimal new_val = (Convert.ToDecimal(sd.RecordCount)/child.RecordCount) * (child.RecordCount / node.RecordCount);
						ret[sd.Value] = ret[sd.Value] + new_val;
					}
				}
			}
			
			return ret;
		}
	}
}

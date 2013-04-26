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

namespace pmml4net
{
	/// <summary>
	/// Description of Node.
	/// </summary>
	public class Node
	{
		private string id;
		private string score;
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
		/// <param name="dict">Values</param>
		/// <param name="res" >Result to return</param>
		/// <returns></returns>
		public bool Evaluate(Dictionary<string, object> dict, ScoreResult res)
		{
			// Test predicates
			bool res_predicate = false;
			if (predicate.Evaluate(dict) == PredicateResult.True)
			{
				res_predicate = true;
				
				res.Nodes.Add(this);
				
				// Test childs
				foreach(Node child in nodes)
				{
					if (child.Evaluate(dict, res))
						break;
				}
			}
				
			return res_predicate;
		}
	}
}

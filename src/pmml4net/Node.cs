/*
 * Created by SharpDevelop.
 * User: Damien
 * Date: 24/04/2013
 * Time: 11:37
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
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
		private List<Node> nodes;
		private AbstractPredicate predicate;
		
		private Node()
		{
			nodes = new List<Node>();
		}
		
		/// <summary>
		/// siblings of this node
		/// </summary>
		public List<Node> Nodes { get { return nodes; } set { nodes = value; } }
		
		/// <summary>
		/// Predicates of this node
		/// </summary>
		public AbstractPredicate Predicate { get { return predicate; } set { predicate = value; } }
		
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
			
			foreach(XmlNode item in node.ChildNodes)
			{
				if ("node".Equals(item.Name.ToLowerInvariant()))
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
			}
			
			return root;
		}
	}
}

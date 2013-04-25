/*
 * Created by SharpDevelop.
 * User: Damien
 * Date: 25/04/2013
 * Time: 09:10
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Xml;

namespace pmml4net
{
	/// <summary>
	/// Description of SimplePredicate.
	/// </summary>
	public class SimplePredicate : AbstractPredicate
	{
		private string field;
		private string foperator;
		private string fvalue;
		
		/// <summary>
		/// This attribute of <code>SimplePredicate</code> element is the information to evaluate / compare against.
		/// </summary>
		public string Value { get { return fvalue; } set { fvalue = value; } }
		
		/// <summary>
		/// Load Node from XmlNode
		/// </summary>
		/// <param name="node"></param>
		/// <returns></returns>
		public static SimplePredicate loadFromXmlNode(XmlNode node)
		{
			SimplePredicate root = new SimplePredicate();
			
			// TODO : Add extention reading
			
			root.field = node.Attributes["field"].Value;
			
			root.foperator = node.Attributes["operator"].Value;
			
			root.fvalue = node.Attributes["value"].Value;
			
			
			/*foreach(XmlNode item in node.ChildNodes)
			{
				if ("node".Equals(item.Name.ToLowerInvariant()))
				{
					root.Nodes.Add(Node.loadFromXmlNode(item));
				}
				else if ("simplepredicate".Equals(item.Name.ToLowerInvariant()))
				{
					root.Predicate = SimplePredicate.loadFromXmlNode(item);
				}
			}*/
			
			return root;
		}
		
		/// <summary>
		/// Test predicate
		/// </summary>
		/// <param name="dict"></param>
		/// <param name="res"></param>
		/// <returns></returns>
		public override bool Evaluate(Dictionary<string, object> dict, ScoreResult res)
		{
			return false;
		}
	}
}

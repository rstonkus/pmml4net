/*
 * Created by SharpDevelop.
 * User: Damien
 * Date: 25/04/2013
 * Time: 12:33
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Xml;

namespace pmml4net
{
	/// <summary>
	/// Description of CompoundPredicate.
	/// </summary>
	public class CompoundPredicate : AbstractPredicate
	{
		private string fbooleanOperator;
		private List<AbstractPredicate> fpredicates;
		
		/// <summary>
		/// This attribute of <code>SimplePredicate</code> element is the information to evaluate / compare against.
		/// </summary>
		public List<AbstractPredicate> Predicates { get { return fpredicates; } set { fpredicates = value; } }
		
		/// <summary>
		/// Load Node from XmlNode
		/// </summary>
		/// <param name="node"></param>
		/// <returns></returns>
		public static CompoundPredicate loadFromXmlNode(XmlNode node)
		{
			CompoundPredicate root = new CompoundPredicate();
			
			// TODO : Add extention reading
			
			root.fbooleanOperator = node.Attributes["booleanOperator"].Value;
			
			root.fpredicates = new List<AbstractPredicate>();
			foreach(XmlNode item in node.ChildNodes)
			{
				if ("extension".Equals(item.Name.ToLowerInvariant()))
				{
					//root.Nodes.Add(Node.loadFromXmlNode(item));
				}
				else if ("simplepredicate".Equals(item.Name.ToLowerInvariant()))
				{
					root.fpredicates.Add(SimplePredicate.loadFromXmlNode(item));
				}
				else if ("true".Equals(item.Name.ToLowerInvariant()))
				{
					root.fpredicates.Add(new TruePredicate());
				}
				else if ("false".Equals(item.Name.ToLowerInvariant()))
				{
					root.fpredicates.Add(new FalsePredicate());
				}
				else if ("compoundpredicate".Equals(item.Name.ToLowerInvariant()))
				{
					root.fpredicates.Add(CompoundPredicate.loadFromXmlNode(item));
				}
				else if ("simplesetpredicate".Equals(item.Name.ToLowerInvariant()))
				{
					root.fpredicates.Add(SimpleSetPredicate.loadFromXmlNode(item));
				}
				else
					throw new NotImplementedException();
			}
			
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
			//double var_test_double = Convert.ToDouble(dict[field]);
			//double ref_double = Convert.ToDouble(fvalue);
			
			if ("or".Equals(fbooleanOperator.Trim().ToLowerInvariant()))
			{
				foreach(AbstractPredicate pred in fpredicates)
					if (pred.Evaluate(dict, res))
						return true;
				return false;
			}
			else
				throw new PmmlException();
		}
	}
}

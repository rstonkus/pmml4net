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
	/// Description of SimpleSetPredicate.
	/// </summary>
	public class SimpleSetPredicate : AbstractPredicate
	{
		private string field;
		private string foperator;
		private List<string> farray;
		
		/// <summary>
		/// Load Node from XmlNode
		/// </summary>
		/// <param name="node"></param>
		/// <returns></returns>
		public static SimpleSetPredicate loadFromXmlNode(XmlNode node)
		{
			SimpleSetPredicate root = new SimpleSetPredicate();
			
			// TODO : Add extention reading
			
			root.field = node.Attributes["field"].Value;
			
			root.foperator = node.Attributes["booleanOperator"].Value;
			
			root.farray = new List<string>();
			foreach(XmlNode item in node.ChildNodes)
			{
				if ("array".Equals(item.Name.ToLowerInvariant()))
				{
					root.farray.Add(item.InnerText.Trim());
				}
			}
			
			return root;
		}
		
		/// <summary>
		/// Test predicate
		/// </summary>
		/// <param name="dict"></param>
		/// <param name="res"></param>
		/// <returns></returns>
		public override PredicateResult Evaluate(Dictionary<string, object> dict)
		{
			object var_test = dict[field];
			bool is_in = farray.Contains(var_test.ToString());
			
			if ("isIn".Equals(foperator))
				return ToPredicateResult(is_in);
			else if ("isIn".Equals(foperator))
				return ToPredicateResult(!is_in);
			else
				throw new PmmlException();
		}
	}
}

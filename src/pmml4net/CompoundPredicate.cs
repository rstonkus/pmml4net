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
		/// <returns></returns>
		public override PredicateResult Evaluate(Dictionary<string, object> dict)
		{
			//double var_test_double = Convert.ToDouble(dict[field]);
			//double ref_double = Convert.ToDouble(fvalue);
			
			if ("or".Equals(fbooleanOperator.Trim().ToLowerInvariant()))
			{
				PredicateResult ret = fpredicates[0].Evaluate(dict);
				for(int i=1; i < fpredicates.Count; i++)
				{
					ret = Or(ret, fpredicates[i].Evaluate(dict));
					
					if (ret == PredicateResult.True)
						return PredicateResult.True;
				}
				return ret;
			}
			else if ("and".Equals(fbooleanOperator.Trim().ToLowerInvariant()))
			{
				foreach(AbstractPredicate pred in fpredicates)
					if (pred.Evaluate(dict) == PredicateResult.False)
						return PredicateResult.False;
				return PredicateResult.True;
			}
			else if ("surrogate".Equals(fbooleanOperator.Trim().ToLowerInvariant()))
			{
				foreach(AbstractPredicate pred in fpredicates)
				{
					PredicateResult ret = pred.Evaluate(dict);
					if (ret != PredicateResult.Unknown)
						return ret;
				}
				return PredicateResult.Unknown;
			}
			else
				throw new PmmlException();
		}
	}
}

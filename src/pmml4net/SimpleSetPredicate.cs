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
	/// Description of SimpleSetPredicate.
	/// </summary>
	public class SimpleSetPredicate : Predicate
	{
		private string field;
		private string foperator;
		private List<string> farray;
		
		/// <summary>
		/// 
		/// </summary>
		public string Field { get { return field; }}
		
		/// <summary>
		/// 
		/// </summary>
		public string BooleanOperator { get { return foperator; } set { foperator = value; } }
		
		/// <summary>
		/// Load Node from XmlNode
		/// </summary>
		/// <param name="node"></param>
		/// <returns></returns>
		public static SimpleSetPredicate loadFromXmlNode(XmlNode node)
		{
			SimpleSetPredicate root = new SimpleSetPredicate();
			
			root.field = node.Attributes["field"].Value;
			root.foperator = node.Attributes["booleanOperator"].Value;
			
			root.farray = new List<string>();
			foreach(XmlNode item in node.ChildNodes)
			{
				if ("extension".Equals(item.Name.ToLowerInvariant()))
				{
					//root.farray.Add(item.InnerText.Trim());
				}
				else if ("array".Equals(item.Name.ToLowerInvariant()))
				{
					root.farray.Add(item.InnerText.Trim());
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
			object var_test = dict[field];
			bool is_in = farray.Contains(var_test.ToString());
			
			if ("isIn".Equals(foperator))
				return ToPredicateResult(is_in);
			else if ("isIn".Equals(foperator))
				return ToPredicateResult(!is_in);
			else
				throw new PmmlException();
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="writer"></param>
		public override void save(XmlWriter writer)
		{
			writer.WriteStartElement("SimpleSetPredicate");
			
			writer.WriteAttributeString("field", this.Field);
			writer.WriteAttributeString("booleanOperator", this.BooleanOperator);
			
			// Save array
			foreach (string arr in this.farray)
			{
				writer.WriteStartElement("Array");
				writer.WriteValue(arr);
				writer.WriteEndElement();
			}
			
			writer.WriteEndElement();
		}
	}
}

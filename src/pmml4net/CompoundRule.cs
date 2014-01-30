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
	/// Each CompoundRule consists of a predicate and one or more rules.
	/// CompoundRules offer a shorthand for a more compact representation 
	/// of rulesets and suggest a more efficient execution mechanism.
	/// </summary>
	public class CompoundRule : Rule
	{
		private IList<Rule> rules = new List<Rule>();
		
		/// <summary>
		/// contains 0 or more rules which comprise the ruleset.
		/// </summary>
		public IList<Rule> Rules { get { return rules; } set { rules = value; } }
		
		/// <summary>
		/// 
		/// </summary>
		public CompoundRule()
		{
		}
		
		/// <summary>
		/// Test predicate
		/// </summary>
		/// <param name="dict"></param>
		/// <returns></returns>
		public override PredicateResult Evaluate(Dictionary<string, object> dict)
		{
			throw new PmmlException();
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="writer"></param>
		public override void save(XmlWriter writer)
		{
			writer.WriteStartElement("CompoundRule");
			
			/*if (!string.IsNullOrEmpty(this.Id))
				writer.WriteAttributeString("id", this.Id);
			
			writer.WriteAttributeString("operator", this.Operator);
			if (!string.IsNullOrEmpty(this.Value))
				writer.WriteAttributeString("value", this.Value);*/
			
			writer.WriteEndElement();
		}
	}
}

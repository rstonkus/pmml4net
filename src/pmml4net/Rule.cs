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
	/// Description of Rule.
	/// </summary>
	public abstract class Rule
	{
		/// <summary>
		/// Test if predicate is true
		/// </summary>
		/// <param name="dict"></param>
		public abstract PredicateResult Evaluate(Dictionary<string, object> dict);
		
		/// <summary>
		/// Convert boolean in predicate result.
		/// </summary>
		/// <param name="val"></param>
		/// <returns></returns>
		protected PredicateResult ToPredicateResult(bool val)
		{
			if (val)
				return PredicateResult.True;
			
			return PredicateResult.False;
		}
		
		/// <summary>
		/// Compute OR operator between 2 predicates.
		/// </summary>
		/// <param name="pred1"></param>
		/// <param name="pred2"></param>
		/// <returns></returns>
		protected PredicateResult Or(PredicateResult pred1, PredicateResult pred2)
		{
			if (pred1 == PredicateResult.True)
				return PredicateResult.True;
		
			if (pred2 == PredicateResult.True)
				return PredicateResult.True;
			
			if (pred1 == PredicateResult.False && pred2 == PredicateResult.False)
				return PredicateResult.False;
			
			return PredicateResult.Unknown;
		}
		
		/// <summary>
		/// Add predicate to <see cref="XmlWriter">XmlWriter</see>.
		/// </summary>
		/// <param name="writer">writer</param>
		public abstract void save(XmlWriter writer);
	}
}

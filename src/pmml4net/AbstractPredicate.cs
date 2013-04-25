/*
 * Created by SharpDevelop.
 * User: Damien
 * Date: 25/04/2013
 * Time: 08:52
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace pmml4net
{
	/// <summary>
	/// Description of AbstractPredicate.
	/// </summary>
	public abstract class AbstractPredicate
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
	}
	
	
}

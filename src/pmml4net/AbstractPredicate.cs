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
		/// <param name="res"></param>
		public abstract bool Evaluate(Dictionary<string, object> dict, ScoreResult res);
	}
}

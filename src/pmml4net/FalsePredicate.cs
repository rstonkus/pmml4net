/*
 * Created by SharpDevelop.
 * User: Damien
 * Date: 25/04/2013
 * Time: 09:38
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace pmml4net
{
	/// <summary>
	/// Description of FalsePredicate.
	/// </summary>
	public class FalsePredicate : AbstractPredicate
	{
		public override bool Evaluate(Dictionary<string, object> dict, ScoreResult res)
		{
			return false;
		}
	}
}

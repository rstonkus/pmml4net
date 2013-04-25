/*
 * Created by SharpDevelop.
 * User: Damien
 * Date: 25/04/2013
 * Time: 15:39
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace pmml4net
{
	/// <summary>
	/// Description of PredicateResult.
	/// </summary>
	public enum PredicateResult
	{
		/// <summary>
		/// true = predicate is ok
		/// </summary>
		True = 0,
		/// <summary>
		/// false = predicate is not true
		/// </summary>
		False = 1,
		/// <summary>
		/// default unspecified value
		/// </summary>
		Unknown = 2
	}
}

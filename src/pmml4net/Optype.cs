/*
 * Created by SharpDevelop.
 * User: Damien
 * Date: 15/05/2013
 * Time: 17:10
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace pmml4net
{
	/// <summary>
	/// Description of Optype.
	/// </summary>
	public enum Optype
	{
		/// <summary>
		/// Categorical field values can only be tested for equality.
		/// </summary>
		categorical,
		
		/// <summary>
		/// ordinal field values can be tested for equality and have an order defined.
		/// </summary>
		ordinal,
		
		/// <summary>
		/// Values of continuous fields can be used with arithmetic operators.
		/// </summary>
		continuous
	}
}

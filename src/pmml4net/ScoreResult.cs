/*
 * Created by SharpDevelop.
 * User: Damien
 * Date: 24/04/2013
 * Time: 17:02
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace pmml4net
{
	/// <summary>
	/// Description of ScoreResult.
	/// </summary>
	public class ScoreResult
	{
		//private string f_name;
		private object f_value;
		private List<Node> nodes;
		
		/*/// <summary>
		/// name of var out
		/// </summary>
		public string Name { get { return f_name; } set { f_name = value; } }*/
		
		/// <summary>
		/// Value of score
		/// </summary>
		public object Value { get { return f_value; } set { f_value = value; } }
		
		/// <summary>
		/// Node which validate
		/// </summary>
		public List<Node> Nodes { get { return nodes; } set { nodes = value; } }
		
		/// <summary>
		/// Make result with data
		/// </summary>
		/// <param name="p_name"></param>
		/// <param name="p_value"></param>
		public ScoreResult(string p_name, object p_value)
		{
			//f_name = p_name;
			f_value = p_value;
			nodes = new List<Node>();
		}
	}
}

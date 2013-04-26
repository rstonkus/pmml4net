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

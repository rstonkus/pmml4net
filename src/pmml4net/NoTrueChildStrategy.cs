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

namespace pmml4net
{
	/// <summary>
	/// During the scoring of a case, if the scoring reaches an internal Node at which none of the subnodes' 
	/// predicates evaluate to TRUE, and no missing value handling strategy (if defined) is invoked for any 
	/// of these subnodes, this optional attribute of TreeModel determines what to do next
	/// </summary>
	public enum NoTrueChildStrategy
	{
		/// <summary>
		/// No prediction is returned (this is the default behaviour)
		/// </summary>
		ReturnNullPrediction,
		
		/// <summary>
		/// If the parent has a score attribute return the value of this attribute. Otherwise, no prediction is returned.
		/// </summary>
		ReturnLastPrediction
	}
}

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
	/// Description of FieldUsageType.
	/// </summary>
	public enum FieldUsageType
	{
		/// <summary>
		/// Field used as input (independent field).
		/// </summary>
		Active,
		
		/// <summary>
		/// Field whose value is predicted by the model.
		/// </summary>
		Predicted,
		
		/// <summary>
		/// Field holding additional descriptive information. Supplementary fields are not required to apply a model. 
		/// They are provided as additional information for explanatory purpose, though. When some field has gone through 
		/// preprocessing transformations before a model is built, then an additional supplementary field is typically used 
		/// to describe the statistics for the original field values.
		/// </summary>
		Supplementary
	}
}

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

namespace pmml4net
{
	/// <summary>
	/// Description of ModelElement.
	/// 
	/// modelName: the value in modelName identifies the model with a unique name in the context of the PMML file. 
	/// This attribute is not required. Consumers of PMML models are free to manage the names of the models at their discretion.
	/// 
	/// functionName and algorithmName describe the kind of mining model, e.g., whether it is intended to be used for clustering 
	/// or for classification. The algorithm name is free-type and can be any description for the specific algorithm that 
	/// produced the model. This attribute is for information only.
	/// </summary>
	public abstract class ModelElement
	{
		private string modelName;
		private MiningFunction functionName;
		private string algorithmName;
		
		/// <summary>
		/// Identifies the model with a unique name in the context of the PMML file.
		/// This attribute is not required. Consumers of PMML models are free to manage the names of the models at their discretion.
		/// </summary>
		public string ModelName { get { return modelName; } set { modelName = value; } }
		
		/// <summary>
		/// Identifies the model with a unique name in the context of the PMML file.
		/// This attribute is not required. Consumers of PMML models are free to manage the names of the models at their discretion.
		/// </summary>
		public MiningFunction FunctionName { get { return functionName; } set { functionName = value; } }
		
		/// <summary>
		/// Identifies the model with a unique name in the context of the PMML file.
		/// This attribute is not required. Consumers of PMML models are free to manage the names of the models at their discretion.
		/// </summary>
		public string AlgorithmName { get { return algorithmName; } set { algorithmName = value; } }
		
		/// <summary>
		/// Score the model.
		/// </summary>
		/// <param name="dict">Data input</param>
		/// <returns></returns>
		public abstract ScoreResult Score(Dictionary<string, object> dict);
		
		/// <summary>
		/// Add ModelElement to <see cref="XmlWriter">XmlWriter</see>.
		/// </summary>
		/// <param name="writer">writer</param>
		public abstract void save(XmlWriter writer);
	}
}

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
	/// The MiningSchema is the Gate Keeper for its model element. All data entering a model must pass through the MiningSchema. 
	/// Each model element contains one MiningSchema which lists fields as used in that model. While the MiningSchema contains 
	/// information that is specific to a certain model, the DataDictionary contains data definitions which do not vary per 
	/// model. The main purpose of the MiningSchema is to list the fields that have to be provided in order to apply the model.
	/// 
	/// MiningFields also define the usage of each field (active, supplementary, predicted, ...) as well as policies for treating 
	/// missing, invalid or outlier values.
	/// </summary>
	public class MiningSchema
	{
		private IList<MiningField> miningFields = new List<MiningField>();
		
		/// <summary>
		/// Mining fields.
		/// </summary>
		public IList<MiningField> MiningFields { get { return miningFields; } }
		
		/// <summary>
		/// Load mining schema from xmlnode
		/// </summary>
		/// <param name="node"></param>
		/// <returns></returns>
		public static MiningSchema loadFromXmlNode(XmlNode node)
		{
			MiningSchema schema = new MiningSchema();
			
			foreach(XmlNode item in node.ChildNodes)
			{
				if ("Extension".Equals(item.Name))
				{
					//tree.Node = Node.loadFromXmlNode(item);
				}
				else if ("MiningField".Equals(item.Name))
				{
					schema.MiningFields.Add(MiningField.loadFromXmlNode(item));
				}
			}
			
			return schema;
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="writer"></param>
		public void save(XmlWriter writer)
		{
			writer.WriteStartElement("MiningSchema");
			
			/*writer.WriteAttributeString("modelName", this.ModelName);
			
			writer.WriteAttributeString("functionName", MiningFunctionToString(this.FunctionName));
			
			writer.WriteAttributeString("algorithmName", this.AlgorithmName);
			
			// Save Mining fields
			this.MiningSchema.save(writer);*/
			
			// Save mining fields
			foreach(MiningField miningField in this.miningFields) {
				miningField.save(writer);
			}
			
			writer.WriteEndElement();
		}
	}
}

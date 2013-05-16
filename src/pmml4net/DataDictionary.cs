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
	/// Description of DataDictionary.
	/// </summary>
	public class DataDictionary
	{
		private IList<DataField> dataFields = new List<DataField>();
		
		/// <summary>
		/// The name of a DataField must be unique from other names in the DataDictionary and, with few exceptions, 
		/// unique from the names of other fields in the PMML document. For information on the naming and scope of DataFields, see Scope of Fields.
		/// </summary>
		public IList<DataField> DataFields { get { return this.dataFields; } set { dataFields = value; } }
		
		/// <summary>
		/// Load data dictionary from xmlnode
		/// </summary>
		/// <param name="node"></param>
		/// <returns></returns>
		public static DataDictionary loadFromXmlNode(XmlNode node)
		{
			DataDictionary dict = new DataDictionary();
			
			/*tree.ModelName = node.Attributes["modelName"].Value;
			
			if (node.Attributes["missingValueStrategy"] != null)
				tree.MissingValueStrategy = MissingValueStrategyfromString(node.Attributes["missingValueStrategy"].Value);
			
			// By default noTrueChildStrategy = returnNullPrediction
			tree.noTrueChildStrategy = NoTrueChildStrategy.ReturnNullPrediction;
			if (node.Attributes["noTrueChildStrategy"] != null)
				tree.noTrueChildStrategy = NoTrueChildStrategyfromString(node.Attributes["noTrueChildStrategy"].Value);
			*/
			
			foreach(XmlNode item in node.ChildNodes)
			{
				if ("Extention".Equals(item.Name))
				{
					//tree.Node = Node.loadFromXmlNode(item);
				}
				else if ("DataField".Equals(item.Name))
				{
					dict.DataFields.Add(DataField.loadFromXmlNode(item));
				}
				else
					throw new NotImplementedException();
			}
			
			return dict;
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="writer"></param>
		public void save(XmlWriter writer)
		{
			writer.WriteStartElement("DataDictionary");
			
			// The value numberOfFields is the number of fields which are defined in the content of DataDictionary, 
			// this number can be added for consistency checks.
			writer.WriteAttributeString("numberOfFields", this.dataFields.Count.ToString());
			
			// Write data fields
			foreach (DataField dataField in this.dataFields)
				dataField.save(writer);
			
			writer.WriteEndElement();
		}
	}
}

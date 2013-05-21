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
	/// Description of DataField.
	/// </summary>
	public class DataField
	{
		private string name;
		private string displayName;
		private Optype optype;
		private string dataType;
		
		private List<string> fInterval;
		private IList<DataFieldValue> fValues = new List<DataFieldValue>();
		
		/// <summary>
		/// Name of the data field.
		/// </summary>
		public string Name { get { return name; } }
		
		/// <summary>
		/// The displayName is a string which may be used by applications to refer to that field. 
		/// Within the XML document only the value of name is significant. If displayName is not given, then it defaults to 
		/// the value of name. For example, there may be a field with name="CSTAGE" and displayName="Customer age". An 
		/// application may use the label Customer age, e.g., at the user interface in order to ask for input values. 
		/// That is, displayName can be used when the application calls the PMML consumer. Once the consumer has received 
		/// the parameters and matched to the MiningFields, the displayName is not relevant anymore. Only name is 
		/// significant for internal processing.
		/// </summary>
		public string DisplayName { get { return displayName; } set { displayName = value; } }
		
		/// <summary>
		/// Build data field.
		/// </summary>
		/// <param name="name"></param>
		/// <param name="optype"></param>
		/// <param name="dataType"></param>
		public DataField(string name, Optype optype, string dataType)
		{
			this.name = name;
			this.optype = optype;
			this.dataType = dataType;
		}
		
		/// <summary>
		/// Load data dictionary from xmlnode
		/// </summary>
		/// <param name="node"></param>
		/// <returns></returns>
		public static DataField loadFromXmlNode(XmlNode node)
		{
			string name = node.Attributes["name"].Value;
			Optype optype = OptypeFromString(node.Attributes["optype"].Value);
			string dataType = node.Attributes["dataType"].Value;
			DataField field = new DataField(name, optype, dataType);
			
			if (node.Attributes["displayName"] != null)
				field.displayName = node.Attributes["displayName"].Value;
			
			/*tree.ModelName = node.Attributes["modelName"].Value;
			
			if (node.Attributes["missingValueStrategy"] != null)
				tree.MissingValueStrategy = MissingValueStrategyfromString(node.Attributes["missingValueStrategy"].Value);
			
			// By default noTrueChildStrategy = returnNullPrediction
			tree.noTrueChildStrategy = NoTrueChildStrategy.ReturnNullPrediction;
			if (node.Attributes["noTrueChildStrategy"] != null)
				tree.noTrueChildStrategy = NoTrueChildStrategyfromString(node.Attributes["noTrueChildStrategy"].Value);
			*/
			
			field.fInterval = new List<string>();
			field.fValues = new List<DataFieldValue>();
			foreach(XmlNode item in node.ChildNodes)
			{
				if ("extension".Equals(item.Name.ToLowerInvariant()))
				{
					//
				}
				else if ("Interval".Equals(item.Name))
				{
					field.fInterval.Add(item.InnerText.Trim());
				}
				else if ("Value".Equals(item.Name))
				{
					field.fValues.Add(DataFieldValue.loadFromXmlNode(item));
				}
				else
					throw new NotImplementedException();
			}
			
			return field;
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="writer"></param>
		public void save(XmlWriter writer)
		{
			writer.WriteStartElement("DataField");
			
			writer.WriteAttributeString("name", this.name);
			
			if (!string.IsNullOrEmpty(this.DisplayName))
				writer.WriteAttributeString("displayName", this.DisplayName);
			
			writer.WriteAttributeString("optype", OptypeToString(this.optype));
			
			writer.WriteAttributeString("dataType", this.dataType);
			
			foreach (DataFieldValue dataval in this.fValues)
				dataval.save(writer);
			
			writer.WriteEndElement();
		}
		
		private static string OptypeToString(Optype val)
		{
			switch(val)
			{
			case Optype.categorical :
				return "categorical";
			case Optype.continuous :
				return "continuous";
			case Optype.ordinal :
				return "ordinal";
				default :
					throw new NotImplementedException();
			}
		}
		
		private static Optype OptypeFromString(string val)
		{
			switch(val.ToLowerInvariant().Trim())
			{
			case "categorical" :
				return Optype.categorical;
			case "continuous" :
				return Optype.continuous;
			case "ordinal" :
				return Optype.ordinal;
				default :
					throw new NotImplementedException();
			}
		}
	}
}

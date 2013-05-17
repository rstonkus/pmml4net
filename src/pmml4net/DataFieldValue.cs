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
using System.Xml;

namespace pmml4net
{
	/// <summary>
	/// Description of DataFieldValue.
	/// </summary>
	public class DataFieldValue
	{
		private string valu;
		private string displayValue;
		private string property;
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="valu"></param>
		/// <param name="property"></param>
		public DataFieldValue(string valu, string property)
		{
			this.valu = valu;
			this.property = property;
		}
		
		/// <summary>
		/// Load data dictionary from xmlnode
		/// </summary>
		/// <param name="node"></param>
		/// <returns></returns>
		public static DataFieldValue loadFromXmlNode(XmlNode node)
		{
			string valu = node.Attributes["value"].Value;
			string property = "valid";
			if (node.Attributes["property"] != null)
				property = node.Attributes["property"].Value;
			
			DataFieldValue val = new DataFieldValue(valu, property);
			
			if (node.Attributes["displayValue"] != null)
				val.displayValue = node.Attributes["displayValue"].Value;
			
			
			foreach(XmlNode item in node.ChildNodes)
			{
				if ("extension".Equals(item.Name.ToLowerInvariant()))
				{
					//
				}
				else
					throw new NotImplementedException();
			}
			
			return val;
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="writer"></param>
		public void save(XmlWriter writer)
		{
			writer.WriteStartElement("Value");
			
			writer.WriteAttributeString("value", this.valu);
			
			if (!string.IsNullOrEmpty(this.displayValue))
				writer.WriteAttributeString("displayValue", this.displayValue);
			
			writer.WriteAttributeString("property", this.property);
			
			writer.WriteEndElement();
		}
	}
}

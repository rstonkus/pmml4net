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
	/// Description of MiningField.
	/// </summary>
	public class MiningField
	{
		private string name;
		
		/// <summary>
		/// 
		/// </summary>
		public MiningField(string name)
		{
			this.name = name;
		}
		
		/// <summary>
		/// Load mining field from xmlnode
		/// </summary>
		/// <param name="node"></param>
		/// <returns></returns>
		public static MiningField loadFromXmlNode(XmlNode node)
		{
			MiningField field = new MiningField(node.Attributes["name"].Value);
			
			//field.name = node.Attributes["name"].Value;
			
			/*if (node.Attributes["probability"] != null)
				field.fprobability = node.Attributes["probability"].Value;
			
			if (node.Attributes["confidence"] != null)
				field.fconfidence = node.Attributes["confidence"].Value;*/
			
			return field;
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="writer"></param>
		public void save(XmlWriter writer)
		{
			writer.WriteStartElement("MiningField");
			
			writer.WriteAttributeString("name", this.name);
			
			/*writer.WriteAttributeString("functionName", MiningFunctionToString(this.FunctionName));
			
			writer.WriteAttributeString("algorithmName", this.AlgorithmName);
			
			// Save Mining fields
			this.MiningSchema.save(writer);*/
			
			
			
			writer.WriteEndElement();
		}
	}
}

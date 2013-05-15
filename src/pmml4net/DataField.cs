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
	/// Description of DataField.
	/// </summary>
	public class DataField
	{
		private string name;
		private Optype optype;
		private string dataType;
		
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
		/// 
		/// </summary>
		/// <param name="writer"></param>
		public void save(XmlWriter writer)
		{
			writer.WriteStartElement("DataField");
			
			writer.WriteAttributeString("name", this.name);
			
			writer.WriteAttributeString("optype", OptypeToString(this.optype));
			
			writer.WriteAttributeString("dataType", this.dataType);
			
			writer.WriteEndElement();
		}
		
		private string OptypeToString(Optype val)
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
	}
}

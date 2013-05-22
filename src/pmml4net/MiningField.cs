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
		private FieldUsageType usageType = FieldUsageType.Active;
		
		/// <summary>
		/// Symbolic name of field, must refer to a field in the scope of the parent of the MiningSchema's model element.
		/// 
		/// If the DataDictionary defines a displayName for a certain field, still the attribute name is used for matching 
		/// the input parameters to the internal formulas. displayName allows using human readable names at the interface 
		/// while using artificial identifiers within the semantics of model.
		/// </summary>
		public string Name { get { return name; } }
		
		/// <summary>
		/// usageType
		///
		/// active: field used as input (independent field).
		/// predicted: field whose value is predicted by the model.
		/// supplementary: field holding additional descriptive information. Supplementary fields are not required to apply a model. 
		/// They are provided as additional information for explanatory purpose, though. When some field has gone through 
		/// preprocessing transformations before a model is built, then an additional supplementary field is typically used 
		/// to describe the statistics for the original field values.
		/// group: field similar to the SQL GROUP BY. For example, this is used by AssociationModel and SequenceModel to group 
		/// items into transactions by customerID or by transactionID.
		/// order: This field defines the order of items or transactions and is currently used in SequenceModel and TimeSeriesModel. 
		/// Similarly to group, it is motivated by the SQL syntax, namely by the ORDER BY statement.
		/// frequencyWeight and analysisWeight: These fields are not needed for scoring, but provide very important information 
		/// on how the model was built. Frequency weight usually has positive integer values and is sometimes called "replication 
		/// weight". Its values can be interpreted as the number of times each record appears in the data. Analysis weight can 
		/// have fractional positive values, it could be used for regression weight in regression models or for case weight in 
		/// trees, etc. It can be interpreted as different importance of the cases in the model. Counts in ModelStats and 
		/// Partitions can be computed using frequency weight, mean and standard deviation values can be computed using both 
		/// weights.
		/// 
		/// The definition of predicted fields in the MiningSchema is not required and it does not have an impact on the scoring 
		/// results. But it is very useful because it gives a user a first hint about the detailed results that can be computed by the model.
		/// </summary>
		public FieldUsageType UsageType { get { return usageType; } set { usageType = value; } }
		
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
			
			if (node.Attributes["usageType"] != null)
				field.usageType = FieldUsageTypeFromString(node.Attributes["usageType"].Value);
			
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
			
			writer.WriteAttributeString("usageType", FieldUsageTypeToString(this.UsageType));
			
			/*writer.WriteAttributeString("algorithmName", this.AlgorithmName);
			
			// Save Mining fields
			this.MiningSchema.save(writer);*/
			
			
			
			writer.WriteEndElement();
		}
		
		private static FieldUsageType FieldUsageTypeFromString(string val)
		{
			switch (val.ToLowerInvariant().Trim())
			{
			case "active": 
				return FieldUsageType.Active;
			
			case "predicted":
				return FieldUsageType.Predicted;
				
			case "supplementary":
				return FieldUsageType.Supplementary;
				
			default:
				throw new NotImplementedException();
			}
		}
		
		private static string FieldUsageTypeToString(FieldUsageType val)
		{
			switch (val)
			{
			case FieldUsageType.Active: 
				return "active";
			
			case FieldUsageType.Predicted:
				return "predicted";
				
			case FieldUsageType.Supplementary:
				return "supplementary";
				
			default:
				throw new NotImplementedException();
			}
		}
	}
}
